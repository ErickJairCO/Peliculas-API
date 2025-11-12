using Microsoft.EntityFrameworkCore;
using Peliculas.Context;
using Peliculas.Core;
using Peliculas.Dtos;
using Peliculas.Entities;

namespace Peliculas.Persistence
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDbContext _appDbContext;
        public MovieRepository(AppDbContext appDbContext)
        { 
            _appDbContext = appDbContext;
        }

        public async Task<int> AddAsync(MovieEntity movieEntity)
        {
            _appDbContext.Movie.Add(movieEntity);
            await _appDbContext.SaveChangesAsync();
            return movieEntity.Id;
        }

        /*
        public async Task<List<MovieEntity>> GetMoviesAsync()
        {
            List<MovieEntity> movies;

            movies = await _appDbContext.Movie.Where(x => x.IsActivate == true).ToListAsync();
            return new List<MovieEntity>();
        }*/

        public async Task<List<MovieEntity>> GetMoviesAsync()
        => await _appDbContext.Movie.Where(x => x.IsActivate == true).ToListAsync();

        public async Task<MovieEntity> GetMovieAsync(int movieId) 
        => await  _appDbContext.Movie.FirstOrDefaultAsync(x => x.Id == movieId && x.IsActivate == true);

        public async Task UpdateAsync(MovieEntity movieEntity)
        {
            _appDbContext.Entry(movieEntity).State = EntityState.Modified;

            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int movieId)
        {
            await _appDbContext.Movie
                .Where(x => x.Id == movieId)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.IsActivate, false));
        }
    }
}
