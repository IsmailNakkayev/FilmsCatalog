using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Models
{
    [Table("Movies")]
    public class Movie
    {
        [Key]
        [Column]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Description { get; set; }

        [Column]
        public DateTime ReleaseDate { get; set; }

        [Column]
        public string Director { get; set; }

        [Column]
        public string Poster { get; set; }

        public User Author { get; set; }
        public string AuthorId { get; set; }

    }
}
