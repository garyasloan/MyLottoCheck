using System;
using System.Collections.Generic;

namespace MyLottoCheck.Models
{
    public class CaliforniaMegaMillionsRepositoryFake : ICaliforniaMegaMillionRepository
    {
        public List<CaliforniaMegaMillionUserPick> SaveList = new List<CaliforniaMegaMillionUserPick>();
        public List<CaliforniaMegaMillionUserPick> GetList = new List<CaliforniaMegaMillionUserPick>();

        public void DeleteMegaMillionPick(CaliforniaMegaMillionUserPick megaMillionPick)
        {
            return;
        }

        public DateTime? GetLatestWinningNumberUpdateDate()
        {
            throw new NotImplementedException();
        }

        public CaliforniaMegaMillionUserPick GetMegaMillionPick(Guid megaMillionPickId)
        {
            return new CaliforniaMegaMillionUserPick();
        }

        public IEnumerable<CaliforniaMegaMillionUserPick> GetMegaMillionPicksForUser(string userId)
        {
            CaliforniaMegaMillionUserPick pick = new CaliforniaMegaMillionUserPick
            {
                Id = Guid.NewGuid(),
                FirstPick = 1,
                SecondPick = 2,
                ThirdPick = 3,
                FourthPick = 4,
                FifthPick = 5,
                MegaPick = 6
            };

            List<CaliforniaMegaMillionUserPick> picks = new List<CaliforniaMegaMillionUserPick>();
            picks.Add(pick);
            return picks;
        }

        public void InsertMegaMillionPick(CaliforniaMegaMillionUserPick megaMillionPick)
        {
            return;
        }

        public void Save()
        {
            return;
        }

        public void UpdateMegaMillionPick(CaliforniaMegaMillionUserPick megaMillionPick)
        {
            return;
        }
    }
}