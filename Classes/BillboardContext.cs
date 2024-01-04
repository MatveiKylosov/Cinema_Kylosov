using Cinema_Kylosov_Finally.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using WorkingBD;

namespace Cinema_Kylosov_Finally.Classes
{
    public class BillboardContext : Billboard
    {
        public BillboardContext(int id, int cinemaId, string movie, DateTime showTime, decimal ticketPrice, int numberOfTickets)
            : base(id, cinemaId, movie, showTime, ticketPrice, numberOfTickets)
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
                    billboardQuery.GetString(2),
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
         
        }

        //udpate insert
        public void UIBillboard(bool Update = false)
        {

        }
    }
}
