using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Forum0
{
    class Program
    {
        static void Main(string[] args)
        {
            // ТЗ - Техническое задание

            // 1. Объявите переменную Resault запишите туда результат сложения двух целых чисел 
            // и выведите на экран
            int resault = 2 + 6;
            Console.WriteLine("2 + 6 = " + resault);

            // первая переменная равна ли второму 
            // первая_переменная != вторая_переменная
            // == - равно
            // != - не равно
            // >= - больше или равно
            // <= - меньше или равно


            // 2. Напишите программу, которая просит ввести год рождения человека и
            // текущий год, после чего выдаёт результат его точный возраст

            Console.Write("Введите ваш год рождения: ");
            int dateOfBirth = int.Parse(Console.ReadLine());
            Console.Write("Введите текущий год: ");
            int nowYear = int.Parse(Console.ReadLine());

            int age = nowYear - dateOfBirth;
            Console.WriteLine("Ваш возраст: " + age);

            // название типа -> имя переменной -> присаиваем значени

            byte Age = 18;

            // Тип данных INT
            int valueINT = -10; // 10

            uint valueUINT = 10;

            // Тип данных Float 
            float valueFLOAT = 10f; // 10.0

            // Тип данных Double 
            double valueDOUBLE = 9.4;
            
            // Тип данных String
            string valueSTRING = "200000000000";
            string valueSTRING2 = "3";

            // Тип данных Boolean
            bool valueBOOLEAN = true;
               
            Console.ReadKey();
            // Инициализация и Создание - слова имеют одно и то же значение
        }

        //static void HelloGapiz(string Name)
        //{
        //    Name = Console.ReadLine();
        //    Console.WriteLine(Name + ", привет!");
        //}
    }
}
