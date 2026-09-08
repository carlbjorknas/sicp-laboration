---
name: Ny primitiv procedur
about: Lägg till en inbyggd procedur (t.ex. modulo, abs, string-append)
title: "Primitiv: <namn>"
labels: primitive
---

## Procedur

`<namn>` – symbolen som anropas från Scheme-kod

## Signatur & semantik

- Argument: antal och typer
- Returvärde:
- SICP-avsnitt / R5RS-referens:
- Fel: vad händer vid fel antal eller fel typ?

## Acceptanskriterier

- [ ] Klass `SICP/Expressions/PrimitiveProcedure<Namn>.cs` som ärver `PrimitiveProcedure`
- [ ] Validering via hjälpmetoderna (`EnsureOperandsHaveExpectedCount` m.fl.)
- [ ] Registrerad i `SICP/Environment.cs`
- [ ] End-to-end-test – happy path **och minst ett felfall**
- [ ] `dotnet build` och `dotnet test` gröna
- [ ] README uppdaterad (primitiv-lista + ev. begränsningar)

Följ skillen `add-primitive-procedure`.

## Kör

`/new-feature <slug>` för RED-fasen.
