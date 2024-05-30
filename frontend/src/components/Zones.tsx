import { Zone } from "../Types"
import { useNavigate } from "react-router-dom"
import Background from "./Background"


type ZonesProps = {
  zoneData: Zone[]
}

export default function Zones({ zoneData } : ZonesProps) {

  const navigate = useNavigate()

  return (
    <div className="w-72">
      <h1 className="italic text-zinc-400 font-bold mb-3">Encounters</h1>
      <div className="flex flex-row justify-between w-full px-2 text-zinc-500 mb-2">
      </div>
      <ul className="w-full h-60 p-5 overflow-scroll bg-[#2C2C2C] rounded-xl shadow-heavy flex flex-row items-start flex-wrap justify-between">
        {zoneData.map(zone => {
          return (
            <button key={zone.name} onClick={() => navigate('/encounter', {state: zone})}>
              <Background background={zone.background} className="w-20 h-32 rounded-md p-1 text-center flex flex-col justify-center m-5" creditClassName="text-[0.5rem] mt-auto">
                <h1 className='text-sm font-bold mt-10'>{zone.name}</h1>
              </Background>
            </button>
          )
        })}
      </ul>
    </div>
  )
}
