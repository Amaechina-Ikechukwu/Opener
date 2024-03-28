using System;
using System.Collections.Generic;
using System.Diagnostics; // Importing the necessary namespace
using Newtonsoft.Json;
class AppEntry
{
    public required string Key { get; set; }
    public required string Value { get; set; }
}
class SaveProcess
{
    public static void ReceiverInput(Dictionary<string,string> openApps)
    {
        Console.Write("What will this shortcut be called?: ");
        string? shortcutName = Console.ReadLine();
        if(!string.IsNullOrEmpty(shortcutName)){
     Dictionary<string, List<AppEntry>> workspace = new Dictionary<string, List<AppEntry>>();
        List<AppEntry> items = new List<AppEntry>();

        foreach (var apps in openApps)
        {
            items.Add(new AppEntry { Key = apps.Key, Value = apps.Value });
        }

        workspace[shortcutName] = items;
            string jsonData = JsonConvert.SerializeObject(workspace);
            if (!File.Exists("data.json")){
              File.WriteAllText("data.json", jsonData);  
            }else{
                File.AppendAllText("data.json", jsonData);
            }
            
        Console.WriteLine($"Saved {shortcutName.ToUpper()}");
            Thread.Sleep(6000);
} 
       
    }
}
