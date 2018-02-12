using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using Serilog;
using System.Threading.Tasks;
using Avalonia.Logging.Serilog;
using static Serilog.Log;
using System.ComponentModel;
using System;

namespace ReflectionDerping
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.AttachDevTools();

        }
        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);

            BackgroundWorker worker=new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            worker.DoWork += Worker_DoWork;
            this.FindControl<Button>("Change").Click += delegate
            {
                var Selected = (new OpenFolderDialog() { DefaultDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Title = "Select Folder " }).ShowAsync().GetAwaiter().GetResult();
                if (string.IsNullOrWhiteSpace(Selected)) Selected = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                this.DataContext=new FilesTreeView(new DirectoryInfo(Selected)).Sub;
            };
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            _ = sender is BackgroundWorker worker;

        }
    }
    
    internal class FilesTreeView
    {
        public string Name { get; private set; }

        public IList<FilesTreeView> Sub { get; private set; }
        public FilesTreeView(string name){
            Name = name;
        }
        public FilesTreeView(DirectoryInfo directory)
        {
            Name = directory.Name;
            var files = new List<string>();
            List<FilesTreeView> sub = new List<FilesTreeView>();
            foreach (DirectoryInfo info in directory.EnumerateDirectories())
            {
                Information("{0}", info.Name);
                sub.Add(new FilesTreeView(info));
            }
            foreach (FileInfo info in directory.EnumerateFiles())
            {
                sub.Add(new FilesTreeView(info.Name));
            }
            Sub = sub.ToArray();
        }
    }
}
