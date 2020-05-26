namespace MyCoolWebServer.ByTheCakeApplication.Services.Contracts
{
    using ViewModels.Account;

    public interface IUserService
    {
        bool Create(string username, string password);

        bool Exists(string username, string password);

        ProfileUserViewModel Get(string username);
    }
}
