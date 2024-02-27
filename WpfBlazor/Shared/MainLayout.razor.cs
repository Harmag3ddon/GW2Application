using MudBlazor;
using System.Text.Json;

namespace WpfBlazor.Shared
{
    public partial class MainLayout
    {
        public MudTheme _currentTheme = new MudTheme
        {
            PaletteDark = new Palette()
            {
                Primary = "#fcdd08",
                Black = "#303030",
                Background = "#1f1f1f",
                BackgroundGrey = "#303030",
                Surface = "#1f1f1f",
                TextPrimary = "#ffffffb3",
                TextSecondary = "rgba(255,255,255, 0.50)",
                AppbarBackground = "#303030",
                AppbarText = "#ffffffb3",
                DrawerBackground = "#303030",
                DrawerText = "#ffffffb3",
                DrawerIcon = "#ffffffb3",
                TextDisabled = "#ffffffb3",
                ActionDisabled = "#ffffffb3",
                ActionDisabledBackground = "#ffffffb3",
                ActionDefault = "#ffffffb3",
            },
            Palette = new Palette()
            {
                Secondary = "#ffdd00",
                Info = "#FFFDD00",
                Primary = "#000000",
                PrimaryDarken = "#000000",
                TextPrimary = "#000000b3",
            }
        };
        public NavMenu NavMenu;

        public async Task ToggleTheme()
        {
            MainApp.isDarkMode =!MainApp.isDarkMode;

            await localStorage.SetItemAsync("theme", MainApp.isDarkMode);
            StateHasChanged();
        }

        public async Task ToggleMenu()
        {
            if (!string.IsNullOrEmpty(MainWindow.Width))
                MainWindow.Width = string.Empty;
            else MainWindow.Width = "50px";

            await localStorage.SetItemAsync("menu", MainWindow.Width);
            Refresh();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await Theme();
                await Menu();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                await localStorage.SetItemAsync("theme",MainApp.isDarkMode);
                await localStorage.SetItemAsync("menu",MainWindow.Width);
            }
        }


        private async Task Theme()
        {
            string _isDarkMode = await localStorage.GetItemAsync<string>("theme");

            if (string.IsNullOrEmpty(_isDarkMode))
                _isDarkMode = "false";

            bool cookie = JsonSerializer.Deserialize<bool>(_isDarkMode);
            MainApp.isDarkMode = cookie;

            if(MainApp.isDarkMode != cookie)
            {
                StateHasChanged();
            }
        }

        private async Task Menu()
        {
            var menu = await localStorage.GetItemAsStringAsync("menu");
            if (string.IsNullOrEmpty(menu))
                menu = "";

            string value= JsonSerializer.Deserialize<string>(menu);
            MainWindow.Width = value;
            NavMenu?.Refresh();
            StateHasChanged();
        }

        public void Refresh()
        {
                StateHasChanged();
        }

    }
}
