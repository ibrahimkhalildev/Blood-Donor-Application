namespace BloodDonar.MVC.Models
{
    internal class ViewModels
    {
        internal class DonationViewModel
        {
            public object BloodGroup { get; internal set; }
            public object LastDonationDate { get; internal set; }
            public object DaysSinceLastDonation { get; internal set; }
        }
    }
}