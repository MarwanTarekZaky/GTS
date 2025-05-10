using Microsoft.AspNetCore.Mvc.Localization;

namespace Formula.Models.SharedResources;

public class LocalizationExampleViewModel
{
    public string? IStringLocalizerInController { get; set; }
    public LocalizedHtmlString? IHtmlLocalizerInController { get; set; }
}