using Microsoft.EntityFrameworkCore;
public class PMSDb : DbContext {
  public PMSDb(DbContextOptions options) : base(options) { }
  public virtual DbSet<PlayerPokemon> PokemonItems { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<PlayerPokemon>(entity => {
            entity.OwnsOne(e => e.NatureDetails);
            entity.OwnsOne(e => e.Move1);
            entity.OwnsOne(e => e.Move2);
            entity.OwnsOne(e => e.Move3);
            entity.OwnsOne(e => e.Move4);
            entity.OwnsOne(e => e.Ability);
        });
    }
}