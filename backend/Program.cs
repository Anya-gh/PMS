using Microsoft.EntityFrameworkCore;
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

app.MapGet("/", () => "PMS API. Hello!");

PokemonGenerator generator = new PokemonGenerator();

app.MapGet("/box", async (PMSDb db) => await RouteHandlers.GetBox(db, generator));

app.MapGet("/zones", () => RouteHandlers.GetZones());

app.MapPost("/encounter", async (EncounterRequest request, PMSDb db) => await RouteHandlers.GetEncounter(request.ZoneID, db, generator));

app.Run();

public class EncounterRequest {
  public int ZoneID {get; set;}
}

public static class RouteHandlers {

  private static List<Zone> Zones;

  static RouteHandlers() {
    Zones = new List<Zone>();
    using (StreamReader r = new StreamReader("Data/Zones.json"))
    {
      string json = r.ReadToEnd();
      List<Zone>? items = JsonConvert.DeserializeObject<List<Zone>>(json);
      if (items is not null) {
        Zones = items;
      }
    }
  }

  public static async Task<IResult> GetBox(PMSDb db, IPokemonGenerator generator) {
    var pokemonList = new List<PlayerPokemonWrapper>();
    foreach(var playerPokemon in db.PokemonItems)
    {
      PlayerPokemonWrapper currentPokemonWrapper = await generator.GenerateWrapper(playerPokemon);
      pokemonList.Add(currentPokemonWrapper);
    }
    return Results.Ok(pokemonList);
  }
  
  public static IResult GetZones() {
    if (Zones.Count > 0)
    {
      return Results.Ok(Zones);
    }
    else {
      return Results.StatusCode(500);
    }
  }

  public static async Task<IResult> GetEncounter(int id, PMSDb db, IPokemonGenerator generator) {
    Zone? encounterZone = null;
    foreach(var zone in Zones) {
      if (zone.ID == id) {
        encounterZone = zone;
        break;
      }
    }
    if (encounterZone is not null) {
      PlayerPokemon pokemonEncounter = await generator.GenerateEncounter(encounterZone);
      db.PokemonItems.Add(pokemonEncounter);
      db.SaveChanges();
      PlayerPokemonWrapper pokemonEncounterWrapper = await generator.GenerateWrapper(pokemonEncounter);
      return Results.Ok(pokemonEncounterWrapper);
    }
    else {
      return Results.NotFound();
    }
  }
}