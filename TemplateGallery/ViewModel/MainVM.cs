using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
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

            Templates.Clear();

            var favoriteIds = LoadFavorites(); // Load saved favorites 

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
                        template.IsFavorite = favoriteIds.Contains(template.Title);
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
                SaveFavorites(); // ← save after change

                //System.Diagnostics.Debug.WriteLine($"Favorite toggled for: {template.Title} → {template.IsFavorite}");
                // OnPropertyChanged(nameof(Templates));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string name = null) { 
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void SaveFavorites()
        {
            try
            {
                var favoriteIds = Templates
                    .Where(t => t.IsFavorite)
                    .Select(t => t.Title) // or a unique ID if you add one
                    .ToList();

                var json = JsonSerializer.Serialize(favoriteIds);
                File.WriteAllText("favorites.json", json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to save favorites: {ex.Message}");
            }
        }

        private List<string> LoadFavorites()
        {
            try
            {
                if (File.Exists("favorites.json"))
                {
                    var json = File.ReadAllText("favorites.json");
                    return JsonSerializer.Deserialize<List<string>>(json) ?? new List<string>();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load favorites: {ex.Message}");
            }

            return new List<string>();
        }
    }
}
