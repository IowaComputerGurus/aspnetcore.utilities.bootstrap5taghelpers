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
        public SampleEnum SelectedListItem { get; set; }
    }
}
