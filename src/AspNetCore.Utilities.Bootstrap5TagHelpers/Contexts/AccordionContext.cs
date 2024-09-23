namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;

/// <summary>
///     Context helper for accordion tag helper
/// </summary>
public class AccordionContext
{
    /// <summary>
    ///     The ID of the parent accordion item
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     Should it render as always open
    /// </summary>
    public bool AlwaysOpen { get; set; }
}