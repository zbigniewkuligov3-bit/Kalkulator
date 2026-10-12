# Klucz prowadzącego — nie udostępniać agentowi podczas diagnozy

## Stan materiałów i organizacja

Pięć błędów jest wprowadzonych świadomie. Nie są podatnościami security.
Nie ma celowych sekretów, zdalnego wykonywania kodu, zawieszenia ani
uszkadzania danych. Aplikacja kompiluje się i obsługuje koniec wejścia.
Nie oznaczamy wadliwych linii komentarzem TODO, aby diagnoza nie była pozorna.

Przed modyfikacjami 23/23 testy przechodziły, w tym dwa dodatkowe testy
konsoli izolujące mnożenie i wynik ułamkowy dzielenia.
Oczekiwania wszystkich wcześniejszych testów pozostawiono bez zmian.
Stan początkowy warsztatu: **23 testy, 9 nieudanych, 14 poprawnych**.
Po naprawach: **23/23**. Po dalszych ćwiczeniach liczba testów wzrośnie.

Plan studentów: [CWICZENIA.md](CWICZENIA.md).
Najlepiej udostępnić klucz wyłącznie prowadzącemu poza workspace studentów.
Zakaz czytania w prompcie nie jest techniczną izolacją danych.

## Klucz pięciu usterek

| # | Lokalizacja | Usterka | Reprodukcja | Poprawka |
|---|---|---|---|---|
| 1 | [SubtractionOperation](../src/Calculator.Domain/Operations/SubtractionOperation.cs) | Odwrócona kolejność argumentów | `2 - 7` zwraca `5` zamiast `-5` | `return firstNumber - secondNumber;` |
| 2 | [DivisionOperation](../src/Calculator.Domain/Operations/DivisionOperation.cs) | Obcinanie części ułamkowej | `5 / 2` zwraca `2` zamiast `2.5` | `return firstNumber / secondNumber;` bez `Math.Truncate` |
| 3 | [DivisionOperation](../src/Calculator.Domain/Operations/DivisionOperation.cs) | Zero udaje poprawny wynik dzielenia przez zero | `1 / 0` daje `Wynik: 0` | Przy `secondNumber == 0` przywrócić `throw new DivideByZeroException(message: "Nie można dzielić przez zero.");` |
| 4 | [CalculationOperationFactory](../src/Calculator.Domain/Operations/CalculationOperationFactory.cs) | `*` tworzy dodawanie | `3 * 4` daje `7`; test samej klasy mnożenia przechodzi | `"*" => new MultiplicationOperation(),` |
| 5 | [CalculatorApplication](../src/Calculator.Console/CalculatorApplication.cs) | Parser nie normalizuje przecinka | `1,5` jest odrzucane, `1.5` działa | `s: text.Replace(oldChar: ',', newChar: '.')` przy `InvariantCulture` |

## Czerwone testy — punkt odniesienia

Z [testów domeny](../tests/Calculator.Tests/Operations/CalculationOperationTests.cs):
- `should_subtract_in_operand_order`
- `should_divide_in_operand_order`
- `should_reject_division_by_zero`
- `should_create_each_supported_operation`

Z [testów konsoli](../tests/Calculator.Tests/ConsoleApp/CalculatorApplicationTests.cs):
- `should_accept_both_decimal_separators`
- `should_perform_all_operations_in_one_session`
- `should_multiply_using_selected_operation`
- `should_preserve_fractional_division_result`
- `should_continue_after_division_by_zero`

Pierwszy czerwony assert może zasłaniać dalsze usterki. Dlatego test
wielu operacji naraz nie zastępuje testów izolowanych.
Poprawny test klasy mnożenia i niepoprawny test wyboru mnożenia to celowy
przykład problemu w integracji/wiringu, a nie w algorytmie.
Usunięcie `Math.Truncate` nie naprawia ukrytego dzielenia przez zero.

## Weryfikacja i odtworzenie stanu

```powershell
dotnet build .\Calculator.sln --configuration Release
dotnet test .\tests\Calculator.Tests\Calculator.Tests.csproj --configuration Release
```

Pierwsze polecenie ma zakończyć się kodem 0.
Drugie na starcie ma zakończyć się kodem 1 z dokładnie wymienionymi
dziewięcioma niepowodzeniami. Brak restore, brak runtime czy błąd kompilacji
nie są zamierzonymi błędami warsztatowymi.

Do pojedynczego scenariusza użyj filtra VSTest/xUnit 2:
```powershell
dotnet test .\tests\Calculator.Tests\Calculator.Tests.csproj --filter "FullyQualifiedName~should_subtract_in_operand_order"
```

Nie obiecuj zielonego CI dla wersji z celowymi usterkami. Nie wyłączaj
testów, nie stosuj `[Fact(Skip = ...)]` i nie obniżaj progów jakości.
Nie zlecaj „napraw wszystkiego”, gdy studenci mają obserwować proces.

Workspace nie był repozytorium Git podczas przygotowywania materiałów.
Przed warsztatem zachowaj osobną kopię tego stanu albo zainicjuj repozytorium
i zapisz punkt startowy. Nie ma automatycznego resetu ani gotowej gałęzi.
Do kolejnej grupy wracaj do własnego punktu startowego bez nadpisywania
niezapisanej pracy studentów.

## Wskazówki do czterech bloków

1. **Awesome Copilot:** wcześniej wybierz przejrzany zasób do C#/testów.
   Nie polegaj na identycznym UI marketplace u wszystkich uczestników.
   Katalog/plugin i rozszerzenie VS Code to różne mechanizmy.
2. **Skills:** udostępnij sprawdzony skill draw.io i wymagane assets.
   W tym pakiecie nie wygenerowano diagramu — to wynik ćwiczenia.
   Sprawdź w logu narzędzi rzeczywiste załadowanie skilla.
3. **Instructions:** szablon nie jest aktywny na starcie.
   Skopiowanie go do `.github/instructions` jest częścią ćwiczenia.
   Nie są egzekwowalną polityką bezpieczeństwa; pokaż również approval
   narzędzi i ograniczenie praw dostępu.
4. **MCP:** przed zajęciami sprawdź logowanie i odczyt Boards.
   Stwórz jeden work item na grupę na podstawie specyfikacji studentów.
   Nie wpisuj wymyślonych ID. Jeśli wymagany jest custom Azure DevOps Agent,
   upewnij się, że jest udostępniony w ich środowisku.
   Odczyt i implementacja nie wymagają publikacji komentarza ani zmiany stanu.

Materiały nie konfigurują aktywnego MCP, nie logują użytkownika,
nie tworzą work itemów i nie instalują pluginów.
Te zależności należy sprawdzić w docelowym środowisku przed zajęciami.

## Ocena końcowej funkcji procentu

Wymagane są osobno testy: domeny, fabryki, menu/rejestracji, liczb
z przecinkiem, wartości ujemnych, zera i przepełnienia.
Nie akceptuj implementacji modulo ani konwersji do `double`.
Nie wystarczy dodać klasy bez podpięcia jej do konsoli.
Specyfikacja dopuszcza dotychczasowy komunikat przy przepełnieniu
działań pośrednich na `decimal`; nie wymaga obsługi dowolnej precyzji.

Zmiana `%` na `?` w dwóch testach nieobsługiwanej operacji jest konieczna,
gdy `%` staje się poprawne. To jedyna zaplanowana zmiana danych
dotychczasowych testów wynikająca z nowych wymagań — ich asercje pozostają.
Weryfikuj dowody wyników, nie tylko opis agenta.
