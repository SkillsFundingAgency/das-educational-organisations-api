using System;

public static class ServiceProviderExtensions
{
    public static ILogger GetLogger(this ServiceProvider serviceProvider, string typeName) => serviceProvider.GetService<ILoggerProvider>().CreateLogger(typeName);
}
