using API.Models;

namespace API.DAL
{
    public interface IDataAccess
    {
        int CreateUser(User user);
        bool IsEmailAvailable(string email);
    }
}
