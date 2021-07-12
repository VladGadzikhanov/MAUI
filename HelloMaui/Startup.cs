using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.LifecycleEvents;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HelloMaui
{
	public class Startup : IStartup
	{
        public void Execute()
        {
            string connectionString = "server=localhost;port=3306;database=HelloMaui;uid=root";

            using MySqlConnection connection = new MySqlConnection(connectionString);
            // Create database if not exists
            using (Authorization contextDB = new())
            {
                contextDB.Database.RollbackTransactionAsync();
            }

            connection.Open();
            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                // DbConnection that is already opened
                using (Authorization context = new Authorization())
                {

                    // Interception/SQL logging

                    // Passing an existing transaction to the context
                    context.Database.UseTransaction(transaction);

                    // DbSet.AddRange
                    List<User> users = new();

                    users.Add(new User { FirstName = "Vlad", LastName = "Gadzikhanov", Password = "0807" });
                    users.Add(new User { FirstName = "Anya", LastName = "Khromova", Password = "0000" });
                    users.Add(new User { FirstName = "Ihor", LastName = "Bailov", Password = "1111" });
                    users.Add(new User { FirstName = "Ivan", LastName = "Ivanov", Password = "2222" });

                    context.Users.AddRange(users);

                    context.SaveChanges();
                }

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
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