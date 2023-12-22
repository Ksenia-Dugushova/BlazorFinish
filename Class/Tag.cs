using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp1.Class
{
    public partial class Tag
    {
        [Key]
        public string id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public HashSet<Movie> Movies = new HashSet<Movie>();

        //tag.Movies.add(movie)
        public Tag() { }

        public Tag(string Id, string name)
        {
            id = Id;
            Name = name;

        }
        /*
        public void AddMovie(Movie movieName)
        {
            if (!Movies.Any(m => m.MovieCode == movieName.MovieCode))
            {
                Movies.Add(movieName);
            }
            else
            {

            }
        }*/
    }
}
