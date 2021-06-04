using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using PersonalMovieListApi.Data.Users;
using PersonalMovieListApi.Models;

namespace PersonalMovieListApi.Tests
{
    public class MockUsersService : IUserService
    {
        public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
        {
            var authenticationModel = new AuthenticationModel();
            authenticationModel.IsAuthenticated = true;
            authenticationModel.Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJ0ZXN0MiIsImp0aSI6IjlkYWEyZTJkLTlkNjUtNDAyZC04NWUxLTgxNmIzZDkyZGIyZiIsImVtYWlsIjoidGVzdEB0ZXN0IiwidWlkIjoiYTZjMDhiOGQtZTc0Yy00ZDg2LWJjMGItZTE0YWY3ZjY5OTA2Iiwicm9sZXMiOiJVc2VyIiwiZXhwIjoxNjIyODE2NTc4LCJpc3MiOiJTZWN1cmVBcGkiLCJhdWQiOiJTZWN1cmVBcGlVc2VyIn0.Fqz1gcINJ1vPvNN9cV6NJTAZsryMgKO0JBGfVNDJN9I";
            authenticationModel.Email = "test@test";
            authenticationModel.UserName = "test2";
            // pass test2
            authenticationModel.Roles = new List<string> { "User" };
            return authenticationModel;
        }

        public Task<string> RegisterAsync(RegisterModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}