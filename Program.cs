using System;
using System.Collections.Generic;
using System.Globalization;

namespace ConsoleCalculator
{
    class Program
    {
        // constant tax rate
        const decimal TAX_RATE = 0.055m; // 5.5%

        static void Main(string[] args)
        {
            bool continueRunning = true; // bool
            int totalOperations = 0;     // int
            bool lastValid = false;      // bool
            double lastResult = 0;       // double

            Dictionary<string, int> opCounts = new Dictionary<string, int>()
            {
                {"+", 0}, {"-", 0}, {"*", 0}, {"/", 0}, {"avg", 0}, {"tax", 0}
            };

            do
            {
                ShowMenu();
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine().Trim().ToLower();

                switch (choice)
                {
                    case "+":
                    case "-":
                    case "*":
                    case "/":
                    case "avg":
                        if (ReadTwoDoubles(out double a, out double b))
                        {
                            bool valid = true;
                            double result = 0;

                            switch (choice)
                            {
                                case "+": result = a + b; break;
                                case "-": result = a - b; break;
                                case "*": result = a * b; break;
                                case "/":
                                    if (b == 0)
                                    {
                                        Console.WriteLine("Error: Division by zero is not allowed.");
                                        valid = false;
                                    }
                                    else result = a / b;
                                    break;
                                case "avg": result = (a + b) / 2; break;
                            }

                            if (valid)
                            {
                                Console.WriteLine($"Result: {result:F3}");
                                totalOperations++;
                                opCounts[choice]++;
                                lastResult = result;
                                lastValid = true;
                            }
                            else
                            {
                                lastValid = false;
                            }
                        }
                        break;

                    case "tax":
                        Console.Write("Enter amount: ");
                        if (decimal.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal amount))
                        {
                            decimal tax = amount * TAX_RATE;
                            decimal total = amount + tax;

                            Console.WriteLine($"Tax: {tax:C}");
                            Console.WriteLine($"Total: {total:C}");

                            totalOperations++;
                            opCounts["tax"]++;
                            lastValid = true;
                            lastResult = (double)total;
                        }
                        else
                        {
                            Console.WriteLine("Invalid money amount.");
                            lastValid = false;
                        }
                        break;

                    case "exit":
                        continueRunning = false;
                        break;

                    default:
                        Console.WriteLine("Invalid menu option.");
                        break;
                }

            } while (continueRunning);

            // Summary
            Console.WriteLine("\n===== SUMMARY =====");
            Console.WriteLine($"Total operations: {totalOperations}");

            foreach (var op in opCounts)
            {
                Console.WriteLine($"{op.Key} -> {op.Value}");
            }

            string lastLine = lastValid ? lastResult.ToString("F3") : "N/A"; // ternary
            Console.WriteLine($"Last result was: {lastLine}");

            // small for loop usage (requirement)
            Console.Write("Thank you for using the calculator ");
            for (int i = 0; i < 3; i++)
            {
                Console.Write("!");
            }

            Console.WriteLine();
        }

        static void ShowMenu()
        {
            Console.WriteLine("\n==== Console Calculator ====");
            Console.WriteLine("+   Add");
            Console.WriteLine("-   Subtract");
            Console.WriteLine("*   Multiply");
            Console.WriteLine("/   Divide");
            Console.WriteLine("avg Average");
            Console.WriteLine("tax Tax calculation");
            Console.WriteLine("exit Exit program");
        }

        static bool ReadTwoDoubles(out double a, out double b)
        {
            a = 0; b = 0;

            Console.Write("Enter first number: ");
            if (!double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out a))
            {
                Console.WriteLine("Invalid number.");
                return false;
            }

            Console.Write("Enter second number: ");
            if (!double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out b))
            {
                Console.WriteLine("Invalid number.");
                return false;
            }

            return true;
        }
    }
}
