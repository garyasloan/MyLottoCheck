using System;
using System.Collections.Generic;
using MyLottoCheck.Models;

namespace MyLottoCheck.Models
{
    public interface ICaliforniaMegaMillionRepository 
    {
        IEnumerable<CaliforniaMegaMillionUserPick> GetMegaMillionPicksForUser(string userId);
        CaliforniaMegaMillionUserPick GetMegaMillionPick(Guid megaMillionPickId);
        void InsertMegaMillionPick(CaliforniaMegaMillionUserPick megaMillionPick);
        void DeleteMegaMillionPick(CaliforniaMegaMillionUserPick megaMillionPick);
        void UpdateMegaMillionPick(CaliforniaMegaMillionUserPick megaMillionPick);
        void Save();
        DateTime? GetLatestWinningNumberUpdateDate();
    }
}
