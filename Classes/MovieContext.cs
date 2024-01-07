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
        public MovieContext(int id, string movieName, string genre, int duration)
            : base(id, movieName, genre, duration)
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
                    movieQuery.GetString(1),
                    movieQuery.GetString(2),
                    movieQuery.GetInt32(3)
                ));
            }

            Connection.CloseConnection(connection);

            return allMovies;
        }

        public void Update(string movieName, string genre, int duration)
        {
            this.MovieName = movieName; 
            this.Genre = genre;
            this.Duration = duration;

            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query($"UPDATE `Cinema`.`Movies` SET " +
                $"MovieName = '{this.MovieName}', " +
                $"Genre = '{this.Genre}', " +
                $"Duration = {this.Duration} " +
                $"WHERE MovieID = {this.MovieID}", connection);
            Connection.CloseConnection(connection);
        }

        public void DeleteMovie()
        {
            //добавить удаление афиш
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query($"DELETE FROM `Cinema`.`Movies` WHERE MovieID = {this.MovieID};\nDELETE FROM `Cinema`.`Billboard` WHERE MovieID = {this.MovieID};" +
                $"DELETE FROM `Cinema`.`Billboard` where MovieID = {this.MovieID}", connection);
            Connection.CloseConnection(connection);
        }

        public static void Insert(int id, string movieName, string genre, int duration)
        {
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query($"INSERT INTO `Cinema`.`Movies` VALUES({id}, '{movieName}', '{genre}', {duration})", connection);
            Connection.CloseConnection(connection);
        }
    }
}
