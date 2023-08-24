import { render, screen } from '@testing-library/vue'
import PageHeader from '../PageHeader.vue'

describe('PageHeader component', () => {
  it('should render component', async () => {
    render(PageHeader, {
      slots: {
        default: '<h2 data-testid="header-title">Title</h2>',
        actions: '<button>Click me</button>'
      }
    })

    expect(await screen.findByTestId('header-title')).toHaveTextContent('Title')
    expect(screen.queryByTestId('actions')).toBeInTheDocument()
    expect(await screen.findByRole('button')).toHaveTextContent('Click me')
  })

  it('should render component without actions section when slot is empty', async () => {
    render(PageHeader, {
      slots: {
        default: '<h2 data-testid="header-title">Title</h2>'
      }
    })

    expect(await screen.findByTestId('header-title')).toHaveTextContent('Title')
    expect(screen.queryByTestId('actions')).not.toBeInTheDocument()
  })
})
