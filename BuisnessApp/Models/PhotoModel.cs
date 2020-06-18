using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessApp.Models
{
    public class PhotoModel
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string ImgTitle { get; set; }
        public string Description { get; set; }
        public string LargeOpen { get; set; }
        public TypePhoto Type { get; set; }

        public PhotoModel()
        {
            LargeOpen = "lg_display_none";
        }

        
    }

    public enum TypePhoto
    {
        Family,
        Vacation,
        Work
    }
}
