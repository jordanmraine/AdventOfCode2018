using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;

using Kirkin.CommandLine;

namespace ConsoleApp
{
    internal static class Out
    {
        public static void Write(string value)
        {
            System.Console.Write(value);
        }

        public static void Write(string value, ConsoleColor color)
        {
            using (ConsoleFormatter.ForegroundColorScope(color))
            {
                System.Console.Write(value);
            }
        }

        public static void WriteWithColouredPrefix(string value)
        {
            if (System.Console.IsOutputRedirected)
            {
                Write(value);

                return;
            }

            const string pattern = @"\[(.+)\] (.*)";
            Match match = Regex.Match(value, pattern);

            if (match.Success)
            {
                // Pretty colours.
                Write("[");
                Write(match.Groups[1].Value, ConsoleColor.Magenta);
                Write("] ");
                Write(match.Groups[2].Value);
            }
            else
            {
                Write(value);
            }
        }

        public static void WriteLine()
        {
            System.Console.WriteLine();
        }

        public static void WriteLine(string value)
        {
            System.Console.WriteLine(value);
        }

        public static void WriteLine(string value, ConsoleColor color)
        {
            using (ConsoleFormatter.ForegroundColorScope(color))
            {
                System.Console.WriteLine(value);
            }
        }

        public static void WriteLineWithColouredPrefix(string value)
        {
            WriteWithColouredPrefix(value);
            WriteLine();
        }

        public static void OverwriteCurrentLine(string value)
        {
            if (System.Console.IsOutputRedirected)
            {
                WriteLine(value);
            }
            else
            {
                if (value.Length >= System.Console.CursorLeft)
                {
                    System.Console.SetCursorPosition(0, System.Console.CursorTop);
                }
                else
                {
                    EraseLine();
                }

                Write(value);
            }
        }

        public static void EraseLine()
        {
            if (System.Console.IsOutputRedirected)
            {
                WriteLine();

                return;
            }

            int length = System.Console.CursorLeft;

            if (length != 0)
            {
                OverwriteCurrentLine(new string(' ', length));
                System.Console.SetCursorPosition(0, System.Console.CursorTop);
            }
        }

        public static void EnsureNewLine()
        {
            if (System.Console.IsOutputRedirected)
            {
                WriteLine();

                return;
            }

            if (System.Console.CursorLeft != 0)
            {
                System.Console.WriteLine();
            }
        }

        public static void WriteException(Exception exception)
        {
#if DEBUG
            WriteLine(exception.ToString(), ConsoleColor.Red);
#else
            WriteLine(FormatException(exception), ConsoleColor.Red);
#endif
        }

        public static string FormatException(Exception exception)
        {
            return $"{exception.GetType()}: {exception.Message}";
        }

        public static bool PromptForYesOrNo(string message)
        {
            bool userCanceled = false;

            void handler(object s, ConsoleCancelEventArgs e) => userCanceled = true;
            System.Console.CancelKeyPress += handler;

            try
            {
                while (true)
                {
                    if (!message.EndsWith("(Y/N)?"))
                    {
                        message = message.TrimEnd('?') + " (Y/N)?";
                    }

                    WriteLine(message);

                    string response = System.Console.ReadLine();

                    if (string.Equals(response, "N", StringComparison.OrdinalIgnoreCase))
                    {
                        return false;
                    }

                    if (string.Equals(response, "Y", StringComparison.OrdinalIgnoreCase))
                    {
                        return true;
                    }

                    // Generally enough of a delay to let the CancelKeyPress handler to run when CTRL+C is pressed.
                    Thread.Sleep(10);

                    if (userCanceled || message == null)
                    {
                        Out.WriteLine("Cancelled.", ConsoleColor.Red);

                        // Avoid "Invalid response" message when CTRL+C is pressed.
                        throw new OperationCanceledException();
                    }

                    WriteLine("Invalid response.");
                }
            }
            finally
            {
                System.Console.CancelKeyPress -= handler;
            }
        }

        public static string ReadPassword()
        {
            List<char> chars = new List<char>();

            while (true)
            {
                ConsoleKeyInfo key = System.Console.ReadKey(true);

                if (!char.IsControl(key.KeyChar))
                {
                    chars.Add(key.KeyChar);
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && chars.Count > 0)
                    {
                        chars.RemoveAt(chars.Count - 1);
                        System.Console.Write("\b \b");
                    }
                }

                if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
            }

            return new string(chars.ToArray());
        }
    }
}
