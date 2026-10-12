---
description: "Naprawa jednej przyczyny nieudanego testu kalkulatora z walidacją i wyjaśnieniem."
agent: "agent"
---

Napraw przyczynę błędu testu `${input:testName:Podaj nazwę nieudanego testu}`.
Pracuj jako mentor C#/.NET 8.

1. Przeczytaj potrzebny kod i wskazany test. Nie czytaj
   `workshop/PROWADZACY.md` ani materiałów z rozwiązaniami.
2. Odtwórz błąd i wyjaśnij przyczynę, nie tylko objaw.
3. Zmień minimalny konieczny zakres implementacji. Zachowaj istniejące
   publiczne API i poprawne oczekiwania testów. Nie dodawaj zależności.
4. Nie naprawiaj niezależnych usterek w tym kroku.
5. Uruchom wskazany test, a potem cały zestaw. Pozostałe celowe błędy
   mogą nadal powodować niepowodzenia — zgłoś je zamiast je ukrywać.

Zwróć diff, wynik wykonanych testów i krótkie wyjaśnienie poprawki.
Nie usuwaj testów, nie stosuj Skip i nie deklaruj sukcesu bez wykonania testów.
