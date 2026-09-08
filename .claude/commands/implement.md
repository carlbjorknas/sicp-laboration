Testet är godkänt (RED-fasen är klar). Gör nu följande i ordning:

1. **Implementera** funktionaliteten så att:
   - `dotnet build` går igenom utan fel
   - `dotnet test` går igenom
   Om ändringen lägger till en primitiv procedur: följ skillen `add-primitive-procedure`.
   Committa (GREEN).

2. **Skapa en draft-PR** mot `main`. Pusha branchen, `gh pr create --draft`.
   Beskrivningen ska ha en självgranskning, och `Closes #<nr>` om steget kom från
   ett issue. Vänta tills CI hunnit starta.

3. **Kodgranskning** – delegera till subagenten `code-reviewer` via Task-verktyget
   och ge den PR-numret. Den granskar PR:en och postar fynden som PR-kommentarer.
   Åtgärda alla blockerande punkter, committa och pusha, och kör om granskningen
   tills den ger grönt ljus.

4. **Dokumentation** – delegera till subagenten `doc-writer` via Task-verktyget så
   att README speglar den nya funktionaliteten. Committa och pusha.

5. **Rapportera** resultatet (build, test, CI, granskning, doc-ändringar).

6. **Markera PR:en "ready for review"** (`gh pr ready <nr>`). Merga inte själv –
   det gör jag.
