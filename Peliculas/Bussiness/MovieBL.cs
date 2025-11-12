using Peliculas.Core;
using Peliculas.Dtos;
using Peliculas.Entities;

namespace Peliculas.Bussiness
{
    public class MovieBL
    {
        private readonly IMovieRepository _movieRepository;
        public MovieBL(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieDto> GetMovieAsync(int movieId)
        {
            MovieEntity entity;
            MovieDto dto;
            entity =  await _movieRepository.GetMovieAsync(movieId);
            dto = new MovieDto
            {
                Date = entity.Date,
                EncodeKey = entity.EncodeKey,
                IsWatched = entity.IsWatched,
                Sinopsis = entity.Sinopsis,
                Title = entity.Title
            };
            return dto;
        }

        public async Task<List<MovieDto>> GetMoviesAsync()
        {
            List<MovieEntity> entities;
            List<MovieDto> dtos;

            entities = await _movieRepository.GetMoviesAsync();
            dtos = entities.Select(x => new MovieDto
            {
                Date = x.Date,
                EncodeKey = x.EncodeKey,
                IsWatched = x.IsWatched,
                Sinopsis = x.Sinopsis,
                Title = x.Title
            }).ToList();

            return dtos;
        }

        public async Task UpdateAsync(int movieId, MovieDtoIn movie)
        {
            MovieEntity entity = await _movieRepository.GetMovieAsync(movieId);

            if (entity == null)
            {
                throw new KeyNotFoundException($"No se encontró la película con ID: {movieId}");

            }
            entity.Title = movie.Title;
            entity.Sinopsis = movie.Sinopsis;
            await _movieRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int movieId)
        {
            await _movieRepository.DeleteAsync(movieId);
        }

        public async Task<int> AddAsync(MovieDtoIn movie)
        {
            int id;
            MovieEntity entity;

            entity = new MovieEntity
            {
                EncodeKey = movie.EncodeKey,
                Sinopsis = movie.Sinopsis,
                Title = movie.Title,
            };

            id = await _movieRepository.AddAsync(entity);
            return id;
        }
    }
}
