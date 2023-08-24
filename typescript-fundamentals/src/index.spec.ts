
// Stap 1: maak een interface aan voor `Person` en `Pet`, en zorg ervoor dat de `persons` en `pets` arrays typed zijn

// Stap 2: implementeer de volgende testen door gerbuik te maken van JS Array methodes op een pure manier

it("wat is de naam van persoon met id 3", () => {});

it("hoeveel mannelijke personen zijn er", () => {});

it("wat zijn de volledige namen van de personen", () => {});

it("zoek alle personen met een cat als pet", () => {});

it("transformeer de personen array naar een nieuwe array met properties {id, fullName, age} en sorteer by leeftijd", () => {});

it("maak een nieuwe array waarbij de persoon de details bevat van de pets", () => {});

// Stap 3: functions testen

it("implementeer updatePerson (in functions.ts) en wijzig een persoon op een immutable manier", () => {});

// Stap 4: testen met gebruik te maken van Mocks

class DatetimeService {
  getCurrentTime() {
    return Date.now();
  }
}

it("zorg ervoor dat getCurrentTime altijd dezelfde waarde returned (via een mock)", () => {});

class GitHubProfileService {
  getUser(username: string): Promise<{ username: string }> {
    throw new Error("Not implemented");
  }
}


it("mock de getUser methode van GitHubProfileService zodat het een gemockte user returned", () => {});
