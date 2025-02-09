using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("FW1 Start");

// Set the current directory to the directory where the executable is located
//Directory.SetCurrentDirectory(AppContext.BaseDirectory);

string FolderToWatch = GetFolderToWatch();
Console.WriteLine($"Watching folder: {FolderToWatch}");

using var watcher = new FileSystemWatcher(FolderToWatch);


watcher.NotifyFilter = NotifyFilters.Attributes
                     | NotifyFilters.CreationTime
                     | NotifyFilters.DirectoryName
                     | NotifyFilters.FileName
                     | NotifyFilters.LastAccess
                     | NotifyFilters.LastWrite
                     | NotifyFilters.Security
                     | NotifyFilters.Size;
                     
watcher.Created += OnCreated;
watcher.Filter = "*.*";
watcher.IncludeSubdirectories = false;
watcher.EnableRaisingEvents = true;

Console.WriteLine("Press enter to exit.");
Console.ReadLine();

static void OnCreated(object sender, FileSystemEventArgs e)
{
    string value = $"Created: {e.FullPath}";
    Console.WriteLine(value);
}

static string GetFolderToWatch()
{
    var builder = Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration((hostingContext, configuration) =>
        {
            configuration.Sources.Clear();
            var env = hostingContext.HostingEnvironment;
            configuration.AddJsonFile("/workspaces/FileWatchPrint/FW1/appsettings.json", optional: true, reloadOnChange: true);
            // FW1 / appsettings.json
            // / workspaces / FileWatchPrint / FW1 / appsettings.json
            configuration.AddEnvironmentVariables();
        });

    var host = builder.Build();

    // Get the IConfiguration service from the built host
    var configuration = host.Services.GetRequiredService<IConfiguration>();

    var folderPath = configuration["FileWatcher:FolderPath"];

    return folderPath?.ToString() ?? "C:\\temp";

}