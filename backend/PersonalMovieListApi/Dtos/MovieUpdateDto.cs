using System.ComponentModel.DataAnnotations;

namespace PersonalMovieListApi.Dtos
{
    public class MovieUpdateDto
    {
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }

        [MaxLength(250)]
        public string Comment { get; set; }

        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }

        [MaxLength(300000)] // change later
        public string ImageBase64 { get; set; }
    }
}