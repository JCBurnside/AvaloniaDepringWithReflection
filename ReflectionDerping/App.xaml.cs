using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Diagnostics;
using Avalonia.Logging.Serilog;
using Avalonia.Themes.Default;
using Avalonia.Markup.Xaml;
using Serilog;
using System.IO;
using System.Reflection;

namespace ReflectionDerping
{
    class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            base.Initialize();
        }

        static void Main(string[] args)
        {
            InitializeLogging();
            AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .Start<MainWindow>(()=> {
                    var Selected = (new OpenFolderDialog() { DefaultDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Title = "Select Folder " }).ShowAsync().GetAwaiter().GetResult();
                    if (string.IsNullOrWhiteSpace(Selected)) Selected = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    return new FilesTreeView(new DirectoryInfo(Selected)).Sub;
                });
        }

        public static void AttachDevTools(Window window)
        {
#if DEBUG
            DevTools.Attach(window);
#endif
        }

        private static void InitializeLogging()
        {
#if DEBUG
            SerilogLogger.Initialize(new LoggerConfiguration()
                .MinimumLevel.Warning()
                .WriteTo.Trace(outputTemplate: "{Area}: {Message}")
                .CreateLogger());
#endif
        }
    }
}
