Givet valt steg "$ARGUMENTS" (ett issue-nummer som `#7` / `7`, eller en fri beskrivning):

1. **Om det är ett issue-nummer:** hämta det med `gh issue view <nr>` och använd
   titel + acceptanskriterier som spec. Notera numret för senare (`Closes #<nr>` i PR:n).
   Annars: behandla texten som spec.
2. Skapa en feature-branch: `feature/<slug>` (slug från issue-titeln eller beskrivningen).
3. Skriv minst ett unit-test för funktionaliteten – happy path **och minst ett felfall**
   (RED-fasen). End-to-end-test i `SICP_Tests/EndToEndTests/` enligt `EndToEndTestBase`.
4. Kör testerna och visa att de är röda.
5. Presentera testet och vänta på mitt godkännande innan du fortsätter.

Nästa steg efter godkännande är `/implement`, som stänger issuet via PR:ns `Closes #<nr>`.
