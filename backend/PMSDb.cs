using Microsoft.EntityFrameworkCore;
public class PMSDb : DbContext {
  public PMSDb(DbContextOptions options) : base(options) { }
  public DbSet<PlayerPokemon> PokemonItems { get; set; }
}