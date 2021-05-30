using System.Threading.Tasks;
using PersonalMovieListApi.Models;

namespace PersonalMovieListApi.Data.Users
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterModel model);
        Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
    }
}