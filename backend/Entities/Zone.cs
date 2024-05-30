using Microsoft.IdentityModel.Tokens;
using PokeApiNet;

public class Zone {
  public string Name { get; set; }
  public string Background { get; set; }
  public int ID { get; set; }
  public List<ZonePokemon> PokemonList { get; set; }
  public int MinLevel { get; set; }
  public int MaxLevel { get; set; }

  public Zone(string name, string background, int id, List<ZonePokemon> pokemonList, int minLevel, int maxLevel)
  {
    Name = name;
    Background = background;
    ID = id;
    PokemonList = pokemonList;
    MinLevel = minLevel;
    MaxLevel = maxLevel;
  }
}

public class ZonePokemon {
  public int PokeID { get; set; }
  public string Name { get; set; }
  public string Sprite { get; set; }
  public int MinLevel { get; set; }
  public int MaxLevel { get; set; }

  public ZonePokemon(int pokeID, string name, int minLevel, int maxLevel, string sprite)
  {
    PokeID = pokeID;
    Name = name;
    MinLevel = minLevel;
    MaxLevel = maxLevel;
    Sprite = sprite;
  }
}