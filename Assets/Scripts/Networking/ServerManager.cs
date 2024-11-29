using System.Diagnostics;
using UnityEngine;

public class ServerManager : MonoBehaviour
{
    private Process serverProcess;

    void Awake()
    {
        StartServer();
    }

    void OnApplicationQuit()
    {
        StopServer();
    }

     private void StartServer()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe", // Use "bash" on macOS/Linux
            Arguments = "/C npm run start", // /C runs the command and exits
            WorkingDirectory = System.IO.Path.Combine(Application.dataPath, "../"), // Adjust if needed
            CreateNoWindow = false,
            UseShellExecute = false,
            RedirectStandardOutput = true, // Optional: capture output
            RedirectStandardError = true   // Optional: capture errors
        };

        serverProcess = Process.Start(startInfo);
    }
    private void StopServer()
    {
        if (serverProcess != null && !serverProcess.HasExited)
        {
            serverProcess.Kill();
        }
    }
}
