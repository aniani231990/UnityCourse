# Шпаргалка для работы с ИИ-наставником

Быстрые команды и промпты для эффективной работы с ИИ.

**ℹ️ Примечание:** Инструкция для ИИ написана на английском для лучшего понимания, но ИИ **всегда отвечает на русском**. Команды из этой шпаргалки работают как есть.

---

## 🚨 ЭКСТРЕННЫЕ КОМАНДЫ (если ИИ отклоняется)

### ❌ ИИ выдает всю теорию сразу:

```
STOP! Follow instruction V2!

DON'T give all theory at once. Give in parts:
1. Problem → STOP → wait for my answer
2. Concept (2-3 paragraphs) → STOP → wait
3. Example → STOP → wait

Start over. ONLY PROBLEM, nothing else.

Respond in RUSSIAN!
```

### ❌ ИИ дает все задачи сразу:

```
STOP! Don't give all tasks at once.

Give ONLY Task 1.
WAIT until I solve it.
Check solution.
Ask questions.
Only THEN give Task 2.

Start with Task 1. Respond in RUSSIAN!
```

### ❌ ИИ дает готовый код:

```
STOP! Don't give ready code!

Your role - guide with QUESTIONS:
- What data structure fits?
- How will you search elements?
- What if input is invalid?

I write code. You ask leading questions.

Ask me question instead of code. Respond in RUSSIAN!
```

### ❌ ИИ не проверяет понимание:

```
Don't move to practice!

First check understanding of theory:
1. Explain concept in your words
2. What problem does it solve?
3. When will you use it?

Ask me these 3 questions. WAIT for answers. Respond in RUSSIAN!
```

### ❌ ИИ считает тему изученной без проверки:

```
STOP! Topic NOT learned!

To complete topic need:
✅ Solve minimum 2 tasks
✅ Answer understanding questions
✅ Explain concept back to me (Feynman)

Let's do understanding check first. Respond in RUSSIAN!
```

### 🔄 Перезапуск сессии:

```
RESTART!

Read again AI-MENTOR-INSTRUCTION-V2.md

CRITICAL RULES:
1. After EVERY block → STOP and question
2. DON'T give ready code
3. ONE task at a time, not all at once
4. Follow every "STOP ⛔ WAIT FOR ANSWER"

Continue with: [specify topic/task]

Start correctly! Respond in RUSSIAN!
```

---

## 🎬 ПРОМПТЫ ДЛЯ НАЧАЛА СЕССИИ

### Первая сессия:

```
Hi! Read instruction file:
05-AI-Learning-Guide/AI-MENTOR-INSTRUCTION-V2.md

CRITICAL:
- After each block do STOP and wait for answer
- DON'T output all theory at once
- DON'T give all tasks at once
- Follow every "STOP ⛔ WAIT FOR ANSWER"
- ALWAYS respond in RUSSIAN!

Student info:
- Level: Junior+ Unity Developer
- Goal: Middle/Senior
- Time: 2 hours per day

Let's start with: [topic]

Ready? Start with PROBLEM.
```

### Продолжение обучения:

```
Hi! Continuing learning.

Follow AI-MENTOR-INSTRUCTION-V2.md strictly!
ALWAYS respond in RUSSIAN!

Last topic: [topic] ✅
Today: [new topic]

Start with PROBLEM!
```

### Повторение темы:

```
Хочу повторить: [тема]

НЕ объясняй теорию заново.

Сразу:
1. Вопросы на понимание (5-7 вопросов)
2. Код с ошибками (я найду проблемы)
3. Новая практическая задача

Начинай с вопросов!
```

---

## 💡 ПОЛЕЗНЫЕ КОМАНДЫ ВО ВРЕМЯ ОБУЧЕНИЯ

### Если застрял на задаче:

```
Застрял на [конкретная часть].

Помоги декомпозировать:
1. Разбей задачу на подзадачи
2. Дай подсказку для первой подзадачи
3. НЕ давай готовое решение

С чего начать?
```

### Если концепция непонятна:

```
Не понял [концепция/часть].

Объясни по-другому:
1. Через аналогию из жизни
2. Простой пример (3-5 строк кода)
3. Как это используется в Unity

Объясняй пошагово, буду задавать вопросы.
```

### Если хочу углубиться:

```
Понял базовую концепцию [тема].

Хочу углубиться:
- Advanced use cases
- Подводные камни и ошибки
- Best practices из production

Какие продвинутые аспекты мне знать?
```

### Проверка готовности к новой теме:

```
Хочу убедиться, что готов к следующей теме.

Дай мне:
1. 5 вопросов на понимание [текущая тема]
2. Код с ошибками (я найду)
3. Мини-задачу на применение

Если 80%+ правильно → переходим дальше.

Начинай проверку!
```

### Просьба о code review:

```
Решил задачу. Вот мой код:

[ваш код]

Проверь:
1. Правильность логики
2. Edge cases
3. Clean Code (naming, responsibility)
4. Performance

Задавай вопросы, НЕ давай готовое решение.
```

---

## 🎯 СПЕЦИАЛЬНЫЕ СИТУАЦИИ

### Нужна мотивация:

```
Теряю мотивацию на [тема].

Напомни:
- Зачем это нужно в реальной разработке
- Пример из коммерческих игр
- Что я смогу делать, освоив это

Замотивируй меня!
```

### Слишком легко:

```
Задачи слишком легкие.

Дай challenge:
- Сложную задачу (комбинация концепций)
- Edge cases которые я не учел
- Оптимизацию моего решения

Усложни!
```

### Нужен практический проект:

```
Хочу применить [концепция] в проекте.

Предложи:
1. Идею мини-проекта (2-3 часа)
2. Требования (что должно быть)
3. План реализации

НЕ пиши код за меня, только план!
```

---

## 📋 НАПОМИНАНИЕ О SOCRATIC METHOD

Если ИИ отвечает прямо вместо вопросов:

```
Не отвечай прямо! Используй Socratic Method.

Вместо: "Используй Dictionary"

Спроси:
- Как будешь искать элементы?
- Какая сложность поиска нужна?
- Что важнее - скорость или порядок?

Я сам должен дойти до ответа.

Задай мне наводящие вопросы.
```

---

## ✅ ЧЕКЛИСТ ПЕРЕД ПЕРЕХОДОМ К НОВОЙ ТЕМЕ

```
Стоп! Перед переходом нужна проверка.

ЧЕКЛИСТ:
[ ] Объяснил концепцию своими словами?
[ ] Решил минимум 2 задачи?
[ ] Ответил на вопросы понимания (80%+)?
[ ] Понимаю КОГДА использовать, КОГДА НЕТ?
[ ] Объяснил тебе тему обратно (Feynman)?

Если хоть один НЕТ → доделываем!

Давай проверку по чеклисту.
```

---

## 🔥 БЫСТРЫЕ КОМАНДЫ (одна строка)

**Остановить простыню:** `STOP! Parts: question → STOP → answer. Respond in RUSSIAN!`

**Не давай код:** `DON'T give code! Guide with questions. RUSSIAN!`

**По одной задаче:** `ONLY Task 1. Others later. RUSSIAN!`

**Проверь понимание:** `Ask 3 questions on theory understanding. RUSSIAN!`

**Feynman:** `Now I'll explain concept to you. Ask questions. RUSSIAN!`

**Socratic Method:** `Answer with question, not directly. RUSSIAN!`

**Декомпозиция:** `Break task into subtasks. RUSSIAN!`

**Застрял:** `Hint through leading question. RUSSIAN!`

**Следующая тема:** `First checklist: 2 tasks done? questions answered? Feynman? RUSSIAN!`

---

## 💾 СОХРАНИТЕ ЭТУ ШПАРГАЛКУ

Держите этот файл открытым во время сессии с ИИ.
Копируйте команды по мере необходимости.

**Успешного обучения!** 🚀
