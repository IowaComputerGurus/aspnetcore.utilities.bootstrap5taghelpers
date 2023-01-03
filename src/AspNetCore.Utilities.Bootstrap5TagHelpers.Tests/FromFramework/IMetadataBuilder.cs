using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace ICG.AspNetCore.Utilities.Bootstrap5TagHelpers.Tests.FromFramework;

public interface IMetadataBuilder
{
    IMetadataBuilder BindingDetails(Action<BindingMetadata> action);

    IMetadataBuilder DisplayDetails(Action<DisplayMetadata> action);

    IMetadataBuilder ValidationDetails(Action<ValidationMetadata> action);
}