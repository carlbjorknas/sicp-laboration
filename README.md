# SICP – LISP-tolk i C#

En interaktiv LISP-tolk (REPL) skriven i C#, inspirerad av *Structure and Interpretation of Computer Programs* (SICP). Tolkaren läser uttryck rad för rad, utvärderar dem och skriver ut resultatet.

## Köra tolkaren

```bash
dotnet run --project SICP
```

Avsluta med `(quit)`.

Resultat skrivs ut med prefixet `-->`.

## Datatyper

| Typ | Exempel | Beskrivning |
|-----|---------|-------------|
| Heltal | `42`, `-7` | 32-bitars `int` |
| Booleska värden | `true`, `false` | Självutvärderande |
| Strängar | `"hello"`, `"c:\\temp"` | Dubbelcitattecken; `\"` och `\\` kan escapas |
| Listor | `(1 2 3)`, `()` | Byggda med par (`cons`) eller `list` |
| Prickade par | `(1 . 2)` | CDR behöver inte vara en lista |
| Symboler | `x`, `factorial` | Variabelreferenser (utvärderas via miljö) |
| Procedurer | `(lambda (x) (* x x))` | Sammansatta procedurer; primitiva procedurer skrivs ut som `PrimitiveProcedure` |

## Specialformer

| Form | Syntax | Beskrivning |
|------|--------|-------------|
| `define` | `(define x 10)` | Binder ett namn till ett utvärderat värde. Returnerar `ok`. |
| `define` (procedur) | `(define (square x) (* x x))` | Syntactic sugar för `(define square (lambda (x) (* x x)))`. |
| `lambda` | `(lambda (x y) (+ x y))` | Skapar en sammansatt procedur med lexikal stängsel. |
| `if` | `(if pred konsekvens alternativ)` | Om predikatet är booleskt `false` utvärderas alternativet; annars konsekvensen. Saknas alternativ returneras `false`. Icke-booleska predikat räknas som sanna. |
| `quote` | `(quote (a b))` | Returnerar uttrycket utan utvärdering. |
| `'…` | `'abc`, `'(1 2)` | Förkortning för `(quote …)`. |
| `and` | `(and e1 e2 …)` | Utvärderar uttryck i ordning. Returnerar `false` vid första falska värdet, annars sista uttrycket. `(and)` ger `true`. |
| `or` | `(or e1 e2 …)` | Returnerar första icke-falska värdet, annars `false`. `(or)` ger `false`. |

`and` och `or` utvärderar inte uttryck efter att resultatet är bestämt (kortslutning).

## Inbyggda procedurer

### Aritmetik

| Procedur | Beskrivning |
|----------|-------------|
| `(+ n1 n2 …)` | Summerar heltal. `(+)` → `0`. |
| `(- n)` | Negation. `(-)` → `0`. |
| `(- n1 n2 …)` | Subtraherar resterande tal från första. |
| `(* n1 n2 …)` | Multiplicerar. `(*)` → `1`. |

### Jämförelser

Alla tar exakt två heltalsargument och returnerar `true` eller `false`.

| `<` | `<=` | `=` | `>=` | `>` |

### Logik

| Procedur | Beskrivning |
|----------|-------------|
| `(not x)` | Returnerar `true` endast om `x` är booleskt `false`; alla andra värden ger `false`. |

### Listor

| Procedur | Beskrivning |
|----------|-------------|
| `(cons a d)` | Skapar ett par `(a . d)` eller `(a)` om `d` är `()`. |
| `(car list)` | Första elementet. |
| `(cdr list)` | Resten av listan. |
| `(list x …)` | Skapar en proper lista. `(list)` → `()`. |
| `(append list … last)` | Slår ihop listor. Sista argumentet kan vara valfritt värde (ger prickat par). `(append)` → `()`. |

### Strängar

| Procedur | Beskrivning |
|----------|-------------|
| `(string? x)` | `true` om `x` är en sträng. |
| `(string-length s)` | Antal tecken i strängen `s`. |

### Högre ordningens funktioner

| Procedur | Beskrivning |
|----------|-------------|
| `(map proc list1 list2 …)` | Applicerar `proc` på motsvarande element i en eller flera listor. Stoppar vid kortaste listans längd. |

### Meta / REPL

| Procedur | Beskrivning |
|----------|-------------|
| `(eval expr)` | Utvärderar `expr` i en tom miljö (användbart tillsammans med `quote`). |
| `(quit)` | Avslutar REPL:en. |

## REPL-beteende

- Ett uttryck kan sträcka sig över flera rader; utvärdering sker först när uttrycket är syntaktiskt komplett.
- Tomma rader och whitespace ignoreras vid tokenisering.
- Fel (t.ex. obunden variabel) skrivs ut som felmeddelande; REPL:en fortsätter köra.
- Variabelnamn får innehålla bokstäver, siffror, bindestreck och `!` (t.ex. `xyz123-!`).

## Begränsningar

Följande saknas eller är ofullständigt jämfört med Scheme/R5RS:

- **Ingen `set!`** – variabler kan inte omdefinieras efter bindning; `(define x 1)` följt av `(define x 2)` kastar undantag.
- **Ingen `begin`** – lambda-kroppen kan bara vara ett enda uttryck.
- **Ingen `cond`** – endast `if` för villkor.
- **Inga makron.**
- **Ingen tail recursion-optimering** – djup rekursion kan ge stack overflow.
- **Endast heltal** – inga decimaltal eller godtyckligt stora heltal.
- **`eval` utan miljö** – `(eval expr)` utvärderar alltid i en ny, tom miljö; definierade variabler nås inte.
- **Begränsad lexikal analys** – variabelnamn får inte börja med `+` eller `-` (men `-` och `+` fungerar som procedurnamn). Tokenisering av identifierare är förenklad.
- **Förenklad sanning** – endast booleskt `false` är falskt i `if`, `and` och `or`. `0`, tom lista m.m. räknas som sanna.
- **Aritmetik utan typkontroll** – `+` och `-` kastar undantag vid icke-numeriska argument; `*` kräver heltal.
- **`append` gör grundkopiering** – listor kopieras ytligt; beteende vid nästlade/immutabla strukturer är begränsat.
- **Primitiva procedurer** – skrivs ut som strängen `PrimitiveProcedure`, inte som läsbar procedurtext.
- **Ingen division eller modulo.**
- **Begränsade strängoperationer** – endast `string?` och `string-length`; ingen sammanslagning, substring m.m.
