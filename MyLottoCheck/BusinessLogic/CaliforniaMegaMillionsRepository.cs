using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyLottoCheck.Models
{
    public class CaliforniaMegaMillionsRepository : ICaliforniaMegaMillionRepository
    {
        private readonly MyLottoCheckModels _context;

        public CaliforniaMegaMillionsRepository(MyLottoCheckModels context)
        {
            _context = context;
        }

        public IEnumerable<CaliforniaMegaMillionUserPick> GetMegaMillionPicksForUser(string userId)
        {
            var megaMillionPicks = _context.CaliforniaMegaMillionUserPicks.Where(m => m.UserId == userId).OrderBy(m => m.DateCreated);
            return megaMillionPicks;
        }

        public void DeleteMegaMillionPick(CaliforniaMegaMillionUserPick megaMillionPick)
        {
            _context.CaliforniaMegaMillionUserPicks.Remove(megaMillionPick);
        }

        public CaliforniaMegaMillionUserPick GetMegaMillionPick(Guid megaMillionPickId)
        {
            CaliforniaMegaMillionUserPick megaMillionPick = _context.CaliforniaMegaMillionUserPicks.Find(megaMillionPickId);
            return megaMillionPick;
        }

        public void UpdateMegaMillionPick(CaliforniaMegaMillionUserPick megaMillionPick)
        {
            _context.Entry(megaMillionPick).State = EntityState.Modified;
            _context.Entry(megaMillionPick).Property(x => x.DateCreated).IsModified = false;
        }

        public void InsertMegaMillionPick(CaliforniaMegaMillionUserPick megaMillionPick)
        {
            _context.CaliforniaMegaMillionUserPicks.Add(megaMillionPick);
        }


        public void Save()
        {
            _context.SaveChanges();
        }

        public DateTime? GetLatestWinningNumberUpdateDate()
        {
            DateTime? dateCreated = _context.CaliforniaMegaMillionsAllWinningNumbersAndPrizes.OrderByDescending(w => w.DrawNumber).Take(1).Select(w => w.DateCreated).FirstOrDefault();
            return dateCreated;
        }
    }
}