---
name: add-primitive-procedure
description: Repo-mönstret för att lägga till en ny primitiv procedur (t.ex. modulo, abs, string-append) i LISP-tolken. Använd när en uppgift innebär att en ny inbyggd procedur ska bli anropbar från Scheme-koden.
---

# Lägga till en primitiv procedur

Så här ser mönstret ut i det här projektet. Följ det för att matcha befintlig kod.

## 1. Skapa klassen

`SICP/Expressions/PrimitiveProcedure<Namn>.cs`:

```csharp
namespace SICP.Expressions;

public class PrimitiveProcedureModulo : PrimitiveProcedure
{
    public override Expression Apply(List<Expression> operands, Environment callerEnvironment)
    {
        EnsureOperandsHaveExpectedCount(operands, 2, "modulo");
        var numbers = EnsureOperandHaveExpectedType<NumberExpression>(operands);

        return new NumberExpression(numbers[0].Value % numbers[1].Value);
    }
}
```

- Ärv från `PrimitiveProcedure`.
- `Apply` tar emot anropsmiljön (`callerEnvironment`). De flesta primitiver behöver
  den inte – ta bara med parametern. `eval` och `help` använder den.
- Använd hjälpmetoderna för validering: `EnsureOperandsHaveExpectedCount`,
  `EnsureOperandsHaveMinimumCount`, `EnsureOperandHaveExpectedType<T>`.
- Vid fel argumentantal/typ: kasta – felmeddelandet skrivs ut av REPL:en.
- Variabelt antal operander: se `PrimitiveProcedurePlus` / `PrimitiveProcedureDivision`.

## 2. Registrera i miljön

I `SICP/Environment.cs`, konstruktorn utan parametrar:

```csharp
AddVariable("modulo", new PrimitiveProcedureModulo());
```

Namnet är exakt symbolen som Scheme-koden anropar (`modulo`, `string-append`, `abs`).

## 3. Skriv end-to-end-test

`SICP_Tests/EndToEndTests/<Namn>Tests.cs`, enligt mönstret i `EndToEndTestBase`:

```csharp
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace SICP_Tests.EndToEndTests;

[TestClass]
public class ModuloTests : EndToEndTestBase
{
    [TestMethod]
    public void Modulo_returns_remainder()
    {
        SetupInputSequence("(modulo 7 3)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("1"), Times.Once);
    }

    [TestMethod]
    public void Modulo_with_wrong_number_of_operands_throws()
    {
        SetupInputSequence("(modulo 7)");
        _sut!.Run();
        _printerMock!.Verify(x => x.Print("'modulo' expects 2 operand(s), got 1"), Times.Once);
    }
}
```

Krav (CLAUDE.md): **happy path + minst ett felfall**. Om beteendet medvetet avviker
från R5RS (som heltalsdivision gör), skriv ett test som dokumenterar det med en
kommentar `// Documents current behaviour: ...`.

## 4. Verifiera

```
dotnet build
dotnet test
```

## 5. Dokumentation

Låt `doc-writer`-agenten uppdatera README:s primitiv-lista och begränsningar.

## Checklista

- [ ] Klass i `SICP/Expressions/PrimitiveProcedure<Namn>.cs`, ärver `PrimitiveProcedure`
- [ ] Validering via hjälpmetoderna
- [ ] Registrerad i `SICP/Environment.cs`
- [ ] End-to-end-test med happy path + felfall
- [ ] `dotnet build` och `dotnet test` gröna
- [ ] README uppdaterad
