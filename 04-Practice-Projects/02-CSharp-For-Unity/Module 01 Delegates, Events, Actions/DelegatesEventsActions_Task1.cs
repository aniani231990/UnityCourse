// DONE
// Задачка 1: Простой делегат (начальный уровень)
using System;
using System.Diagnostics;
using System.Dynamic;
using System.Runtime.CompilerServices;


// Создаем делегат MessagePrinter - он будет ссылкой на методы, которые
// принимают строку и ничего не возвращают 
public delegate void MessagePrinter(string message);

static class Program
{
    static void PrintToConsole(string message)
    {
        Console.WriteLine(message);
    }

    static void PrintWithStars(string message)
    {
         Console.WriteLine("***" + message + "***");
    }

    static void Main()
    {
        // Создаем экземпляр делегата messagePrinter
        MessagePrinter messagePrinter = null;
        messagePrinter += PrintToConsole;

        messagePrinter("Hello world!");

        messagePrinter += PrintWithStars;

        messagePrinter("Hello world!");
    }
}

// DONE
// Задачка 2: Делегат с возвращаемым значением
// Создаем делегат с возвращаемым значением
public delegate int MathOperation(int a, int b);

class MathCalculator
{
    static int Add(int a, int b)
    {
        return a + b;
    }

    static int Multiply(int a, int b)
    {
        return a * b;
    }

    static int Subtract(int a, int b)
    {
        return a - b;
    }

    static void Main()
    {
        // Создаем экземпляр делегата
        MathOperation mathOperation = null;

        mathOperation += Add;

        int result1 = mathOperation(1,2);

        mathOperation -= Add;
        
        mathOperation = Multiply;
        int result2 = mathOperation(2,2);

        mathOperation = Subtract;

        int result3 = mathOperation(3,2);
    }
}

public class PlayerScore 
{
    public delegate void ScoreChangedDelegate(int newScore);
    public ScoreChangedDelegate OnScoreChanged;
    
    private int score;
    
    public void IncreaseScore(int points) 
    {
        score += points;
        OnScoreChanged?.Invoke(score); // Вызов делегата
    }
    
    public void Subscribe(ScoreChangedDelegate subscriber) 
    {
        OnScoreChanged += subscriber;
    }
}


public class AchievementManager
{
    public delegate void AchievementUnlockedDelegate(string achievement);

    public AchievementUnlockedDelegate OnAchievementUnlocked;

    public void Subscribe(AchievementUnlockedDelegate subscriber)
    {
        OnAchievementUnlocked += subscriber;
    }

    public void RegisterProgress(string achievementName, int currentProgress, int requiredProgress)
    {
        if (currentProgress >= requiredProgress)
        {
            AchievementUnlocked(achievementName);
        }
    }

    public void AchievementUnlocked(string achievement)
    {
        OnAchievementUnlocked?.Invoke(achievement);
    }

}

public class UIAchievementManger
{
    public AchievementManager achievementManager;
    
    public void showAchievement(string achievement)
    {
        Debug.Log(achievement);
    }

    private void Start()
    {
        AchievementManager achievementManager = new AchievementManager();
        achievementManager.OnAchievementUnlocked += showAchievement;
    }
}

class Math
{
    public delegate int MathOperation(int a, int b);

    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    } 

    public int Multiply(int a, int b)
    {
        return a * b;
    } 

    public void Main()
    {
        MathOperation mathOperation = Add;
        Console.WriteLine(mathOperation(1, 2));

        mathOperation -= Add;

        mathOperation = Subtract;
        Console.WriteLine(mathOperation(15, 6));

        mathOperation += Multiply;
        Console.WriteLine(mathOperation(5, 3));
    }
}


class PlayerNotify
{
    public delegate void NotifyPlayer(string message);

    static void SendToChat(string msg)
    {
        Console.WriteLine("[Chat]:" + msg);
    }

    static void SendToLog(string msg)
    {
        Console.WriteLine("[Log]:" + msg);
    }

    static void SendPopup(string msg)
    {
        Console.WriteLine("[Popup]:" + msg);
    }   

    static void Main()
    {
        NotifyPlayer notify = SendToChat;
        notify += SendToLog;
        if (notify != null)
        {
            notify("Вы получили 100 золота!");
        }
    }
}





