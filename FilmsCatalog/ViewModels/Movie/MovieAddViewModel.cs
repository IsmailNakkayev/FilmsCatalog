using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace FilmsCatalog.ViewModels.Movie
{
    public class MovieAddViewModel
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        [StringLength(10000)]
        public string Description { get; set; }
        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Director { get; set; }
        
        [DataType(DataType.Upload)]
        public IFormFile Poster { get; set; }
    }
}
