import { describe, test, expect, vi, beforeEach } from "vitest"
import { render, screen, fireEvent } from "@testing-library/react"
import Encounter from "../pages/Encounter"
import { MemoryRouter } from "react-router-dom"

const mocks = vi.hoisted(() => {

  const locationMockValue:({state : { id : number } | undefined}) = { state : { id : 1 } }

  return {
    fetchMock: vi.fn(),
    navigateMock: vi.fn(),
    locationMock: locationMockValue
  }
})

global.fetch = mocks.fetchMock

vi.mock('react-router-dom', async () => {
  const imports = await import('react-router-dom')
  return {
    ...imports,
    useLocation: () => mocks.locationMock,
    useNavigate: () => mocks.navigateMock
  }
})

describe("Encounter testing", () => {
  beforeEach(() => {
    mocks.navigateMock.mockClear()
    mocks.locationMock = {state : {id : 1}}
  })
  test("navigates if location is undefined", () => {

    mocks.locationMock.state = undefined

    render(
      <MemoryRouter>
        <Encounter />
      </MemoryRouter>
    )

    expect(mocks.navigateMock).toHaveBeenCalledWith("/")
  })
  test("does not navigate if location is defined", () => {

    render(
      <MemoryRouter>
        <Encounter />
      </MemoryRouter>
    )

    expect(mocks.navigateMock).not.toHaveBeenCalledWith('/')
  })
  test("API called correctly", () => {
    
    render(
      <MemoryRouter>
        <Encounter />
      </MemoryRouter>)

    const encounterButton = screen.getByTestId("encounter-button")

    fireEvent.click(encounterButton)

    expect(mocks.fetchMock).toHaveBeenCalledWith('http://localhost:5287/getEncounter/1')
  })
})