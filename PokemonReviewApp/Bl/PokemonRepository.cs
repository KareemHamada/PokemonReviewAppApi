using PokemonReviewApp.Data;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Bl
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokemonRating(int pokeId);
        bool PokemonExist(int pokeId);
    }
    public class PokemonRepository : IPokemonRepository
    {
        private readonly DataContext _context;
        public PokemonRepository(DataContext context)
        {
            this._context = context;
        }

        public Pokemon GetPokemon(int id)
        {
            return _context.Pokemons.FirstOrDefault(x => x.Id == id);
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.FirstOrDefault(x => x.Name == name);
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _context.Reviews.Where(x => x.Pokemon.Id == pokeId);
            if (review.Count() <= 0)
                return 0;
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Pokemon> GetPokemons()
        {
            return _context.Pokemons.OrderBy(a=>a.Id).ToList();
        }

        public bool PokemonExist(int pokeId)
        {
            return _context.Pokemons.Any(x => x.Id == pokeId);
        }
    }
}
