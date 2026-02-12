//Создай generic класс Container<T>, который хранит один элемент и умеет с ним работать.
//Требования:

//Хранит один элемент типа T
//Метод Set(T item) — сохраняет элемент
//Метод Get() — возвращает элемент
//Метод HasValue — возвращает bool, есть ли элемент внутри
//Метод Clear() — очищает элемент (делает null)


public class Container<T> where T: class
{
    private T item;
    public void Set(T item)
    {
        this.item = item;
    }

    public T Get()
    {
        return item;
    }

    public bool HasValue()
    {
        return item != null;
    }

    public void Clear()
    {
        this.item = null;
    }
}
