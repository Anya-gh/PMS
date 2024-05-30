import { useEffect, useState, Dispatch, SetStateAction } from 'react'
import { useLocation, useNavigate } from 'react-router-dom'
import { Pokemon, Zone, ZonePokemon } from '../Types'
import Background from '../components/Background'
import pokeball from '../assets/pokeball.svg'
import dropdown from '../assets/dropdown.svg'

export default function Encounter() {

  const location = useLocation()
  const navigate = useNavigate()
  const zone : Zone = location.state
  const [pokemonEncounter, setPokemonEncounter] = useState<Pokemon>()
  const [encountered, setEncountered] = useState(false)

  useEffect(() => {
    console.log(pokemonEncounter)
  }, [pokemonEncounter])


  return (
    <div className='flex flex-col m-10 items-center'>
      <button className="rounded-xl shadow-heavy bg-[#2C2C2C] p-1 w-20 self-center mb-5" onClick={() => {navigate('/')}}>Back</button>
      <h1 className="text-white tracking-widest mb-5 text-xl">{zone.name} (Lv. {zone.minLevel} - {zone.maxLevel})</h1>
      <span className="self-end"><ViewEncounters pokemonList={zone.pokemonList}/></span>
      <Background className="w-72 flex flex-col items-center h-72 p-2" background={zone.background} creditClassName="mt-auto text-white text-xs text-wrap">
        <div className='flex flex-col items-center justify-center mt-10'>
          {/* pokeball shows up in front of encounter list; need to make it so bg is bg image eventually*/}
          {encountered && pokemonEncounter != undefined ?
            <div className="flex flex-col items-center mt-5">
              <img src={pokemonEncounter.sprite} className='w-36'/> 
              <p className="capitalize tracking-widest absolute top-96 font-bold">{pokemonEncounter.name} Lv. {pokemonEncounter.pokemonDetails.level}</p>
            </div>
           : !encountered && 
            <button onClick={() => {getEncounter(zone.id, setPokemonEncounter); setEncountered(true)}} className="flex flex-col items-center mt-20">
              <img className="w-10 animate-bounce" src={pokeball} />
            </button>
          }
        </div>
      </Background>
      {encountered && pokemonEncounter && 
        <div>
          <p className='italic text-zinc-500 text-sm mt-3'>{pokemonEncounter.pokemonDetails.description}</p>
        </div>
      }
    </div>
  )
}

type ViewEncountersProps = {
  pokemonList: ZonePokemon[]
}

function ViewEncounters( { pokemonList } : ViewEncountersProps) {

  const [showEncounters, setShowEncounters] = useState(false)

  return (
    <div style={{direction: "rtl"}}>
      <button style={{direction: "ltr"}} onClick={() => {setShowEncounters(showEncounters => !showEncounters)}}><span className="mb-3 flex flex-row rounded-xl shadow-heavy bg-[#2C2C2C] py-1 px-2 items-center"><p className="mr-2 italic text-sm text-zinc-500">View Encounters</p><img className={`${showEncounters ? "rotate-0" : "rotate-180"} w-3 opacity-50`} src={dropdown} /></span></button>
      {showEncounters && 
      <div style={{direction: "ltr"}} className="absolute self-end flex flex-col items-start rounded-xl shadow-heavy bg-[#2C2C2C] py-1 px-2 z-10">
        {pokemonList.map(pokemon => {
          return (
          <div  className="flex flex-row items-center">
            <img className="w-10" src={pokemon.sprite} />
            <p className="text-sm tracking-wide">{pokemon.name} Lv. {pokemon.minLevel} - {pokemon.maxLevel}</p>
          </div>
        )
        })}
      </div>}
    </div>
  )
}

const getEncounter = ( zoneID:number, setPokemonEncounter:Dispatch<SetStateAction<Pokemon | undefined>> ) => {
  fetch(`http://localhost:5287/getEncounter/${zoneID}`)
  .then(res => res.json())
  .then(res => setPokemonEncounter(res))
}

/*
Needs:
- Name
- Level range
- Background
- Encounter list
*/
