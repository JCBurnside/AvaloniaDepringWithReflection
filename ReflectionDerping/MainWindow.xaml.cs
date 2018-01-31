using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Serilog;
using System.Threading.Tasks;

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
        }


    }

    internal class FilesTreeView
    {
        public string Name { get; private set; }

        public IList<FilesTreeView> Sub { get; private set; } = new List<FilesTreeView>();
        public IList<string> Files { get; private set; } = new List<string>();
        public FilesTreeView(DirectoryInfo directory)
        {
            Name = directory.Name;
            foreach (FileInfo info in directory.EnumerateFiles())
            {
                Files.Add(info.Name);
            }
            foreach (DirectoryInfo info in directory.EnumerateDirectories())
            {
                Sub.Add(new FilesTreeView(info));
            }
        }
    }
}
