using System.ComponentModel.DataAnnotations;

namespace PersonalMovieListApi.Models
{
    public class MovieModel 
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Comment { get; set; }
        [Required]
        public int Rating { get; set; }
        [Required]
        public string OwnerUsername { get; set; }
    }
}