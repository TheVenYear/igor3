using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karpenko3
{
    class Program
    {
        public static bool mainStatus = true;
        public static bool secondStatus = true;
        public static Connection auth = new Connection();

        public static void ChooseMenu()
        {
            secondStatus = true;
            while (secondStatus)
            {
                switch (Console.ReadLine())
                {
                    case "1":
                        RegisterMenu();
                        break;
                    case "2":
                        LoginMenu();
                        break;
                    case "3":
                        mainStatus = false;
                        secondStatus = false;
                        break;
                    default:
                        Console.WriteLine("Неверный пункт меню, введите ещё раз");
                        break;
                }
            }

        }

        public static void RegisterMenu()
        {
            Console.Clear();
            secondStatus = true;
            while (secondStatus)
            {
                Console.Write("Введите ваш номер телефона: ");
                var cellPhone = Console.ReadLine();
                if (!auth.CheckCellPhone(cellPhone))
                {
                    Console.Clear();
                    Console.WriteLine("Неверный формат телефона(верный формат xxx-xxx-xx-xx)");
                }
                else
                {
                    Console.Write("Введите логин: ");
                    var login = Console.ReadLine();
                    Console.Write("Введите пароль: ");
                    var password = Console.ReadLine();

                    if (auth.Register(login, cellPhone, password) == 1)
                    {
                        AuthorizedMenu(auth.AuthorizedAcc.Power);
                        secondStatus = false;
                    }
                    else if (auth.Register(login, cellPhone, password) == 2)
                    {
                        Console.Clear();
                        Console.WriteLine("Ваш пароль не должен совпадать с вашим номером телефона телефона");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Аккаунт с таким логином уже существет");
                    }
                }
                
            }

        }

        public static void AuthorizedMenu(int level)
        {
            Console.Clear();
            if (level < 5)
            {
                Console.WriteLine("У вас нет прав доступа для работы, ожидайте одобрения администратора");
                Console.ReadLine();
            }
            else if (level < 10)
            {
                var accList = auth.ShowAccounts();
                foreach (var item in accList)
                {
                    Console.WriteLine($"Логин: {item.Key}, Пароль: {item.Value}");
                }
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Выберите действие:\n1: Удалить пользователя\n2: Изменить уровень доступа пользователю");
                var line = Console.ReadLine();
                if (line == "1")
                {
                    while (secondStatus)
                    {
                        DeleteMenu();
                        line = null;
                    }
                }
                else if (line == "2")
                {
                    while (secondStatus)
                    {

                        SetLvl();
                        line = null;
                    }
                }

            }
        }

        public static void SetLvl()
        {
            Console.Clear();
            var accList = auth.ShowAccounts();
            foreach (var item in accList)
            {
                Console.WriteLine($"Логин: {item.Key}, Пароль: {item.Value}");
            }

            Console.WriteLine("Введите аккаунт для увеличения уровня доступа или напишите комманду exit для выхода: ");
            var line = Console.ReadLine();
            if (line == "exit")
            {
                secondStatus = false;
                return;
            }
            Console.WriteLine("Введите уровень доступа: ");
            var lvlLine = Console.ReadLine();

            if (!auth.SetLevel(line, lvlLine))
            {
                Console.WriteLine("Не удалось установить доступ, проверьте парвильность введённых данных!");
            }
        }

        public static void DeleteMenu()
        {
            Console.Clear();
            Console.WriteLine("Введите аккаунт для удаления или напишите комманду exit для выхода: ");
            var accList = auth.ShowAccounts();
            foreach (var item in accList)
            {
                Console.WriteLine($"Логин: {item.Key}, Пароль: {item.Value}");
            }
            var line = Console.ReadLine();
            if (line == "exit")
            {
                secondStatus = false;
                return;
            }
            else if (!auth.Delete(line))
            {
                Console.WriteLine("Не удалось удалить аккаунт, проверьте парвильность введённых данных!");
            }
        }

        public static void LoginMenu()
        {
            Console.Clear();
            secondStatus = true;
            while (secondStatus)
            {
                Console.Write("Введите логин: ");
                var login = Console.ReadLine();
                Console.Write("Введите пароль: ");
                var password = Console.ReadLine();

                if (auth.Auth(login, password))
                {
                    AuthorizedMenu(auth.AuthorizedAcc.Power);
                    secondStatus = false;
                }

                else
                {
                    Console.WriteLine("Вы ввели неверные данные, выйти y/n: ");
                    if (Console.ReadLine() == "y")
                    {
                        secondStatus = false;
                    }
                }
            }
        }
        static void Main(string[] args)
        {
            while (mainStatus)
            {
                Console.Clear();
                Console.WriteLine("Выберите действие:\n1: Регистрация\n2: Вход\n3: Выход\n");
                ChooseMenu();
            }
        }
    }
}
