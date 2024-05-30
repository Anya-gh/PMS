using System.ComponentModel;
using Microsoft.IdentityModel.Tokens;
using PokeApiNet;

public class PlayerPokemon {
  public int ID { get; set; } 
  public int PokeID { get; set; }
  public int HP { get; set; }
  public int Atk { get; set; }
  public int Def { get; set; }
  public int SpAtk { get; set; }
  public int SpDef { get; set; }
  public int Spd { get; set; }
  public PlayerPokemonNature NatureDetails { get; set; } = null!;
  public PlayerPokemonMove? Move1 { get; set; }
  public PlayerPokemonMove? Move2 { get; set; }
  public PlayerPokemonMove? Move3 { get; set; }
  public PlayerPokemonMove? Move4 { get; set; }
  public PlayerPokemonAbility Ability { get; set; } = null!;
  public string Description { get; set; } = "";
  public int Level { get; set; }
}

public class PlayerPokemonMove {
  public string Name { get; set; }
  public int? Power { get; set; }
  public int? PP { get; set; }
  public int? Accuracy { get; set; }
  public string Description { get; set; }
  public string Type { get; set; }

  public PlayerPokemonMove(string name, int? power, int? PP, int? accuracy, string description, string type)
  {
    Name = name;
    Power = power;
    this.PP = PP;
    Accuracy = accuracy;
    Description = description;
    Type = type;
  }
}

public class PlayerPokemonAbility {
  public string Name { get; set; }
  public string Description { get; set; }

  public PlayerPokemonAbility(string name, string description) {
    Name = name;
    Description = description;
  }
}

public class PlayerPokemonNature {
  public string Name { get; set; }
  public float? Atk { get; set; }
  public float? Def { get; set; }
  public float? SpAtk { get; set; }
  public float? SpDef { get; set; }
  public float? Spd { get; set; }

  public PlayerPokemonNature(string name, float? atk, float? def, float? spAtk, float? spDef, float? spd)
  {
    Name = name;
    Atk = atk;
    Def = def;
    SpAtk = spAtk;
    SpDef = spDef;
    Spd = spd;
  }
}
