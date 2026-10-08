using System;
using System.Collections.Generic;

namespace LambdaExpressions
{
    public class Program
    {
        public static void Main()
        {
            const int ActionsCount = 3;// количество действий по заданию

            while (true)
            {
                Console.WriteLine("\nГенератор отчётов\n1. Вычислить процент\n2. Классифицировать процент\n3. Проверить захват внешней переменной\n4. Ошибка захвата в for\n5. Исправление захвата в for\n0. Выход");
                Console.Write("\nВыберите действие: ");
                string choice = Console.ReadLine()!;

                try
                {
                    if (choice == "0")
                        break;

                    if (choice == "1")
                    {
                        Console.Write("Введите часть: ");
                        int part = int.Parse(Console.ReadLine()!);
                        Console.Write("Введите целое значение: ");
                        int whole = int.Parse(Console.ReadLine()!);

                        if (part < 0 || whole <= 0 || part > whole)
                            throw new ArgumentException("Неверные значения.");

                        Func<int, int, int> calculatePercent = (p, w) => p * 100 / w;

                        Console.WriteLine($"\nПроцент: {calculatePercent(part, whole)}");
                    }
                    else if (choice == "2")
                    {
                        Console.Write("Введите процент: ");
                        int percent = int.Parse(Console.ReadLine()!);

                        if (percent < 0 || percent > 100)
                            throw new ArgumentException("Процент должен быть от 0 до 100.");

                        Func<int, string> classify = value =>
                        {
                            if (value < 50)
                                return "низкий";
                            if (value < 75)
                                return "средний";
                            return "высокий";
                        };

                        Console.WriteLine($"\nКатегория: {classify(percent)}");
                    }
                    else if (choice == "3")
                    {
                        Console.Write("Введите название отчёта: ");
                        string reportName = Console.ReadLine()!;

                        Action showReport = () => Console.WriteLine($"Текущий отчёт: {reportName}");

                        Console.WriteLine("\nДо изменения:");
                        showReport();

                        Console.Write("Введите новое название отчёта: ");
                        reportName = Console.ReadLine()!;

                        Console.WriteLine("\nПосле изменения:");
                        showReport();
                    }
                    else if (choice == "4")
                    {
                        List<Action> actions = new List<Action>();

                        for (int i = 0; i < ActionsCount; i++)
                            actions.Add(() => Console.WriteLine(i));

                        Console.WriteLine("\nРезультат до исправления:");

                        for (int i = 0; i < actions.Count; i++)
                            actions[i]();
                    }
                    else if (choice == "5")
                    {
                        List<Action> actions = new List<Action>();

                        for (int i = 0; i < ActionsCount; i++)
                        {
                            int captured = i;
                            actions.Add(() => Console.WriteLine(captured));
                        }

                        Console.WriteLine("\nРезультат после исправления:");

                        for (int i = 0; i < actions.Count; i++)
                            actions[i]();
                    }
                    else
                    {
                        Console.WriteLine("\nОшибка: такого пункта меню нет.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nОшибка: нужно ввести целое число.");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"\nОшибка: {ex.Message}");
                }
            }
        }
    }
}
