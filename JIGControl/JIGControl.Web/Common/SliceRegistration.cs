namespace JIGControl.Web.Common;

/// Every class whose name ends in "Handler" inside Features/ is one vertical slice's logic.
public static class SliceRegistration
{
    public static IServiceCollection AddFeatureSlices(this IServiceCollection services)
    {
        var handlers = typeof(SliceRegistration).Assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.Name.EndsWith("Handler")
                        && t.Namespace?.StartsWith("JIGControl.Web.Features") == true);
        foreach (var h in handlers) services.AddScoped(h);
        return services;
    }
}
