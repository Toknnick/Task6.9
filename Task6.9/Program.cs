using System;
using System.Collections.Generic;

namespace Task6._9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Admin admin = new Admin();
            admin.Work();
        }
    }

    class Admin
    {
        public void Work()
        {
            bool isWork = true;
            List<Client> clients = new List<Client>();
            AddClients(clients);
            int shopMoney = 0;

            while (isWork)
            {
                Console.WriteLine("1.Показать клиентов. \n2.Обслужить клиентов. \n3.Показать монеты магазина. \n4.Выход. \nВыберите вариант:");
                string userInput = Console.ReadLine();
                Console.Clear();

                switch (userInput)
                {
                    case "1":
                        ShowClients(clients);
                        break;
                    case "2":
                        shopMoney += ServeClient(clients);
                        break;
                    case "3":
                        Console.WriteLine($"Монеты магазина: {shopMoney}.");
                        break;
                    case "4":
                        isWork = false;
                        break;
                }

                if (clients.Count <= 0)
                {
                    Console.WriteLine("Клиентов в очереди нет.");
                    Console.WriteLine("Добавить клиентов? \nY or N");
                    userInput = Console.ReadLine();

                    if (userInput == "N")
                    {
                        isWork = false;
                        Console.WriteLine("Рабочий день закончен.");
                    }
                    else
                    {
                        AddClients(clients);
                    }
                }

                Console.WriteLine(" \nДля продолжнеия нажмите любую клавишу:");
                Console.ReadKey();
                Console.Clear();
            }
            Console.WriteLine($"За сегодня вы заработали {shopMoney} монет. \n");
        }
        private static int ServeClient(List<Client> clients)
        {
            ShowClients(clients);
            int shopMoney = 0;
            int number = 0;
            Console.WriteLine("Для обслуживания клиента нажмите Enter:");

            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                shopMoney = AddMoneyToShop(clients, number);
                DeleteItemFromCart(clients, number);
            }
            else
            {
                Console.WriteLine("Данные некорректные!");
            }

            return shopMoney;
        }

        private static void DeleteItemFromCart(List<Client> clients, int number)
        {
            int numberOfPosted = 0;
            bool isLaidOut = false;

            while (clients[number].Money < clients[number].Total)
            {
                isLaidOut = true;
                clients[number].RemoveItem();
                Console.WriteLine("Товар выложен из тележки.");
                numberOfPosted++;
            }

            if (isLaidOut == true)
            {
                Console.WriteLine($" \nПокупатель номер {number + 1} выложил {numberOfPosted} товаров.");
            }

            clients.RemoveAt(number);

        }

        private static int AddMoneyToShop(List<Client> clients, int number)
        {
            int shopMoney = clients[number].Money;
            return shopMoney;
        }

        private static void AddClients(List<Client> clients)
        {
            Random random = new Random();
            int maxMoney = 100;
            int maxCountOFClients = 10;
            int minCountOFClients = 1;
            int countOfClients = random.Next(minCountOFClients, maxCountOFClients);

            for (int i = 0; i < countOfClients; i++)
            {
                int money = random.Next(maxMoney);
                clients.Add(new Client(money));
            }
        }

        private static void ShowClients(List<Client> clients)
        {
            int number = 1;

            foreach (Client client in clients)
            {
                Console.Write((number) + ".");
                client.ShowInfo();
                number++;
            }
        }
    }

    class Item
    {
        private string _name;
        public int Price { get; private set; }

        public Item(string name, int price)
        {
            _name = name;
            Price = price;
        }
    }

    class Client
    {
        private List<Item> _items = new List<Item>();
        private Random _random = new Random();

        public int Money { get; private set; }
        public int Total
        {
            get
            {
                int total = 0;

                for (int i = 0; i < _items.Count; i++)
                {
                    total += _items[i].Price;
                }

                return total;
            }
        }

        public Client(int money)
        {
            Money = money;
            AddItems();
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Сумма монет с собой: {Money}. Количество предметов в тележке: {_items.Count}");
        }

        public void RemoveItem()
        {
            int number = _random.Next(_items.Count);
            _items.RemoveAt(number);
        }

        private void AddItems()
        {
            _items.Add(new Item("Яблоко", 2));
            _items.Add(new Item("Банан", 4));
            _items.Add(new Item("Молоко", 5));
            _items.Add(new Item("Арбуз", 12));
            _items.Add(new Item("Дыня", 15));
            _items.Add(new Item("Одежда", 10));
            _items.Add(new Item("Сок", 12));
            _items.Add(new Item("Тыква", 19));
            _items.Add(new Item("Вишни", 11));
            _items.Add(new Item("Персики", 18));
        }
    }
}
