using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Indexers_lab
{
    public delegate void myDelegate();
    public class CustomMenu
    {
        private string _path = "output.txt";
        private List<myDelegate> _arr = new List<myDelegate>();
      
        public void SetFileName()
        {
            string _fileName = Console.ReadLine();
            Console.WriteLine("Первый {0} ", _fileName);
        }
        public void WriteToFile()
        {
            Console.Write("Введите текст для добавления в файл -> ");
            string text = Console.ReadLine();
            var sw = new StreamWriter(_path, true);
            sw.WriteLine(text);
            sw.Close();
        }
        public void LastString()
        {
            string lastStr = File.ReadLines(_path).Last();
            Console.WriteLine(lastStr);
        }
        public void RandomString()
        {
            int count = File.ReadAllLines(_path).Length;
            Console.Write("Количество строк в файле {0}.\nКакую строчку считать из файла -> ", count);
            int numStr = Convert.ToInt32(Console.ReadLine());
            numStr -= 1;
            string randomStr = File.ReadLines(_path).Skip(numStr).First(); ;
            Console.WriteLine(randomStr);
        }
        public void ReplaceRandomString()
        {
            Console.Write("Какую строчку хотите заменить -> ");
            int numStr = Convert.ToInt32(Console.ReadLine());
            numStr -= 1;
            string randomStr = File.ReadLines(_path).Skip(numStr).First(); 
            Console.WriteLine(randomStr);
            
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
            sw.Close();
        }
        public myDelegate this[int index]
        {
            get => _arr[index];
        }
    }
    class Program
    {
        static void Main()
        {
            CustomMenu myMenu = new CustomMenu();
            myDelegate _mainDelegate = myMenu[3];
            _mainDelegate();

            Console.ReadKey();
        }
    }
}