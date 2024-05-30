using System.Runtime.CompilerServices;
using PokeApiNet;
using Newtonsoft.Json;
using System.Diagnostics;

public static class PokemonGenerator {

  private static Random random = new Random();
  private static PokeApiClient pokeClient = new PokeApiClient();
  private static List<PlayerPokemonNature> Natures = new List<PlayerPokemonNature>();

  public static async Task<PlayerPokemon> Generate(int pokeID, int level = -1, List<PokemonMove>? pokemonMoves = null, PokemonAbility? ability = null) {
    Pokemon pokemon = await pokeClient.GetResourceAsync<Pokemon>(pokeID);

    level = (level < 0) ? random.Next(1, 101) : level; // if level not given generate random level from 1-100

    using (StreamReader r = new StreamReader("Data/Natures.json"))
    {
      string json = r.ReadToEnd();
      List<PlayerPokemonNature>? items = JsonConvert.DeserializeObject<List<PlayerPokemonNature>>(json);
      if (items is not null) {
        Natures = items;
      }
    }
    PlayerPokemonNature nature = Natures[random.Next(0, Natures.Count)]; // pick a random nature
    if (pokemonMoves is null) {
      pokemonMoves = new List<PokemonMove>(pokemon.Moves.OrderBy(x => random.Next()).Take(4)); // if no moves given generate a random move set
    }
    List<PlayerPokemonMove> moves = await GetMoves(pokemonMoves);
    int numberOfMoves = moves.Count;

    if (ability is null) {
      ability = pokemon.Abilities[random.Next(0, pokemon.Abilities.Count)]; // if ability not given pick one randomly
    }

    Ability abilityDetails = await pokeClient.GetResourceAsync(ability.Ability);
    string abilityDescription = "";
    foreach (var flavourText in abilityDetails.FlavorTextEntries) {
      if (flavourText.Language.Name.Equals("en")) { abilityDescription = flavourText.FlavorText; }
    }

    PokemonSpecies speciesDetails = await pokeClient.GetResourceAsync(pokemon.Species);
    string description = "";
    foreach (var flavourText in speciesDetails.FlavorTextEntries) {
      if (flavourText.Language.Name.Equals("en")) { description = flavourText.FlavorText; }
    }

    PlayerPokemon playerPokemon = new PlayerPokemon {
      PokeID = pokeID, 
      HP = GenerateStat(pokemon.Stats[0].BaseStat, level, 1, true),
      Atk = GenerateStat(pokemon.Stats[1].BaseStat, level, nature.Atk),
      Def = GenerateStat(pokemon.Stats[2].BaseStat, level, nature.Def),
      SpAtk = GenerateStat(pokemon.Stats[3].BaseStat, level, nature.SpAtk),
      SpDef = GenerateStat(pokemon.Stats[4].BaseStat, level, nature.SpDef),
      Spd = GenerateStat(pokemon.Stats[5].BaseStat, level, nature.Spd),
      NatureDetails = nature, 
      Move1 = moves[0],
      Move2 = numberOfMoves > 0 ? moves[1] : null,
      Move3 = numberOfMoves > 1 ? moves[2] : null,
      Move4 = numberOfMoves > 2 ? moves[3] : null,
      Ability = new PlayerPokemonAbility(ability.Ability.Name, abilityDescription),
      Level = level,
      Description = description
    };
    return playerPokemon;
  }

  public static async Task<PlayerPokemonWrapper> GenerateWrapper(PlayerPokemon playerPokemon) {
    Pokemon pokemon = await pokeClient.GetResourceAsync<Pokemon>(playerPokemon.PokeID);
    List<string> types = new List<string>();
    foreach (var type in pokemon.Types) {
      types.Add(type.Type.Name);
    }
    return new PlayerPokemonWrapper(playerPokemon, pokemon.Moves, pokemon.Sprites.FrontDefault, types, pokemon.Name);
  }

  private static int GenerateStat(int baseStat, int level, float? nature, bool HP = false) {
    float natureModifier = nature is not null ? (float) nature : 1;
    return (int) Math.Floor((((baseStat * 2 * level) / 100) + (HP ? level + 10 : 5)) * natureModifier);
  }

  private static async Task<List<PlayerPokemonMove>> GetMoves(List<PokemonMove> pokemonMoves)
  {
    List<PlayerPokemonMove> moves = new List<PlayerPokemonMove>();
    foreach (var pokemonMove in pokemonMoves) {
      Move pokemonMoveDetails = await pokeClient.GetResourceAsync(pokemonMove.Move);
      string name = pokemonMoveDetails.Name;
      int? power = pokemonMoveDetails.Power;
      int? pp = pokemonMoveDetails.Pp;
      int? accuracy = pokemonMoveDetails.Accuracy;
      string description = "";
      string type = pokemonMoveDetails.Type.Name;
      foreach (var flavourText in pokemonMoveDetails.FlavorTextEntries) {
        if (flavourText.Language.Name.Equals("en")) { description = flavourText.FlavorText; }
      }
      PlayerPokemonMove move = new PlayerPokemonMove(name, power, pp, accuracy, description, type);
      moves.Add(move);
    }
    return moves;
  }

  public static async Task<PlayerPokemon> GenerateEncounter(Zone zone) {
    ZonePokemon encounter = zone.PokemonList[random.Next(0, zone.PokemonList.Count)];
    PlayerPokemon pokemon = await Generate(encounter.PokeID, random.Next(encounter.MinLevel, encounter.MaxLevel + 1));
    return pokemon;
  }
}