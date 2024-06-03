import { Dispatch, SetStateAction, useState, useEffect } from "react"
import type { Pokemon, move } from "../Types";

type CurrentPokemonDisplayTypes = {
  currentPokemon: Pokemon,
  setShowBox: Dispatch<SetStateAction<boolean>>
}

interface TypeColors {
  [key: string]: string;
}

const typeColors: TypeColors = {
  normal: "bg-gray-400",
  fire: "bg-red-600",
  fighting: "bg-orange-600",
  water: "bg-blue-500",
  flying: "bg-blue-300",
  grass: "bg-green-500",
  poison: "bg-purple-600",
  electric: "bg-yellow-400",
  ground: "bg-amber-800",
  psychic: "bg-pink-600",
  rock: "bg-stone-400",
  ice: "bg-cyan-400",
  bug: "bg-lime-400",
  dragon: "bg-blue-700",
  ghost: "bg-violet-900",
  steel: "bg-slate-500",
  fairy: "bg-fuchsia-400"
}

const formatName = (name:string) => {
  return name.replace('-', ' ')
}

export default function CurrentPokemonDisplay( { currentPokemon, setShowBox } : CurrentPokemonDisplayTypes) {

  const [movesetActive, setMovesetActive] = useState(false)
  const [moveset, setMoveset] = useState<move[]>([])

  useEffect(() => {
    setMoveset(
      [currentPokemon.pokemonDetails.move1, currentPokemon.pokemonDetails.move2, 
        currentPokemon.pokemonDetails.move3, currentPokemon.pokemonDetails.move4]
    )
  }, [])

  return (
    <div className='flex flex-col items-center text-white w-48'>
      <button className="rounded-xl shadow-heavy bg-[#2C2C2C] p-1 w-20 self-center mb-5" onClick={() => {setShowBox(true)}}>Back</button>
      <span className="flex flex-row items-baseline italic self-start">
        <button onClick={() => {setMovesetActive(false)}}><h1 className={`pr-2 tracking-wide font-bold transition-all duration-200 ease-in-out ${movesetActive ? "text-sm text-gray-500" : "text-2xl"}`}>Stats</h1></button>
        <p className="text-gray-700">/</p>
        <button onClick={() => {setMovesetActive(true)}}><h1 className={`pl-2 tracking-wide font-bold transition-all duration-200 ease-in-out ${movesetActive ? "text-2xl" : "text-sm text-gray-500"} `}>Moveset</h1></button>
      </span>
      <span className="flex flex-row self-start tracking-widest pb-3"><h1 className="capitalize pr-2">{currentPokemon.name}</h1><p>Lv. {currentPokemon.pokemonDetails.level}</p></span>
      <span className="flex flex-row self-start">
        {currentPokemon.types.map(type => {
          return (<Type name={type} key={type}/>)
        })}
      </span>
      <img className="w-32" src={currentPokemon.sprite}/>
      <AbilityDisplay name={currentPokemon.pokemonDetails.ability.name} description={currentPokemon.pokemonDetails.ability.description} />
      <span className="mb-5"/>
      {movesetActive ? <MovesetDisplay moveset={moveset}/> : <StatsDisplay hp={currentPokemon.pokemonDetails.hp} atk={currentPokemon.pokemonDetails.atk} def={currentPokemon.pokemonDetails.def} spAtk={currentPokemon.pokemonDetails.spAtk} spDef={currentPokemon.pokemonDetails.spDef} spd={currentPokemon.pokemonDetails.spd} />}
    </div>
  )
}

type TypeProps = {
  name: string
}

function Type({ name } : TypeProps) {

  return (
    <div className={`rounded-lg py-1 px-4 ${typeColors[name]} mr-2`}><p className="drop-shadow capitalize">{name}</p></div>
  )
}

type AbilityDisplayProps = {
  name: string,
  description: string
}

function AbilityDisplay({ name, description } : AbilityDisplayProps) {

  return (
    <div className='w-full my-1'>
      <span className='flex flex-row justify-between items-center w-full'>
        <h1 className="font-bold capitalize">{formatName(name)}</h1><p className="italic text-gray-500">ability</p>
      </span>
      <p className="font-light">{description}</p>
    </div>
  )
}

type MoveDisplayProps = {
  name: string,
  power: number | null,
  accuracy: number | null,
  pp: number
  type: string
}

function MoveDisplay({ name, power, accuracy, pp, type } : MoveDisplayProps) {

  return (
    <div className="w-full flex flex-row my-1">
      <span className={`border-2 border-zinc-300 p-1 rounded-l-lg capitalize flex-grow whitespace-nowrap overflow-scroll ${typeColors[type]}`}>{formatName(name)}</span>
      <span className={`border-r-2 border-t-2 border-b-2 border-zinc-300 p-1 bg-zinc-500`}>{power ? power : "--"}</span>
      <span className="border-r-2 border-t-2 border-b-2 border-zinc-300 p-1 bg-zinc-500">{accuracy ? accuracy : "--"}</span>
      <span className="border-r-2 border-t-2 border-b-2 border-zinc-300 p-1 rounded-r-lg bg-zinc-500">{pp}</span>
    </div>
  )
}

type MovesetDisplayProps = {
  moveset: move[]
}

function MovesetDisplay( { moveset } : MovesetDisplayProps) {

  return (
    <>
      {moveset.map(move => {
        return (
          <MoveDisplay name={move.name} power={move.power} accuracy={move.accuracy} pp={move.pp} type={move.type} key={move.name}/>
        )
      })}
    </>
  )
}

type StatsDisplayProps = {
  hp: number,
  atk: number,
  def: number,
  spAtk: number,
  spDef: number,
  spd: number
}

function StatsDisplay({ hp, atk, def, spAtk, spDef, spd} : StatsDisplayProps) {

  const [maxStat, setMaxStat] = useState(0)

  const values = [hp, atk, def, spAtk, spDef, spd]
  values.forEach(stat => {
    if (stat > maxStat) {
      setMaxStat(stat)
    }
  })
  
  // still need to set minimum

  return (
    <div className="flex flex-col items-start self-start text-sm">
      <span style={{width: (hp/maxStat)*200}} className={`bg-green-400 rounded-r-xl p-1 flex flex-row justify-between`}><p>HP:</p><p>{hp}</p></span>
      <span style={{width: (atk/maxStat)*200}} className={`bg-yellow-400 rounded-r-xl p-1 flex flex-row justify-between`}><p>Atk:</p><p>{atk}</p></span>
      <span style={{width: (def/maxStat)*200}} className={`bg-orange-400 rounded-r-xl p-1 flex flex-row justify-between`}><p>Def:</p><p>{def}</p></span>
      <span style={{width: (spAtk/maxStat)*200}} className={`bg-cyan-400 rounded-r-xl p-1 flex flex-row justify-between`}><p>Sp.Atk:</p><p>{spAtk}</p></span>
      <span style={{width: (spDef/maxStat)*200}} className={`bg-blue-400 rounded-r-xl p-1 flex flex-row justify-between`}><p>Sp.Def:</p><p>{spDef}</p></span>
      <span style={{width: (spd/maxStat)*200}} className={`bg-purple-400 rounded-r-xl p-1 flex flex-row justify-between`}><p>Spd:</p><p>{spd}</p></span>
    </div>
  )
}