using AutoMapper;
using PersonalMovieListApi.Models;
using PersonalMovieListApi.Dtos;

namespace PersonalMovieListApi.Profiles
{
    public class MoviesProfile : Profile
    {
        public MoviesProfile()
        {
            CreateMap<MovieModel, MovieReadDto>();
            CreateMap<MovieUpdateDto, MovieModel>();
            CreateMap<MovieCreateDto, MovieModel>();
        }
    }
}