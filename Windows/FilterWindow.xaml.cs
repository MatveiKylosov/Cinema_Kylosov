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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Cinema_Kylosov_Finally.Windows
{
    /// <summary>
    /// Логика взаимодействия для FilterWindow.xaml
    /// </summary>
    public partial class FilterWindow : Window
    {
        List<Classes.CinemaContext> cinemas;
        List<Classes.MovieContext> movies;

        public bool ActiveFilter = false;

        public FilterWindow(List<Classes.CinemaContext> cinemas, List<Classes.MovieContext> movies)
        {
            InitializeComponent();

            this.cinemas = cinemas;
            this.movies = movies;

            foreach(var x in cinemas)
            {
                CheckBox cb = new CheckBox() { Content = x.CinemaName };
                cb.Click += CinemaCheckedBox;
                CinemaList.Items.Add(cb);
            }

            List <string> genre = new List<string>();

            foreach(var x in movies)
            {
                CheckBox cb = new CheckBox() { Content = x.MovieName };
                cb.Click += MovieCheckedBox;
                MovieList.Items.Add(cb);

                if (x.Genre.Any(f => f == ','))
                {
                    string[] str = x.Genre.Split(',');
                    foreach (var i in str)
                        if (!genre.Exists(f => f == i))
                            genre.Add(i);
                }
                else 
                    GenreList.Items.Add(new CheckBox() { Content = x.Genre });
            }

            foreach (var x in genre)
                GenreList.Items.Add(new CheckBox() { Content = x });
        }

        private void MovieChecked(object sender, RoutedEventArgs e)
        {
            FilterCinema.IsEnabled = false;
            FilterMovie.IsEnabled = true;
        }

        private void CinemaChecked(object sender, RoutedEventArgs e)
        {
            FilterCinema.IsEnabled = true;
            FilterMovie.IsEnabled = false;
        }

        private void CinemaCheckedBox(object sender, RoutedEventArgs e)
        {
            bool check = CinemaList.Items.OfType<CheckBox>().Any(cb => cb.IsChecked == true);

            ViewBillboardCinema.IsEnabled = check;
        }

        private void MovieCheckedBox(object sender, RoutedEventArgs e)
        {
            bool check = MovieList.Items.OfType<CheckBox>().Any(cb => cb.IsChecked == true);

            ViewBillboardMovie.IsEnabled = check;
        }

        private void ApplyClick(object sender, RoutedEventArgs e)
        {
            ActiveFilter = true;
            this.Hide();
        }

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            foreach (var x in MovieList.Items)
                ((CheckBox)x).IsChecked = false;

            foreach (var x in CinemaList.Items)
                ((CheckBox)x).IsChecked = false;

            foreach (var x in GenreList.Items)
                ((CheckBox)x).IsChecked = false;

            Duration.Text = NumberOfHalls.Text = NumberOfSeats.Text = "";

            RBMovie.IsChecked = RBCinema.IsChecked = ViewBillboardCinema.IsChecked = ViewBillboardMovie.IsChecked = false;
        }

        private void NumberOfHalls_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void Duration_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void NumberOfSeats_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }
    }
}
