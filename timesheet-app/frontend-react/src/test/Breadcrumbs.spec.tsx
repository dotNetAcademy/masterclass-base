import { describe, expect } from 'vitest';
import {render, screen} from "./test-utils.tsx";
import Breadcrumbs from "../components/Breadcrumbs.tsx";

describe('Breadcrumbs component', () => {
  it('should render component', async () => {
    render(<Breadcrumbs />);

    const list = await screen.findByRole('list');
    expect(list.children).length(2);

    const items = await screen.findAllByRole('listitem');
    expect(items[0]).toHaveTextContent('Home');
    expect(items[1]).toHaveTextContent('Timesheets');
  });
});