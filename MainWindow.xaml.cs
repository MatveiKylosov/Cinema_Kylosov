using Cinema_Kylosov_Finally.Classes;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WorkingBD;

namespace Cinema_Kylosov_Finally
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Classes.CinemaContext> cinema = Classes.CinemaContext.AllCinemas();
        public List<Classes.BillboardContext> billboard = Classes.BillboardContext.AllBillboards();
        public List<Classes.MovieContext> movies = Classes.MovieContext.AllMovies();

        public static MainWindow main;

        public MainWindow()
        {
            InitializeComponent();
            main = this;
        }

        public void UpdateInfo()
        {
            cinema = Classes.CinemaContext.AllCinemas();
            billboard = Classes.BillboardContext.AllBillboards();
            movies = Classes.MovieContext.AllMovies();
        }

        public void Cinema_Click(object sender, RoutedEventArgs e)
        {
            parent.Children.Clear();
            UpdateInfo();

            parent.Children.Add(new Element.Cinema(null));
            foreach (var x in cinema)
                parent.Children.Add(new Element.Cinema(x));
        }

        public void Billboard_Click(object sender, RoutedEventArgs e)
        {
            parent.Children.Clear();
            UpdateInfo();

            parent.Children.Add(new Element.Billboard(null));
            foreach (var x in billboard)
                parent.Children.Add(new Element.Billboard(x));
        }

        public void Movie_Click(object sender, RoutedEventArgs e)
        {
            parent.Children.Clear();

            parent.Children.Add(new Element.Movie(null));
            UpdateInfo();
            foreach (var x in movies)
                parent.Children.Add(new Element.Movie(x));
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            parent.Children.Clear();
            Windows.FilterWindow filter = new Windows.FilterWindow(cinema, movies);
            filter.ShowDialog();

            if (!filter.ActiveFilter) return;

            if (filter.RBCinema.IsChecked == true)
            {
                List<Classes.CinemaContext> cinemaSort = new List<Classes.CinemaContext>();

                // Поиск по имени
                var checkedItems = filter.CinemaList.Items.OfType<CheckBox>().Where(cb => cb.IsChecked == true);
                var CinemaName = checkedItems.Select(cb => cb.Content.ToString()).ToList();

                if (CinemaName.Any())
                    cinemaSort.AddRange(cinema.Where(x => CinemaName.Contains(x.CinemaName)));

                // Поиск по количеству мест
                if (!string.IsNullOrEmpty(filter.NumberOfSeats.Text))
                {
                    var seatsSort = cinema.Where(x => x.NumberOfSeats == int.Parse(filter.NumberOfSeats.Text)).ToList();
                    if (seatsSort.Any())
                        cinemaSort = cinemaSort.Any() ? cinemaSort.Intersect(seatsSort).ToList() : seatsSort;
                }

                // Поиск по количеству залов
                if (!string.IsNullOrEmpty(filter.NumberOfHalls.Text))
                {
                    var hallsSort = cinema.Where(x => x.NumberOfHalls == int.Parse(filter.NumberOfHalls.Text)).ToList();
                    if (hallsSort.Any())
                        cinemaSort = cinemaSort.Any() ? cinemaSort.Intersect(hallsSort).ToList() : hallsSort;
                }

                foreach (var x in cinemaSort)
                {
                    parent.Children.Add(new Element.Cinema(x));
                    if (filter.ViewBillboardCinema.IsChecked == true)
                    {
                        var matchingBillboards = billboard.Where(i => x.CinemaID == i.CinemaID);
                        foreach (var billboard in matchingBillboards)
                            parent.Children.Add(new Element.Billboard(billboard));
                        
                    }
                }
            }

            else if (filter.RBMovie.IsChecked == true)
            {
                List<Classes.MovieContext> MovieSort = new List<Classes.MovieContext>();

                var checkedItems = filter.MovieList.Items.OfType<CheckBox>().Where(cb => cb.IsChecked == true);
                var MovieName = checkedItems.Select(cb => cb.Content.ToString()).ToList();

                if (MovieName.Any())
                    MovieSort.AddRange(movies.Where(x => MovieName.Contains(x.MovieName)));

                checkedItems = filter.GenreList.Items.OfType<CheckBox>().Where(cb => cb.IsChecked == true);
                var Genre = checkedItems.Select(cb => cb.Content.ToString()).ToList();

                var GenreSort = movies.Where(movie => Genre.Any(genre => movie.Genre.Contains(genre))).ToList();

                if(GenreSort.Any())
                    MovieSort = MovieSort.Any() ? MovieSort.Intersect(GenreSort).ToList() : GenreSort;

                if (!string.IsNullOrEmpty(filter.Duration.Text))
                {
                    var durationSort = movies.Where(x => x.Duration == int.Parse(filter.Duration.Text)).ToList();
                    if(durationSort.Any())
                        MovieSort = MovieSort.Any() ? MovieSort.Intersect(durationSort).ToList() : durationSort;
                }

                foreach (var x in MovieSort)
                {
                    parent.Children.Add(new Element.Movie(x));
                    if(filter.ViewBillboardMovie.IsChecked == true)
                    {
                        var matchingBillboards = billboard.Where(i => x.MovieID == i.MovieID);
                        foreach (var billboard in matchingBillboards)
                            parent.Children.Add(new Element.Billboard(billboard));
                    }
                }
            }
        }
    }
}
