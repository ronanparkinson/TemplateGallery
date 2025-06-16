using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TemplateGallery.Models;
using TemplateGallery.Services;

namespace TemplateGallery.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        private readonly TemplateService _templateService = new();
        public ObservableCollection<Template> Templates { get; set; } = new ();
        public ICommand FavouriteCommand { get; }

        public MainVM() {
            FavouriteCommand = new RelayCommand<Template>(ToggleFavorite);
            LoadTemplate();
        }

        private async void LoadTemplate()
        {
            var results = await _templateService.GetTemplatesAsync();
            //foreach (var template in results)
            //{
            //    Templates.Add(template);
            //}
            Templates.Clear();

            var tasks = results.Select(async template =>
            {
                try
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(template.ImageUrl);
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();

                    await Task.Delay(250);

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        template.Image = image;
                        Templates.Add(template);
                    }, System.Windows.Threading.DispatcherPriority.Background);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                }
            });
            await Task.WhenAll(tasks);
        }

        private void ToggleFavorite(Template template)
        {
            if (template != null)
            {
                template.IsFavorite = !template.IsFavorite;
                System.Diagnostics.Debug.WriteLine($"Favorite toggled for: {template.Title} → {template.IsFavorite}");
                // OnPropertyChanged(nameof(Templates));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null) { 
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
