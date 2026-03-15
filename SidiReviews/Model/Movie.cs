using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SidiReviews.Model
{
    public enum MovieGenre
    {
        Action,
        Adventure,
        Comedy,
        Drama,
        Crime,
        Fantasy,
        Horror,
        Historical,
        Romance,
        Satire,
        ScienceFiction,
        Thriller,
        Speculative,
        Animation
    }
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public MovieGenre Genre { get; set; }
        public string Director { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ImagePath { get; set; }
        public string Synopsis {  get; set; }
        public ICollection<Review> Reviews { get; set; }

    }
}
