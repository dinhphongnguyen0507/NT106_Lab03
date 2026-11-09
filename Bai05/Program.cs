using System.Runtime.InteropServices;

namespace Bai05;

internal static class Program
{
    [DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    [STAThread]
    static void Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "server")
        {
            AllocConsole();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            new Server().ChayAsync().GetAwaiter().GetResult();
            return;
        }

        ApplicationConfiguration.Initialize();
        Application.Run(new FormMain());
    }
}