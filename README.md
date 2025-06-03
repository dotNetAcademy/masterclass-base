# dotNET lab masterclass

## Linken met JIRA
Om ervoor te zorgen dat code die je schrijft gelinkt kan worden aan een specifieke taak is het belangrijk dat je hier een bepaalde workflow bij volgt namelijk:
- Vanaf je een nieuwe taak opneemt moet je in je repository een nieuwe branch aanmaken (**aftakken van de main branch**), de naam van deze branch moet hetzelfde zijn als de issue key (bijvoorbeeld `MASTER-10`)
- Het is de bedoeling dat je alle relevante code voor de taak in gaat committen en pushen
- Vanaf dat je klaar bent met deze taak moet je hiervoor een pull request aanmaken (pull request van jouw branch naar main)
- Deze pull request mag je zelf goedkeuren (probeer wel al eens eerst te kijken of alle file changes die eraan staan voldoen aan jouw verwachtigen. Hiermee spot je vaak al voor de hand liggende fouten)
- In Project kan je nu deze taak verslepen naar de done kolom
- Nu kan je een nieuwe taak opnemen in Project en terug vanaf de eerste stap beginnen
