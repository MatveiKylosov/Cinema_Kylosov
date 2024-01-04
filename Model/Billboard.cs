using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_Kylosov_Finally.Model
{
    public class Billboard
    {
        public int BillboardID { get; set; }
        public int CinemaID { get; set; }
        public int MovieID { get; set; }
        public DateTime ShowTime { get; set; }
        public decimal TicketPrice { get; set; }
        public int NumberOfTickets { get; set; }

        public Billboard() { }
        public Billboard(int billboardID, int cinemaID, int movieID, DateTime showTime, decimal ticketPrice, int numberOfTickets)
        {
            BillboardID = billboardID;
            CinemaID = cinemaID;
            MovieID = movieID;
            ShowTime = showTime;
            TicketPrice = ticketPrice;
            NumberOfTickets = numberOfTickets;
        }
    }
}
