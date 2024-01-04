using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cinema_Kylosov_Finally.Element
{
    /// <summary>
    /// Логика взаимодействия для Cinema.xaml
    /// </summary>
    public partial class Cinema : UserControl
    {
        Classes.CinemaContext cinema;
        public Cinema(Classes.CinemaContext cinema)
        {
            InitializeComponent();
            this.cinema = cinema;
            
            Name.Text = cinema.CinemaName;
            Number_of_Halls.Text = $"{cinema.NumberOfHalls}";
            Number_of_Seats.Text = $"{cinema.NumberOfSeats}";
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
