using System;
using System.Collections.Generic;
using System.Threading;

namespace BarSimulator
{
    public class Bar
    {
       public bool IsBarClosed { get; set; }
       List<Student> students = new List<Student>();
       public List<Drink> drinks = new List<Drink>() 
        { 
            new Drink("Vodka",5,30,0,0),
            new Drink("Water",1,30,0,0),
            new Drink("Beer",3,30,0,0),
            new Drink("Sprite",2,30,0,0),
        };
        Semaphore semaphore = new Semaphore(10, 10); 
        //sometimes it throws SemaphoreFullException

        public void Enter(Student student)
        {
            semaphore.WaitOne();
            lock (students)
            {
                students.Add(student);
            }
        }

        public void Leave(Student student)
        {
            lock (students)
            {
                students.Remove(student);
            }
            semaphore.Release();
        }
        public void Report()
        {
            foreach (var drink in drinks)
            {
            Console.WriteLine($"{drink.Type} - {drink.Income} income and {drink.BoughtDrinks} drink out of stock.");
            }
        }

        public void Close()
        {
            for (int i = 0; i < students.Count; i++) 
            {
                Student student = students[i];
                Leave(student);
            }
            Console.WriteLine("Bar closes");
            Report();
        }
    }
}