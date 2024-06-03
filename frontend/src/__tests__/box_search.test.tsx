import { describe, test, expect } from 'vitest'
import { render, screen, fireEvent, within, waitFor } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import Home from '../pages/Home'

describe('Seach function testing', () => {
  test("Filters for Pokemon with 'a' in their name", async () => {

    const searchTerm = "a"

    render(
      <MemoryRouter>
        <Home />
      </MemoryRouter>
    )

    const searchInput = screen.getByPlaceholderText("Search")

    fireEvent.change(searchInput, {target: {value: searchTerm}})
    const container = screen.getByTestId("filtered-pokemon-container")

    await waitFor(() => {
      expect(within(container).getAllByRole('img').length).toBeGreaterThan(0)
    })

    screen.debug()

    const items = within(container).getAllByRole('img')
    items.forEach(item => {
      expect(item.hasAttribute('alt'))
      expect(item.getAttribute('alt')?.includes(searchTerm.toLowerCase()))
    })

  })
})