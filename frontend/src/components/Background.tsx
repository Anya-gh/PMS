import { PropsWithChildren, useEffect, useState, ReactElement } from "react"
import grassland from '../assets/grassland.png'
import clouds from '../assets/clouds.png'
import desertification from '../assets/desertification.png'
import manmade from '../assets/manmade.png'
import mountain from '../assets/mountain.png'
import riparian from '../assets/riparian.png'
import space from '../assets/space.png'
import volcano from '../assets/volcano.png'

type BackgroundProps = {
  className?: string,
  creditClassName?: string,
  background: string
}

export default function Background( props: PropsWithChildren<BackgroundProps> ) {

  const [bg, setBg] = useState("")
  const [credit, setCredit] = useState<ReactElement>()

  useEffect(() => {
    if (props.background == "grassland") {
      setBg(grassland)
      setCredit(<a href="https://www.flaticon.com/free-icons/landscape" title="landscape icons">Landscape icons created by Freepik - Flaticon</a>)
    }
    if (props.background == "clouds") {
      setBg(clouds)
      setCredit(<a href="https://www.flaticon.com/free-icons/sky" title="sky icons">Sky icons created by Freepik - Flaticon</a>)
    }
    if (props.background == "desertification") {
      setBg(desertification)
      setCredit(<a href="https://www.flaticon.com/free-icons/desertification" title="desertification icons">Desertification icons created by Freepik - Flaticon</a>)
    }
    if (props.background == "manmade") {
      setBg(manmade)
      setCredit(<a href="https://www.flaticon.com/free-icons/town" title="town icons">Town icons created by Freepik - Flaticon</a>)
    }
    if (props.background == "mountain") {
      setBg(mountain)
      setCredit(<a href="https://www.flaticon.com/free-icons/mountain" title="mountain icons">Mountain icons created by Freepik - Flaticon</a>)
    }
    if (props.background == "riparian") {
      setBg(riparian)
      setCredit(<a href="https://www.flaticon.com/free-icons/forest" title="forest icons">Forest icons created by Freepik - Flaticon</a>)
    }
    if (props.background == "space") {
      setBg(space)
      setCredit(<a href="https://www.flaticon.com/free-icons/astronomy" title="astronomy icons">Astronomy icons created by Freepik - Flaticon</a>)
    }
    if (props.background == "volcano") {
      setBg(volcano)
      setCredit(<a href="https://www.flaticon.com/free-icons/lava" title="lava icons">Lava icons created by Freepik - Flaticon</a>)
    }
  }, [setBg, props.background])

  return (
    <div style={{ backgroundImage: `url(${bg})` }} className='bg-cover bg-center'>
      <div className={props.className}>
        {props.children}
        <span className={props.creditClassName}>{credit}</span>
      </div>
    </div>
  )
}
