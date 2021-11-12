using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BarSimulator
{
    public class Student
    {
        enum NightlifeActivities { Walk, VisitBar, GoHome };

        //added close to bar activities enum
        enum BarActivities { Drink, Dance, Leave, Close };

        Random random = new Random();

        public string Name { get; set; }
        public Bar Bar { get; set; }
        public double Budget { get; set; }
        public int Age { get; set; }

        private NightlifeActivities GetRandomNightlifeActivity()
        {
            int n = random.Next(10);
            if (n < 3) return NightlifeActivities.Walk;
            if (n < 8) return NightlifeActivities.VisitBar;
            return NightlifeActivities.GoHome;
        }

        private BarActivities GetRandomBarActivity()
        {
            int n = random.Next(10);
            if (n < 4) return BarActivities.Dance;
            if (n < 7) return BarActivities.Drink;
            if (n < 9) return BarActivities.Leave;
            return BarActivities.Close;
        }

        private void WalkOut()
        {
            Console.WriteLine($"{Name} is walking in the streets.");
            Thread.Sleep(100);
        }

        bool stillInLine = true;

        private void LeaveOrStayWhenWaiting()
        {
            Random random = new Random();
            int n = random.Next(15);
            if (n > 11)
            {
                stillInLine = false;
                Console.WriteLine($"{Name} decided to leave while waiting.");
            }
            
        }

        private string GetRandomDrink()
        {
            Random rnd = new Random();
            int n = rnd.Next(10);
            if (n < 4) return "Water";
            if (n < 7) return "Sprite";
            if (n < 9) return "Beer";
            return "Vodka";

        }

        private void BuyDrink()
        {
            var drinkType = GetRandomDrink();
            var drinkToBuy = Bar.drinks.Where(x => x.Type == drinkType).FirstOrDefault();
            if (Budget > drinkToBuy.Price)
            {
                Budget -= drinkToBuy.Price;
                drinkToBuy.Stock -= 1;
                drinkToBuy.Income += drinkToBuy.Price;
                drinkToBuy.BoughtDrinks++;
                Console.WriteLine($"{Name} is drinking.");
            }
            else
            {
                Console.WriteLine($"{Name} doesn't have enough money for drinks anymore.");
            }
        }
        

        private void VisitBar()
        {
            if (!Bar.IsBarClosed)
            {
                Console.WriteLine($"{Name} is getting in the line to enter the bar.");
                LeaveOrStayWhenWaiting();

                if (Age < 18)
                {
                    Console.WriteLine($"{Name} is underage and can't enter the bar.");
                    GetRandomNightlifeActivity();
                    return;
                }
                if (stillInLine == false)
                {
                    return;
                }
                Bar.Enter(this);
                Console.WriteLine($"{Name} entered the bar!");
                bool staysAtBar = true;
                while (staysAtBar)
                {
                    var nextActivity = GetRandomBarActivity();
                    switch (nextActivity)
                    {
                        case BarActivities.Dance:
                            Console.WriteLine($"{Name} is dancing.");
                            Thread.Sleep(100);
                            break;
                        case BarActivities.Drink:
                            BuyDrink();
                            Thread.Sleep(100);
                            break;
                        case BarActivities.Leave:
                            Console.WriteLine($"{Name} is leaving the bar.");
                            Bar.Leave(this);
                            staysAtBar = false;
                            break;
                        case BarActivities.Close:
                            Bar.IsBarClosed = true;
                            Bar.Close();
                            staysAtBar = false;
                            //stopping students from entering once it's closed doesn't work
                            break;

                        default: throw new NotImplementedException();
                    }
                }
            }
        }

        public void PaintTheTownRed()
        {
            if (!Bar.IsBarClosed)
            {
                WalkOut();
                bool staysOut = true;
                while (staysOut)
                {
                    var nextActivity = GetRandomNightlifeActivity();
                    switch (nextActivity)
                    {
                        case NightlifeActivities.Walk:
                            WalkOut();
                            break;
                        case NightlifeActivities.VisitBar:
                            VisitBar();
                            staysOut = false;
                            break;
                        case NightlifeActivities.GoHome:
                            Console.WriteLine($"{Name} is going back home.");
                            staysOut = false;
                            break;
                        default: throw new NotImplementedException();
                    }
                }
            }
        }

        public Student(string name, Bar bar, double budget, int age)
        {
            this.Name = name;
            this.Bar = bar;
            this.Budget = budget;
            this.Age = age;
        }
    }
}