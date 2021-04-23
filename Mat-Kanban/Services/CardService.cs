using Email;
using Mat_Kanban.Data;
using Mat_Kanban.Models;
using Mat_Kanban.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mat_Kanban.Services
{
    public class CardService
    {
        private ApplicationDbContext _dbContext;

        public CardService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CardDetails GetDetails(int id)
        {
            var c = _dbContext
                .Cards
                .Include(b => b.Column)
                .SingleOrDefault(x => x.Id == id);
            if (c == null)
                return new CardDetails();

            var b = _dbContext
                .Boards
                .Include(x => x.Columns)
                .SingleOrDefault(x => x.Id == c.Column.BoardId);

            if (b != null)
            {
                var aC = b
                    .Columns
                    .Select(x => new SelectListItem
                    {
                        Text = x.Title,
                        Value = x.Id.ToString()
                    });


                return new CardDetails
                {
                    Id = c.Id,
                    Contents = c.Contents,
                    Notes = c.Notes,
                    Columns = aC.ToList(),
                       
                    Column = c.ColumnId // map current column
                };
            }
            return null;
        }
        public void Update(CardDetails cD)
        {
            var c = _dbContext.Cards.SingleOrDefault(x => x.Id == cD.Id);
            c.Contents = cD.Contents;
            c.Notes = cD.Notes;
            c.ColumnId = cD.Column;

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var c = _dbContext.Cards.SingleOrDefault(x => x.Id == id);
            _dbContext.Remove(c ?? throw new Exception($"Could not remove {(Card)null}"));

            _dbContext.SaveChanges();
        }
    }
}
