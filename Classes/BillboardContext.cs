using Cinema_Kylosov_Finally.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WorkingBD;
using System.Globalization;
using Cinema_Kylosov_Finally.Element;

namespace Cinema_Kylosov_Finally.Classes
{
    public class BillboardContext : Model.Billboard
    {
        public BillboardContext(int id, int cinemaId, int movieID, DateTime showTime, decimal ticketPrice, int numberOfTickets)
            : base(id, cinemaId, movieID, showTime, ticketPrice, numberOfTickets)
        {
        }

        public static List<BillboardContext> AllBillboards()
        {
            List<BillboardContext> allBillboards = new List<BillboardContext>();
            
            MySqlConnection connection = Connection.OpenConnection();

            MySqlDataReader billboardQuery = Connection.Query("SELECT * FROM `Cinema`.`Billboard`", connection);

            while (billboardQuery.Read())
            {
                allBillboards.Add(new BillboardContext(
                    billboardQuery.GetInt32(0),
                    billboardQuery.GetInt32(1),
                    billboardQuery.GetInt32(2),
                    billboardQuery.GetDateTime(3),
                    billboardQuery.GetDecimal(4),
                    billboardQuery.GetInt32(5)
                    ));
            }

            Connection.CloseConnection(connection);

            return allBillboards;
        }

        public void DeleteBillboard()
        {
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query($"DELETE FROM `Cinema`.`Billboard` WHERE BillboardID = {this.BillboardID}", connection);
            Connection.CloseConnection(connection);
        }

        //udpate insert
        public void UpdateBillboard(string cinemaName, string movieName, DateTime showTime, decimal ticketPrice, int numberOfTickets)
        {
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query($"UPDATE `Cinema`.`Billboard` SET "+
                            $"CinemaID           = (SELECT CinemaID FROM Cinema.Cinemas WHERE CinemaName = '{cinemaName}'), " +
                            $"MovieID            = (SELECT MovieID  FROM Cinema.Movies  WHERE MovieName  = '{movieName}'), " +
                            $"ShowTime           = '{showTime.ToString("yyyy-MM-dd HH:mm:ss")}', " +
                            $"TicketPrice        = {ticketPrice.ToString(CultureInfo.InvariantCulture)}, " +
                            $"NumberOfTickets    = {numberOfTickets} " +
                            $"WHERE (BillboardID = {this.BillboardID});", connection);
            Connection.CloseConnection(connection);
        }

        public static void Insert(int id, string cinemaName, string movieName, DateTime showTime, decimal ticketPrice, int numberOfTickets)
        {
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query($"INSERT INTO `Cinema`.`Billboard` VALUES(" +
                $"{id}, " +
                $"(SELECT CinemaID FROM Cinema.Cinemas WHERE CinemaName = '{cinemaName}'), " +
                $"(SELECT MovieID  FROM Cinema.Movies  WHERE MovieName  = '{movieName}'), " +
                $"'{showTime.ToString("yyyy-MM-dd HH:mm:ss")}'," +
                $"{ticketPrice.ToString(CultureInfo.InvariantCulture)}," +
                $"{numberOfTickets})", 
                connection);
            Connection.CloseConnection(connection);
        }

        public void Issue()
        {
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query($"UPDATE `Cinema`.`Billboard` SET NumberOfTickets = NumberOfTickets + 1 WHERE (BillboardID = {this.BillboardID});", connection);
            Connection.CloseConnection(connection);
        }
    }
}
