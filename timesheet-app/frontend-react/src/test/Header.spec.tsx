import { describe, expect } from 'vitest';
import {render, screen} from "./test-utils.tsx";
import Header from "../components/Header.tsx";

describe('Header component', () => {
  it('should render component', async () => {
    render(<Header title="Timesheets" />);

    expect(await screen.findByTestId('header-title')).toHaveTextContent('Timesheets');
    expect(await screen.findByRole('button')).toHaveTextContent('New registration');
  });
});