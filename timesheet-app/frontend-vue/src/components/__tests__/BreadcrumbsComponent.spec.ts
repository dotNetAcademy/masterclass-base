import { render, screen } from '@testing-library/vue'
import BreadcrumbsComponent from '../BreadcrumbsComponent.vue'

describe('Breadcrumbs component', () => {
  it('should render component', async () => {
    render(BreadcrumbsComponent)

    const list = await screen.findByRole('list')
    expect(list.children).length(2)

    const items = await screen.findAllByRole('listitem')
    expect(items[0]).toHaveTextContent('Home')
    expect(items[1]).toHaveTextContent('Timesheets')
  })
})
