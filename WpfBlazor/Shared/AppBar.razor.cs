using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;

namespace WpfBlazor.Shared
{
    public partial class AppBar
    {
        [Parameter]
        public EventCallback<MudTheme> OnThemeToggled { get; set; }
        private bool isSopMode;
        private string? previousPage;
        private bool isDarkMode;


        public async Task ToggleTheme()
        {
            isDarkMode = !isDarkMode;
            await OnThemeToggled.InvokeAsync();
        }

        public async Task ToggleSOP()
        {
            try
            {
                if (isSopMode)
                {
                    isSopMode = !isSopMode;
                    if (nav.Uri.ToString().Replace("https://0.0.0.0", "") == "/StandardWork")
                        nav.NavigateTo(previousPage);
                    else
                    {
                        await ToggleSOP();
                    }
                }
                else
                {
                    previousPage = nav.Uri.ToString().Replace("https://0.0.0.0", "");
                    isSopMode = !isSopMode;
                    nav.NavigateTo("/StandardWork");
                }
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message);
            }
        }
    }
}