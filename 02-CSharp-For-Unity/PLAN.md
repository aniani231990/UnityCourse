# C# for Unity: Junior → Senior Level Plan

**Duration:** 8 недель (60 часов total)
**Format:** AI-Assisted Learning (теория + практика + проверка)
**Goal:** Свободно читать и писать продвинутый C# код для Unity

---

## Структура обучения с AI

Каждый модуль следует циклу:

```
1. AI объясняет концепцию (с примерами для Unity)
   ↓
2. Студент решает задачки (AI проверяет решения)
   ↓
3. AI задает вопросы на понимание (Socratic method)
   ↓
4. Студент объясняет концепцию AI (Feynman technique)
   ↓
5. AI делает code review финального проекта модуля
```

---

## Module 01: Delegates, Events, Actions (Week 1)

**Why it matters:** Вся архитектура Unity (EventBus, callbacks, UI events) построена на delegates

### Теория (1 час)
**AI должен объяснить:**
- Что такое delegate (указатель на метод)
- Разница: delegate vs Action vs Func vs Event
- Когда использовать каждый
- Multicast delegates (+=, -=)
- Memory leaks с events

**Примеры для Unity:**
```csharp
// Delegate
public delegate void OnHealthChanged(int newHealth);
OnHealthChanged healthCallback;

// Action (готовый delegate для void)
public Action<int> OnScoreChanged;

// Func (готовый delegate с return)
public Func<int, bool> CanAffordItem;

// Event (защищенный delegate)
public event Action OnPlayerDied;
```

### Практика (5 часов)
**Задачка 1:** Simple EventBus
```csharp
// AI дает задачу:
// Создайте EventBus, который может публиковать/подписываться на события
// Требования:
// - Subscribe<T>(Action<T> handler)
// - Unsubscribe<T>(Action<T> handler)
// - Publish<T>(T eventData)

// Студент пишет решение
// AI проверяет код и задает вопросы:
// - "Почему вы использовали Dictionary?"
// - "Что случится, если Unsubscribe вызван, но подписки нет?"
// - "Как избежать memory leak?"
```

**Задачка 2:** Health System с events
```csharp
// AI дает задачу:
// Создайте Health систему с событиями:
// - OnDamaged (int damage)
// - OnHealed (int amount)
// - OnDied ()
// Используйте Events (не Action!)

// После решения AI спрашивает:
// - "Почему event, а не public Action?"
// - "Что если подписчик выбрасывает exception?"
```

**Задачка 3:** UI Button System
```csharp
// Создайте систему UI кнопок с разными типами callbacks:
// - OnClick (простой клик)
// - OnHold (удержание, передать duration)
// - OnDoubleClick (двойной клик)
// Используйте Action<T> где нужно
```

### Проверка понимания (30 мин)
**AI задает вопросы:**
1. "Объясни разницу между Action и Event. Когда что использовать?"
2. "Почему `event Action OnDied` безопаснее чем `Action OnDied`?"
3. "Покажи код, который создаст memory leak с events. Как его исправить?"
4. "Зачем нужен `Invoke?.()` вместо `Invoke()`?"

### Мини-проект (2 часа)
**Задача:** Game Events Manager
- Централизованная система событий для игры
- События: PlayerSpawned, EnemyKilled, LevelCompleted, ScoreChanged
- UI должен подписаться и реагировать
- Cleanup при смене сцены (без memory leaks)

**AI делает code review:**
- Проверяет memory leak protection
- Проверяет naming conventions
- Предлагает улучшения

**Результат:** ✅ Понимаете, как работает EventBus в вашем BlackJack проекте

---

## Module 02: Generics (Week 2)

**Why it matters:** Generic классы/методы - основа переиспользуемого кода (ObjectPool<T>, EventBus<T>)

### Теория (1 час)
**AI объясняет:**
- Generic classes, methods, interfaces
- Type constraints (where T : class, IComparable, new())
- Variance (in, out, covariance/contravariance)
- Generic vs object (boxing/unboxing)

**Примеры для Unity:**
```csharp
// Generic ObjectPool
public class ObjectPool<T> where T : Component
{
    private Stack<T> pool = new Stack<T>();

    public T Get() { ... }
    public void Return(T obj) { ... }
}

// Generic Singleton
public class Singleton<T> where T : MonoBehaviour
{
    private static T instance;
    public static T Instance { get { ... } }
}
```

### Практика (5 часов)
**Задачка 1:** Generic Stack<T>
```csharp
// Реализуйте Stack<T> с нуля (не используйте System.Collections.Generic)
// Методы: Push, Pop, Peek, Count, Clear
// AI проверяет: почему struct лучше для internal array?
```

**Задачка 2:** ObjectPool<T> для Unity
```csharp
// Создайте generic ObjectPool<T> where T : Component
// - Prefab для instantiate
// - Get() / Return()
// - Warmup(int count)
// AI спрашивает: "Что если Return вызван для объекта не из этого пула?"
```

**Задачка 3:** Generic EventBus (улучшение Module 01)
```csharp
// Перепишите EventBus из Module 01 с generics
// - Subscribe<TEvent>(Action<TEvent> handler) where TEvent : IGameEvent
// - Publish<TEvent>(TEvent eventData)
// AI проверяет: зачем constraint "where TEvent : IGameEvent"?
```

### Проверка понимания (30 мин)
1. "Объясни, почему `List<T>` лучше чем `ArrayList`"
2. "Что такое constraint 'where T : new()' и зачем он нужен?"
3. "Можно ли сделать Generic MonoBehaviour? Почему нет?"
4. "Что такое covariance? Покажи пример с `IEnumerable<T>`"

### Мини-проект (2 часа)
**Задача:** Generic Service Locator
```csharp
// Создайте ServiceLocator с generic методами:
// - Register<T>(T service) where T : class
// - Get<T>() where T : class
// - TryGet<T>(out T service)
// - Clear()
// Используйте Dictionary<Type, object> внутри
```

**Результат:** ✅ Можете написать ObjectPool<T> с нуля

---

## Module 03: LINQ (Week 3)

**Why it matters:** LINQ = читаемый код без циклов (но нужно знать performance implications)

### Теория (1.5 часа)
**AI объясняет:**
- IEnumerable<T> vs ICollection vs IList vs Array
- Lazy evaluation (deferred execution)
- Query syntax vs Method syntax
- Performance: когда LINQ OK, когда нет
- Методы: Where, Select, First, Any, All, OrderBy, GroupBy, Aggregate

**Примеры для Unity:**
```csharp
// BAD (в Update)
void Update() {
    var enemies = FindObjectsOfType<Enemy>()
        .Where(e => e.Health > 0)
        .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
        .FirstOrDefault();
}

// GOOD (кешированный список)
void Update() {
    var nearestEnemy = activeEnemies
        .Where(e => e.Health > 0)
        .MinBy(e => Vector3.SqrMagnitude(transform.position - e.transform.position));
}
```

### Практика (5 часов)
**Задачка 1:** Inventory filtering
```csharp
// Дан List<Item> inventory
// Задачи (все через LINQ):
// 1. Найти все Weapons
// 2. Найти самый дорогой предмет
// 3. Сгруппировать по ItemType
// 4. Получить сумму веса всех предметов
// 5. Есть ли предмет дороже 1000 gold?

// AI проверяет: студент использует method syntax или query?
```

**Задачка 2:** Enemy AI targeting
```csharp
// Дан List<Player> players
// Найти через LINQ:
// 1. Ближайшего игрока
// 2. Игрока с наименьшим HP
// 3. Всех игроков в радиусе 10 units
// 4. Игрока с наибольшим уроном за последние 5 секунд

// AI спрашивает: "Почему SqrMagnitude, а не Distance?"
```

**Задачка 3:** Custom LINQ методы (extension methods)
```csharp
// Создайте extension methods для IEnumerable<T>:
// 1. Shuffle<T>() - случайный порядок
// 2. TakeRandom<T>(int count) - N случайных элементов
// 3. MaxBy<T>(Func<T, float> selector) - элемент с макс значением
// 4. Batch<T>(int size) - разбить на батчи

public static class EnumerableExtensions {
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) {
        // Студент реализует
    }
}
```

### Проверка понимания (30 мин)
1. "Что такое lazy evaluation? Покажи пример"
2. "Почему этот код выполнится только один раз?"
   ```csharp
   var query = list.Where(x => { Debug.Log("Filter"); return x > 5; });
   query.ToList(); // тут выполнится
   query.ToList(); // тут снова выполнится!
   ```
3. "Когда НЕ стоит использовать LINQ в Unity?"
4. "В чем разница между `First()` и `FirstOrDefault()`?"

### Мини-проект (2 часа)
**Задача:** Quest System с LINQ
```csharp
// Создайте систему квестов:
// - List<Quest> с фильтрацией (Active, Completed, Available)
// - Поиск квестов по критериям (level, type, reward)
// - Сортировка (priority, difficulty, reward)
// - Statistics (total XP, total Gold, completion %)
// ВСЕ через LINQ (но эффективно!)
```

**Результат:** ✅ Читаете LINQ код как обычный текст

---

## Module 04: Async/Await (Week 4)

**Why it matters:** Асинхронные операции (загрузка, network, animations) без блокировки main thread

### Теория (1.5 часа)
**AI объясняет:**
- Task vs Coroutine в Unity
- async/await syntax
- async void vs async Task (NEVER use async void!)
- CancellationToken (как прервать задачу)
- ConfigureAwait(false) (Unity не нужен)
- UniTask для Unity (better Task)

**Примеры для Unity:**
```csharp
// BAD (блокирующий код)
void LoadLevel() {
    var data = File.ReadAllText("level.json"); // FREEZES game!
    ParseLevel(data);
}

// GOOD (async)
async Task LoadLevelAsync() {
    var data = await File.ReadAllTextAsync("level.json"); // doesn't freeze
    ParseLevel(data);
}

// BAD (async void - exception crash!)
async void OnButtonClick() { // NEVER DO THIS
    await Task.Delay(1000);
    throw new Exception(); // Unhandled exception!
}

// GOOD (async Task + try-catch)
async Task OnButtonClickAsync() {
    try {
        await Task.Delay(1000);
    } catch (Exception ex) {
        Debug.LogError(ex);
    }
}
```

### Практика (5 часов)
**Задачка 1:** Async file loading
```csharp
// Создайте AssetLoader:
// - LoadTextAsync(string path) - загрузить текст
// - LoadJsonAsync<T>(string path) - загрузить и распарсить JSON
// - LoadAllAsync(string[] paths) - загрузить несколько файлов параллельно
// Используйте CancellationToken для отмены

// AI проверяет: студент использует async Task или async void?
```

**Задачка 2:** Countdown Timer
```csharp
// Создайте CountdownTimer:
// - StartAsync(int seconds, CancellationToken token)
// - Callback каждую секунду: Action<int> OnTick
// - Event по завершению: Action OnComplete
// Можно отменить через CancellationToken

// AI спрашивает: "Почему лучше async Task чем Coroutine?"
```

**Задачка 3:** Retry механизм
```csharp
// Создайте метод RetryAsync<T>:
// - Func<Task<T>> operation - асинхронная операция
// - int maxAttempts - сколько попыток
// - TimeSpan delay - задержка между попытками
// Если все попытки провалились - throw exception

public async Task<T> RetryAsync<T>(Func<Task<T>> operation, int maxAttempts, TimeSpan delay) {
    // Студент реализует
}
```

### Проверка понимания (30 мин)
1. "Объясни разницу между Task и Coroutine"
2. "Почему async void опасен?"
3. "Что такое CancellationToken и зачем он нужен?"
4. "Покажи code smell: async method без await"

### Мини-проект (2 часа)
**Задача:** Network Manager (mock)
```csharp
// Создайте NetworkManager с async методами:
// - ConnectAsync(string url, CancellationToken token)
// - SendRequestAsync<TResponse>(string endpoint, object data)
// - DownloadFileAsync(string url, string savePath, IProgress<float> progress)
// Используйте retry механизм, timeout, progress reporting
```

**Результат:** ✅ Понимаете, когда Task, а когда Coroutine

---

## Module 05: Records, Structs, Immutability (Week 5)

**Why it matters:** Immutable state = предсказуемый код (как в вашем BlackJack проекте!)

### Теория (1.5 часа)
**AI объясняет:**
- Class vs Struct (value type vs reference type)
- When to use struct (small, immutable data)
- readonly struct (performance + safety)
- Record types (C# 9+) - immutable by default
- `with` expression (non-destructive mutation)
- Value equality vs Reference equality

**Примеры для Unity:**
```csharp
// BAD (mutable class)
public class PlayerData {
    public int Health; // можно изменить!
    public int Score;
}

void TakeDamage(PlayerData player, int damage) {
    player.Health -= damage; // MUTATION! Другие части кода увидят изменение
}

// GOOD (immutable record)
public record PlayerData(int Health, int Score);

PlayerData TakeDamage(PlayerData player, int damage) {
    return player with { Health = player.Health - damage }; // NEW object
}

// GOOD (readonly struct for small data)
public readonly struct Vector2Int {
    public readonly int X;
    public readonly int Y;

    public Vector2Int(int x, int y) {
        X = x;
        Y = y;
    }
}
```

### Практика (5 часов)
**Задачка 1:** Game State с records
```csharp
// Создайте immutable game state:
// - GameState(Level, Score, Lives)
// - Методы возвращают НОВЫЙ GameState:
//   - AddScore(int points)
//   - LoseLife()
//   - NextLevel()
// Используйте record + with expression

// AI проверяет: студент НЕ мутирует state?
```

**Задачка 2:** Inventory System (immutable)
```csharp
// Создайте Inventory с immutable коллекциями:
// - record Inventory(ImmutableList<Item> Items, int Gold)
// - AddItem(Item item) -> new Inventory
// - RemoveItem(Item item) -> new Inventory
// - BuyItem(Item item, int cost) -> Result<Inventory>

// AI спрашивает: "Зачем ImmutableList, а не обычный List?"
```

**Задачка 3:** State History (Undo/Redo)
```csharp
// Создайте StateHistory<T>:
// - Push(T state) - сохранить состояние
// - Undo() -> T - откатить на шаг назад
// - Redo() -> T - повторить отмененное
// - CanUndo, CanRedo properties
// T должен быть immutable (record)

// AI проверяет: как студент хранит историю? Stack? List?
```

### Проверка понимания (30 мин)
1. "Объясни разницу между class и struct. Когда что использовать?"
2. "Что такое boxing? Покажи пример"
3. "Зачем record, если есть class?"
4. "Что произойдет с памятью, если каждое изменение создает новый объект?"

### Мини-проект (2 часа)
**Задача:** Card Game State (как в BlackJack проекте!)
```csharp
// Создайте immutable state для карточной игры:
// - record RoundData(Phase, DrawnCard, PlayerHands, Scores)
// - record PlayerHand(List<Card> Cards, int TotalValue)
// - Методы возвращают НОВЫЙ RoundData:
//   - DrawCard(int playerIndex)
//   - TakeCard(int playerIndex)
//   - EndRound()
// Никаких mutation!
```

**Результат:** ✅ Понимаете Intent Pattern + Immutable State в вашем проекте

---

## Module 06: Pattern Matching (Week 6)

**Why it matters:** Чистый код без if/else ада

### Теория (1 час)
**AI объясняет:**
- is/as operators
- switch expressions (C# 8+)
- Property patterns
- Positional patterns
- Relational patterns (>, <, >=)
- Logical patterns (and, or, not)

**Примеры для Unity:**
```csharp
// OLD (ugly if/else)
if (obj is Enemy) {
    var enemy = (Enemy)obj;
    if (enemy.Health > 0 && enemy.Health < 50) {
        // low health logic
    }
}

// NEW (pattern matching)
if (obj is Enemy { Health: > 0 and < 50 } enemy) {
    // low health logic
}

// OLD (ugly switch)
string GetMessage(int status) {
    switch (status) {
        case 0: return "Idle";
        case 1: return "Moving";
        case 2: return "Attacking";
        default: return "Unknown";
    }
}

// NEW (switch expression)
string GetMessage(int status) => status switch {
    0 => "Idle",
    1 => "Moving",
    2 => "Attacking",
    _ => "Unknown"
};
```

### Практика (4 часов)
**Задачка 1:** Damage Calculator
```csharp
// Создайте CalculateDamage с pattern matching:
// - Weapon { Type: "Sword", Rarity: "Legendary" } => базовый урон * 2
// - Weapon { Type: "Bow", Ammo: > 0 } => базовый урон
// - Weapon { Type: "Bow", Ammo: 0 } => 0
// - Weapon { Durability: <= 0 } => 0
// Используйте switch expression

public int CalculateDamage(Weapon weapon) => weapon switch {
    // Студент реализует
};
```

**Задачка 2:** State Machine с pattern matching
```csharp
// Создайте AI State Machine:
// - record IdleState(), PatrolState(Vector3 Target), ChaseState(Player Target), AttackState()
// Метод Update(State current) -> State:
// - Idle + видит игрока -> Chase
// - Chase + близко -> Attack
// - Attack + далеко -> Chase
// - Chase + потерял игрока -> Patrol
// Используйте switch expression с patterns
```

**Задачка 3:** Input Handler
```csharp
// Создайте HandleInput(InputEvent evt):
// InputEvent = KeyPressed(KeyCode) | MouseClicked(Vector2) | TouchEvent(int fingerId, Vector2)
// Pattern matching:
// - KeyPressed(KeyCode.Space) -> Jump
// - MouseClicked({ x: > 0, y: > 0 }) -> Attack
// - TouchEvent(0, var pos) -> Move to pos
```

### Проверка понимания (30 мин)
1. "Объясни разницу между switch statement и switch expression"
2. "Что такое discard pattern (_)?"
3. "Покажи property pattern для nested objects"

### Мини-проект (1.5 часа)
**Задача:** Quest Validator
```csharp
// Создайте QuestValidator:
// Quest = KillQuest(Enemy, Count) | CollectQuest(Item, Count) | EscortQuest(NPC, Destination)
// Метод CanComplete(Quest quest, Player player) -> bool
// Используйте pattern matching для всех типов квестов
```

**Результат:** ✅ Код без if/else вложенности

---

## Module 07: Nullability & Error Handling (Week 7)

**Why it matters:** Избегайте NullReferenceException (самая частая ошибка!)

### Теория (1.5 часа)
**AI объясняет:**
- Nullable Reference Types (C# 8+)
- ?, ??, ??=, ?. operators
- Null-forgiving operator (!)
- Result<T> pattern (вместо exceptions)
- Option<T>/Maybe<T> pattern
- Railway-Oriented Programming

**Примеры для Unity:**
```csharp
// BAD (null explosion)
public Enemy FindEnemy(string name) {
    return enemies.Find(e => e.Name == name); // может быть null!
}

var enemy = FindEnemy("Boss");
enemy.TakeDamage(10); // CRASH если null!

// GOOD (Option<T>)
public Option<Enemy> FindEnemy(string name) {
    var enemy = enemies.Find(e => e.Name == name);
    return enemy != null ? Option.Some(enemy) : Option.None<Enemy>();
}

FindEnemy("Boss").Match(
    some: enemy => enemy.TakeDamage(10),
    none: () => Debug.Log("Enemy not found")
);

// GOOD (Result<T>)
public Result<int> Divide(int a, int b) {
    if (b == 0) return Result.Failure<int>("Division by zero");
    return Result.Success(a / b);
}

Divide(10, 2).Match(
    success: value => Debug.Log($"Result: {value}"),
    failure: error => Debug.LogError(error)
);
```

### Практика (5 часов)
**Задачка 1:** Implement Option<T>
```csharp
// Создайте Option<T> с нуля:
// - Some(T value)
// - None()
// - Match<TResult>(Func<T, TResult> some, Func<TResult> none)
// - Map<TResult>(Func<T, TResult> mapper)
// - IsSome, IsNone properties

public readonly struct Option<T> {
    // Студент реализует
}
```

**Задачка 2:** Implement Result<T>
```csharp
// Создайте Result<T>:
// - Success(T value)
// - Failure(string error)
// - Match<TResult>(Func<T, TResult> success, Func<string, TResult> failure)
// - Bind<TResult>(Func<T, Result<TResult>> binder) // для chaining
// - IsSuccess, IsFailure, Error properties
```

**Задачка 3:** Safe Resource Loader
```csharp
// Создайте ResourceLoader:
// - Result<T> Load<T>(string path) where T : Object
// - Option<T> TryFind<T>(string name) where T : Component
// - Result<Sprite> LoadSprite(string path, string spriteName)
// Никаких exceptions! Только Result/Option
```

### Проверка понимания (30 мин)
1. "Объясни разницу между `T?` для value type и reference type"
2. "Когда использовать Result<T>, а когда Option<T>?"
3. "Что такое Railway-Oriented Programming?"
4. "Зачем null-forgiving operator (!)? Когда его использовать?"

### Мини-проект (2 часа)
**Задача:** Safe Inventory System
```csharp
// Создайте Inventory с Result<T>:
// - Result<Inventory> AddItem(Item item) // может быть "Inventory full"
// - Result<(Inventory, Item)> RemoveItem(int index) // может быть "Index out of range"
// - Result<Inventory> UseItem(int index) // может быть "Cannot use item"
// - Option<Item> FindItem(Predicate<Item> predicate)
// Никаких exceptions! Весь error handling через Result/Option
```

**Результат:** ✅ Понимаете Result<T> в вашем IntentHandler (BlackJack проект)

---

## Module 08: Advanced Techniques (Week 8)

**Why it matters:** "Магия" продвинутого C# кода

### Теория (2 часа)
**AI объясняет:**
- Expression-bodied members (=> syntax)
- Local functions
- Tuple deconstruction
- Discards (_)
- ref/in/out parameters
- Span<T> (для performance)
- Index/Range (^1, [1..5])

**Примеры для Unity:**
```csharp
// Expression-bodied members
public int Health => currentHealth; // вместо { get { return currentHealth; } }
public void Heal(int amount) => currentHealth = Mathf.Min(currentHealth + amount, maxHealth);

// Local functions
public List<Enemy> GetEnemiesInRadius(Vector3 center, float radius) {
    var enemies = new List<Enemy>();

    // Local function (видна только внутри метода)
    bool IsInRadius(Enemy enemy) =>
        Vector3.Distance(center, enemy.transform.position) <= radius;

    foreach (var enemy in allEnemies) {
        if (IsInRadius(enemy)) enemies.Add(enemy);
    }
    return enemies;
}

// Tuple deconstruction
var (min, max) = GetMinMax(values);

// Index/Range
var lastItem = items[^1]; // последний элемент
var firstThree = items[0..3]; // первые 3
var allButFirst = items[1..]; // все кроме первого
```

### Практика (4 часов)
**Задачка 1:** Refactoring practice
```csharp
// AI дает "плохой" код, студент рефакторит с новыми техниками:
// - Длинные методы -> expression-bodied
// - Повторяющийся код -> local functions
// - Multiple return values -> tuples
// - Array копирование -> Span<T>
```

**Задачка 2:** Performance optimization
```csharp
// Оптимизируйте метод с помощью Span<T>:
public int SumArray(int[] array) {
    int sum = 0;
    for (int i = 0; i < array.Length; i++) {
        sum += array[i];
    }
    return sum;
}

// Используйте Span<T> для zero-copy processing
public int SumArray(Span<int> array) {
    // Студент реализует
}
```

**Задачка 3:** Collection slicing
```csharp
// Создайте методы с Index/Range:
// - GetLastN<T>(List<T> list, int n) // последние N элементов
// - RemoveFirstN<T>(List<T> list, int n) // убрать первые N
// - GetMiddle<T>(List<T> list) // средние 50%
// Используйте [^N] и [start..end]
```

### Проверка понимания (30 мин)
1. "Когда использовать local function, а когда private method?"
2. "Объясни разницу между ref, in, out"
3. "Зачем Span<T>, если есть array?"
4. "Что такое [^1]? А [1..^1]?"

### Мини-проект (2 часа)
**Задача:** Performance-critical pathfinding helper
```csharp
// Создайте PathHelper:
// - FindPath(Span<Vector2Int> grid, Vector2Int start, Vector2Int end)
// - IsReachable(Span<bool> walkableMap, int width, int height, Vector2Int target)
// - GetNeighbors(Vector2Int pos) - используйте local function
// Оптимизируйте для минимальных аллокаций
```

**Результат:** ✅ Читаете продвинутый C# код без затруднений

---

## Final Project: Mini Architecture Challenge (Bonus Week)

**Задача:** Создайте систему с применением ВСЕХ техник:

**Requirements:**
- Generic ObjectPool<T>
- EventBus с async events
- Immutable game state (records)
- Result<T> для error handling
- Pattern matching для logic
- LINQ для queries
- Full unit tests (NUnit)

**AI делает полноценный code review:**
- Архитектура (правильное разделение ответственности)
- Производительность (нет лишних аллокаций)
- Читаемость (clean code)
- Тестируемость (покрытие тестами)

**Milestone:** ✅ **Уровень: Middle C# Developer**

---

## Как работать с AI (Qwen)

### Цикл обучения для каждого модуля:

1. **Попросите теорию:**
   ```
   "Qwen, объясни мне delegates в C# для Unity.
   Покажи примеры для UI events и EventBus"
   ```

2. **Попросите задачки:**
   ```
   "Qwen, дай мне 3 задачки на delegates:
   легкую, среднюю и сложную. Проверяй мои решения"
   ```

3. **Объясняйте обратно (Feynman technique):**
   ```
   "Qwen, сейчас я объясню тебе, что такое delegate.
   Скажи, если я что-то понял неправильно:
   [ваше объяснение]"
   ```

4. **Просите code review:**
   ```
   "Qwen, вот мой код для EventBus.
   Сделай code review: что плохо, что хорошо, как улучшить?"
   ```

5. **Запрашивайте вопросы на понимание:**
   ```
   "Qwen, задай мне 5 вопросов на понимание delegates.
   Проверяй мои ответы и объясняй ошибки"
   ```

### Полезные команды для Qwen:

- "Объясни как 5-летнему ребенку"
- "Покажи real-world пример из Unity"
- "Сравни X и Y: когда что использовать?"
- "Какие ошибки новички делают с X?"
- "Дай мне челлендж: сложную задачку на X"

---

## Ресурсы для углубления

**Книги:**
- "C# in Depth" by Jon Skeet (после базы)
- "Functional Programming in C#" by Enrico Buonanno

**Онлайн:**
- Microsoft Learn: C# documentation
- Unity Learn: C# best practices
- Nick Chapsas (YouTube) - modern C# features

**Практика:**
- LeetCode на C# (алгоритмы)
- Codewars C# Katas (TDD)
- Refactoring.Guru (паттерны с C# примерами)

---

**Next Step:** ➡️ [Architecture Plan](../03-Architecture/PLAN.md)
