import { useEffect, useState } from "react"
import { Pokemon, Zone } from "../Types"
import CurrentPokemonDisplay from "../components/CurrentPokemonDisplay"
import Zones from "../components/Zones"
import PokemonBox from "../components/PokemonBox"

// TODO:
// Create a component that takes the background image of each of the icons, and the credit.
// Simple div with the credit at the bottom.

export default function Home() {

  const [pokemonData, setPokemonData] = useState<Pokemon[]>([])
  const [zoneData, setZoneData] = useState<Zone[]>([])
  const [filteredPokemonData, setFilteredPokemonData] = useState<Pokemon[]>([])
  const [showBox, setShowBox] = useState(true)
  const [currentPokemon, setCurrentPokemon] = useState<Pokemon | null>(null)

  useEffect(() => {
    fetch("http://localhost:5287/box")
      .then(res => res.json())
      .then(res => { setPokemonData(res); setFilteredPokemonData(res); });
    fetch("http://localhost:5287/getZones")
      .then(res => res.json())
      .then(res => setZoneData(res))
  }, [])

  useEffect(() => {
    console.log(currentPokemon)
  }, [currentPokemon])

  return (
    <div className='m-5'>
      <div className='flex flex-col items-center justify-evenly'>
        { showBox ? <><PokemonBox pokemonData={pokemonData} filteredPokemonData={filteredPokemonData} setFilteredPokemonData={setFilteredPokemonData} setCurrentPokemon={setCurrentPokemon} setShowBox={setShowBox}/><Zones zoneData={zoneData}/></> : currentPokemon !== null ? <CurrentPokemonDisplay currentPokemon={currentPokemon} setShowBox={setShowBox}/>
         : <div>There was an error.</div>}
      </div>
    </div>
  )
}
