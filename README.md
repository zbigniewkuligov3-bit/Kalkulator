# Kalkulator .NET 8

Prosty kalkulator konsolowy. Obsługuje dodawanie (`+`), odejmowanie (`-`),
mnożenie (`*`) i dzielenie (`/`). Wpisanie `0` w menu kończy aplikację.

> **Wersja warsztatowa:** kod zawiera pięć celowo wprowadzonych błędów.
> Opis poniżej określa wymagane zachowanie, nie gwarantuje obecnej poprawności.
> Kompilacja powinna przechodzić, a część testów powinna być czerwona.
> Rozpocznij od [planu ćwiczeń](workshop/CWICZENIA.md).
> [Klucz dla prowadzącego](workshop/PROWADZACY.md) zawiera rozwiązania —
> nie dołączaj go do kontekstu agenta podczas samodzielnej diagnozy.

## Uruchomienie

Wymagania: SDK .NET 8 lub nowsze oraz środowisko uruchomieniowe .NET 8.

```powershell
dotnet run --project .\src\Calculator.Console\Calculator.Console.csproj
```

Wybierz symbol operacji i podaj kolejno dwie liczby. Możesz używać kropki
lub przecinka jako separatora dziesiętnego; separatory tysięcy nie są obsługiwane.
Wyniki są wyświetlane z kropką. Odejmowanie i dzielenie zachowują kolejność
wprowadzonych liczb.

Przykład: `+`, `1,5`, `2.25` daje `Wynik: 3.75`.

Aplikacja używa typu `decimal`. Błędne dane, dzielenie przez zero oraz
przekroczenie zakresu liczbowego powodują czytelny komunikat, a nie zakończenie
programu. Brak dalszych danych wejściowych kończy program.
Dzielenie podlega precyzji typu `decimal` — rozwinięcia nieskończone są zaokrąglane.

## Struktura

- [Calculator.Domain](src/Calculator.Domain): domena niezależna od konsoli.
  Wspólny kontrakt `ICalculationOperation` i osobne klasy `AdditionOperation`,
  `SubtractionOperation`, `MultiplicationOperation`, `DivisionOperation`.
  Fabryka udostępnia operacje przez interfejs; implementacje są wewnętrzne.
- [Calculator.Console](src/Calculator.Console): menu, odczyt liczb, prezentacja
  wyników i komunikatów błędów. Operacje oraz wejście/wyjście są wstrzykiwane
  przez konstruktor.
- [Calculator.Tests](tests/Calculator.Tests): testy xUnit operacji domenowych
  i scenariuszy konsolowych.

## Budowanie i testy

```powershell
dotnet build .\Calculator.sln
dotnet test .\Calculator.sln
```

Pierwsze uruchomienie przywraca publiczne pakiety testowe z nuget.org.
Lokalna konfiguracja NuGet nie wymaga firmowych feedów ani poświadczeń.
