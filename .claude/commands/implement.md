Testet är godkänt (RED-fasen är klar, draft-PR finns redan från `/new-feature`).
Gör nu följande i ordning:

1. **Implementera** funktionaliteten så att:
   - `dotnet build` går igenom utan fel
   - `dotnet test` går igenom
   Om ändringen lägger till en primitiv procedur: följ skillen `add-primitive-procedure`.
   Committa (GREEN) och pusha till PR:en. Vänta tills CI hunnit starta.

2. **Kodgranskning** – delegera till subagenten `code-reviewer` via Task-verktyget
   och ge den PR-numret. Den granskar PR:en och postar fynden som PR-kommentarer.
   Åtgärda alla blockerande punkter, committa och pusha, och kör om granskningen
   tills den ger grönt ljus.

3. **Dokumentation** – delegera till subagenten `doc-writer` via Task-verktyget så
   att README speglar den nya funktionaliteten. Committa och pusha.

4. **Rapportera** resultatet (build, test, CI, granskning, doc-ändringar).

5. **Markera PR:en "ready for review"** (`gh pr ready <nr>`). Merga inte själv –
   det gör jag.

Om ingen draft-PR finns (steget kördes inte via `/new-feature`): skapa den efter GREEN,
före granskningen.
