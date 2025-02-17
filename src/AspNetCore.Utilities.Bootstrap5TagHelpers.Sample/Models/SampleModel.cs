using System.ComponentModel.DataAnnotations;

namespace AspNetCore.Utilities.Bootstrap5TagHelpers.Sample.Models
{
    public class SampleModel
    {
        [Display(Name = "First Name")]
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = "";

        [Display(Name = "Last Name")]
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = "";

        [Display(Name = "Email Address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Display(Name = "City")]
        public string City { get; set; } = "";

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } = "";

        [Display(Name = "Additional Information")]
        public string AdditionalInfo { get; set; } = "";

        [Display(Name = "Country")]
        [Required]
        public int SelectedCountry { get; set; }

        [Display(Name = "ReadOnly")]
        public string ReadOnlyField { get; set; } = "Readonly Field Example";

        [Display(Name = "Select List Item")]
        public SampleEnum? SelectedListItem { get; set; }

        [Display(Name = "Set Default Password")]
        public bool SetDefaultPassword { get; set; }

        [Display(Name = "Agree to Terms (Disabled)")]
        public bool AgreeToTerms { get; set; } = true;

        [Display(Name = "Item 1")]
        public bool Item1 { get; set; }

        [Display(Name = "Item 2")]
        public bool Item2 { get; set; }

        [Display(Name = "Item 3")]
        public bool Item3 { get; set; }

        [Display(Name = "Item 4")]
        public bool Item4 { get; set; }
    }
}
