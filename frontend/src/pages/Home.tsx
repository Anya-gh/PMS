import { useEffect, useState } from "react"
import { Pokemon } from "../Types"
import CurrentPokemonDisplay from "../components/CurrentPokemonDisplay"
import EncountersBox from "../components/EncountersBox"
import PokemonBox from "../components/PokemonBox"

export default function Home() {

  const [pokemonData, setPokemonData] = useState<Pokemon[]>([])
  const [filteredPokemonData, setFilteredPokemonData] = useState<Pokemon[]>([])
  const [showBox, setShowBox] = useState(true)
  const [currentPokemon, setCurrentPokemon] = useState<Pokemon | null>(null)

  useEffect(() => {
    const fetchRequest = () => {
      fetch("http://localhost:5287/test/add")
      .then(res => res.json())
      .then(res => { setPokemonData(res); setFilteredPokemonData(res); })
    }
    fetchRequest()
  }, [])

  useEffect(() => {
    console.log(currentPokemon)
  }, [currentPokemon])

  return (
    <div className='m-5'>
      <div className='flex flex-col items-center justify-evenly'>
        { showBox ? <><PokemonBox pokemonData={pokemonData} filteredPokemonData={filteredPokemonData} setFilteredPokemonData={setFilteredPokemonData} setCurrentPokemon={setCurrentPokemon} setShowBox={setShowBox}/><EncountersBox /></> : currentPokemon !== null ? <CurrentPokemonDisplay currentPokemon={currentPokemon} setShowBox={setShowBox}/>
         : <div>There was an error.</div>}
      </div>
    </div>
  )
}
