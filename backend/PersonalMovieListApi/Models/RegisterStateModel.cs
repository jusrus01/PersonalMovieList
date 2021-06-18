using System.ComponentModel.DataAnnotations;

namespace PersonalMovieListApi.Models
{
    public class RegisterStateModel
    {
        public bool AccountCreated { get; set; }
        public string Message { get; set; }
    }
}