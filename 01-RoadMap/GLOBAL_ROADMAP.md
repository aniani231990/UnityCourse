# Global Learning Roadmap: Junior+ → Senior Unity Developer

**Студент:** aniani231990
**Входной уровень:** Junior+ (Unity basics, простые игры, теория паттернов)
**Целевой уровень:** Middle/Senior Unity Developer
**Срок:** 12-24 месяца
**Интенсивность:** 2 часа/день (14 часов/неделя)

---

## Текущий уровень (ваши сильные стороны)

✅ **Технические навыки:**
- Python (Data Analysis) - понимание ООП, работа с данными
- Unity Editor - навигация, Inspector, Prefabs, ScriptableObjects
- C# basics - классы, методы, базовый синтаксис
- Практика - FlappyBird, Snake (полный цикл разработки)

✅ **Теоретические знания:**
- SOLID принципы (теория)
- Паттерны: ObjectPool, EventBus (теория)
- Unity lifecycle (Awake, Start, Update)

🎯 **Зона роста:**
- C# продвинутые фичи (generics, delegates, LINQ, records)
- Применение паттернов на практике
- Проектирование архитектуры ДО кодинга
- Clean Code practices
- Unit-тестирование

---

## Roadmap Overview (3 фазы x 6-8 месяцев)

```
Phase 1: Foundation Deep Dive (6 месяцев)
├─ Month 1-2: C# Advanced (продвинутые фичи)
├─ Month 3-4: Unity Patterns (практика паттернов)
└─ Month 5-6: Clean Code + Testing

Phase 2: Architecture Thinking (6 месяцев)
├─ Month 7-8: Design Patterns (GoF + Game-specific)
├─ Month 9-10: System Design (проектирование систем)
└─ Month 11-12: Production Skills (CI/CD, Performance)

Phase 3: Senior Mastery (6-12 месяцев)
├─ Month 13-15: Advanced Architecture (CQRS, ECS, DDD)
├─ Month 16-18: Multiplayer (Netcode, PurrNet)
└─ Month 19-24: Portfolio + Real Projects
```

---

## Phase 1: Foundation Deep Dive (Месяц 1-6)

**Цель:** Перейти от "делаю по примеру" к "понимаю, почему так"

### Month 1-2: C# Advanced Features

**Что изучаем:**
- Delegates, Events, Actions, Funcs
- Generics (constraints, variance)
- LINQ (IEnumerable, lazy evaluation)
- Async/Await (Task, async void vs async Task)
- Records, Structs, readonly structs
- Pattern Matching (switch expressions, is, as)
- Nullability (?, ??, ??=, null-forgiving operator)

**Практика:**
- Проект: EventBus с generics
- Проект: State Machine с delegates
- Проект: Data pipeline с LINQ

**Результат:**
- ✅ Читаете продвинутый C# код без словаря
- ✅ Используете LINQ вместо циклов
- ✅ Понимаете, когда struct, когда class, когда record

**Время:** 60 часов (2 часа/день x 30 дней)

---

### Month 3-4: Unity Patterns in Practice

**Что изучаем:**
- Object Pooling (реализация generic pool)
- EventBus (typed events, subscription management)
- State Pattern (FSM for AI, UI, Game States)
- Command Pattern (Undo/Redo system)
- Observer Pattern (reactive UI)
- Factory Pattern (procedural generation)
- Dependency Injection (Zenject basics)

**Практика:**
- Проект 1: Top-Down Shooter (ObjectPool для пуль, EventBus для UI)
- Проект 2: Turn-Based Tactics (Command Pattern для ходов)
- Проект 3: Platformer с AI (State Machine для врагов)

**Результат:**
- ✅ Применяете паттерны осознанно (знаете, КОГДА какой нужен)
- ✅ Рефакторите "плохой" код в "хороший"
- ✅ Объясняете trade-offs каждого паттерна

**Время:** 60 часов (2 часа/день x 30 дней)

---

### Month 5-6: Clean Code + Unit Testing

**Что изучаем:**
- Clean Code principles (naming, functions, comments)
- SOLID на практике (разбор примеров)
- Unit Testing (NUnit, Unity Test Framework)
- TDD (Test-Driven Development)
- Refactoring techniques (Extract Method, Replace Conditional with Polymorphism)
- Code Smells (God Object, Feature Envy, Primitive Obsession)

**Практика:**
- Проект 1: Рефакторинг legacy кода (намеренно плохой → чистый)
- Проект 2: TDD для игровой логики (tests first, code second)
- Проект 3: Code Review вашего старого кода (FlappyBird, Snake)

**Результат:**
- ✅ Пишете тесты для игровой логики
- ✅ Рефакторите без страха сломать
- ✅ Код читается как статья (self-documenting)

**Время:** 60 часов (2 часа/день x 30 дней)

**Milestone:** ✅ **Уровень: Solid Middle Developer**

---

## Phase 2: Architecture Thinking (Месяц 7-12)

**Цель:** Проектировать системы, а не классы

### Month 7-8: Design Patterns Deep Dive

**Что изучаем:**
- Creational: Singleton, Factory, Builder, Prototype
- Structural: Adapter, Facade, Composite, Flyweight
- Behavioral: Strategy, Template Method, Iterator, Mediator
- Game-Specific: Update Method, Component, Service Locator

**Практика:**
- Проект 1: RPG Inventory System (Composite для items, Flyweight для data)
- Проект 2: Dialogue System (Builder для conversations, Mediator для UI)
- Проект 3: Save/Load System (Memento для state, Prototype для cloning)

**Результат:**
- ✅ Узнаете паттерны в чужом коде (Unity, open-source)
- ✅ Выбираете паттерн под задачу (не "вбиваете гвозди микроскопом")
- ✅ Комбинируете паттерны (Factory + ObjectPool + EventBus)

**Время:** 60 часов

---

### Month 9-10: System Design

**Что изучаем:**
- Architectural Patterns (MVC, MVP, MVVM, ECS)
- Immutable State (functional approach)
- Event-Driven Architecture (pub/sub, message queues)
- Separation of Concerns (layers, modules)
- Data Flow (unidirectional, reactive)
- Dependency Management (IoC containers, Service Locator)

**Практика:**
- Проект 1: Card Game (Intent Pattern + Immutable State - как ваш BlackJack!)
- Проект 2: Tower Defense (ECS-like architecture)
- Проект 3: RTS (Command Queue + Event Sourcing)

**Результат:**
- ✅ Проектируете архитектуру ДО кодинга (ADR documents)
- ✅ Обосновываете выбор (trade-offs analysis)
- ✅ Понимаете код вашего BlackJack проекта (Intent Pattern)

**Время:** 60 часов

---

### Month 11-12: Production Skills

**Что изучаем:**
- Unity Profiler (CPU, Memory, Rendering)
- Performance optimization (GC, boxing, string allocation)
- CI/CD для Unity (GitHub Actions, Unity Cloud Build)
- Logging/Debugging strategies
- Error Handling (Result<T>, Either, Railway-Oriented Programming)
- Documentation (Architecture Decision Records, API docs)

**Практика:**
- Проект 1: Профилирование вашей игры (60 FPS на мобильном)
- Проект 2: Setup CI/CD pipeline (auto-build на GitHub)
- Проект 3: Добавить тесты в BlackJack проект

**Результат:**
- ✅ Код готов к production (не только "работает")
- ✅ Оптимизируете bottlenecks (не "преждевременно")
- ✅ Автоматизируете рутину (testing, building)

**Время:** 60 часов

**Milestone:** ✅ **Уровень: Senior-Ready**

---

## Phase 3: Senior Mastery (Месяц 13-24)

**Цель:** Стать экспертом в конкретных областях

### Month 13-15: Advanced Architecture

**Что изучаем:**
- ECS (Entity Component System) - Unity DOTS
- CQRS (Command Query Responsibility Segregation)
- DDD (Domain-Driven Design) для игр
- Reactive Programming (UniRx)
- Functional Programming в C# (immutability, monads)

**Практика:**
- Проект: DOTS-based simulation (1000+ entities)
- Проект: Event Sourcing для multiplayer
- Проект: DDD для complex game logic

---

### Month 16-18: Multiplayer

**Что изучаем:**
- Authoritative Server architecture
- Client Prediction + Server Reconciliation
- Lag Compensation techniques
- PurrNet / Unity Netcode
- Serialization (protobuf, flatbuffers)

**Практика:**
- Проект: Multiplayer BlackJack (ваша игра онлайн!)
- Проект: Real-time multiplayer shooter

---

### Month 19-24: Portfolio + Real Projects

**Что делаем:**
- Выпускаем 2-3 игры в Steam/Mobile
- Contribute в open-source Unity projects
- Пишем технические статьи (Medium, Dev.to)
- Участвуем в Game Jams (показываете скорость разработки)

**Milestone:** ✅ **Уровень: Senior Unity Developer**

---

## Метрики прогресса

### После Phase 1 (6 месяцев):
- [ ] Могу реализовать любой паттерн с нуля
- [ ] Пишу unit-тесты для игровой логики
- [ ] Рефакторю код без страха
- [ ] Код ревью: понимаю, что "хорошо" и "плохо"

### После Phase 2 (12 месяцев):
- [ ] Проектирую архитектуру игры ДО кодинга
- [ ] Обосновываю технические решения (trade-offs)
- [ ] Понимаю performance implications
- [ ] Setup CI/CD для проектов

### После Phase 3 (24 месяца):
- [ ] 2-3 игры в портфолио
- [ ] Эксперт в 1-2 областях (multiplayer / DOTS / DDD)
- [ ] Менторю джуниоров
- [ ] Пишу технические статьи

---

## Следующий шаг

➡️ Начните с [C# for Unity Plan](../02-CSharp-For-Unity/PLAN.md)

➡️ Прочитайте [AI Learning Guide](../05-AI-Learning-Guide/HOW_TO_LEARN.md)

---

**Важно:** Этот roadmap - не закон, а карта. Вы можете менять порядок, глубину, скорость. Главное - **consistent daily practice**.

**Помните:** Senior разработчик отличается не количеством знаний, а умением применять нужное знание в нужное время.
