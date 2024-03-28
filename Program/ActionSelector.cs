using System;
class ActionSelector
{
    public static void Select()
    {
        string[] Options = ["Enter Workspace", "Select Workspace"];
        int selectedIndex=0; 
       
        while (true)
        {
             ShowOptions(Options, selectedIndex);
            var keyInfo = Console.ReadKey(true);
            switch (keyInfo.Key){
                case ConsoleKey.UpArrow:
                    selectedIndex = Math.Max(0, selectedIndex - 1);
                    break;
                case ConsoleKey.DownArrow:
                    selectedIndex = Math.Min(Options.Length - 1, selectedIndex + 1);
                    break;
                case ConsoleKey.Enter:
                
                    if(selectedIndex==0)
                    {
                    Proccesses.ListOfRunningProcesses();
                    }
                    else
                    {
                    Console.WriteLine("Hey");                   
                    }
                    break;
            }
            
        }
    }
    private static void ShowOptions(string[] options, int selectedIndex)
    {
        Console.Clear();
        for(int i =0; i < options.Length; i++)
        {
            if (i == selectedIndex)
            {
                Console.ForegroundColor = ConsoleColor.Green;

            }
            Console.WriteLine($">{options[i]}");
            Console.ResetColor();
        }
    }
}