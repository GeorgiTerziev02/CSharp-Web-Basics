using Andreys.ViewModels.Users;

namespace Andreys.Services
{
    public interface IUserService
    {
        void Create(RegisterInputViewModel model);

        string GetUserId(string username, string password);
    }
}
