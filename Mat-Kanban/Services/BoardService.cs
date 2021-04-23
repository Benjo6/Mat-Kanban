using Email;
using Mat_Kanban.Data;
using Mat_Kanban.Models;
using Mat_Kanban.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mat_Kanban.Services
{
    public class BoardService
    {
        private readonly ApplicationDbContext _dbContext;

        public BoardService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BoardList List()
        {

            var m = new BoardList();
            foreach (var board in _dbContext.Boards)
            {
                m.Boards.Add(new BoardList.Board
                {
                    Id = board.Id,
                    Title = board.Title
                });
            }
            return m;
        }

        public BoardView GetBoard(int id)
        {
            var m = new BoardView();

            var b = _dbContext.Boards
                .Include(h => h.Columns)
                .ThenInclude(v => v.Cards)
                .SingleOrDefault(x => x.Id == id);

            if (b == null)
                return m;
            m.Id = b.Id;
            m.Title = b.Title;

            foreach (var c in b.Columns)
            {
                var mc = new BoardView.Column
                {
                    Title = c.Title,
                    Id = c.Id
                };
                foreach (var ca in c.Cards)
                {
                    var mca = new BoardView.Card
                    {
                        Id = ca.Id,
                        Content = ca.Contents
                    };

                    mc.Cards.Add(mca);
                }
                m.Columns.Add(mc);
            }
            return m;

        }

        public void AddCard(AddCard vm)
        {
            var b = _dbContext.Boards
                .Include(l => l.Columns)
                .SingleOrDefault(x => x.Id == vm.Id);

            if (b != null)
            {
                var fC = b.Columns.FirstOrDefault();
                var sC = b.Columns.FirstOrDefault();
                var tC = b.Columns.FirstOrDefault();

                if (fC == null || sC == null || tC == null)
                {
                    fC = new Column { Title = "To Do" };
                    sC = new Column { Title = "Doing" };
                    tC = new Column { Title = "Done" };
                    b.Columns.Add(fC);
                    b.Columns.Add(sC);
                    b.Columns.Add(tC);
                }
                fC.Cards.Add(new Card
                {
                    Contents = vm.Contents
                });
            }
            _dbContext.SaveChanges();
        }

        public void AddBoard(NewBoard vm)
        {
            _dbContext.Boards.Add(new Board
            {
                Title = vm.Title
            });
            _dbContext.SaveChanges();
        }

        public void Move(MoveCardCommand command)
        {
            var c = _dbContext.Cards.SingleOrDefault(t => t.Id == command.CardId);
            c.ColumnId = command.ColumnId;
            _dbContext.SaveChanges();
        }
    }
}
