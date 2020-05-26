namespace MyCoolWebServer.ByTheCakeApplication.ViewModels.Account
{
    using System;

    public class ProfileUserViewModel
    {
        public string Username { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int TotalOrders { get; set; }
    }
}
