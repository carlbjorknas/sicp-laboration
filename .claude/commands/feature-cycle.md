Kör hela feature-cykeln för LISP-tolken med gates. Argument (valfritt): "$ARGUMENTS"
= det steg som ska byggas. Om inget argument ges, börja på steg 1.

Pausa vid varje **[GATE]** och vänta på mitt uttryckliga godkännande innan du går vidare.

1. **Idé** (hoppa över om ett issue-nummer/steg redan angetts som argument)
   Delegera till `feature-scout`. Presentera de 5 förslagen.
   **[GATE]** Jag väljer ett steg. Är det ett nytt förslag: skapa issuet först.

2. **RED + draft-PR**
   - Om steget är ett issue: hämta det med `gh issue view <nr>` och använd
     acceptanskriterierna som spec. Notera numret för `Closes #<nr>` i PR:n.
   - Skapa branch `feature/<slug>`.
   - Skriv minst ett unit-test (happy path) + minst ett felfall. Committa (RED).
   - Kör `dotnet test` för att visa att de är röda.
   - Pusha och skapa en draft-PR mot `main` (`Closes #<nr>`, notis om att bara
     RED-testerna finns än).
   - Presentera testerna + PR-länken.
   **[GATE]** Jag godkänner testerna.

3. **GREEN**
   Implementera tills `dotnet build` och `dotnet test` är gröna. Följ skillen
   `add-primitive-procedure` om det gäller en primitiv. Committa och pusha till PR:en.

4. **Granskning**
   Delegera till `code-reviewer` med PR-numret. Den postar fynden som
   PR-kommentarer. Åtgärda blockerande punkter, pusha, kör om tills grönt.
   Presentera slutrapporten.
   **[GATE]** Jag godkänner att gå vidare.

5. **Dokumentation**
   Delegera till `doc-writer` för README-uppdatering. Committa och pusha.

6. **Klar**
   Rapportera resultatet och markera PR:en "ready for review" (`gh pr ready <nr>`).
   Merga inte själv.
   **[GATE]** Jag godkänner innan PR:en markeras redo.
