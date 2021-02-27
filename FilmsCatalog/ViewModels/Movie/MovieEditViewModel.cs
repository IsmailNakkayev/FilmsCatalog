using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FilmsCatalog.Models;
using Microsoft.AspNetCore.Http;

namespace FilmsCatalog.ViewModels.Movie
{
    public class MovieEditViewModel
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
        public Models.Movie Movie { get; set; }
    }
}
