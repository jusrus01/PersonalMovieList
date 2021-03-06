namespace PersonalMovieListApi.Dtos
{
    public class MovieReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public byte[] Image { get; set; }
    }
}