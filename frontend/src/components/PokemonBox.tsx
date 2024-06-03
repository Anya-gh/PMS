import { Dispatch, SetStateAction, useEffect, useState } from "react"
import { Pokemon } from "../Types"
import search from "../assets/search.svg"
import dropdown from "../assets/dropdown.svg"

type BoxProps = {
  pokemonData: Pokemon[],
  filteredPokemonData: Pokemon[],
  setFilteredPokemonData: Dispatch<SetStateAction<Pokemon[]>>,
  setCurrentPokemon: Dispatch<SetStateAction<Pokemon | null>>,
  setShowBox: Dispatch<SetStateAction<boolean>>
}

export default function PokemonBox({ pokemonData, filteredPokemonData, setFilteredPokemonData, setCurrentPokemon, setShowBox } : BoxProps) {

  const [searchTerm, setSearchTerm] = useState("")

  useEffect(() => {
    const newData = pokemonData.filter(pokemon => pokemon.name.includes(searchTerm.toLowerCase()))
    setFilteredPokemonData(newData)
  }, [searchTerm, pokemonData, setFilteredPokemonData])

  return (
    <div className="w-72 mb-5">
      <h1 className="italic text-zinc-400 font-bold mb-3">Your Pokemon</h1>
      <div className="flex flex-row justify-between w-full px-2 text-zinc-500 mb-2">
        <Search searchTerm={searchTerm} setSearchTerm={setSearchTerm}/>
        <span className="flex flex-row"><Sort setFilteredPokemonData={setFilteredPokemonData}/><BoxNumber /></span>
      </div>
      <ul data-testid="filtered-pokemon-container" className="w-full h-60 overflow-scroll bg-[#2C2C2C] p-1 rounded-xl shadow-heavy flex flex-row items-start flex-wrap">
        {filteredPokemonData.map((pokemon, index) => {
          return (
              <li key={index}><button onClick={() => {onClickHandler(index, pokemonData, setCurrentPokemon, setShowBox)}}><img alt={pokemon.name} src={pokemon.sprite} className="w-16"/></button></li>
          )
        })}
      </ul>
    </div>
  )
}

type SearchProps = {
  searchTerm: string
  setSearchTerm: Dispatch<SetStateAction<string>>
}

function Search({ searchTerm, setSearchTerm } : SearchProps) {
  return (
      <span className="flex flex-row items-center italic bg-[#2C2C2C] shadow-heavy rounded-xl py-1 px-2"><img className="w-3 mr-1" src={search} /><input className="w-24 outline-none rounded-xl bg-[#2C2C2C] italic" alt="box search" placeholder="Search" type="text" onChange={(e) => {setSearchTerm(e.target.value)}} value={searchTerm}/></span>
  )
}

const onClickHandler = (id: number, pokemonData: Pokemon[], setCurrentPokemon: Dispatch<SetStateAction<Pokemon | null>>, setShowBox: Dispatch<SetStateAction<boolean>>) => {
  setCurrentPokemon(pokemonData[id])
  setShowBox(false)
}

function BoxNumber() {

  const [showMenu, setShowMenu] = useState(false)

  return (
    <button onClick={() => setShowMenu(showMenu => !showMenu)}><span className="flex flex-row rounded-xl shadow-heavy bg-[#2C2C2C] py-1 px-2 items-center"><p className="mr-2 italic">Box</p><img className={`${showMenu ? "rotate-0" : "rotate-180"} w-3 opacity-50`} src={dropdown} /></span></button>
  )
}

type SortProps = {
  setFilteredPokemonData: Dispatch<SetStateAction<Pokemon[]>>
}

function Sort( { setFilteredPokemonData } : SortProps) {

  const [showMenu, setShowMenu] = useState(false)

  const onClickHandler = ( sort: string ) => {
    switch (sort) {
      case "Name":
        setFilteredPokemonData(pokemonData => {
          const newData = [...pokemonData].sort((a,b) => a.name.localeCompare(b.name))
          return newData
        })
        break
      case "Level":
        setFilteredPokemonData(pokemonData => {
          const newData = [...pokemonData].sort((a,b) => a.pokemonDetails.level > b.pokemonDetails.level ? 1 : -1)
          return newData
        })
    }
  }

  return (
    <div>
      <div className="flex flex-col items-start bg-[#2C2C2C] rounded-xl shadow-heavy py-1 px-2 mr-2"> 
        <button onClick={() => setShowMenu(showMenu => !showMenu)}><span className="flex flex-row"><p className="mr-2 italic">Sort</p><img className={`${showMenu ? "rotate-0" : "rotate-180"} w-3 opacity-50`} src={dropdown} /></span></button>
      </div>
      {showMenu && 
        <ul className='absolute flex flex-col items-start rounded-xl shadow-heavy bg-[#2C2C2C] py-1 px-2 mt-1'>
          <li className='pb-2' key="Name"><button onClick={() => { onClickHandler("Name"); setShowMenu(false) }}><p>Name</p></button></li>
          <li key="Level"><button onClick={() => { onClickHandler("Level"); setShowMenu(false) }}><p>Level</p></button></li>
        </ul>
        }
    </div>
  )
}