/*
У тебя есть список предметов инвентаря:


public class Item
{
    public string Name;
    public string Type;    // "Weapon", "Armor", "Potion"
    public int Price;      // цена в золотых
    public float Weight;   // вес
}

List<Item> inventory = new List<Item>();
Напиши через LINQ (method syntax, то есть через точку):

Найти все предметы типа "Weapon"
Найти самый дорогой предмет во всём инвентаре
Посчитать сумму веса всех предметов
Каждый пункт — это одна цепочка LINQ. Промежуточных переменных не нужно.

Начинай писать код. Покажи, когда будет готово.*/


public class Item
{
    public string Name;
    public string Type;    // "Weapon", "Armor", "Potion"
    public int Price;      // цена в золотых
    public float Weight;   // вес
}

public class Inventory
{
    List<Item> inventory = new List<Item>();

    // Все предметы типа "Weapon"
    public List<Item> GetWeapon()
    {
        return inventory.Where(e => e.Type == "Weapon").ToList();
    }

    // Самый дорогой предмет во всём инвентаре
    public Item GetMostExpensiveItem()
    {
        return inventory.MaxBy(e => e.Price);
    }

    public float TotalWeight()
    {
        return inventory.Sum(e => e.Weight);
    }

}

