using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

namespace MyLottoCheck.Areas.CaliforniaMegaMillions.ViewModels
{
    public class CaliforniaMegaMillionsUserPickViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Range(1, 70)]
        [Display(Name = "1st Number")]
        public int FirstPick { get; set; }

        [Required]
        [Range(1, 70)]
        [NotDuplicateOf("FirstPick", ErrorMessage = "2nd Number is a Duplicate")]
        [Display(Name = "2nd Number")]
        public int SecondPick { get; set; }

        [Required]
        [Range(1, 70)]
        [NotDuplicateOf("FirstPick, SecondPick", ErrorMessage = "3rd Number is a Duplicate")]
        [Display(Name = "3rd Number")]
        public int ThirdPick { get; set; }

        [Required]
        [Range(1, 70)]
        [NotDuplicateOf("FirstPick, SecondPick, ThirdPick", ErrorMessage = "4th Number is a Duplicate")]
        [Display(Name = "4th Number")]
        public int FourthPick { get; set; }

        [Required]
        [Range(1, 70)]
        [NotDuplicateOf("FirstPick, SecondPick, ThirdPick, FourthPick", ErrorMessage = "5th Number is a Duplicate")]
        [Display(Name = "5th Number")]
        public int FifthPick { get; set; }

        [Required]
        [Range(1, 25)]
        [Display(Name = "Mega Number")]
        public int MegaPick { get; set; }
    }
}

public class NotDuplicateOf : ValidationAttribute
{
    private readonly string _otherProperty;
    private readonly string[] _otherProperties;
    public NotDuplicateOf(string otherProperty)
    {
        _otherProperty = otherProperty;
        _otherProperties = otherProperty.Split(',').Select(p => p.Trim()).ToArray();
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        foreach (var field in _otherProperties)
        {
            var property = validationContext.ObjectType.GetProperty(field);
            var otherPropertyValue = property.GetValue(validationContext.ObjectInstance, null);
            if ((int)value == (int)otherPropertyValue)
            {
                return new ValidationResult(string.Format(
                        CultureInfo.CurrentCulture,
                        FormatErrorMessage(validationContext.DisplayName),
                        new[] { _otherProperty }
                    ));
            }
        }
        return ValidationResult.Success;
    }

}
