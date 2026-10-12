---
description: "Utworzenie edytowalnego diagramu draw.io kalkulatora na podstawie kodu przy użyciu skilla."
agent: "agent"
---

Użyj skilla `draw-io-diagram-generator`.
Jeśli skill jest niedostępny, zgłoś to i poproś o jego udostępnienie;
nie twierdź, że go użyto.

Na podstawie aktualnego `src` utwórz `docs/calculator-architecture.drawio`.
Nie czytaj klucza prowadzącego i nie zmieniaj kodu C#.

Pokaż Program, CalculatorApplication, CalculationOperationFactory,
ICalculationOperation i wszystkie istniejące klasy operacji.
Rozróżnij tworzenie obiektów, zależności i implementację interfejsu.
Dodaj ścieżkę wyboru operacji, odczytu danych, obliczenia,
wyniku/błędu oraz powrotu do menu i zakończenia.
Nie wymyślaj bazy danych, HTTP ani kontenera DI.

Sprawdź poprawność XML i spróbuj otworzyć diagram w draw.io.
Jeśli edytor jest niedostępny, jawnie zgłoś brak weryfikacji wizualnej.
Wskaż plik i podsumuj relacje potwierdzone w kodzie.
