using System;
using System.Collections.Generic;
using System.Diagnostics; // Importing the necessary namespace
using Newtonsoft.Json;
class SaveProcess
{
    public static void ReceiverInput(Process[] processes)
    {
        Console.Write("What will this shortcut be called?: ");
        string? shortcutName = Console.ReadLine();
        if(!string.IsNullOrEmpty(shortcutName)){
     Dictionary<string, List<string>> workspace = new Dictionary<string, List<string>>();
        List<string> items = new List<string>();

        foreach (Process process in processes)
        {
            items.Add(process.ProcessName);
        }

        workspace[shortcutName] = items;
            string jsonData = JsonConvert.SerializeObject(workspace);
            if (!File.Exists("data.json")){
              File.WriteAllText("data.json", jsonData);  
            }else{
                File.AppendAllText("data.json", jsonData);
            }
            
        Console.WriteLine($"Saved {shortcutName.ToUpper()}");
} 
       
    }
}
