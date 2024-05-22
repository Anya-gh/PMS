using Microsoft.EntityFrameworkCore;
using PokeApiNet;
using Newtonsoft.Json;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PMSDb>( options => options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")) );
builder.Services.AddCors(options => {
  options.AddPolicy(name: MyAllowSpecificOrigins,
    policy => {
      policy.WithOrigins("http://localhost:3000")
      .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

var optionsBuilder = new DbContextOptionsBuilder<PMSDb>();
    optionsBuilder.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));

List<Zone> Zones = new List<Zone>();
using (StreamReader r = new StreamReader("Data/Zones.json"))
{
  string json = r.ReadToEnd();
  List<Zone>? items = JsonConvert.DeserializeObject<List<Zone>>(json);
  if (items is not null) {
    Zones = items;
  }
}

PokeApiClient pokeClient = new PokeApiClient();

app.MapGet("/", () => "Hello World!");

app.MapGet("/test/add", async (PMSDb db) => {
  var pokemonList = new List<PlayerPokemonWrapper>
  {
      await PokemonGenerator.GenerateEncounter(Zones[0]),
      await PokemonGenerator.GenerateEncounter(Zones[0]),
      await PokemonGenerator.GenerateEncounter(Zones[0]),
      await PokemonGenerator.GenerateEncounter(Zones[0]),
      await PokemonGenerator.GenerateEncounter(Zones[0])
  };
  /*foreach (var pokemon in pokemonList) {
    db.PokemonItems.Add(pokemon);
  }
  await db.SaveChangesAsync();

  return Results.Created("/test/add", pokemonList);*/
  return pokemonList;
});

/*app.MapGet("/test/box", async (PMSDb db) => {
  var pokemonList = new List<PlayerPokemonWrapper>();
  foreach(var PokemonItem in db.PokemonItems)
  {
    Pokemon currentPokemon = await pokeClient.GetResourceAsync<Pokemon>(PokemonItem.PokeID);
    PlayerPokemonWrapper currentPokemonWrapper = new PlayerPokemonWrapper(PokemonItem, currentPokemon.Moves, currentPokemon.Sprites.FrontDefault, currentPokemon.Types, currentPokemon.Name);
    pokemonList.Add(currentPokemonWrapper);
  }
  return pokemonList;
});*/

app.Run();
