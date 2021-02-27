using FilmsCatalog.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace FilmsCatalog.ModelsConfigs
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder
                .HasOne(movie => movie.Author)
                .WithMany(author => author.Movies)
                .HasForeignKey(movie => movie.AuthorId)
                .HasPrincipalKey(author => author.Id);
        }
    }
}
