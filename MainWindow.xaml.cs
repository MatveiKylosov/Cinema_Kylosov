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

        }
    }
}
