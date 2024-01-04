using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Kylosov_Finally.Model
{
    public class Cinema
    {
        public int CinemaID { get; set; }
        public string CinemaName { get; set; }
        public int NumberOfHalls { get; set; }
        public int NumberOfSeats { get; set; }

        Cinema() { }
        Cinema(int cinemaID, string cinemaName, int numberOfHalls, int numberOfSeats)
        {
            CinemaID = cinemaID;
            CinemaName = cinemaName;
            NumberOfHalls = numberOfHalls;
            NumberOfSeats = numberOfSeats;
        }
    }
}
