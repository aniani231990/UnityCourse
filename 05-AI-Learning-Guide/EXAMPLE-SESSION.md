# Пример учебной сессии с ИИ-наставником

**Пример реального диалога для понимания, как должна выглядеть работа с ИИ.**

---

## 📖 Контекст

**Тема:** Module 01 - Delegates, Events, Actions (Week 1)
**Время:** ~2 часа
**Студент:** Новичок в delegates
**Цель:** Понять концепцию и научиться применять на практике

---

## 🎬 ФАЗА 1: ТЕОРИЯ (30-45 мин)

### Начало сессии

**Student:**
```
Привет! Начинаем Module 01: Delegates, Events, Actions

Следуй инструкции AI-MENTOR-INSTRUCTION.md:
1. Объясни теорию (проблема → решение → примеры)
2. Дай задачи для практики
3. Проверь понимание вопросами
4. Попроси объяснить обратно

Начинай с теории!
```

**AI (Mentor):**
```
Привет! Начинаем изучение Delegates!

📖 ПЛАН НА СЕГОДНЯ:
1. Теория: ~30 мин
2. Практика: ~60 мин (3 задачи)
3. Проверка: ~20 мин
4. Закрепление: ~15 мин

⏱️ Total: ~2 часа

Готов? Начнем с теории.

---

🎯 ПРОБЛЕМА

Представь: у тебя Health System в игре.
Когда HP игрока меняется, нужно:
- Обновить UI (healthBar)
- Проиграть звук (если урон)
- Сохранить в аналитику
- Проверить достижения
- Вызвать particle effect

Как бы ты это реализовал?

Попробуй написать псевдокод метод ChangeHealth(int newHealth).
```

**Student:**
```
Ну, наверное так:

void ChangeHealth(int newHealth) {
    health = newHealth;

    // Обновить UI
    healthBar.UpdateValue(health);

    // Проиграть звук
    soundManager.PlayHealthChangeSound();

    // Аналитика
    analytics.LogHealthChange(health);

    // Достижения
    achievementManager.CheckHealthAchievements(health);

    // Particle
    particleManager.PlayHealthEffect(health);
}
```

**AI (Mentor):**
```
Хорошее начало! Логика понятна.

Но теперь представь проблемы:

❌ ПРОБЛЕМА 1: Зависимости
HealthSystem знает про:
- healthBar
- soundManager
- analytics
- achievementManager
- particleManager

Если нужно добавить новую реакцию (например, вибрацию) → нужно изменять HealthSystem.
Это нарушает Open/Closed Principle (открыт для расширения, закрыт для изменений).

❌ ПРОБЛЕМА 2: Связанность
Что если в другом проекте нет achievementManager?
Придется удалять код из HealthSystem.

❌ ПРОБЛЕМА 3: Гибкость
Хочу включить/выключить звук → нужно комментировать код.
Хочу разный звук для разных ситуаций → усложнение логики.

Видишь проблемы?

Как думаешь, можно ли сделать так, чтобы HealthSystem НЕ знал,
кто реагирует на изменение HP?
```

**Student:**
```
Хм... может, через события как-то? Чтобы HealthSystem просто говорил
"HP изменилось", а кто хочет - тот слушает?
```

**AI (Mentor):**
```
ТОЧНО! Это правильная идея! 🎯

Это называется Observer Pattern.
И в C# для этого используются DELEGATES!

---

📚 ЧТО ТАКОЕ DELEGATE?

Delegate - это "переменная", которая хранит МЕТОД (а не данные).

Аналогия:
- int health = 100; // переменная хранит число
- string name = "Player"; // переменная хранит текст
- OnHealthChanged callback; // "переменная" хранит МЕТОД

Ты можешь:
1. Присвоить метод в delegate
2. Передать delegate как параметр
3. Вызвать метод через delegate

Пример:

```csharp
// 1. Объявляем тип delegate
public delegate void OnHealthChanged(int newHealth);

// 2. Создаем переменную этого типа
OnHealthChanged callback;

// 3. Присваиваем метод
callback = UpdateUI;

// 4. Вызываем
callback(50); // вызовет UpdateUI(50)

void UpdateUI(int health) {
    Debug.Log($"Health: {health}");
}
```

Понятна базовая идея?

Объясни своими словами:
1. Что такое delegate?
2. Чем отличается от обычной переменной?
```

**Student:**
```
Если я правильно понял:

1. Delegate - это как тип переменной, но вместо данных он хранит метод
2. Обычная переменная хранит значение (int, string), а delegate хранит
   ссылку на функцию, которую можно вызвать

Правильно?
```

**AI (Mentor):**
```
ПРАВИЛЬНО! 👍

Уточню: delegate хранит не саму функцию, а ССЫЛКУ на метод
(как указатель в C++, если знаком с этим).

Теперь важная часть - MULTICAST delegates:

```csharp
OnHealthChanged callback;

// Можно добавлять НЕСКОЛЬКО методов!
callback += UpdateUI;        // добавили 1-й метод
callback += PlaySound;       // добавили 2-й метод
callback += SendAnalytics;   // добавили 3-й метод

// Когда вызываем:
callback(50); // вызовутся ВСЕ 3 метода по порядку!
```

Это решает нашу проблему!

HealthSystem теперь:

```csharp
public class HealthSystem {
    public delegate void OnHealthChanged(int newHealth);
    public OnHealthChanged healthCallbacks; // любой может подписаться!

    private int health;

    public void ChangeHealth(int newHealth) {
        health = newHealth;
        healthCallbacks?.Invoke(newHealth); // вызвать всех подписчиков
    }
}

// UI подписывается
healthSystem.healthCallbacks += UpdateUI;

// Sound подписывается
healthSystem.healthCallbacks += PlaySound;

// Добавили новую фичу? Просто подпишись!
healthSystem.healthCallbacks += NewFeature;
```

Видишь разницу?

Вопросы:
1. Что делает `+=`?
2. Зачем `?.Invoke` вместо просто `Invoke`?
```

**Student:**
```
1. += добавляет метод в список callbacks?
2. ?. - это null check? Если никто не подписался, то callback null и крашнется?
```

**AI (Mentor):**
```
ОТЛИЧНО! Понял верно! 🎉

`+=` добавляет метод к delegate (multicast)
`?.` - null-conditional operator (если null, не вызовет, не крашнется)

---

Теперь про ACTION, FUNC, EVENT:

C# предоставляет готовые delegate типы:

1️⃣ ACTION (для void методов)

```csharp
// Вместо:
public delegate void OnHealthChanged(int newHealth);

// Можно:
public Action<int> OnHealthChanged; // Action<параметры>

// Action без параметров:
public Action OnPlayerDied; // void метод без параметров

// Action с несколькими параметрами:
public Action<int, float> OnDamageTaken; // void Method(int damage, float multiplier)
```

2️⃣ FUNC (для методов с return)

```csharp
// Метод с возвратом:
public Func<int, bool> CanAffordItem; // bool Method(int cost)

Func<int, int, int> AddNumbers; // int Method(int a, int b)
AddNumbers = (a, b) => a + b;
int result = AddNumbers(5, 3); // result = 8
```

3️⃣ EVENT (защищенный delegate)

```csharp
// Проблема с public delegate:
public Action OnPlayerDied;

// Кто-то может:
OnPlayerDied = null; // УДАЛИЛИ ВСЕ подписки! Баг!
OnPlayerDied?.Invoke(); // Чужой код вызвал событие! Баг!

// Решение - EVENT:
public event Action OnPlayerDied;

// Теперь можно только:
OnPlayerDied += SomeMethod; // подписаться
OnPlayerDied -= SomeMethod; // отписаться

// НЕЛЬЗЯ:
OnPlayerDied = null; // ERROR!
OnPlayerDied?.Invoke(); // ERROR! (только внутри класса)
```

---

📊 КОГДА ЧТО ИСПОЛЬЗОВАТЬ:

| Тип | Когда использовать | Пример |
|-----|-------------------|--------|
| delegate | Кастомная сигнатура | delegate void Custom(ref int a, out string b) |
| Action | void метод | Action<int> OnScoreChanged |
| Func | метод с return | Func<int, bool> CanAfford |
| event | Public events | event Action OnPlayerDied |

---

Понятно?

Объясни разницу между Action и Event.
Когда что использовать?
```

**Student:**
```
Разница:
- Action - это просто public delegate, его можно перезаписать (=) и вызвать
  откуда угодно
- Event - это protected версия, можно только += и -=, а вызвать только
  внутри класса

Использовать:
- Event - когда хочу публичное событие, но контролировать кто его вызывает
- Action - когда нужен callback, который может быть переназначен

Правильно?
```

**AI (Mentor):**
```
СОВЕРШЕННО ВЕРНО! 🎯

Добавлю нюанс:
- Event → для публичных событий класса (OnDied, OnScoreChanged)
- Action → для callbacks в параметрах методов или private полей

Пример:

```csharp
public class Enemy {
    // PUBLIC событие - используем event
    public event Action OnDied;

    // PRIVATE callback - можно Action
    private Action<int> damageCallback;

    // ПАРАМЕТР метода - используем Action/Func
    public void DoSomethingAsync(Action onComplete) {
        // ...
        onComplete?.Invoke();
    }
}
```

---

🎯 ВАЖНО: Memory Leaks!

Проблема:

```csharp
// UI подписался
healthSystem.OnHealthChanged += ui.UpdateHealth;

// UI уничтожен (Destroy)
// Но подписка осталась! MEMORY LEAK!
```

Решение:

```csharp
void OnEnable() {
    healthSystem.OnHealthChanged += UpdateHealth; // подписка
}

void OnDisable() {
    healthSystem.OnHealthChanged -= UpdateHealth; // ОТПИСКА!
}
```

Правило: Где += там должен быть -=

---

Теперь проверю понимание.

Ответь на вопросы:
1. Зачем нужны delegates? (Какую проблему решают?)
2. В чем разница delegate vs Action vs Func vs Event?
3. Что случится, если забыть -= при OnDisable?
4. Когда использовать event, а когда Action?

Отвечай по порядку.
```

**Student:**
```
1. Delegates нужны для того, чтобы один класс мог уведомить другие классы
   о событии, не зная про них. Решает проблему tight coupling.

2. delegate - базовый тип, можешь задать любую сигнатуру
   Action - готовый delegate для void методов
   Func - готовый delegate для методов с return
   Event - protected версия delegate, контролирует кто вызывает

3. Если забыть -=, то подписка останется даже после Destroy объекта,
   и когда событие вызовется, будет пытаться вызвать метод уничтоженного
   объекта. Это memory leak и возможный краш.

4. Event использовать для публичных событий класса (OnDied, OnDamaged)
   Action использовать для callbacks в параметрах или приватных полей
```

**AI (Mentor):**
```
ПРЕВОСХОДНО! 🎉

Понимание 100%!

Маленькое уточнение к пункту 3:
Не просто memory leak - объект НЕ будет garbage collected,
потому что на него есть ссылка через delegate.
В Unity это видно в Profiler - "leaked" objects.

---

✅ ТЕОРИЯ ЗАВЕРШЕНА

Понимание: 5/5 ⭐⭐⭐⭐⭐

Готов к практике?
Дам 3 задачи:
🟢 Легкая (разогрев)
🟡 Средняя (основная)
🔴 Сложная (challenge)

Начнем?
```

---

## 💻 ФАЗА 2: ПРАКТИКА (60-90 мин)

**Student:**
```
Готов! Давай задачи!
```

**AI (Mentor):**
```
---

🟢 ЗАДАЧА 1: SIMPLE HEALTH SYSTEM (Легкая)

Создай Health систему с событиями.

Требования:
1. Класс Health
2. События (используй event Action):
   - OnDamaged (int damage)
   - OnHealed (int amount)
   - OnDied ()
3. Методы:
   - TakeDamage(int damage)
   - Heal(int amount)
4. Текущий health должен быть readonly (можно менять только через методы)

Пример использования:

```csharp
var health = new Health(100);

health.OnDamaged += (damage) => Debug.Log($"Took {damage} damage!");
health.OnHealed += (amount) => Debug.Log($"Healed {amount}");
health.OnDied += () => Debug.Log("Player died!");

health.TakeDamage(30); // "Took 30 damage!"
health.Heal(10);       // "Healed 10"
health.TakeDamage(80); // "Took 80 damage!" → "Player died!"
```

⚠️ Подумай:
- Что если damage > current health?
- Что если heal когда health = 0 (мертв)?
- Можно ли "перехилить" (больше maxHealth)?

Начинай писать код! Показывай частями, буду проверять.
```

**Student:**
```
Начинаю:

public class Health {
    public event Action<int> OnDamaged;
    public event Action<int> OnHealed;
    public event Action OnDied;

    private int currentHealth;
    private int maxHealth;

    public int CurrentHealth => currentHealth;
    public bool IsDead => currentHealth <= 0;

    public Health(int maxHealth) {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }

    public void TakeDamage(int damage) {
        if (IsDead) return; // уже мертв

        currentHealth -= damage;
        OnDamaged?.Invoke(damage);

        if (currentHealth <= 0) {
            currentHealth = 0;
            OnDied?.Invoke();
        }
    }

    public void Heal(int amount) {
        if (IsDead) return; // не воскрешаем

        currentHealth += amount;
        if (currentHealth > maxHealth) {
            currentHealth = maxHealth; // не перехиливаем
        }

        OnHealed?.Invoke(amount);
    }
}
```

**AI (Mentor):**
```
ОТЛИЧНО! 🎉

✅ Что хорошо:
- События используешь правильно (event Action)
- Проверка IsDead перед Heal/TakeDamage
- Ограничение maxHealth при heal
- Клампинг currentHealth в 0 при смерти
- Использование ?.Invoke (null-safe)

💡 Вопросы на понимание:

1. Почему ты используешь `event Action<int>`, а не просто `Action<int>`?

2. Строка `OnDamaged?.Invoke(damage)`:
   Что произойдет, если никто не подписался? Правильно ли это?

3. Edge case: Что если damage отрицательное?

   health.TakeDamage(-50); // баг или фича?

4. Порядок событий: ты вызываешь OnDamaged, потом OnDied.
   Это правильный порядок? Почему?

Отвечай по порядку!
```

**Student:**
```
1. event Action, чтобы защитить от случайной перезаписи (=) или вызова
   извне класса. Только класс Health должен контролировать когда вызывать.

2. Если никто не подписался, ?.Invoke ничего не сделает (null check).
   Это правильно, потому что события optional - может и не быть слушателей.

3. Отрицательный damage - это баг. Надо добавить проверку?

4. Порядок правильный: сначала OnDamaged (урон получен), потом OnDied
   (если умер). Логично - сначала реакция на урон, потом на смерть.
```

**AI (Mentor):**
```
ПРАВИЛЬНО на все вопросы! 🎯

К пункту 3:
Да, стоит добавить валидацию:

```csharp
public void TakeDamage(int damage) {
    if (damage < 0) {
        Debug.LogWarning("Damage cannot be negative!");
        return;
    }
    // ... rest
}
```

Или использовать exception:

```csharp
if (damage < 0) throw new ArgumentException("Damage cannot be negative");
```

Что лучше: Warning или Exception?
Подумай: это программерская ошибка или возможная ситуация в игре?
```

**Student:**
```
Это программерская ошибка - TakeDamage с отрицательным значением
не должно вызываться вообще. Значит лучше exception, чтобы сразу
увидеть баг при разработке.

Для Heal тоже нужна проверка amount >= 0?
```

**AI (Mentor):**
```
ТОЧНО! 👍

Если это programmer error → Exception (fail fast)
Если это game logic → validation + return/warning

Да, для Heal тоже нужна проверка.

---

✅ ЗАДАЧА 1 ЗАВЕРШЕНА

Оценка:
- Логика: 5/5 ⭐
- Edge cases: 4/5 ⭐ (забыли валидацию, но сами нашли)
- Clean Code: 5/5 ⭐

Отлично! Переходим к задаче 2.

---

🟡 ЗАДАЧА 2: EVENT BUS (Средняя)

Создай EventBus - систему для глобальных событий.

Требования:
1. Класс EventBus (Singleton)
2. Методы:
   - Subscribe<T>(Action<T> handler) where T : IGameEvent
   - Unsubscribe<T>(Action<T> handler)
   - Publish<T>(T eventData) where T : IGameEvent
   - Clear() - очистить все подписки

3. IGameEvent - пустой интерфейс-маркер для событий

Пример использования:

```csharp
// Определяем события
public struct EnemyKilledEvent : IGameEvent {
    public int Score;
}

public struct PlayerDiedEvent : IGameEvent {
}

// Подписка
EventBus.Instance.Subscribe<EnemyKilledEvent>(OnEnemyKilled);
EventBus.Instance.Subscribe<PlayerDiedEvent>(OnPlayerDied);

// Публикация
EventBus.Instance.Publish(new EnemyKilledEvent { Score = 100 });

void OnEnemyKilled(EnemyKilledEvent evt) {
    Debug.Log($"Enemy killed! Score: {evt.Score}");
}
```

⚠️ Подумай:
- Как хранить handlers для разных типов событий?
- Что если Subscribe вызван дважды для одного handler?
- Что если Unsubscribe для handler, которого нет?
- Как реализовать Singleton?

Сначала продумай структуру данных.
Какую коллекцию будешь использовать?
```

**Student:**
```
Хм... нужно хранить handlers для разных типов событий.

Dictionary<Type, List<Delegate>> ?

Type - это тип события (EnemyKilledEvent, PlayerDiedEvent)
List<Delegate> - список handlers для этого типа

Правильно?
```

**AI (Mentor):**
```
ПРАВИЛЬНАЯ МЫСЛЬ! 👍

Dictionary<Type, List<Delegate>> - хорошая основа.

Но вопрос:
Action<EnemyKilledEvent> и Action<PlayerDiedEvent> - это разные типы.
Можно ли их хранить в одном List<Delegate>?

Ответ: ДА! Action<T> наследуется от Delegate.

Но будет проблема при вызове:

```csharp
List<Delegate> handlers = ...;
foreach (var handler in handlers) {
    handler.Invoke(eventData); // ERROR! Delegate не имеет типизированного Invoke
}
```

Придется кастить:

```csharp
foreach (var handler in handlers) {
    ((Action<T>)handler).Invoke(eventData); // но мы не знаем T здесь!
}
```

Подумай: как решить эту проблему?

Подсказка: можно ли хранить `object` вместо `List<Delegate>`?
```

**Student:**
```
Ааа, понял!

Можно Dictionary<Type, object>?

И при Subscribe:
- Получить object
- Кастить в List<Delegate>
- Добавить handler

При Publish:
- Получить object
- Кастить в List<Action<T>>
- Вызвать каждый

Но это не очень type-safe...

Может, лучше Dictionary<Type, List<object>> и кастить при вызове?
```

**AI (Mentor):**
```
Ты на правильном пути!

Оба варианта работают, но есть более элегантное решение.

Подсказка: можно использовать вложенный Dictionary:

```csharp
Dictionary<Type, List<Delegate>>
```

И при Publish делать так:

```csharp
public void Publish<T>(T eventData) where T : IGameEvent {
    var eventType = typeof(T);

    if (events.TryGetValue(eventType, out var handlers)) {
        foreach (var handler in handlers) {
            // Мы ЗНАЕМ, что handler это Action<T>
            // потому что Subscribe<T> добавил его для типа T
            ((Action<T>)handler).Invoke(eventData);
        }
    }
}
```

Это safe, потому что Subscribe<T> гарантирует,
что в handlers для типа T только Action<T>.

Понятна логика?

Попробуй реализовать Subscribe метод с этой структурой.
```

**Student:**
```
Попробую:

```csharp
public interface IGameEvent { }

public class EventBus {
    private static EventBus instance;
    public static EventBus Instance => instance ?? (instance = new EventBus());

    private Dictionary<Type, List<Delegate>> events = new Dictionary<Type, List<Delegate>>();

    public void Subscribe<T>(Action<T> handler) where T : IGameEvent {
        var eventType = typeof(T);

        if (!events.ContainsKey(eventType)) {
            events[eventType] = new List<Delegate>();
        }

        events[eventType].Add(handler);
    }

    public void Unsubscribe<T>(Action<T> handler) where T : IGameEvent {
        var eventType = typeof(T);

        if (events.ContainsKey(eventType)) {
            events[eventType].Remove(handler);
        }
    }

    public void Publish<T>(T eventData) where T : IGameEvent {
        var eventType = typeof(T);

        if (events.TryGetValue(eventType, out var handlers)) {
            foreach (var handler in handlers) {
                ((Action<T>)handler).Invoke(eventData);
            }
        }
    }

    public void Clear() {
        events.Clear();
    }
}
```
```

**AI (Mentor):**
```
ОТЛИЧНО! 🎉 Работает!

✅ Что хорошо:
- Singleton реализован правильно
- Subscribe/Unsubscribe/Publish логика верная
- Используешь generic constraints (where T : IGameEvent)
- TryGetValue для безопасного доступа

🤔 Но есть вопросы:

1. Subscribe дважды для одного handler:

   EventBus.Subscribe<MyEvent>(OnEvent);
   EventBus.Subscribe<MyEvent>(OnEvent); // добавится дважды!

   Publish вызовет OnEvent дважды. Это баг или фича?

2. Что если подписчик выбросит exception?

   handler1 → OK
   handler2 → EXCEPTION
   handler3 → НЕ ВЫЗОВЕТСЯ! (foreach прервется)

3. Unsubscribe во время Publish:

   ```csharp
   void OnEvent(MyEvent evt) {
       EventBus.Unsubscribe<MyEvent>(OnEvent); // unsubscribe себя!
   }
   ```

   Это изменит List во время foreach! Возможен краш.

4. Thread safety: что если Subscribe/Publish из разных потоков?

Какие из этих проблем нужно решать?
Что критично, а что optional?
```

**Student:**
```
1. Двойная подписка - это баг. Нужно проверять Contains перед Add.

2. Exception в handler - критичная проблема. Нужен try-catch вокруг Invoke,
   чтобы один сломанный handler не убил остальные.

3. Unsubscribe во время Publish - это опасно. Может краш.
   Как решить? Может, копировать список перед foreach?

4. Thread safety - зависит от Unity. В Unity большинство API работает
   только в main thread, так что может не критично?
```

**AI (Mentor):**
```
ПРАВИЛЬНЫЙ АНАЛИЗ! 🎯

1. ДА - добавить проверку:
```csharp
if (!events[eventType].Contains(handler)) {
    events[eventType].Add(handler);
}
```

2. ДА - критично:
```csharp
foreach (var handler in handlers) {
    try {
        ((Action<T>)handler).Invoke(eventData);
    } catch (Exception ex) {
        Debug.LogError($"Error in event handler: {ex}");
    }
}
```

3. ДА - копировать список:
```csharp
// Копия списка, чтобы безопасно итерировать
var handlersCopy = handlers.ToList();
foreach (var handler in handlersCopy) {
    ...
}
```

4. Для Unity обычно не критично (main thread only).
   Но если используешь async/await - может быть проблема.

---

Доработай код с этими фиксами.
Покажи финальную версию Subscribe и Publish.
```

**Student:**
```
Финальная версия:

```csharp
public void Subscribe<T>(Action<T> handler) where T : IGameEvent {
    var eventType = typeof(T);

    if (!events.ContainsKey(eventType)) {
        events[eventType] = new List<Delegate>();
    }

    // Проверка двойной подписки
    if (!events[eventType].Contains(handler)) {
        events[eventType].Add(handler);
    }
}

public void Publish<T>(T eventData) where T : IGameEvent {
    var eventType = typeof(T);

    if (events.TryGetValue(eventType, out var handlers)) {
        // Копия для безопасной итерации
        var handlersCopy = handlers.ToList();

        foreach (var handler in handlersCopy) {
            try {
                ((Action<T>)handler).Invoke(eventData);
            } catch (Exception ex) {
                Debug.LogError($"Error in event handler for {eventType.Name}: {ex}");
            }
        }
    }
}
```
```

**AI (Mentor):**
```
PERFECT! 🎉🎉🎉

Production-ready код!

✅ ЗАДАЧА 2 ЗАВЕРШЕНА

Оценка:
- Архитектура: 5/5 ⭐
- Edge cases: 5/5 ⭐
- Error handling: 5/5 ⭐
- Clean Code: 5/5 ⭐

---

У нас есть время на Задачу 3 (сложная)?
Или сначала сделаем проверку понимания?

Что предпочитаешь?
```

---

## ✅ ФАЗА 3: ПРОВЕРКА ПОНИМАНИЯ (20 мин)

**Student:**
```
Давай сначала проверку понимания, потом задачу 3 если останется время.
```

**AI (Mentor):**
```
Хорошее решение! Проверяем понимание.

---

📝 ПРОВЕРКА ПОНИМАНИЯ: DELEGATES

Буду задавать вопросы от простых к сложным.

---

УРОВЕНЬ 1: Знание (Remember)

1. Что такое delegate?
2. Какая разница между Action и Func?
3. Зачем нужен event keyword?

Отвечай кратко.
```

**Student:**
```
1. Delegate - это тип, который может хранить ссылку на метод
2. Action - для void методов, Func - для методов с return
3. event защищает delegate от перезаписи (=) и вызова извне класса
```

**AI (Mentor):**
```
✅ ПРАВИЛЬНО!

---

УРОВЕНЬ 2: Понимание (Understand)

4. Объясни своими словами, почему delegates решают проблему tight coupling.

5. Что произойдет в этом коде:
```csharp
Action callback = null;
callback += Method1;
callback += Method2;
callback();
```

6. В чем разница между:
```csharp
public Action OnClick;      // вариант A
public event Action OnClick; // вариант B
```
Когда использовать A, когда B?
```

**Student:**
```
4. Delegates позволяют одному классу уведомить другие, не зная про них.
   Класс просто вызывает callback, а кто подписался - те и получат уведомление.
   Нет прямой зависимости между классами.

5. callback изначально null, но += создаст новый multicast delegate с
   Method1, потом добавит Method2. callback() вызовет оба метода по порядку.

6. Вариант A - public Action, можно перезаписать (=) и вызвать извне.
   Вариант B - event, можно только += и -= извне, вызвать только внутри класса.

   A использовать для callbacks (параметры методов, private поля)
   B использовать для публичных событий класса
```

**AI (Mentor):**
```
✅ ВСЕ ПРАВИЛЬНО!

Уточнение к п.5: callback += Method1 НЕ создаст новый delegate, а изменит
существующий (который был null). После += callback уже не null, это delegate
содержащий Method1.

---

УРОВЕНЬ 3: Применение (Apply)

7. У тебя есть класс Button в Unity. Как бы ты реализовал onClick event
   используя delegates? Напиши код (5-10 строк).

8. В каком месте MonoBehaviour lifecycle ты бы подписывался на события?
   А где отписывался? Почему?

9. Приведи пример из реальной игры, где delegates критически важны.
```

**Student:**
```
7. Button:
```csharp
public class Button : MonoBehaviour {
    public event Action OnClick;

    void OnMouseDown() {
        OnClick?.Invoke();
    }
}
```

8. Подписка в OnEnable(), отписка в OnDisable().
   Потому что OnEnable вызывается каждый раз когда объект активируется,
   OnDisable - когда деактивируется. Это гарантирует, что подписка есть
   только пока объект активен.

9. UI система - когда кнопка нажата, нужно уведомить разные системы
   (звук, анимация, игровая логика) без того, чтобы Button знал о них всех.
```

**AI (Mentor):**
```
✅ ОТЛИЧНО!

К п.7: Код работает! Можно улучшить:
```csharp
void OnMouseDown() {
    OnClick?.Invoke(); // если null - не крашнется
}
```

---

УРОВЕНЬ 4: Анализ (Analyze)

10. Найди проблемы в этом коде:

```csharp
public class GameManager : MonoBehaviour {
    public event Action<int> OnScoreChanged;

    void Start() {
        OnScoreChanged += UpdateUI;
    }

    void UpdateScore(int newScore) {
        OnScoreChanged(newScore); // ПРОБЛЕМА 1?
    }
}

public class UIManager : MonoBehaviour {
    void Start() {
        FindObjectOfType<GameManager>().OnScoreChanged += OnScoreUpdate;
    }

    void OnScoreUpdate(int score) {
        // ... // ПРОБЛЕМА 2?
    }
}
```

Укажи минимум 2 проблемы и как исправить.
```

**Student:**
```
Проблема 1: OnScoreChanged(newScore) может крашнуться если null.
Исправить: OnScoreChanged?.Invoke(newScore);

Проблема 2: UIManager подписывается в Start, но нигде не отписывается.
Если UIManager уничтожен, подписка останется (memory leak).
Исправить: отписаться в OnDisable:

```csharp
GameManager gameManager;

void Start() {
    gameManager = FindObjectOfType<GameManager>();
    gameManager.OnScoreChanged += OnScoreUpdate;
}

void OnDisable() {
    if (gameManager != null) {
        gameManager.OnScoreChanged -= OnScoreUpdate;
    }
}
```

Еще проблема: FindObjectOfType<GameManager>() каждый кадр искать не стоит,
лучше сохранить ссылку.
```

**AI (Mentor):**
```
PERFECT ANALYSIS! 🎉🎉🎉

Нашел все проблемы:
✅ Null check для Invoke
✅ Memory leak (нет отписки)
✅ Performance (кешировать FindObjectOfType)

Добавлю еще одну проблему (advanced):

ПРОБЛЕМА 3: Start execution order
Что если UIManager.Start() выполнится раньше GameManager.Start()?
UIManager попытается подписаться, но GameManager еще не готов.

Решение: использовать Awake для инициализации GameManager.

---

УРОВЕНЬ 5: Evaluate (Оценка)

11. Твой junior коллега написал:

```csharp
public static event Action<int> OnGlobalScoreChanged;
```

Это static event. Он говорит "так удобно, не нужно искать GameManager".

Согласен ли ты? Какие проблемы могут возникнуть?
```

**Student:**
```
НЕ согласен.

Проблемы:
1. Static events сложнее отследить - кто подписался неизвестно
2. Живут всю программу - memory leaks если забыли отписаться
3. При переходе между сценами подписки остаются (может быть баг)
4. Сложно тестировать - static state shared между тестами
5. Нарушает принцип "explicit dependencies" - неясно кто от кого зависит

Использовать только если действительно нужно глобальное событие
(например, Application.OnQuit).

Лучше использовать Singleton EventBus вместо static events.
```

**AI (Mentor):**
```
ИСКЛЮЧИТЕЛЬНЫЙ ОТВЕТ! 🎯🎯🎯

Ты понимаешь не только КАК работают delegates,
но и КОГДА их использовать, а когда НЕТ.

Это уровень Middle Developer! 👏

---

✅ ПРОВЕРКА ПОНИМАНИЯ ЗАВЕРШЕНА

Оценка по уровням:
- Remember (знание): 5/5 ⭐
- Understand (понимание): 5/5 ⭐
- Apply (применение): 5/5 ⭐
- Analyze (анализ): 5/5 ⭐
- Evaluate (оценка): 5/5 ⭐

📊 ОБЩИЙ УРОВЕНЬ: Middle Developer understanding

---

У нас ~20 минут. Хочешь:
A) Задача 3 (сложная, может не хватить времени)
B) Feynman Technique (объясни мне тему обратно)
C) Закончим на сегодня

Что выбираешь?
```

---

## 🎓 ФАЗА 4: ЗАКРЕПЛЕНИЕ (15 мин)

**Student:**
```
Давай B - объясню тебе
```

**AI (Mentor):**
```
Отлично! Feynman Technique.

Я буду играть роль новичка, который:
- Только начал учить Unity
- Знает C# basics (переменные, методы, классы)
- НЕ знает что такое delegates

Твоя задача: объяснить мне delegates так, чтобы я понял.

Можешь использовать:
- Аналогии из жизни
- Простые примеры кода
- Визуальные объяснения (опиши схему)

Я буду задавать вопросы новичка.

Готов? Начинай объяснять!
```

**Student:**
```
Хорошо. Представь, что delegate - это как подписка на YouTube канал.

Когда ты подписываешься на канал, ты говоришь YouTube:
"Когда выйдет новое видео, уведоми меня!"

YouTube не знает, кто ты, сколько тебе лет, где живешь.
Он просто знает, что нужно отправить тебе уведомление.

То же самое с delegates в C#:

Есть класс HealthSystem (это как YouTube канал).
Когда HP меняется, он "публикует новое видео" (вызывает event).

Есть класс UIManager (это как зритель).
Он "подписывается" на событие OnHealthChanged.

Когда HealthSystem меняет HP, он вызывает OnHealthChanged?.Invoke(),
и все подписчики получают уведомление.

В коде:

```csharp
// HealthSystem = YouTube канал
public class HealthSystem {
    public event Action<int> OnHealthChanged; // подписка

    void ChangeHealth(int newHealth) {
        health = newHealth;
        OnHealthChanged?.Invoke(newHealth); // опубликовать
    }
}

// UIManager = зритель
public class UIManager {
    void Start() {
        healthSystem.OnHealthChanged += UpdateUI; // подписаться
    }

    void UpdateUI(int newHealth) {
        // получил уведомление!
    }
}
```

Понятна аналогия?
```

**AI (Mentor, playing newbie):**
```
О, понятно! Как подписка на YouTube!

А зачем тогда `+=`? Почему не просто `=`?

И что такое Action<int>? Это связано с int health?
```

**Student:**
```
`+=` потому что может быть несколько подписчиков!

Как на YouTube канале - у одного видео может быть 1000 подписчиков,
все получат уведомление.

Если использовать `=`:
```csharp
OnHealthChanged = UpdateUI;    // подписался UIManager
OnHealthChanged = UpdateSound; // ПЕРЕЗАПИСАЛ! UIManager больше не подписан!
```

`+=` ДОБАВЛЯЕТ подписчика:
```csharp
OnHealthChanged += UpdateUI;    // подписался UIManager
OnHealthChanged += UpdateSound; // ДОБАВИЛ Sound, UI остался
OnHealthChanged += LogAnalytics; // ДОБАВИЛ Analytics, оба остались
```

Action<int> - это тип "метода-подписчика".
Action<int> значит: метод без return (void), принимает int параметр.

```csharp
void UpdateUI(int newHealth) { ... } // подходит под Action<int>
```

Если бы был Action<int, float> - метод принимал бы int и float.
```

**AI (Mentor, playing newbie):**
```
Ааа, понял!

А почему `?.Invoke`? Что такое эта точка с вопросом?

И еще: ты сказал event Action<int>. А если просто Action<int> без event?
```

**Student:**
```
`?.` - это "null-conditional operator" (проверка на null).

Представь:
```csharp
OnHealthChanged?.Invoke(100);
```

Это короткая запись для:
```csharp
if (OnHealthChanged != null) {
    OnHealthChanged.Invoke(100);
}
```

Зачем? Потому что если никто не подписался, OnHealthChanged = null.
Если вызвать Invoke на null - крашнется игра!

`?.` - безопасный вызов: если null - просто ничего не делает.

---

event vs без event:

БЕЗ event (просто Action<int>):
```csharp
public Action<int> OnHealthChanged;

// Проблема: любой может перезаписать!
OnHealthChanged = null; // БАХ! Удалили ВСЕ подписки!

// Проблема: любой может вызвать!
OnHealthChanged?.Invoke(100); // Чужой код "подделал" событие!
```

С event:
```csharp
public event Action<int> OnHealthChanged;

// Можно только:
OnHealthChanged += Method; // подписаться
OnHealthChanged -= Method; // отписаться

// НЕЛЬЗЯ:
OnHealthChanged = null;    // ОШИБКА КОМПИЛЯЦИИ!
OnHealthChanged?.Invoke(100); // ОШИБКА! (только внутри класса)
```

event - это защита. Только владелец класса может вызывать событие.
```

**AI (Mentor, playing newbie):**
```
ВАУ! Теперь понял!

Последний вопрос: а в чем разница между delegate и event?
Ты говорил про оба, запутался.
```

**Student:**
```
Хороший вопрос!

delegate - это ТИП (как int, string).
event - это МОДИФИКАТОР (как public, private).

Аналогия:
```csharp
int age;         // int - тип переменной
public int age;  // public - модификатор доступа

Action<int> callback;       // Action<int> - тип delegate
public event Action<int> OnHealthChanged; // event - защита delegate
```

delegate = базовый тип:
```csharp
public delegate void MyDelegate(int x); // создаем свой тип delegate
```

Action/Func = готовые delegate типы от C#:
```csharp
Action<int> = готовый delegate для void Method(int x)
Func<int, bool> = готовый delegate для bool Method(int x)
```

event = защищенный delegate:
```csharp
public event Action OnClick; // Action с защитой event
```

Резюме:
- delegate - базовая концепция (тип, хранящий метод)
- Action/Func - удобные версии delegate от C#
- event - защита для delegate (только += и -= извне)
```

**AI (Mentor):**
```
ПРЕВОСХОДНОЕ ОБЪЯСНЕНИЕ! 🎉🎉🎉

Ты объяснил:
✅ Через аналогию (YouTube подписки)
✅ Простым языком (без сложных терминов)
✅ С примерами кода
✅ Структурированно (от простого к сложному)
✅ Ответил на все вопросы новичка

Если бы я был реальный новичок - я бы понял delegates после твоего объяснения!

---

🎓 FEYNMAN TECHNIQUE: PASSED ✅

Если ты можешь объяснить концепцию так просто - значит понимаешь глубоко.

---

📊 ИТОГИ СЕССИИ

✅ Теория: Понял концепцию delegates полностью
✅ Практика: Решил 2 задачи (легкую и среднюю)
✅ Проверка: 5/5 на всех уровнях понимания
✅ Закрепление: Отлично объяснил тему обратно

📈 УРОВЕНЬ ПО DELEGATES: MIDDLE DEVELOPER ⭐⭐⭐⭐⭐

💪 СИЛЬНЫЕ СТОРОНЫ:
- Понимаешь не только КАК, но и ПОЧЕМУ
- Думаешь об edge cases
- Знаешь best practices (event vs Action, memory leaks)
- Можешь объяснить просто

🎯 NEXT STEPS:
1. Практика: Используй delegates в своих проектах
2. Следующий модуль: Module 02 - Generics (завтра или через день)
3. Optional: Задача 3 (сложная) - можешь попробовать сам для практики

---

📝 ДОМАШНЕЕ ЗАДАНИЕ (optional):

Создай систему UI кнопок с callbacks:
- Разные типы событий (OnClick, OnHover, OnDoubleClick)
- Используй delegates правильно (event, memory leak safe)
- Придумай интересные сценарии использования

Если сделаешь - покажешь в следующей сессии на code review!

---

Отличная работа! 🎉
Вопросы? Или на сегодня достаточно?
```

---

## 📝 ЗАМЕТКИ ДЛЯ СТУДЕНТА

### Что было хорошо в этой сессии:

1. **ИИ не давал готовый код** - направлял вопросами
2. **Практика > теория** - 60% времени на coding
3. **Проверка понимания на разных уровнях** - от базовых знаний до критического мышления
4. **Feynman Technique** - объяснение обратно показало глубину понимания
5. **Code review подход** - нашли edge cases, улучшили код

### Чего избегать:

❌ Не просите "дай готовый код"
❌ Не говорите "понял", если не поняли
❌ Не копируйте код без понимания
❌ Не пропускайте проверку понимания

### Используйте в следующих сессиях:

✅ Такую же структуру (теория → практика → проверка → закрепление)
✅ Задавайте вопросы ИИ
✅ Пишите код сами, показывайте для проверки
✅ Объясняйте концепции обратно

---

**Это пример идеальной сессии обучения с ИИ-наставником!** 🚀

Используйте такой же подход для всех тем!
