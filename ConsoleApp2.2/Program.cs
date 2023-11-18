using System;
using System.Collections.Generic;
using System.Linq;

abstract class Worker
{
    public string Name { get; }
    public string Position { get; }
    public int WorkDay { get; }
    public Worker(string name, string position, int workDay)
    {
        Name = name;
        Position = position;
        WorkDay = workDay;
    }
    public void Call()
    {
        Console.WriteLine($"{Position} розмовляє");
    }
    public void WriteCode()
    {
        Console.WriteLine($"{Position} пише код");
    }
    public void Relax()
    {
        Console.WriteLine($"{Position} відпочиває");
    }
    abstract public void FillWorkDay();
}

class Developer : Worker
{
    public Developer(string name, int workDay) : base(name, "розробник", workDay) { }
    public override void FillWorkDay()
    {
        WriteCode();
        Call();
        Relax();
        WriteCode();
    }
}

class Manager : Worker
{
    private Random random = new Random();
    public Manager(string name, int workDay) : base(name, "менеджер", workDay) { }
    public override void FillWorkDay()
    {
        Console.WriteLine($"{Position} ");
        int firstTime = random.Next(1, 11);
        for (int i = 0; i < firstTime; i++)
        {
            Call();
        }
        Relax();
        int secondTime = random.Next(1, 6);
        for (int i = 0; i < secondTime; i++)
        {
            Call();
        }
    }
}

class Team
{
    public string NameOfTeam { get; }
    private List<Worker> workers = new List<Worker>();

    public Team(string nameOfTeam)
    {
        NameOfTeam = nameOfTeam;
    }

    public void AddWorker(Worker worker)
    {
        // Перевірка на унікальність інформації про працівника,
        if (!WorkerExists(worker) && TotalWorkDay() + worker.WorkDay <= 24)
        {
            workers.Add(worker);
        }
        else if (WorkerExists(worker))
        {
            Console.WriteLine("Інформація про працівника вже існує.");
        }
        else
        {
            Console.WriteLine("Загальна кількість годин перевищує 24.");
        }
    }

    public void PrintTeamInfo()
    {
        Console.WriteLine($"Team: {NameOfTeam}");
        Console.WriteLine("Employees:");
        foreach (var worker in workers)
        {
            Console.WriteLine($"{worker.Name}");
        }
    }

    public void PrintDetailedTeamInfo()
    {
        Console.WriteLine($"Команда: {NameOfTeam}");
        Console.WriteLine("Детальна інформація:");

        foreach (var worker in workers)
        {
            Console.WriteLine($"{worker.Name}  {worker.Position}, працює {worker.WorkDay} годин");
        }
    }

    public bool WorkerExists(Worker worker)
    {
        return workers.Any(existingWorker =>
            existingWorker.Name == worker.Name && existingWorker.Position == worker.Position && existingWorker.WorkDay == worker.WorkDay);
    }

    public int TotalWorkDay()
    {
        return workers.Sum(worker => worker.WorkDay);
    }
}

public class Program
{
    private static void Main()
    {
        List<Team> teams = new List<Team>();

        bool addTeam = true;

        while (addTeam)
        {
            Console.Write("Введіть назву команди: ");
            string teamName = Console.ReadLine();
            Team team = new Team(teamName);
            teams.Add(team);

            bool addWorker = true;

            while (addWorker)
            {
                Console.Write("Бажаєте додати співробітника? Напишіть так або ні: ");
                string answer = Console.ReadLine();

                if (answer == "так")
                {
                    Console.Write("Введіть ім'я вашого робітника: ");
                    string name = Console.ReadLine();

                    Console.Write("Яку посаду він займає? Напишіть менеджер чи розробник: ");
                    string position = Console.ReadLine();

                    Console.Write("Скільки годин приділяє роботі? Напишіть кількість годин: ");
                    int workDay = int.Parse(Console.ReadLine());

                    if (position.ToLower() == "розробник")
                    {
                        Developer developer = new Developer(name, workDay);
                        team.AddWorker(developer);
                    }
                    else if (position.ToLower() == "менеджер")
                    {
                        Manager manager = new Manager(name, workDay);
                        team.AddWorker(manager);
                    }
                    else
                    {
                        Console.WriteLine("Введена некоректна інформація");
                    }
                }
                else if (answer == "ні")
                {
                    addWorker = false;
                }

                Console.Write("Бажаєте додати ще співробітника в цій команді? Якщо так, то введіть +, якщо ні, то - : ");
                string answer3 = Console.ReadLine();
                if (answer3 != "+")
                {
                    addWorker = false;
                }
            }

            Console.Write("Бажаєте додати ще команду? Якщо так, то введіть +, якщо ні, то - : ");
            string answer4 = Console.ReadLine();
            if (answer4 != "+")
            {
                addTeam = false;
            }
        }

        Console.Write("Бажаєте отримати детальну інформацію? Напишіть так або ні: ");
        string answer2 = Console.ReadLine();
        if (answer2 == "так")
        {
            foreach (var team in teams)
            {
                team.PrintDetailedTeamInfo();
            }
        }
    }
}