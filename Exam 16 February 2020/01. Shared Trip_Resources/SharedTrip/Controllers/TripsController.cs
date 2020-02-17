namespace SharedTrip.Controllers
{
    using SharedTrip.Services;
    using SharedTrip.ViewModels;
    using SIS.HTTP;
    using SIS.MvcFramework;


    public class TripsController : Controller
    {
        private readonly ITripsServcie tripsServcie;
        private readonly IUsersService usersService;

        //the front seats arent counted as free seats => seats = seats - 2
        public TripsController(ITripsServcie tripsServcie, IUsersService usersService)
        {
            this.tripsServcie = tripsServcie;
            this.usersService = usersService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var allTrips = new AllTripsViewModel()
            {
                Trips = tripsServcie.GetAllTrips()
            };

            return this.View(allTrips);
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddTripViewModel trip)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrWhiteSpace(trip.StartPoint))
            {
                return this.Redirect("/Trips/Add");
            }

            if (string.IsNullOrWhiteSpace(trip.EndPoint))
            {
                return this.Redirect("/Trips/Add");
            }

            if (string.IsNullOrWhiteSpace(trip.DepartureTime))
            {
                return this.Redirect("/Trips/Add");
            }

            if (trip.Seats < 2 || trip.Seats > 6)
            {
                return this.Redirect("/Trips/Add");
            }

            if (string.IsNullOrWhiteSpace(trip.Description))
            {
                return this.Redirect("/Trips/Add");
            }

            if (trip.Description.Length > 80)
            {
                return this.Redirect("/Trips/Add");
            }

            tripsServcie.Add(trip);

            return this.Redirect("/Trips/All");
        }

        public HttpResponse Details(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var trip = this.tripsServcie.GetTrip(tripId);

            if (trip == null)
            {
                return this.Redirect("/Trips/All");
            }

            return this.View(trip);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var trip = this.tripsServcie.GetTrip(tripId);
           
            //the front seats arent counted as free seats => seats = seats - 2
            if (trip.Seats <= 0)
            {
                return this.Redirect($"/Trips/Details?tripId={tripId}");
            }

            if (this.tripsServcie.UserHasJoinedTrip(this.User, tripId) == true)
            {
                return this.Redirect($"/Trips/Details?tripId={tripId}");
            }

            this.tripsServcie.JoinTrip(this.User, tripId);

            return this.Redirect("/Trips/All");
        }
    }
}
