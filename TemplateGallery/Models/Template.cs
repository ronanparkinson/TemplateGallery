using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TemplateGallery.Models
{
    public class Template
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool IsFavorite { get; set; }
        public BitmapImage Image { get; set; }
    }
}
