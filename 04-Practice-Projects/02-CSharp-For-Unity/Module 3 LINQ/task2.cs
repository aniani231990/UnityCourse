/*
Задача 2 (🟡 Средняя): Enemy AI Targeting

У тебя есть AI врага, который выбирает цель среди игроков:


public class Player
{
    public string Name;
    public int Health;
    public Vector3 Position;
    public int DamageDealt; // урон за последние 5 секунд
}

List<Player> players = new List<Player>();
Vector3 enemyPosition; // позиция врага
Напиши через LINQ (каждый пункт — одна цепочка):

Найти ближайшего игрока к врагу. Используй SqrMagnitude (как мы обсуждали — без корня!). Используй MinBy().

Найти игрока с наименьшим HP (самая лёгкая цель).

Найти всех игроков в радиусе 10 units от врага. Вернуть List<Player>.

Найти игрока с наибольшим уроном за последние 5 секунд (самая опасная цель).*/


public class Player
{
    public string Name;
    public int Health;
    public Vector3 Position;
    public int DamageDealt; // урон за последние 5 секунд
}

public class GameManager
{
    List<Player> players = new List<Player>();
    Vector3 enemyPosition; // позиция врага

    public Player GetNearestPlayer()
    {
        return players.MinBy(player => Vector3.SqrMagnitude(enemyPosition - player.Position));
    }

    public Player GetPlayerMinHP()
    {
        return players.MinBy(player => player.Health);
    }

    public List<Player> GetPlayersNearEnemy()
    {
        return players.Where(player => Vector3.SqrMagnitude(enemyPosition - player.Position) <= 100).ToList();
    }

    public Player GetPlayerMaxDamageDealt()
    {
        return players.MaxBy(player => player.DamageDealt);
    }

    
}
