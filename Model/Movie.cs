using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Kylosov_Finally.Model
{
    public class Movie
    {
        public int MovieID { get; set; }
        public string MovieName { get; set; }

        public Movie() { }
        public Movie(int movieID, string movieName)
        {
            MovieID = movieID;
            MovieName = movieName;
        }
    }
}
