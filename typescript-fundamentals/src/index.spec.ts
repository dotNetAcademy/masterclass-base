
// Stap 1: maak een interface aan voor `Person` en `Pet`, en zorg ervoor dat de `persons` en `pets` arrays typed zijn

// Stap 2: implementeer de volgende testen door gerbuik te maken van JS Array methodes op een pure manier

it("controleer dat de naam van persoon met id 3 'Peeters' is ", () => {});

it("controleer dat er 2 mannelijke personen zijn", () => {});

it("maak een nieuwe lijst aan met alle persoon namen (voornaam + naam)", () => {
    /** 
    * het verwachte resultaat is: 'John Peeters', 'Alice Peeters', 'Bob Peeters'
    */
});

it("zoek de personen met een 'cat' als pet", () => {
  /**
   * de verwachte personen hebben de id 1 en 3"
   */
});

it("transformeer de personen array naar een nieuwe array met properties {id, fullName, age} en sorteer by leeftijd (van jong naar oud)", () => {
  /**  
  * het verwachte resultaat is:
  *   [{id: 3, fullName: 'Bob Peeters', age: ??}, {id: 2, fullName: 'Alice Peeters', age: ??}, {id: 1, fullName: 'John Peeters', age: ??}]
  */
});

it("maak een nieuwe array waarbij de persoon de details bevat van de pets", () => {
  /**  
  * bv de eerste persoon in de array moet er als volgt uitzien:
  *   {
  *     id: 1,
  *     firstName: "John",
  *     lastName: "Peeters",
  *     sex: "M",
  *     birthday: new Date(1991, 5, 27),
  *     pets: [  {id: 10, name: "Bobby", type: "dog"}, {id: 20, name: "Mimi", type: "cat" }],
  *   }
  */
});

// Stap 3: functions testen

it("implementeer updatePerson (in functions.ts) en wijzig een persoon op een immutable manier: update (en controleer) de voornaam van persoon met id 2 naar 'Alicia'", () => {});

it("implementeer updatePerson (in functions.ts) en wijzig een persoon op een immutable manier: pas het geslacht van de mannelijke personen aan naar 'F'", () => {});

// Stap 4: testen met gebruik te maken van Mocks

class DatetimeService {
  getCurrentTime(): number {
    return Date.now();
  }
}

it("zorg ervoor dat getCurrentTime in DatetimeService altijd dezelfde waarde (bv 1693474054994) returned (gebruik hiervoor jest.spyOn)", () => {
  const datetimeService = new DatetimeService();
  expect(datetimeService.getCurrentTime()).toBe(1693474054994);
});

class GitHubProfileService {
  getUser(username: string): Promise<{ username: string }> {
    throw new Error("Not implemented");
  }
}

it("mock (met jest) de getUser methode van GitHubProfileService zodat het een gemockte user returned", () => {});
