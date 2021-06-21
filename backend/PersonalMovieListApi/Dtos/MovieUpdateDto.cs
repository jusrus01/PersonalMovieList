using System.ComponentModel.DataAnnotations;

namespace PersonalMovieListApi.Dtos
{
    public class MovieUpdateDto
    {
        [Required]
        public string Title { get; set; }

        public string Comment { get; set; }

        [Required]
        [Range(0, 5)]
        public int Rating { get; set; }

        [MaxLength(300000)]
        public string ImageBase64 { get; set; }
    }
}