Testet är godkänt (RED-fasen är klar). Gör nu följande i ordning:

1. **Implementera** funktionaliteten så att:
   - `dotnet build` går igenom utan fel
   - `dotnet test` går igenom
   Om ändringen lägger till en primitiv procedur: följ skillen `add-primitive-procedure`.

2. **Kodgranskning** – delegera till subagenten `code-reviewer` via Task-verktyget.
   Åtgärda alla blockerande punkter och kör om granskningen tills den ger grönt ljus.

3. **Dokumentation** – delegera till subagenten `doc-writer` via Task-verktyget så att
   README speglar den nya funktionaliteten.

4. **Rapportera** resultatet (build, test, granskning, doc-ändringar).

5. **Skapa en PR** mot `main` med en självgranskning i beskrivningen. Följ
   branch-konventionen `feature/<kort-beskrivning>`. Om steget kom från ett issue:
   skriv `Closes #<nr>` i PR-beskrivningen så att issuet stängs vid merge.
