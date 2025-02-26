using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyLottoCheck.Models
{
    /// <summary>
    /// Holds the result set of all the
    /// picks selected by the user
    /// </summary>
    [Table("MyLottoCheck.CaliforniaMegaMillionUserPicks")]
    public class CaliforniaMegaMillionUserPick
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        [Range(1, 70)]
        [Display(Name = "1st Number")]
        public int FirstPick { get; set; }

        [Required]
        [Range(1, 70)]
        [Display(Name = "2nd Number")]
        public int SecondPick { get; set; }

        [Required]
        [Range(1, 70)]
        [Display(Name = "3rd Number")]
        public int ThirdPick { get; set; }

        [Required]
        [Range(1, 70)]
        [Display(Name = "4th Number")]
        public int FourthPick { get; set; }

        [Required]
        [Range(1, 70)]
        [Display(Name = "5th Number")]
        public int FifthPick { get; set; }

        [Required]
        [Range(1, 25)]
        [Display(Name = "Mega Number")]
        public int MegaPick { get; set; }

        public DateTime DateCreated { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [StringLength(140)]
        public string RowCheckSum { get; set; }


        public virtual AspNetUser AspNetUser { get; set; }
    }
}
