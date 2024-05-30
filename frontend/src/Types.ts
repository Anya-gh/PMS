export type Pokemon = {
  pokemonDetails: pokemonDetails
  name: string,
  sprite: string,
  types: pokemonType[]
}

export type pokemonDetails = {
  hp: number,
  atk: number,
  def: number,
  spAtk: number,
  spDef: number,
  spd: number,
  move1: move,
  move2: move,
  move3: move,
  move4: move,
  ability: { name: string, description: string },
  level: number,
  description: string
}

export type move = {
  name: string,
  accuracy: number | null,
  description: string,
  power: number | null,
  pp: number,
  type: pokemonType
}

export type pokemonType = "normal" | "fire" | "fighting" | "water" | "flying" | "grass" | "poison" | 
"electric" | "ground" | "psychic" | "rock" | "ice" | "bug" | "dragon" | "ghost" |
"dark" | "steel" | "fairy"

export type Zone = {
  name: string,
  background: string,
  id: number,
  pokemonList: ZonePokemon[],
  minLevel: number,
  maxLevel: number
}

export type ZonePokemon = {
  name: string,
  pokeID: number,
  sprite: string,
  minLevel: number,
  maxLevel: number
}