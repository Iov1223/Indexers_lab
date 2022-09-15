using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Indexers_lab
{
    public delegate void myDelegate();
    public class CustomMenu
    {
        private string _path = "text.txt";
        private List<myDelegate> _arr = new List<myDelegate>();
        public void SetFileName()
        {
            Console.Write("Введите имя файла без расширения.\nВвод -> ");
            string path = Console.ReadLine() + ".txt";
            File.Move(_path, path);           
        }      
        public void WriteToFile()
        {
            Console.Write("Введите текст для добавления в файл.\nВвод -> ");
            string text = Console.ReadLine();
            var sw = new StreamWriter(_path, true);
            sw.WriteLine(text);
            sw.Close();
        }
        public void LastString()
        {
            try
            {
                string lastStr = File.ReadLines(_path).Last();
                Console.WriteLine(lastStr);
            }
            catch
            {
                Console.WriteLine("В файле отсутствуют записи! Выберети пункт \"Записать строку в файл\"\n");
            }
        }
        public void RandomString()
        {
            int count = File.ReadAllLines(_path).Length, numStr;
            if (count == 0)
            {
                Console.Write("\nКоличество строк в файле {0}.\nВыберети пункт \"Записать строку в файл\"\n", count);
                return;
            }
            Console.Write("\nКоличество строк в файле {0}.\nКакую строчку считать из файла.\nВвод -> ", count);
            bool isCorrect = false;
            do
            {
                try
                {
                    bool isCorrect2 = false;
                    do
                    {
                        numStr = Convert.ToInt32(Console.ReadLine());
                        if (numStr > count || numStr <= 0)
                        {
                            Console.Write("Строки с таким номером не существует! Попробуйте ещё раз -> ");
                        }
                        else
                        {
                            isCorrect2 = true;
                        }
                    } while (!isCorrect2);
                    numStr -= 1;
                    string randomStr = File.ReadLines(_path).Skip(numStr).First(); ;
                    Console.WriteLine(randomStr);
                    isCorrect = true;
                }
                catch
                {
                    Console.WriteLine("Неверный формат ввода!");
                }
            } while (isCorrect != true);
        }
        public void ReplaceRandomString()
        {
            string[] arr = File.ReadAllLines(_path);
            if (arr.Length == 0)
            {
                Console.Write("\nКоличество строк в файле {0}.\nВыберети пункт \"Записать строку в файл\".\n", arr.Length);
                return;
            }
            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine("{0}) {1}", i + 1, arr[i]);
            }
            Console.Write("\nКоличество строк в файле {0}.\nКакую строку заменить в файле.\nВвод -> ", arr.Length);
            int element;
            bool isCorrect = false;
            do
            {
                try
                {
                    bool isCorrect2 = false;
                    do
                    {
                        element = Convert.ToInt32(Console.ReadLine());
                        if (element > arr.Length || element <= 0)
                        {
                            Console.Write("Строки с таким номером не существует! Попробуйте ещё раз -> ");
                        }
                        else
                        {
                            isCorrect2 = true;
                        }
                    } while (!isCorrect2);
                    Console.Write("Введите текст -> ");
                    arr[element - 1] = Console.ReadLine();
                    File.WriteAllLines(_path, arr);
                    isCorrect = true;
                }
                catch
                {
                    Console.Write("Неверный формат ввода! Попробуйте ещё раз -> ");
                }
            } while (isCorrect != true);
        }
    public CustomMenu()
        {
            var sw = new StreamWriter(_path, true);
            myDelegate _delegate;
            _delegate = SetFileName;
            _arr.Add(_delegate);
            _delegate = WriteToFile;
            _arr.Add(_delegate);
            _delegate = LastString;
            _arr.Add(_delegate);
            _delegate = RandomString;
            _arr.Add(_delegate);
            _delegate = ReplaceRandomString;
            _arr.Add(_delegate);
            sw.Close();
        }
        public myDelegate this[int index]
        {
            get => _arr[index];
        }
        public void ShowMenu()
        {
            Console.WriteLine("1) Задать имя файла (если не выбрать этот пункт имя файла будет задано поумолчанию: \"text.txt\").\n2) Записать строку в файл.\n" +
                "3) Считать последнюю строку из файла.\n4) Считать N-ную строку из файла.\n" +
                "5) Изменить N-ную строку.\n");
        }
        public int Select()
        {
            Console.Write("Выберите пункт меню.\nВвод -> ");
            bool isCorrect = false;
            int menu = 0;
            do
            {
                try
                {
                    menu = Convert.ToInt32(Console.ReadLine());
                    if (menu > 5 || menu < 1)
                    {   
                        return menu;
                    }
                    isCorrect = true;
                }
                catch
                {
                    Console.Write("Такого варианта нет! Попробйте ещё раз -> ");
                }
            } while (!isCorrect);
            return menu;
        }
    }

    class Program
    {
        static void Main()
        {
            CustomMenu myMenu = new CustomMenu();
            myMenu.ShowMenu();
            int menu = myMenu.Select();
            myDelegate _mainDelegate = myMenu[menu - 1];
            _mainDelegate();
        }
    }
}