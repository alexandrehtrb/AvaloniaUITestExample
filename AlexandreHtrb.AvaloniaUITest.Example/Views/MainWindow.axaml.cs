using Avalonia.Controls;

namespace AlexandreHtrb.AvaloniaUITest.Example.Views;

public partial class MainWindow : Window
{
#nullable disable warnings
    public static MainWindow Instance;
#nullable restore warnings

    public MainWindow()
    {
        InitializeComponent();
        Instance = this;
    }
}