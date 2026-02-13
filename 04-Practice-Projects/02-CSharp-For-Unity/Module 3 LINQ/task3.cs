/*
Задача 3 (🔴 Сложная): Custom LINQ Extension Methods

Создай свои extension methods для IEnumerable<T>:

1. Shuffle<T>() — перемешать элементы в случайном порядке
2. TakeRandom<T>(int count) — взять N случайных элементов (без повторений)
3. Batch<T>(int size) — разбить коллекцию на батчи (группы) по size элементов

Пример использования:

var items = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// Shuffle - случайный порядок
var shuffled = items.Shuffle(); // например: [3, 7, 1, 9, 2, ...]

// TakeRandom - 3 случайных элемента
var random3 = items.TakeRandom(3); // например: [5, 2, 9]

// Batch - разбить на группы по 3
var batches = items.Batch(3); // [[1,2,3], [4,5,6], [7,8,9], [10]]

⚠️ ВАЖНО: НЕ кодь сразу!

1. Сначала продумай архитектуру
2. Напиши план для каждого метода:
   - Какие шаги нужны для реализации?
   - Какие edge cases нужно обработать?
   - Какие вспомогательные структуры данных понадобятся?
3. Покажи план МЕНТОРУ
4. Потом кодинг

Твоя задача:
Напиши ПЛАН ниже в комментариях, НЕ пиши код пока!
*/

// ============================================
// ПЛАН РЕАЛИЗАЦИИ
// ============================================

/*
МЕТОД 1: Shuffle<T>()
---------------------
Что делает: Перемешивает элементы в случайном порядке

Шаги реализации:
1. Скопировать IEnumerable<T> в List
2. Используя LINQ перемешать список: list.OrderBy(x => Random.Shared.Next())

Edge cases:
- Если коллекция пустая, вернется пустой список.
- Если один элемент, то вернется список с одним элементом.

Структуры данных:
- List<T>


МЕТОД 2: TakeRandom<T>(int count)
----------------------------------
Что делает: Берёт N случайных элементов без повторений

Шаги реализации:
1. Слелать Shuffle<T>
2. Выбрать первые N элементов из списка.

Edge cases:
- нужна проверка, что count < количество элементов
- нужна проверка, что count > 0

Структуры данных:
- List<T>


МЕТОД 3: Batch<T>(int size)
----------------------------
Что делает: Разбивает коллекцию на группы по size элементов

Шаги реализации:
1. создаем список collections, где будут хранится батчи
2. пробегаемся циклом, берем по size элементов, и в виде списка добавляем их в collections
3. Последняя группа может быть меньшего размера 

Edge cases:
- Проверяем, что size > 0
- Проверяем, что коллекция не пустая.

Структуры данных:
- List<List<T>>

*/

// ============================================
// КОД (ПИСАТЬ ТОЛЬКО ПОСЛЕ ОДОБРЕНИЯ ПЛАНА!)
// ============================================


using System.Runtime.CompilerServices;

public static class EnumerableExtensions
{
   public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> items)
   {
      return items.OrderBy(item => Random.Shared.Next());
   }

   public static IEnumerable<T> TakeRandom<T>(this IEnumerable<T> items, int count)
   {
      if (count < 0)
      {
         throw new ArgumentException("count не может быть отрицательным"); 
      }

      var shuffled = items.Shuffle();

      return shuffled.Take(count);

   }

   public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> items, int size)
   {
      if (size <= 0)
      {
         throw new ArgumentException("size должен быть больше 0");
      }

      var itemsList = items.ToList();

      for (int i = 0; i < itemsList.Count; i += size)
      {
         yield return itemsList.Skip(i).Take(size);
      }
   }
}

