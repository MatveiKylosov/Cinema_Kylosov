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
        bool change = false;

        Classes.CinemaContext cinema;
        public Cinema(Classes.CinemaContext cinema = null)
        {
            InitializeComponent();
            this.cinema = cinema;
            
            if(cinema != null)
            {
                Name.Text = cinema.CinemaName;
                Number_of_Halls.Text = $"{cinema.NumberOfHalls}";
                Number_of_Seats.Text = $"{cinema.NumberOfSeats}";
            }
            else
            {
                AddGrid.Visibility = Visibility.Visible;
                TBName.Visibility = Visibility.Visible;
                TBNumber_of_Halls.Visibility = Visibility.Visible;
                TBNumber_of_Seats.Visibility = Visibility.Visible;

                TBName.Width = 100;
                TBNumber_of_Halls.Width = 100;
                TBNumber_of_Seats.Width = 100;

                EditAddBTN.Click -= EditCinema_Click;
                DeleteCancelBTN.Click -= DeleteCinema_Click;

                EditAddBTN.Click += AddCinema_Click; 
                DeleteCancelBTN.Click += CancelCinema_Click;

                EditAddBTN.Content = "Добавить";
                DeleteCancelBTN.Content = "Отменить";
            }
        }

        private void EditCinema_Click(object sender, RoutedEventArgs e)
        {
            if (change)
            {
                bool cinemaExists = MainWindow.main.cinema.Find(x => x.CinemaName == TBName.Text) != null & Name.Text != TBName.Text;
                bool noChanges = (Name.Text == TBName.Text & Number_of_Halls.Text == TBNumber_of_Halls.Text & Number_of_Seats.Text == TBNumber_of_Seats.Text) || 
                                 (TBName.Text == "" || TBNumber_of_Halls.Text == "" || TBNumber_of_Seats.Text == "");

                if (cinemaExists || noChanges)
                {
                    if (cinemaExists)
                        MessageBox.Show($"Такой кинотеатр существует!", $"Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);

                    TBName.Visibility = Visibility.Hidden;
                    TBNumber_of_Halls.Visibility = Visibility.Hidden;
                    TBNumber_of_Seats.Visibility = Visibility.Hidden;
                    change = false;
                }
                else
                {
                    Name.Text = TBName.Text;
                    Number_of_Halls.Text = TBNumber_of_Halls.Text;
                    Number_of_Seats.Text = TBNumber_of_Seats.Text;

                    TBName.Visibility = Visibility.Hidden;
                    TBNumber_of_Halls.Visibility = Visibility.Hidden;
                    TBNumber_of_Seats.Visibility = Visibility.Hidden;

                    cinema.Update(Name.Text, int.Parse(Number_of_Halls.Text), int.Parse(Number_of_Seats.Text));

                    change = false;
                }

            }
            else
            {
                TBName.Text = Name.Text;
                TBNumber_of_Halls.Text = Number_of_Halls.Text;
                TBNumber_of_Seats.Text = Number_of_Seats.Text;

                TBName.Visibility = Visibility.Visible;
                TBNumber_of_Halls.Visibility = Visibility.Visible;
                TBNumber_of_Seats.Visibility = Visibility.Visible;

                change = true;
            }
        }

        private void DeleteCinema_Click(object sender, RoutedEventArgs e)
        {
            List<Classes.BillboardContext> billboardContexts = MainWindow.main.billboard.FindAll(x => x.CinemaID == cinema.CinemaID);

            if (billboardContexts.Count > 0)
                if (MessageBox.Show($"У этого кинотеатра есть {billboardContexts.Count} афиши.\nАфиши тоже удаляться.\nПродолжить удаление?", $"Удаление кинотеатра {cinema.CinemaName}", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    return;

            this.Visibility = Visibility.Visible;
            this.Height = 0;
            this.Width = 0;

            cinema.Delete();
        }

        private void AddCinema_Click(object sender, RoutedEventArgs e)
        {
            if (TBName.Text != "" && TBNumber_of_Halls.Text != "" && TBNumber_of_Seats.Text != "")
                if (MainWindow.main.cinema.Find(x => x.CinemaName == TBName.Text) == null)
                {
                    Classes.CinemaContext.Insert(MainWindow.main.cinema[MainWindow.main.cinema.Count - 1].CinemaID + 1, TBName.Text, int.Parse(TBNumber_of_Halls.Text), int.Parse(TBNumber_of_Seats.Text));
                    MainWindow.main.Cinema_Click(null,null);
                }
                else MessageBox.Show($"Такой кинотеатр существует!", $"Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
            else MessageBox.Show($"Заполните все данные!", $"Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void CancelCinema_Click(object sender, RoutedEventArgs e)
        {
            TBName.Text = "";
            TBNumber_of_Halls.Text = "";
            TBNumber_of_Seats.Text = "";
            AddGrid.Visibility = Visibility.Visible;
        }


        private void Add_Click(object sender, MouseButtonEventArgs e) => AddGrid.Visibility = Visibility.Hidden;

        private void DigitCheck(object sender, TextCompositionEventArgs e)
        {
           if (!Char.IsDigit(e.Text, 0)) 
                e.Handled = true;
        }
    }
}
