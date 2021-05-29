using System.ComponentModel.DataAnnotations;

namespace PersonalMovieListApi.Dtos
{
    public class MovieUpdateDto
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [Required]
        [MaxLength(250)]
        public string Comment { get; set; }

        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }
    }
}