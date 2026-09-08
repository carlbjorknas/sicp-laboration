---
name: Ny special-form
about: Lägg till stöd för en Scheme-special-form (t.ex. cond, begin, let)
title: "Stöd för <special-form>"
labels: special-form
---

## Special-form

`<namn>` – t.ex. `cond`, `begin`, `let`, `set!`

## Syntax

```scheme
(<namn> ...)
```

## Semantik (R5RS / SICP)

- Vad ska den göra?
- SICP-avsnitt: _t.ex. 1.1.6_
- Är det en primitiv special-form eller en derived expression (kan uttryckas via
  befintliga former)?
- Kantfall: tom form, kortslutning, retur­värde när inget matchar?

## Acceptanskriterier

- [ ] `Recognises` + `Evaluate` i `SICP/SpecialForms/`
- [ ] Inkopplad i `SICP/Evaluator.cs`
- [ ] End-to-end-test i `SICP_Tests/EndToEndTests/` – happy path **och minst ett felfall**
- [ ] `dotnet build` och `dotnet test` gröna
- [ ] README uppdaterad (tabellen "Specialformer" + ev. begränsningar)

## Kör

`/new-feature <slug>` för RED-fasen.
