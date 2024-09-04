namespace MyLottoCheck.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Holds all of the winning numbers (not prizes)
    /// after October 18th, 2013 (draw# 870 and above)
    /// This is when California MegaMillions changed
    /// from 56 regular numbers and 46 mega numbers
    /// to 75 regular numbers and 15 mega numbers.  After
    /// October 28, 2017, California Mega Millions changed
    /// to 70 regular numbers and 25 mega numbers.
    /// This data set is used for statistical
    /// analysis of number frequency via odata webapi
    /// methods.
    /// </summary>
    [Table("MyLottoCheck.CaliforniaMegaMillionsAllWinningNumbers")]
    public class CaliforniaMegaMillionsAllWinningNumber
    {
        public Guid Id { get; set; }
        public int DrawNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime DrawDate { get; set; }
        public int Number { get; set; }
        public bool IsMegaNumber { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
