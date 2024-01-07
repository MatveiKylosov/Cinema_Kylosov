using Cinema_Kylosov_Finally.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        bool change = false;
        public Movie(Classes.MovieContext movie = null)
        {
            InitializeComponent();
            this.movie = movie;

            if(movie != null)
            {
                Name.Text = movie.MovieName;
                Genre.Text = movie.Genre;
                Duration.Text = $"{movie.Duration}";
            }
            else
            {
                AddGrid.Visibility      = Visibility.Visible;
                TBName.Visibility       = Visibility.Visible;
                TBGenre.Visibility      = Visibility.Visible;
                TBDuration.Visibility   = Visibility.Visible;

                TBName.Width = 100;
                TBGenre.Width = 100;
                TBDuration.Width = 100;

                EditAddBTN.Click -= EditMovie_Click;
                DeleteCancelBTN.Click -= DeleteMovie_Click;

                EditAddBTN.Click += AddMovie_Click;
                DeleteCancelBTN.Click += CancelMovie_Click;

                EditAddBTN.Content = "Добавить";
                DeleteCancelBTN.Content = "Отменить";
            }
        }

        private void Add_Click(object sender, MouseButtonEventArgs e) => AddGrid.Visibility = Visibility.Hidden;

        private void EditMovie_Click(object sender, RoutedEventArgs e)
        {
            if (change)
            {
                bool noChanges = (Name.Text == TBName.Text & Genre.Text == TBGenre.Text & Duration.Text == TBDuration.Text) ||
                                 (TBName.Text == "" || TBGenre.Text == "" || TBDuration.Text == "");
                if (!noChanges)
                {
                    Name.Text = TBName.Text;
                    Genre.Text = TBGenre.Text;
                    Duration.Text = TBDuration.Text;
                    movie.Update(Name.Text, Genre.Text, int.Parse(Duration.Text));
                }

                TBName.Visibility = Visibility.Hidden;
                TBDuration.Visibility = Visibility.Hidden;
                TBGenre.Visibility = Visibility.Hidden;

                Name.Visibility = Visibility.Visible;
                Duration.Visibility = Visibility.Visible;
                Genre.Visibility = Visibility.Visible;

                change = false;
            }
            else
            {
                TBName.Text = Name.Text;
                TBGenre.Text = Genre.Text;
                TBDuration.Text = Duration.Text;

                TBName.Visibility = Visibility.Visible;
                TBDuration.Visibility = Visibility.Visible;
                TBGenre.Visibility = Visibility.Visible;

                Name.Visibility = Visibility.Hidden;
                Duration.Visibility = Visibility.Hidden;
                Genre.Visibility = Visibility.Hidden;

                DeleteCancelBTN.Content = "Отменить";

                change = true;
            }
        }

        private void DeleteMovie_Click(object sender, RoutedEventArgs e)
        {
            if (change)
            {
                TBName.Visibility = Visibility.Hidden;
                TBDuration.Visibility = Visibility.Hidden;
                TBGenre.Visibility = Visibility.Hidden;

                Name.Visibility = Visibility.Visible;
                Duration.Visibility = Visibility.Visible;
                Genre.Visibility = Visibility.Visible;

                change = false;
                DeleteCancelBTN.Content = "Удалить";
            }
            else
            {
                List<Classes.BillboardContext> billboardContexts = MainWindow.main.billboard.FindAll(x => x.MovieID == movie.MovieID);

                if (billboardContexts.Count > 0)
                    if (MessageBox.Show($"У этого фильма есть {billboardContexts.Count} афиши.\nАфиши тоже удаляться.\nПродолжить удаление?", $"Удаление фильма {movie.MovieName}", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                        return;

                this.Visibility = Visibility.Visible;
                this.Height = 0;
                this.Width = 0;

                movie.DeleteMovie();
            }
        }

        private void AddMovie_Click(object sender, RoutedEventArgs e)
        {
            if (TBName.Text != "" && TBGenre.Text != "" && TBDuration.Text != "")
                if (MainWindow.main.cinema.Find(x => x.CinemaName == TBName.Text) == null)
                {
                    int index = MainWindow.main.movies.Count == 0 ? 0 : MainWindow.main.movies[MainWindow.main.movies.Count - 1].MovieID + 1;
                    Classes.MovieContext.Insert(index, TBName.Text, TBGenre.Text, int.Parse(TBDuration.Text));
                    MainWindow.main.Movie_Click(null, null);
                }
                else MessageBox.Show($"Такой фильм существует!", $"Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show($"Заполните все данные!", $"Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void CancelMovie_Click(object sender, RoutedEventArgs e)
        {
            TBName.Text = "";
            TBDuration.Text = "";
            TBGenre.Text = "";
            AddGrid.Visibility = Visibility.Visible;
        }

        private void DigitCheck(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }

        private void GenreCheck(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^а-яА-Я0-9,-]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
