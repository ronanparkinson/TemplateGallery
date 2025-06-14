using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TemplateGallery.Models;
using TemplateGallery.Services;

namespace TemplateGallery.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        private readonly TemplateService _templateService = new();

        public ObservableCollection<Template> Templates { get; set; } = new ();

        public MainVM() { 
            LoadTemplate();
        }

        private async void LoadTemplate()
        {
            var results = await _templateService.GetTemplatesAsync();
            foreach (var template in results)
            {
                Templates.Add(template);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null) { 
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
