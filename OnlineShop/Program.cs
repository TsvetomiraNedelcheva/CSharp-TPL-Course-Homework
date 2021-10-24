using System;
using System.Collections.Generic;
using System.Threading;

namespace OnlineShop
{
    class Program
    {
        static Dictionary<string, int> itemsQty = new Dictionary<string, int>()
        {
            { "laptop", 8 },
            { "phone", 10 },
            {"headphones", 20 }
        };

        static object obj = new object();
        static void Main(string[] args)
        {
            
            Thread t1 = new Thread(() => Buyers("laptop", 4));
            Thread t2 =new Thread(() => Buyers("phone", 5));
            Thread t3 = new Thread(() => Buyers("neshto", 10));
            Thread t4 = new Thread(() => Suppliers("laptop", 2));
            Thread t5 = new Thread(() => Suppliers("phone", 5));
            t1.Start();
           t2.Start();
           t3.Start();
           t4.Start();
            t5.Start();
            t1.Join();
           t2.Join();
            t3.Join();
            t4.Join();
            t5.Join();

        }

        public static void Buyers(string itemToBuy, int quantityToBuy)
        {
            Monitor.Enter(obj);

            for (int i = 1; i < 100; i++)
            { 
                if (itemsQty.ContainsKey(itemToBuy))
                {
                    if ((itemsQty[itemToBuy] -= quantityToBuy) <= 0)
                    {
                        Console.WriteLine("Not enough available items");
                        break;
                    }
                    if (itemsQty[itemToBuy] <= 0)
                    {
                        itemsQty[itemToBuy] = 0;
                        Console.WriteLine($"No available items from {itemToBuy}!");
                        break;
                    }

                    itemsQty[itemToBuy] -= quantityToBuy;
                }
                else
                {
                    Console.WriteLine("No such item");
                    break;
                }
                
                Console.WriteLine
                    ($"{quantityToBuy} numbers of {itemToBuy} -> just bought! Available quantity of this item -> {itemsQty[itemToBuy]}.");
                
               
            }
            Thread.Sleep(500);
            Monitor.Exit(obj);
        }

        public static void Suppliers(string itemToRestock, int quantityToRestock)
        {

            for (int i = 1; i < 5; i++)
            {
                Monitor.Enter(obj);
                if (itemsQty.ContainsKey(itemToRestock))
                {

                    itemsQty[itemToRestock] += quantityToRestock;
                }
                else
                {
                    Console.WriteLine("No such item");
                    break;
                }
                Console.WriteLine
                    ($"{quantityToRestock} numbers of {itemToRestock} -> restocked! Available quantity of this item -> {itemsQty[itemToRestock]}");
                Thread.Sleep(500);
                Monitor.Exit(obj);
            }
        }

    }
}


