# LISP Interpreter – Claude context

## Byggsystem
- .NET 10, `dotnet build`, `dotnet test`

## Teststil
- SICP_Tests är testprojektet och MSTest används
- Tester ska täcka happy path + minst ett felfall

## Branch-konvention
- feature/<kort-beskrivning>

## Backlog
- Öppna arbetsuppgifter ligger i GitHub Issues (`gh issue list`). `notes.txt` är bara en pekare.
- Designfrågor/spikes ligger i GitHub Discussions tills riktningen är vald.

## Agentupplägg
Fyra roller, se `.claude/agents/` och `.claude/commands/`:
- **feature-scout** (subagent) – går igenom öppna issues + koden och föreslår nästa steg. Trigga via `/suggest-steps`.
- **code-reviewer** (subagent, ändrar aldrig kod) – granskar PR:en mot kvalitetskrav
  + Scheme-semantik och postar fynden som PR-kommentarer.
- **doc-writer** (subagent, får bara röra README.md) – håller README i synk med koden.
  Ersätter den gamla Cursor-regeln `.cursor/rules/doc-agent.mdc`.
- Kod-rollen = kommandona `/new-feature #<nr>` (RED, tar issue-nr eller fritext) →
  `/implement` (GREEN → draft-PR med `Closes #<nr>` → granskning på PR:en → doc →
  "ready for review"). Granskningen sker alltid på en PR, inte på en lokal diff.
- `/feature-cycle` kör hela kedjan med godkännande-gates.
- Skill `add-primitive-procedure` = repo-mönstret för nya inbyggda procedurer.