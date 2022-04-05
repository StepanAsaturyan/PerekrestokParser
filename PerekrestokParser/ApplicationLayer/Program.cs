namespace PerekrestokParser
{
    internal static class Program
    {
        [STAThread]
        static Task Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainPage());
            
            return Task.CompletedTask;
        }
    }
}