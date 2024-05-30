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

app.MapGet("/", () => "PMS API. Hello!");

app.MapGet("/test/add", async (PMSDb db) => {
  var pokemonList = new List<PlayerPokemonWrapper>
  {
      await PokemonGenerator.GenerateWrapper(await PokemonGenerator.GenerateEncounter(Zones[0])),
      await PokemonGenerator.GenerateWrapper(await PokemonGenerator.GenerateEncounter(Zones[0])),
      await PokemonGenerator.GenerateWrapper(await PokemonGenerator.GenerateEncounter(Zones[0])),
      await PokemonGenerator.GenerateWrapper(await PokemonGenerator.GenerateEncounter(Zones[0])),
      await PokemonGenerator.GenerateWrapper(await PokemonGenerator.GenerateEncounter(Zones[0]))
  };
  /*foreach (var pokemon in pokemonList) {
    db.PokemonItems.Add(pokemon);
  }
  await db.SaveChangesAsync();

  return Results.Created("/test/add", pokemonList);*/
  return pokemonList;
});

app.MapGet("/test/addToDB", async (PMSDb db) => {
  var pokemonList = new List<PlayerPokemon>
  {
      await PokemonGenerator.GenerateEncounter(Zones[0]),
      await PokemonGenerator.GenerateEncounter(Zones[0]),
      await PokemonGenerator.GenerateEncounter(Zones[0]),
      await PokemonGenerator.GenerateEncounter(Zones[0]),
      await PokemonGenerator.GenerateEncounter(Zones[0])
  };
  foreach (var pokemon in pokemonList) {
    db.PokemonItems.Add(pokemon);
  }
  db.SaveChanges();

  return Results.Created("/test/addToDB", pokemonList);
});

app.MapGet("/box", async (PMSDb db) => {
  var pokemonList = new List<PlayerPokemonWrapper>();
  foreach(var playerPokemon in db.PokemonItems)
  {
    PlayerPokemonWrapper currentPokemonWrapper = await PokemonGenerator.GenerateWrapper(playerPokemon);
    pokemonList.Add(currentPokemonWrapper);
  }
  return pokemonList;
});

app.MapGet("/getZones", () => {
  if (Zones.Count > 0)
  {
    return Results.Ok(Zones);
  }
  else {
    return Results.StatusCode(500);
  }
});

app.MapGet("/getEncounter/{id}", async (int id, PMSDb db) => {
  Zone? encounterZone = null;
  foreach(var zone in Zones) {
    if (zone.ID == id) {
      encounterZone = zone;
      break;
    }
  }
  if (encounterZone is not null) {
    PlayerPokemon pokemonEncounter = await PokemonGenerator.GenerateEncounter(encounterZone);
    db.PokemonItems.Add(pokemonEncounter);
    db.SaveChanges();
    PlayerPokemonWrapper pokemonEncounterWrapper = await PokemonGenerator.GenerateWrapper(pokemonEncounter);
    return Results.Ok(pokemonEncounterWrapper);
  }
  else {
    return Results.NotFound();
  }
});

app.Run();
