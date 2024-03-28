using System;

class ActionSelector
{
    public static void Select()
    {
        string[] Options = { "Enter Workspace", "Select Workspace" };
        int selectedIndex = 0;

        while (true)
        {
            ShowOptions(Options, selectedIndex);
            var keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    selectedIndex = Math.Max(0, selectedIndex - 1);
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex = Math.Min(Options.Length - 1, selectedIndex + 1);
                    break;
                case ConsoleKey.Enter:
                    // Execute code based on selectedIndex
                    if (selectedIndex == 0)
                    {
                        Console.WriteLine("Loading...");
                        Proccesses.ListOfRunningProcesses();
                    }
                    else if (selectedIndex == 1)
                    {
                        Console.WriteLine("Hey");
                    }
                    break;
                // Handle other key presses if needed
                // case ConsoleKey.Escape:
                //     Environment.Exit(0);
                //     break;
            }
        }
    }

    private static void ShowOptions(string[] options, int selectedIndex)
    {
        Console.Clear();
        for (int i = 0; i < options.Length; i++)
        {
            if (i == selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine($"> {options[i]}");
            Console.ResetColor();
        }
    }
}
