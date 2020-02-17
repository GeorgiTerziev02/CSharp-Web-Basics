namespace SharedTrip.ViewModels
{
    using System.Collections.Generic;

    public class AllTripsViewModel
    {
        public IEnumerable<TripOutputViewModel> Trips { get; set; }
    }
}
