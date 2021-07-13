using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.LifecycleEvents;
using System.Collections.Generic;
using System;

namespace HelloMaui
{
	public class Startup : IStartup
	{
        public void Execute()
        {

            using var context = new Authorization();
            try
            {
                context.Database.EnsureCreated();
            }
            catch(Exception e)
            {
                var ll = e.Message;
                Console.WriteLine(ll);
            }
            List<User> users = new();
            users.Add(new User { FirstName = "Vlad", LastName = "Gadzikhanov", Password = "0807" });
            users.Add(new User { FirstName = "Anya", LastName = "Khromova", Password = "0000" });
            users.Add(new User { FirstName = "Ihor", LastName = "Bailov", Password = "1111" });
            users.Add(new User { FirstName = "Ivan", LastName = "Ivanov", Password = "2222" });
            context.Users.AddRange(users);
            context.SaveChanges();
        }
		public void Configure(IAppHostBuilder appBuilder)
		{
			appBuilder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts => {
					fonts.AddFont("ionicons.ttf", "IonIcons");
				})
				.ConfigureLifecycleEvents(lifecycle => {
					#if ANDROID
					lifecycle.AddAndroid(d => {
						d.OnBackPressed(activity => {
							System.Diagnostics.Debug.WriteLine("Back button pressed!");
						});
					});
					#endif
				});
            Execute();
		}
	}
}