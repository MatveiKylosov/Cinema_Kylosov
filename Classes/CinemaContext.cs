﻿using Cinema_Kylosov_Finally.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WorkingBD;

namespace Cinema_Kylosov_Finally.Classes
{
    public class CinemaContext : Cinema
    {
        public CinemaContext(int cinemaID, string cinemaName, int numberOfHalls, int numberOfSeats)
            : base(cinemaID, cinemaName, numberOfHalls, numberOfSeats)
        {
        }

        public static List<CinemaContext> AllCinemas()
        {
            List<CinemaContext> allCinemas = new List<CinemaContext>();

            MySqlConnection connection = Connection.OpenConnection();

            MySqlDataReader cinemaQuery = Connection.Query("SELECT * FROM `Cinema`.`Cinemas`", connection);

            while (cinemaQuery.Read())
            {
                allCinemas.Add(new CinemaContext(
                    cinemaQuery.GetInt32(0),
                    cinemaQuery.GetString(1),
                    cinemaQuery.GetInt32(2),
                    cinemaQuery.GetInt32(3)
                    ));
            }

            Connection.CloseConnection(connection);

            return allCinemas;
        }

        public void Delete()
        {
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query($"DELETE FROM `Cinema`.`Cinemas` WHERE CinemaID = {this.CinemaID};\nDELETE FROM `Cinema`.`Billboard` WHERE CinemaID = {this.CinemaID};", connection);
            Connection.CloseConnection(connection);
        }

        //udpate insert
        public void Update(string CinemaName, int NumberOfHalls, int NumberOfSeats)
        {
            this.CinemaName = CinemaName;
            this.NumberOfHalls = NumberOfHalls;
            this.NumberOfSeats = NumberOfSeats;

            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query($"UPDATE `Cinema`.`Cinemas` SET CinemaName = '{this.CinemaName}', NumberOfHalls = {this.NumberOfHalls}, NumberOfSeats = {this.NumberOfSeats} WHERE CinemaID = {this.CinemaID}", connection);
            Connection.CloseConnection(connection);
        }

        public static void Insert(int cinemaID, string cinemaName, int numberOfHalls, int numberOfSeats)
        {
            MySqlConnection connection = Connection.OpenConnection();
            Connection.Query($"INSERT INTO `Cinema`.`Cinemas` VALUES({cinemaID}, '{cinemaName}', {numberOfHalls}, {numberOfSeats})", connection);
            Connection.CloseConnection(connection);
        }
    }

}
