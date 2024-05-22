
export default function EncountersBox() {
  return (
    <div className="w-72">
      <h1 className="italic text-zinc-400 font-bold mb-3">Encounters</h1>
      <div className="flex flex-row justify-between w-full px-2 text-zinc-500 mb-2">
      </div>
      <ul className="w-full h-60 p-5 overflow-scroll bg-[#2C2C2C] rounded-xl shadow-heavy flex flex-row items-start flex-wrap">
        <button className='w-28 h-28 rounded-md bg-emerald-500 p-1 text-center flex flex-col justify-center m-1'>
          <h1 className='text-xs'>Emerald Grove</h1>
        </button>
      </ul>
    </div>
  )
}
