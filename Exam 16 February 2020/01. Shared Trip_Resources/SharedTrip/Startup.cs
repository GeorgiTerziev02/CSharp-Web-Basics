namespace SharedTrip
{
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using SharedTrip.Services;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class Startup : IMvcApplication
    {
        //the front seats arent counted as free seats => seats = seats - 2

        public void Configure(IList<Route> routeTable)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            db.Database.EnsureCreated();
            db.Database.Migrate();

        }

        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.Add<IUsersService, UsersService>();
            serviceCollection.Add<ITripsServcie, TripsService>();
        }
    }
}
