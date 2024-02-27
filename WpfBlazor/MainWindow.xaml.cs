using System.Security.Cryptography.X509Certificates;
using System.Windows;
using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace WpfBlazor;

public partial class MainWindow : Window
{
    public static string Width = "";
    public MainWindow()
    {
        InitializeComponent();

        var Services = new ServiceCollection();
        Services.AddWpfBlazorWebView();
        Services.AddMudServices();
        Services.AddBlazoredLocalStorage();
        
        Resources.Add("services", Services.BuildServiceProvider());
    }
}