namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Contexts;

/// <summary>
///     Context for an accordion item
/// </summary>
public class AccordionItemContext
{
    /// <summary>
    ///     What is the unique item id for this item
    /// </summary>
    public string ItemId { get; set; }

    /// <summary>
    ///     Should this render as expanded
    /// </summary>
    public bool Expanded { get; set; }
}