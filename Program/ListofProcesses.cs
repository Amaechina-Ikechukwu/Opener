using System;
using System.Diagnostics;
class Proccesses {
public static void ListOfRunningProcesses()
{
        Process[] processes = Process.GetProcesses();
        Console.WriteLine("Running Processes: ");
        foreach (Process process in processes)
        {
            if(!string.IsNullOrEmpty(process.MainWindowTitle) && IsUserProcess(process)){
               Console.WriteLine($"{process.ProcessName} (PID: ${process.Id})"); 
            }
            
        }
        SaveProcess.ReceiverInput(processes);
}
private static bool IsUserProcess(Process process){
    try{
        return process.SessionId > 0;
    }catch(Exception){
        return false;
    }
}
}

