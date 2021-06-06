
using FourAceSolitaire.Command;
using FourAceSolitaire.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FourAceSolitaire.ViewModels
{
    class AboutViewModel : BaseViewModel
    {
        public ObservableCollection<AssemblyName> Versions { get; set; } = new ObservableCollection<AssemblyName>();
        public AsyncRelayCommand<Window> OkCommand { get; set; }
        public AsyncRelayCommand<object> LinkedInCommand { get; set; }
        public AsyncRelayCommand<object> GitHubCommand { get; set; }
        public AsyncRelayCommand<object> GoogleCommand { get; set; }


        public AboutViewModel()
        {
            OkCommand = new AsyncRelayCommand<Window>(OnOk);
            LinkedInCommand = new AsyncRelayCommand<object>(OnLinkedIn);
            GitHubCommand = new AsyncRelayCommand<object>(OnGitHub);
            GoogleCommand = new AsyncRelayCommand<object>(OnGoogle);
            Versions.Add(Assembly.GetExecutingAssembly().GetName());
        }

        private Task OnGitHub(object arg)
        {
            Process.Start(new ProcessStartInfo("https://github.com/AMiR-S-P") { UseShellExecute = true });

            return Task.CompletedTask;
        }

        private Task OnGoogle(object arg)
        {
            string uri = $"mailto:AmirSetvatiPaydar@Gmail.com?Subject=SPColorWheel&body=";
            Process.Start(new ProcessStartInfo(uri) { UseShellExecute = true });

            return Task.CompletedTask;
        }

        private Task OnLinkedIn(object arg)
        {
            Process.Start(new ProcessStartInfo("https://www.linkedin.com/in/amir-setvati-paydar-747657152/") { UseShellExecute = true });

            return Task.CompletedTask;
        }

        private Task OnOk(Window arg)
        {
            arg.Close();

            return Task.CompletedTask;
        }
    }
}
