using BlazorApp.Shared;

namespace BlazorAppPrint
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<Person>(); // インスタンス化
        }
    }
}
