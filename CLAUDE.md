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
- **code-reviewer** (subagent, read-only) – granskar diff mot kvalitetskrav + Scheme-semantik.
- **doc-writer** (subagent, får bara röra README.md) – håller README i synk med koden.
  Ersätter den gamla Cursor-regeln `.cursor/rules/doc-agent.mdc`.
- Kod-rollen = kommandona `/new-feature #<nr>` (RED, tar issue-nr eller fritext) →
  `/implement` (GREEN + granskning + doc + PR med `Closes #<nr>`).
- `/feature-cycle` kör hela kedjan med godkännande-gates.
- Skill `add-primitive-procedure` = repo-mönstret för nya inbyggda procedurer.