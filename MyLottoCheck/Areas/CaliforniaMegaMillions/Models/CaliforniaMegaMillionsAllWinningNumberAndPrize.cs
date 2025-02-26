using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLottoCheck.Models
{
    /// <summary>
    /// Holds a subset of the result set of winning numbers and 
    /// thier prizes that cover non expired draws (since April 2016)
    /// </summary>
    [Table("MyLottoCheck.CaliforniaMegaMillionsAllWinningNumbersAndPrizes")]
    public class CaliforniaMegaMillionsAllWinningNumberAndPrize
    {
        public Guid Id { get; set; }
        public int DrawNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime DrawDate { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Number3 { get; set; }
        public int Number4 { get; set; }
        public int Number5 { get; set; }
        public int MegaNumber { get; set; }
        public long FiveMatchesPlusMegaPrizeAmount { get; set; }
        public long FiveMatchesPrizeAmount { get; set; }
        public long FourMatchesPlusMegaPrizeAmount { get; set; }
        public long FourMatchesPrizeAmount { get; set; }
        public long ThreeMatchesPlusMegaPrizeAmount { get; set; }
        public long ThreeMatchesPrizeAmount { get; set; }
        public long TwoMatchesPlusMegaPrizeAmount { get; set; }
        public long OneMatchPlusMegaPrizeAmount { get; set; }
        public long MegaMatchOnlyPrizeAmount { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
