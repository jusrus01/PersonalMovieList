using System.ComponentModel.DataAnnotations;

namespace PersonalMovieListApi.Models
{
    public class RegisterModel
    {
        [Required]
        [MaxLength(25)]
        public string Username { get; set; }
        [Required]
        [MaxLength(25)]
        public string Email { get; set; }
        [Required]
        [MaxLength(25)]
        public string Password { get; set; }
    }
}