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
        List<Classes.CinemaContext> cinema = Classes.CinemaContext.AllCinemas();
        List<Classes.BillboardContext> billboard = Classes.BillboardContext.AllBillboards();
        List<Classes.MovieContext> movies = Classes.MovieContext.AllMovies();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Cinema_Click(object sender, RoutedEventArgs e)
        {
            parent.Children.Clear();

            foreach(var x in cinema)
                parent.Children.Add(new Element.Cinema(x));
        }

        private void Billboard_Click(object sender, RoutedEventArgs e)
        {
            parent.Children.Clear();

            foreach (var x in billboard)
                parent.Children.Add(new Element.Billboard(x, movies.Find(f => x.MovieID == f.MovieID).MovieName));
        }

        private void Movie_Click(object sender, RoutedEventArgs e)
        {
            parent.Children.Clear();

            foreach (var x in movies)
                parent.Children.Add(new Element.Movie(x));
        }
    }
}
