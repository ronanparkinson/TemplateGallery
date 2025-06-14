using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateGallery.Models;

namespace TemplateGallery.Services
{
    public class TemplateService
    {
        public async Task<List<Template>> GetTemplatesAsync()
        {
            await Task.Delay(500);

            return new List<Template>
            {
                new Template { Title = "Pitch Deck", Description = "Startup-style layout", ImageUrl = "https://via.placeholder.com/150", IsFavorite = false },
                new Template { Title = "Marketing Plan", Description = "Clean and visual", ImageUrl = "https://via.placeholder.com/150", IsFavorite = false },
                new Template { Title = "Product Roadmap", Description = "Timeline format", ImageUrl = "https://via.placeholder.com/150", IsFavorite = false }
            };
        }
    }
}
