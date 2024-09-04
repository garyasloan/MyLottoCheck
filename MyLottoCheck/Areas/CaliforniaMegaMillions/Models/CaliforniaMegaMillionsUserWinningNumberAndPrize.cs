using System.ComponentModel.DataAnnotations;

namespace MyLottoCheck.Models
{
    /// <summary>
    /// Holds the result set of winning numbers and thier prizes
    /// for the picks selected by the user
    /// </summary>

    public class CaliforniaMegaMillionsUserWinningNumberAndPrize
    {
        [Display(Name = "Draw#")]
        public long DrawNumber { get; set; }
        [Display(Name = "Draw Date")]
        public string DrawDate { get; set; }
        [Display(Name = "1st")]
        public int FirstPick { get; set; }
        public int MatchedNumber1 { get; set; }
        [Display(Name = "2nd")]
        public int SecondPick { get; set; }
        public int MatchedNumber2 { get; set; }
        [Display(Name = "3rd")]
        public int ThirdPick { get; set; }
        public int MatchedNumber3 { get; set; }
        [Display(Name = "4th")]
        public int FourthPick { get; set; }
        public int MatchedNumber4 { get; set; }
        [Display(Name = "5th")]
        public int FifthPick { get; set; }
        public int MatchedNumber5 { get; set; }
        [Display(Name = "Mega")]
        public int MegaPick { get; set; }
        public int MatchedNumberMega { get; set; }
        [Display(Name = "Prize")]
        [DisplayFormat(DataFormatString = "{0:C0}")]
        public long PrizeAmount { get; set; }
    }
}
