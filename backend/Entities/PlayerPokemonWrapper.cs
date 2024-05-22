using PokeApiNet;

public class PlayerPokemonWrapper {
  public PlayerPokemon PokemonDetails { get; set; }
  public List<PokemonMove> MoveList { get; set; }
  public string Sprite { get; set; }
  public List<string> Types { get; set; }
  public string Name { get; set; }

  public PlayerPokemonWrapper(PlayerPokemon pokemonDetails, List<PokemonMove> moveList, string sprite, List<string> types, string name) {
    PokemonDetails = pokemonDetails;
    MoveList = moveList;
    Sprite = sprite;
    Types = types;
    Name = name;
  }
}