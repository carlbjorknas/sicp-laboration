# LISP Interpreter – Claude context

## Byggsystem
- .NET 10, `dotnet build`, `dotnet test`

## Teststil
- SICP_Tests är testprojektet och MSTest används
- Tester ska täcka happy path + minst ett felfall

## Branch-konvention
- feature/<kort-beskrivning>

## Agentupplägg
Fyra roller, se `.claude/agents/` och `.claude/commands/`:
- **feature-scout** (subagent) – föreslår nästa steg kopplade till SICP. Trigga via `/suggest-steps`.
- **code-reviewer** (subagent, read-only) – granskar diff mot kvalitetskrav + Scheme-semantik.
- **doc-writer** (subagent, får bara röra README.md) – håller README i synk med koden.
  Ersätter den gamla Cursor-regeln `.cursor/rules/doc-agent.mdc`.
- Kod-rollen = kommandona `/new-feature` (RED) → `/implement` (GREEN + granskning + doc + PR).
- `/feature-cycle` kör hela kedjan med godkännande-gates.
- Skill `add-primitive-procedure` = repo-mönstret för nya inbyggda procedurer.