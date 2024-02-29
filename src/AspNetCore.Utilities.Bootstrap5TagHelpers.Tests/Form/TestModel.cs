using System.ComponentModel.DataAnnotations;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.Form;
public class TestModel
{
    public string TextField { get; set; }

    [Required]
    public int? RequiredIntField { get; set; }
}
