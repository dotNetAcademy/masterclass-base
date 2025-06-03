
# dotNET lab masterclass

## fork de masterclass base
1. Fork deze repository 
2. download en installeer eerst https://cli.github.com/
3. Voer dit Script uit:

´´´
$sourcerepo = "dotNetAcademy/masterclass-base"
$targetrepo = "<<TODO>> JE EIGEN FORK"

# Check if the user is authenticated with GitHub CLI
$authStatus = gh auth status 2>$null

if ($LASTEXITCODE -ne 0) {
    Write-Host "You are not logged into GitHub CLI. Logging in now..."
    gh auth login

    # Re-check authentication status
    $authStatus = gh auth status 2>$null
    if ($LASTEXITCODE -ne 0) {
        Write-Error "Login failed. Please try again manually with 'gh auth login'."
        exit 1
    }
}

# Download the issues to a JSON file
gh issue list -R $sourcerepo --limit 100 --json title,body > issues.json

# Load the JSON file
$issues = Get-Content "issues.json" | ConvertFrom-Json

# Loop through each issue and create it in your new repo
foreach ($issue in $issues) {
    $title = $issue.title
    $body = $issue.body
    gh issue create -R $targetrepo --title "$title" --body "$body"
}
 

´´´

Om ervoor te zorgen dat code die je schrijft gelinkt kan worden aan een specifieke taak is het belangrijk dat je hier een bepaalde workflow bij volgt namelijk:
- Vanaf je een nieuwe taak opneemt moet je in je repository een nieuwe branch aanmaken (**aftakken van de main branch**), de naam van deze branch moet hetzelfde zijn als de issue key (bijvoorbeeld `MASTER-10`)
- Het is de bedoeling dat je alle relevante code voor de taak in gaat committen en pushen
- Vanaf dat je klaar bent met deze taak moet je hiervoor een pull request aanmaken (pull request van jouw branch naar main)
- Deze pull request mag je zelf goedkeuren (probeer wel al eens eerst te kijken of alle file changes die eraan staan voldoen aan jouw verwachtigen. Hiermee spot je vaak al voor de hand liggende fouten)
- Nu kan je een nieuwe taak opnemen in JIRA en terug vanaf de eerste stap beginnen

