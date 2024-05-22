using Microsoft.IdentityModel.Tokens;
using PokeApiNet;

public class Zone {
  public string Name;
  public List<ZonePokemon> PokemonList = new List<ZonePokemon>();

  public Zone(string name, List<ZonePokemon> pokemonList)
  {
    Name = name;
    PokemonList = pokemonList;
  }
}

public class ZonePokemon {
  public int PokeID;
  public int MinLevel;
  public int MaxLevel;
}