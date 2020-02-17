namespace SharedTrip.Services
{
    using SharedTrip.ViewModels;
    using System.Collections.Generic;


    public interface ITripsServcie
    {
        IEnumerable<TripOutputViewModel> GetAllTrips();

        void Add(AddTripViewModel model);

        TripDetailsViewModel GetTrip(string id);
        bool UserHasJoinedTrip(string userId, string tripId);
        void JoinTrip(string userId, string tripId);
    }
}
