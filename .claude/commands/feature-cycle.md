Kör hela feature-cykeln för LISP-tolken med gates. Argument (valfritt): "$ARGUMENTS"
= det steg som ska byggas. Om inget argument ges, börja på steg 1.

Pausa vid varje **[GATE]** och vänta på mitt uttryckliga godkännande innan du går vidare.

1. **Idé** (hoppa över om ett steg redan angetts som argument)
   Delegera till `feature-scout`. Presentera de 5 förslagen.
   **[GATE]** Jag väljer ett steg.

2. **RED**
   - Skapa branch `feature/<slug>`.
   - Skriv minst ett unit-test (happy path) + minst ett felfall.
   - Presentera testerna och kör `dotnet test` för att visa att de är röda.
   **[GATE]** Jag godkänner testerna.

3. **GREEN**
   Implementera tills `dotnet build` och `dotnet test` är gröna. Följ skillen
   `add-primitive-procedure` om det gäller en primitiv.

4. **Granskning**
   Delegera till `code-reviewer`. Åtgärda blockerande punkter, kör om tills grönt.
   Presentera slutrapporten.
   **[GATE]** Jag godkänner att gå vidare.

5. **Dokumentation**
   Delegera till `doc-writer` för README-uppdatering.

6. **PR**
   Rapportera resultatet och skapa en PR mot `main` med självgranskning.
   **[GATE]** Jag godkänner innan PR skapas.
