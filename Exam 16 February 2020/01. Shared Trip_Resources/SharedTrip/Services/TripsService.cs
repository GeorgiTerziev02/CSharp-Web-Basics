namespace SharedTrip.Services
{
    using SharedTrip.Models;
    using SharedTrip.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;


    public class TripsService : ITripsServcie
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(AddTripViewModel model)
        {
            var trip = new Trip()
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                Seats = model.Seats,
                DepartureTime = DateTime.ParseExact(model.DepartureTime, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture),
                ImagePath = model.ImagePath,
                Description = model.Description
            };

            this.db.Trips.Add(trip);
            this.db.SaveChanges();
        }

        public IEnumerable<TripOutputViewModel> GetAllTrips()
        {
            //front seats arent counted as free seats => seats - 2
            return this.db.Trips.Select(x => new TripOutputViewModel()
            {
                Id = x.Id,
                StartPoint = x.StartPoint,
                EndPoint = x.EndPoint,
                Seats = x.Seats - 2,
                DepartureTime = x.DepartureTime

            }).ToArray();
        }

        public TripDetailsViewModel GetTrip(string id)
        {
            var trip = this.db.Trips.Where(x => x.Id == id).FirstOrDefault();

            if (trip == null)
            {
                return null;
            }

            var model = new TripDetailsViewModel()
            {
                Id = trip.Id,
                StartPoint = trip.StartPoint,
                EndPoint = trip.EndPoint,
                DepartureTime = trip.DepartureTime.ToString("dd.MM.yyyy HH: mm"),
                ImagePath = trip.ImagePath,
                Seats = trip.Seats - 2,
                Description = trip.Description
            };

            return model;
        }

        public void JoinTrip(string userId, string tripId)
        {
            var userTrip = new UserTrip()
            {
                UserId = userId,
                TripId = tripId
            };

            this.DecreaseFreeSeat(tripId);

            this.db.UserTrips.Add(userTrip);
            this.db.SaveChanges();
        }

        public bool UserHasJoinedTrip(string userId, string tripId)
        {
            var userTrip = this.db.UserTrips.FirstOrDefault(x => x.UserId == userId && tripId == x.TripId);
            if (userTrip == null)
            {
                return false;
            }

            return true;
        }

        private void DecreaseFreeSeat(string tripId)
        {
            var trip = this.db.Trips.FirstOrDefault(x=>x.Id == tripId);

            trip.Seats = trip.Seats - 1;

            this.db.SaveChanges();
        }
    }
}
