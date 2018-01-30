using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.IO;

namespace ReflectionDerping
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.AttachDevTools();
            if(this.DataContext is BasicViewModel ctx){
                ctx.X = (1.0 / 3.0) + (1.0 / 3.0) + (1.0 / 3.0);
                this.DataContext = ctx;
            }
        }
        private void InitializeComponent()
        {
            
            AvaloniaXamlLoader.Load(this);
        }
    }
}
