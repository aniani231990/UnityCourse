# Architecture & Design Patterns: From Theory to Practice

**Duration:** 12 недель (90 часов total)
**Prerequisites:** Completed C# for Unity Plan (или эквивалент)
**Goal:** Проектировать game systems с правильной архитектурой

---

## Философия обучения

```
Теория паттернов ≠ Архитектурное мышление

Junior знает паттерны → "Я использую Observer"
Middle применяет паттерны → "Я решаю проблему X через Observer"
Senior проектирует системы → "Я комбинирую Observer + Factory для гибкости"
```

**Ваша цель:** Думать как Senior до написания первой строки кода.

---

## Phase 1: Practical Patterns (Week 1-6)

### Week 1: ObjectPool Pattern

**Problem:** Instantiate/Destroy вызывает GC spike → лаги в игре

**Theory (1 час):**
```csharp
// BAD (каждый кадр аллоцирует память)
void Update() {
    if (shouldSpawnBullet) {
        var bullet = Instantiate(bulletPrefab);
    }
}

// GOOD (переиспользуем объекты)
void Update() {
    if (shouldSpawnBullet) {
        var bullet = bulletPool.Get(); // из пула
    }
}
```

**AI объясняет:**
- Когда нужен ObjectPool (часто создаем/уничтожаем объекты)
- Warmup vs Lazy initialization
- Generic pool vs Type-specific pool
- Return to pool: manual vs automatic (ParticleSystem.OnComplete)

**Practice (5 часов):**

**Challenge 1:** Basic Pool
```csharp
// Создайте ObjectPool<T> where T : Component
// - Get() / Return(T obj)
// - Warmup(int count)
// - Clear()
// AI проверяет: как обрабатываете случай "Get() но pool пустой"?
```

**Challenge 2:** Smart Pool (auto-return)
```csharp
// Создайте PooledObject component:
// - Автовозврат в pool через N секунд
// - Автовозврат при OnDisable
// - Событие OnReturnedToPool
// AI спрашивает: "Что если объект уже в пуле, но Return вызван снова?"
```

**Challenge 3:** Multi-Pool Manager
```csharp
// Создайте PoolManager:
// - Register(GameObject prefab, int initialSize)
// - Get(GameObject prefab) → GameObject
// - ReturnAll() // вернуть все объекты всех пулов
// - GetStatistics() → Dictionary<string, PoolStats>
```

**Mini-Project (2 часа):** Bullet Hell Shooter
- 1000+ bullets on screen
- Используйте ObjectPool для пуль и VFX
- Цель: 60 FPS на мобильном

**AI Code Review:**
- Memory leaks check
- Performance profiling
- Edge cases handling

---

### Week 2: EventBus Pattern

**Problem:** Tight coupling между системами (UI знает про GameManager, GameManager знает про SoundManager)

**Theory (1 час):**
```csharp
// BAD (tight coupling)
public class GameManager {
    public UIManager uiManager; // dependency!
    public SoundManager soundManager; // dependency!

    void OnEnemyKilled() {
        uiManager.UpdateScore(10);
        soundManager.PlaySound("kill");
    }
}

// GOOD (loose coupling через events)
public class GameManager {
    void OnEnemyKilled() {
        EventBus.Publish(new EnemyKilledEvent(10));
    }
}

// UI и Sound подписываются независимо
public class UIManager {
    void OnEnable() {
        EventBus.Subscribe<EnemyKilledEvent>(OnEnemyKilled);
    }
}
```

**AI объясняет:**
- EventBus vs C# events (когда что)
- Typed events vs string-based events
- Memory leak prevention (Unsubscribe!)
- Performance: Dictionary lookup overhead

**Practice (5 часов):**

**Challenge 1:** Generic EventBus
```csharp
// Создайте EventBus:
// - Subscribe<T>(Action<T> handler) where T : IGameEvent
// - Unsubscribe<T>(Action<T> handler)
// - Publish<T>(T eventData)
// - Clear() // очистить все подписки
// AI проверяет: используете ли Dictionary<Type, List<Delegate>>?
```

**Challenge 2:** Priority Events
```csharp
// Добавьте приоритеты:
// - Subscribe<T>(Action<T> handler, int priority = 0)
// - События обрабатываются от высокого priority к низкому
// Пример: UI (priority=100) обрабатывается раньше Sound (priority=0)
```

**Challenge 3:** Event History (Debug tool)
```csharp
// Создайте EventHistory:
// - Записывает последние 100 событий
// - GetHistory() → List<(Type, DateTime, object)>
// - GetHistory<T>() → только события типа T
// - Clear()
// Полезно для дебага: "Что происходило перед багом?"
```

**Mini-Project (2 часа):** Game Events System
- События: GameStarted, GamePaused, PlayerDied, ScoreChanged, LevelCompleted
- UI, Sound, Analytics подписываются на события
- Debug UI показывает event history

**AI Code Review:**
- Проверка memory leaks (unsubscribe)
- Thread safety (если нужен)
- Event naming conventions

---

### Week 3: State Pattern (FSM)

**Problem:** AI/UI logic с множеством if/else → спагетти-код

**Theory (1.5 часа):**
```csharp
// BAD (if/else hell)
void Update() {
    if (isIdle) {
        if (seesPlayer) {
            isIdle = false;
            isChasing = true; // забыли сбросить другие флаги!
        }
    } else if (isChasing) {
        if (closeToPlayer) {
            isChasing = false;
            isAttacking = true;
        } else if (!seesPlayer) {
            isChasing = false;
            isPatrolling = true;
        }
    }
    // 50+ строк if/else...
}

// GOOD (State Machine)
void Update() {
    currentState = currentState.Update(); // state сам решает переход
}

// Каждое состояние - отдельный класс
public class IdleState : IEnemyState {
    public IEnemyState Update() {
        if (seesPlayer) return new ChaseState();
        return this;
    }
}
```

**AI объясняет:**
- State Pattern vs Enum-based FSM (trade-offs)
- State transitions (кто решает: state или context?)
- Hierarchical FSM (substates)
- Pushdown Automaton (стек состояний)

**Practice (6 часов):**

**Challenge 1:** Basic FSM
```csharp
// Создайте StateMachine<TContext>:
// - ChangeState(IState<TContext> newState)
// - Update() // вызывает currentState.Update()
// - CurrentState property
// + создайте 3 состояния для Enemy AI: Idle, Patrol, Chase
```

**Challenge 2:** State Transitions System
```csharp
// Добавьте явные переходы:
// - AddTransition(State from, State to, Func<bool> condition)
// - State машина автоматически переключается при condition = true
// Пример:
// fsm.AddTransition(idle, chase, () => seesPlayer);
// fsm.AddTransition(chase, attack, () => distanceToPlayer < 2f);
```

**Challenge 3:** Hierarchical FSM
```csharp
// Создайте HierarchicalStateMachine:
// - State может содержать sub-FSM
// - Пример: CombatState (parent) → AttackState, DefendState, DodgeState (children)
// - Update вызывается для parent и active child
```

**Mini-Project (2 часа):** Boss AI
- Состояния: Idle → Phase1 → Phase2 → Phase3 → Dead
- Phase1: простые атаки
- Phase2: (health < 50%) добавляются новые атаки
- Phase3: (health < 25%) berserk mode
- Используйте Hierarchical FSM

**AI Code Review:**
- Четкие правила переходов (не спагетти)
- Каждое состояние = Single Responsibility
- OnEnter/OnExit для cleanup

---

### Week 4: Command Pattern

**Problem:** Undo/Redo, Input buffering, Replay systems

**Theory (1 час):**
```csharp
// BAD (прямой вызов)
void OnAttackButton() {
    player.Attack(); // как сделать Undo?
}

// GOOD (Command)
public interface ICommand {
    void Execute();
    void Undo();
}

public class AttackCommand : ICommand {
    private Player player;

    public void Execute() => player.Attack();
    public void Undo() => player.CancelAttack();
}

void OnAttackButton() {
    var cmd = new AttackCommand(player);
    cmd.Execute();
    commandHistory.Push(cmd); // можно Undo!
}
```

**AI объясняет:**
- Command vs Intent Pattern (ваш BlackJack проект!)
- Undo/Redo через стек команд
- Command Queue (turn-based games)
- Replay system (сохраняем команды, воспроизводим)

**Practice (5 часов):**

**Challenge 1:** Basic Command
```csharp
// Создайте ICommand + CommandInvoker:
// - Execute()
// - Undo()
// - CommandInvoker хранит историю
// - Undo() / Redo() методы
// Реализуйте MoveCommand для Platformer
```

**Challenge 2:** Macro Commands
```csharp
// Создайте MacroCommand (composite):
// - Содержит List<ICommand>
// - Execute() выполняет все команды
// - Undo() откатывает в обратном порядке
// Пример: "BuildHouse" = PlaceFoundation + BuildWalls + AddRoof
```

**Challenge 3:** Async Commands
```csharp
// Создайте IAsyncCommand:
// - Task ExecuteAsync()
// - Task UndoAsync()
// Пример: LoadLevelCommand (асинхронная загрузка)
```

**Mini-Project (2 часа):** Turn-Based Strategy
- Команды: MoveUnit, AttackUnit, UseAbility
- Полная поддержка Undo/Redo
- Replay system (сохранить матч, воспроизвести)

**AI Code Review:**
- Immutability (команда не должна хранить mutable state)
- Idempotency (Execute дважды = Execute один раз?)
- Error handling (что если Execute failed?)

---

### Week 5: Factory Pattern

**Problem:** Сложная логика создания объектов, зависимости

**Theory (1 час):**
```csharp
// BAD (создание размазано по коду)
void SpawnEnemy() {
    var enemy = Instantiate(enemyPrefab);
    enemy.GetComponent<Enemy>().Initialize(health, damage);
    enemy.GetComponent<EnemyAI>().SetTarget(player);
    // если забыли Initialize → баг!
}

// GOOD (Factory)
public class EnemyFactory {
    public Enemy Create(EnemyType type) {
        var enemy = Instantiate(GetPrefab(type));
        var component = enemy.GetComponent<Enemy>();
        component.Initialize(GetStats(type));
        component.GetComponent<EnemyAI>().SetTarget(player);
        return component; // гарантия: всё настроено!
    }
}
```

**AI объясняет:**
- Simple Factory vs Factory Method vs Abstract Factory
- Factory + ObjectPool (pooled factory)
- Factory + Dependency Injection
- Procedural generation через factory

**Practice (5 часов):**

**Challenge 1:** Weapon Factory
```csharp
// Создайте WeaponFactory:
// - Create(WeaponType type, Rarity rarity) → Weapon
// - Рассчитывает stats на основе type + rarity
// - Создает prefab
// - Применяет random модификаторы (+10% damage, +5% crit)
```

**Challenge 2:** Configurable Factory
```csharp
// Создайте DataDrivenFactory:
// - Конфигурация через ScriptableObject
// - EnemyConfig { prefab, health, damage, abilities[] }
// - Create(string enemyId) → читает из config
// Легко добавлять новых врагов без изменения кода!
```

**Challenge 3:** Pooled Factory
```csharp
// Объедините Factory + ObjectPool:
// - Create() берет из pool (или создает если pool пустой)
// - Destroy() возвращает в pool (вместо Destroy)
// - Warmup(type, count)
```

**Mini-Project (2 часа):** Loot System
- LootFactory создает items (Weapon, Armor, Consumable)
- Rarity system (Common, Rare, Epic, Legendary)
- Procedural stats generation
- Item affixes (Fire, Ice, Lightning)

**AI Code Review:**
- Конфигурация vs Hardcode (легко ли добавлять новые типы?)
- Separation of Concerns (Factory не должен знать про ObjectPool implementation)

---

### Week 6: Observer Pattern (Advanced)

**Problem:** Reactive UI, data binding, live updates

**Theory (1.5 часа):**
```csharp
// BAD (UI poll data каждый кадр)
void Update() {
    healthText.text = player.Health.ToString(); // каждый кадр!
}

// GOOD (Observer / Reactive)
public class Health : IObservable<int> {
    private int value;
    private List<IObserver<int>> observers = new();

    public int Value {
        get => value;
        set {
            this.value = value;
            NotifyObservers(value);
        }
    }
}

// UI подписывается один раз
health.Subscribe(newValue => healthText.text = newValue.ToString());
```

**AI объясняет:**
- IObservable<T> / IObserver<T> (System.Reactive)
- Property change notification
- Computed properties (auto-update)
- Memory leaks (weak references)
- UniRx для Unity (Reactive Extensions)

**Practice (5 часов):**

**Challenge 1:** Observable<T>
```csharp
// Создайте Observable<T>:
// - Value property (setter вызывает OnNext)
// - Subscribe(Action<T> onNext)
// - Unsubscribe
// + создайте ObservableProperty<T> для инспектора Unity
```

**Challenge 2:** Computed Observable
```csharp
// Создайте ComputedObservable:
// var health = new Observable<int>(100);
// var maxHealth = new Observable<int>(100);
// var healthPercent = health.Combine(maxHealth, (h, m) => (float)h / m);
// healthPercent.Subscribe(p => healthBar.fillAmount = p);
// При изменении health ИЛИ maxHealth → healthPercent автообновляется!
```

**Challenge 3:** Collection Observable
```csharp
// Создайте ObservableList<T>:
// - Add/Remove/Clear вызывают события
// - OnItemAdded, OnItemRemoved, OnCollectionChanged
// Пример: inventory.OnItemAdded += item => UI.AddSlot(item);
```

**Mini-Project (2 часа):** Reactive UI
- ObservableProperty для всех player stats (health, mana, xp, level)
- UI автообновляется при изменении (data binding)
- Computed properties: xpPercent, healthPercent
- ObservableList для inventory

**AI Code Review:**
- Memory leaks (unsubscribe on destroy)
- Performance (не слишком много подписчиков?)
- Error handling (что если observer throws?)

---

## Phase 2: System Design (Week 7-10)

### Week 7: Immutable State Architecture

**Problem:** Mutable state → непредсказуемое поведение, сложный debug

**Theory (2 часа):**
```csharp
// BAD (mutable state)
public class GameState {
    public int Score; // может измениться откуда угодно!
    public List<Player> Players; // кто-то может удалить игрока!
}

void AddScore(GameState state, int points) {
    state.Score += points; // SIDE EFFECT! Другие части кода увидят изменение
}

// GOOD (immutable state)
public record GameState(int Score, ImmutableList<Player> Players);

GameState AddScore(GameState state, int points) {
    return state with { Score = state.Score + points }; // NEW state
}
```

**AI объясняет:**
- Immutability benefits (predictability, time-travel debug, easy undo)
- Records vs Classes (value equality)
- ImmutableList<T>, ImmutableDictionary<K,V>
- Performance implications (GC pressure)
- When to use (game logic, turn-based) vs when NOT (rendering, physics)

**Practice (6 часов):**

**Challenge 1:** Immutable Card Game (как ваш BlackJack!)
```csharp
// Создайте immutable state:
// - record GameState(Round, Players, Deck)
// - record Round(Phase, CurrentPlayer, Board)
// - record Player(Hand, Score, Status)
// Методы возвращают НОВЫЙ GameState:
// - DrawCard(GameState state, int playerIndex) → GameState
// - PlayCard(GameState state, Card card) → GameState
// - EndTurn(GameState state) → GameState
```

**Challenge 2:** Time Travel Debugger
```csharp
// Создайте StateHistory<T>:
// - Push(T state)
// - Undo() → T (откат на 1 шаг)
// - Redo() → T
// - JumpTo(int index) (перейти к любому состоянию)
// - GetHistory() → List<T>
// Используйте для debug: "Что было 5 ходов назад?"
```

**Challenge 3:** State Serialization
```csharp
// Immutable state легко сериализовать:
// - SaveGame(GameState state, string path)
// - LoadGame(string path) → GameState
// - SaveReplay(List<GameState> history, string path)
// JSON serialization через System.Text.Json
```

**Mini-Project (3 часа):** Turn-Based RPG Combat
- Полностью immutable state
- Каждое действие возвращает новый state
- Time-travel debug (откат любого хода)
- Save/Load system
- Replay system

**AI Code Review:**
- Никаких mutations!
- GC pressure analysis (слишком много аллокаций?)
- Data flow clear (easy to follow)

---

### Week 8: Event-Driven Architecture

**Problem:** Расширяемая система без изменения существующего кода

**Theory (2 часа):**
```csharp
// BAD (жесткая связь)
public class GameSession {
    public UIManager uiManager;
    public SoundManager soundManager;
    public AnalyticsManager analyticsManager;

    void OnPlayerDied() {
        uiManager.ShowGameOver();
        soundManager.PlayDeathSound();
        analyticsManager.LogEvent("player_died");
        // Добавили новую систему? Нужно изменить ВСЮ логику!
    }
}

// GOOD (event-driven)
public class GameSession {
    void OnPlayerDied() {
        eventBus.Publish(new PlayerDiedEvent());
        // Добавили новую систему? Она просто подписывается на событие!
    }
}
```

**AI объясняет:**
- Pub/Sub pattern (loose coupling)
- Domain Events (бизнес-логика через события)
- Event Sourcing (состояние = последовательность событий)
- CQRS (Command Query Responsibility Segregation)
- Event ordering / priority

**Practice (6 часов):**

**Challenge 1:** Domain Events System
```csharp
// Создайте систему доменных событий:
// События: PlayerSpawned, PlayerMoved, PlayerAttacked, EnemyKilled
// Каждое событие = immutable record с timestamp
// EventStore хранит все события (event sourcing)
// Можно rebuild состояния из событий!
```

**Challenge 2:** CQRS Light
```csharp
// Разделите Commands и Queries:
// Commands (изменяют state): AttackCommand, MoveCommand
// Queries (читают state): GetPlayerHealth, GetEnemiesInRadius
// CommandHandler обрабатывает команды → генерит события
// QueryHandler читает read-only state
```

**Challenge 3:** Event Replay
```csharp
// Создайте EventReplayer:
// - Record(IGameEvent evt) - сохранить событие
// - Replay(float speed = 1f) - воспроизвести
// - Pause/Resume/Stop
// Полезно для replay system, debug, testing
```

**Mini-Project (3 часа):** RTS Game Core
- Команды: MoveUnit, AttackUnit, BuildStructure
- События: UnitMoved, UnitAttacked, StructureBuilt
- Event Sourcing (rebuild game state из событий)
- Replay system (сохранить/воспроизвести матч)
- Observer systems (FOW, Minimap) подписываются на события

**AI Code Review:**
- Loose coupling (легко добавлять новые системы)
- Event naming (прошедшее время: "PlayerMoved", не "MovePlayer")
- Event granularity (не слишком мелкие? не слишком крупные?)

---

### Week 9: Dependency Injection

**Problem:** Tight coupling, сложное тестирование, god objects

**Theory (2 часа):**
```csharp
// BAD (tight coupling)
public class Player {
    private HealthSystem healthSystem = new HealthSystem(); // hard dependency!
    private WeaponSystem weaponSystem = new WeaponSystem(); // hard dependency!

    // Как тестировать? Как подменить на mock?
}

// GOOD (DI через constructor)
public class Player {
    private readonly IHealthSystem healthSystem;
    private readonly IWeaponSystem weaponSystem;

    public Player(IHealthSystem health, IWeaponSystem weapon) {
        this.healthSystem = health; // инжектим зависимость!
        this.weaponSystem = weapon;
    }
}

// В тестах: подставляем mock
var player = new Player(new MockHealthSystem(), new MockWeaponSystem());
```

**AI объясняет:**
- Constructor Injection vs Property Injection
- Service Locator (anti-pattern или нет?)
- DI Containers (Zenject, VContainer)
- Lifetimes: Transient, Singleton, Scoped
- Unity-specific: ScriptableObject as DI

**Practice (6 часов):**

**Challenge 1:** Manual DI
```csharp
// Создайте систему без DI Container:
// - Interfaces: IHealthSystem, IInventorySystem, IMovementSystem
// - Player принимает зависимости через конструктор
// - Composition Root (один класс создает все зависимости)
// AI проверяет: нет ли God Object в Composition Root?
```

**Challenge 2:** Simple DI Container
```csharp
// Создайте свой DI Container:
// - Register<TInterface, TImplementation>()
// - RegisterSingleton<T>(T instance)
// - Resolve<T>() → T
// - BuildUp(object obj) (property injection)
// Пример:
// container.Register<IHealthSystem, HealthSystem>();
// var health = container.Resolve<IHealthSystem>();
```

**Challenge 3:** ScriptableObject DI
```csharp
// Создайте DI через ScriptableObjects:
// - GameConfig (ScriptableObject) хранит ссылки на системы
// - Systems (ScriptableObjects): HealthSystemConfig, WeaponSystemConfig
// - Player получает зависимости из GameConfig
// Unity-friendly: настройка через Inspector!
```

**Mini-Project (3 часа):** Modular Game Architecture
- Системы: HealthSystem, InventorySystem, QuestSystem, DialogueSystem
- Player/Enemy зависят от интерфейсов (не от конкретных классов)
- DI Container регистрирует все системы
- Unit тесты с mock dependencies

**AI Code Review:**
- Зависимости явные (видны в конструкторе)
- Нет Service Locator внутри классов (только в Composition Root)
- Lifetimes правильные (Singleton для managers, Transient для entities)

---

### Week 10: Layered Architecture

**Problem:** Спагетти-код, где всё знает всё

**Theory (2 часа):**
```csharp
// BAD (no layers)
public class GameManager : MonoBehaviour {
    void Update() {
        // Input
        if (Input.GetKeyDown(KeyCode.Space)) {
            // Business Logic
            player.Health -= 10;
            // Data Access
            PlayerPrefs.SetInt("health", player.Health);
            // Presentation
            healthText.text = player.Health.ToString();
            // Всё в одном месте!
        }
    }
}

// GOOD (layers)
// Layer 1: Presentation (MonoBehaviour, UI)
public class GameView : MonoBehaviour {
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            gameSession.ProcessIntent(new TakeDamageIntent(10));
        }
    }
}

// Layer 2: Business Logic (чистый C#, без Unity API)
public class GameSession {
    public void ProcessIntent(TakeDamageIntent intent) {
        var newState = gameLogic.ApplyDamage(currentState, intent.Amount);
        repository.Save(newState);
        eventBus.Publish(new HealthChangedEvent(newState.Health));
    }
}

// Layer 3: Data (persistence)
public class GameRepository {
    public void Save(GameState state) {
        PlayerPrefs.SetInt("health", state.Health);
    }
}
```

**AI объясняет:**
- Presentation / Business / Data layers
- Dependency direction (всегда вниз)
- Unity-specific: MonoBehaviour vs Pure C#
- Testability (business logic без Unity!)
- Ваш BlackJack проект (GameView → GameSession → Rules/Actions → Data)

**Practice (6 hours):**

**Challenge 1:** Refactoring to Layers
```csharp
// AI дает "плохой" код (всё в одном MonoBehaviour)
// Студент рефакторит на слои:
// - InputLayer (MonoBehaviour) - собирает input
// - GameLogicLayer (pure C#) - обрабатывает логику
// - DataLayer (ScriptableObject / JSON) - сохранение
// - PresentationLayer (MonoBehaviour) - UI
```

**Challenge 2:** Cross-Layer Communication
```csharp
// Настройте связь между слоями:
// Presentation → Business: через Commands/Intents
// Business → Presentation: через Events
// Business → Data: через Repository pattern
// AI проверяет: нет ли обратных зависимостей?
```

**Challenge 3:** Test Business Logic
```csharp
// Напишите unit тесты для GameLogic:
// - Без Unity (чистый C# класс)
// - Без MonoBehaviour
// - Без SceneManager, PlayerPrefs, etc.
// Все зависимости через интерфейсы (DI)
```

**Mini-Project (3 часа):** Refactor вашего старого проекта
- Возьмите FlappyBird или Snake
- Рефакторите на слои (Presentation / Logic / Data)
- Business logic в pure C# (можно тестировать без Unity!)
- Добавьте unit тесты

**AI Code Review:**
- Dependency direction (только вниз)
- Business logic НЕ зависит от Unity API
- Clear separation of concerns

---

## Phase 3: Advanced Architecture (Week 11-12)

### Week 11: ECS Introduction (Unity DOTS)

**Problem:** ООП плохо для performance-critical систем (1000+ entities)

**Theory (3 часа):**
```csharp
// OLD (OOP)
public class Enemy : MonoBehaviour {
    public int Health;
    public float Speed;

    void Update() {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }
}
// 1000 enemies = 1000 Update calls, cache misses, poor performance

// NEW (ECS)
public struct HealthComponent : IComponentData {
    public int Value;
}

public struct SpeedComponent : IComponentData {
    public float Value;
}

public class MovementSystem : SystemBase {
    protected override void OnUpdate() {
        Entities.ForEach((ref Translation pos, in SpeedComponent speed) => {
            pos.Value += math.forward() * speed.Value * deltaTime;
        }).ScheduleParallel(); // 1000 entities parallel!
    }
}
```

**AI объясняет:**
- Entity (ID), Component (Data), System (Logic)
- Data-Oriented Design (cache-friendly)
- Job System (multi-threading)
- Burst Compiler (SIMD)
- Когда ECS (много entities), когда НЕТ (обычный геймплей)

**Practice (5 часов):**

**Challenge 1:** Simple ECS
```csharp
// Создайте простой ECS framework (НЕ используйте Unity DOTS):
// - Entity (int ID)
// - Component (interface IComponent)
// - ComponentManager (Dictionary<Type, Dictionary<int, IComponent>>)
// - System (abstract class, Update())
// Пример: MovementSystem обрабатывает все entities с Position + Velocity
```

**Challenge 2:** Query System
```csharp
// Добавьте Query API:
// - GetEntities<T1, T2>() → все entities с компонентами T1 И T2
// - GetEntitiesWithAny<T1, T2>() → T1 ИЛИ T2
// - GetEntitiesWithout<T>() → без компонента T
```

**Mini-Project (3 часа):** Particle System на ECS
- 10,000 particles
- Components: Position, Velocity, Lifetime, Color
- Systems: MovementSystem, LifetimeSystem, RenderSystem
- Цель: 60 FPS

**AI Code Review:**
- Cache-friendly data layout
- No per-entity overhead (как в OOP)
- Systems независимы (можно распараллелить)

---

### Week 12: Capstone Project

**Задача:** Создать игру с применением ВСЕХ техник

**Requirements:**
- **ObjectPool** для часто создаваемых объектов
- **EventBus** для связи систем
- **State Machine** для AI
- **Command Pattern** для Undo/Redo
- **Factory** для procedural generation
- **Observer** для reactive UI
- **Immutable State** для game logic
- **DI** для зависимостей
- **Layered Architecture** (Presentation / Logic / Data)
- **Unit Tests** (80%+ coverage для logic layer)

**Примеры проектов:**
1. **Tower Defense** (waves, towers, enemies, upgrades)
2. **Turn-Based Tactics** (grid, units, abilities, undo/redo)
3. **Roguelike** (procedural levels, permadeath, runs)

**Deliverables:**
- Working game (playable build)
- Architecture documentation (ADR - Architecture Decision Records)
- Unit tests
- Code review by AI

**AI Code Review (comprehensive):**
- Architecture (правильное применение паттернов)
- Code Quality (clean code, SOLID)
- Performance (profiling results)
- Testability (тесты покрывают logic)

**Milestone:** ✅ **Уровень: Senior-Ready Unity Developer**

---

## How to Work with AI (Qwen)

### Prompt Templates:

**Теория:**
```
"Qwen, объясни мне [Pattern Name] для Unity.
- Какую проблему решает?
- Когда использовать, когда НЕТ?
- Покажи real-world пример из игр
- Trade-offs (плюсы и минусы)"
```

**Практика:**
```
"Qwen, дай мне проект для практики [Pattern].
Требования: [описание]
Проверяй мой код по мере написания и задавай вопросы"
```

**Code Review:**
```
"Qwen, сделай архитектурный code review:
[вставить код]
Проверь:
- Правильно ли применен паттерн?
- Нет ли нарушений SOLID?
- Легко ли тестировать?
- Как улучшить?"
```

**Socratic Method:**
```
"Qwen, я создал EventBus. Задай мне 5 вопросов на понимание:
- Почему я выбрал такую структуру?
- Какие edge cases я не учел?
- Какие проблемы могут возникнуть при scale?"
```

---

## Resources

**Books:**
- "Design Patterns" (GoF) - библия паттернов
- "Game Programming Patterns" by Robert Nystrom (MUST READ!)
- "Clean Architecture" by Robert Martin

**Websites:**
- gameprogrammingpatterns.com (бесплатно!)
- refactoring.guru (паттерны с картинками)

**Unity-Specific:**
- Unity DOTS documentation
- Zenject / VContainer (DI containers)
- UniRx (Reactive Extensions)

---

**Next Step:** ➡️ Build real projects! (Phase 3 of Global Roadmap)
