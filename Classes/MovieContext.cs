using Cinema_Kylosov_Finally.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkingBD;

namespace Cinema_Kylosov_Finally.Classes
{
    public class MovieContext : Movie
    {
        public MovieContext(int id, string movieName)
            : base(id, movieName)
        {
        }

        public static List<MovieContext> AllMovies()
        {
            List<MovieContext> allMovies = new List<MovieContext>();

            MySqlConnection connection = Connection.OpenConnection();

            MySqlDataReader movieQuery = Connection.Query("SELECT * FROM `Cinema`.`Movies`", connection);

            while (movieQuery.Read())
            {
                allMovies.Add(new MovieContext(
                    movieQuery.GetInt32(0),
                    movieQuery.GetString(1)
                    ));
            }

            Connection.CloseConnection(connection);

            return allMovies;
        }

        public void DeleteMovie()
        {

        }

        //udpate insert
        public void UIMovie(bool Update = false)
        {

        }
    }
}
