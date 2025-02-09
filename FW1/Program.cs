// See https://aka.ms/new-console-template for more information
Console.WriteLine("FW1 Start");


using var watcher = new FileSystemWatcher("./filedrop");

watcher.NotifyFilter = NotifyFilters.Attributes
                     | NotifyFilters.CreationTime
                     | NotifyFilters.DirectoryName
                     | NotifyFilters.FileName
                     | NotifyFilters.LastAccess
                     | NotifyFilters.LastWrite
                     | NotifyFilters.Security
                     | NotifyFilters.Size;


watcher.Created += OnCreated;
// watcher.Changed += OnChanged;
// watcher.Deleted += OnDeleted;
// watcher.Renamed += OnRenamed;
// watcher.Error += OnError;

watcher.Filter = "*.*";
watcher.IncludeSubdirectories = true;
watcher.EnableRaisingEvents = true;

Console.WriteLine("Press enter to exit.");
Console.ReadLine();

static void OnCreated(object sender, FileSystemEventArgs e)
{
    string value = $"Created: {e.FullPath}";    
    Console.WriteLine(value);
}