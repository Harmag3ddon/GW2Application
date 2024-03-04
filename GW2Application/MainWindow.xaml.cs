using System.Windows;
using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;

namespace GW2Application;

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