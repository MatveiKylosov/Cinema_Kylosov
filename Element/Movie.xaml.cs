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
    /// Логика взаимодействия для Movie.xaml
    /// </summary>
    public partial class Movie : UserControl
    {
        Classes.MovieContext movie;

        public Movie(Classes.MovieContext movie)
        {
            InitializeComponent();
            this.movie = movie;

            Name.Text = movie.MovieName;
            Genre.Text = movie.Genre;
            Duration.Text = $"{movie.Duration}";
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
