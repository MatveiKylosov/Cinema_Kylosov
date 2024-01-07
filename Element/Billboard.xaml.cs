using Cinema_Kylosov_Finally.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Xml.Linq;

namespace Cinema_Kylosov_Finally.Element
{
    /// <summary>
    /// Логика взаимодействия для Billboard.xaml
    /// </summary>
    public partial class Billboard : UserControl
    {
        Classes.BillboardContext billboard;
        bool change = false;

        public Billboard(Classes.BillboardContext billboard = null)
        {
            InitializeComponent();

            if(billboard != null)
            {
                this.billboard = billboard;
                CinemaName.Text = $"{MainWindow.main.cinema.Find(x => x.CinemaID == billboard.CinemaID).CinemaName}";
                BillboardID.Text = $"{billboard.BillboardID}";
                MovieName.Text = $"{MainWindow.main.movies.Find(x => x.MovieID == billboard.MovieID).MovieName}";
                Time.Text = $"{billboard.ShowTime.ToString("dd.MM.yyy HH:mm")}";
                Tickets.Text = $"{billboard.NumberOfTickets}";
                TicketPrice.Text = $"{billboard.TicketPrice.ToString(CultureInfo.InvariantCulture)}";
            }
            else
            {
                AddGrid.Visibility = Visibility.Visible;
                CBCinemaName.Visibility = Visibility.Visible;
                CBMovieName.Visibility = Visibility.Visible;
                TBTime.Visibility = Visibility.Visible;
                TBTickets.Visibility = Visibility.Visible;
                TBTicketPrice.Visibility = Visibility.Visible;

                CBCinemaName.Width = 100;
                CBMovieName.Width = 100;
                TBTime.Width = 100;
                TBTickets.Width = 100;
                TBTicketPrice.Width = 100;

                EditAddBTN.Click -= EditBillboard_Click;
                DeleteCancelBTN.Click -= DeleteBillboard_Click;

                EditAddBTN.Click += AddBillboard_Click;
                DeleteCancelBTN.Click += CancelBillboard_Click;

                EditAddBTN.Content = "Добавить";
                DeleteCancelBTN.Content = "Отменить";
            }

            foreach(var x in MainWindow.main.cinema)
                CBCinemaName.Items.Add(x.CinemaName);

            foreach(var x in MainWindow.main.movies)
                CBMovieName.Items.Add(x.MovieName);
        }

        private void EditBillboard_Click(object sender, RoutedEventArgs e)
        {
            if (change)
            {
                bool NoChanges = (Time.Text == TBTime.Text & Tickets.Text == TBTickets.Text & CinemaName.Text == CBCinemaName.SelectedItem.ToString() & MovieName.Text == CBMovieName.SelectedItem.ToString()) ||
                                 (TBTicketPrice.Text == "" || TBTickets.Text == "" || TBTime.Text == "");
                bool TimeParse = DateTime.TryParse(TBTime.Text, out DateTime time);

                CinemaName.Visibility = Visibility.Visible;
                MovieName.Visibility = Visibility.Visible;
                Time.Visibility = Visibility.Visible;
                Tickets.Visibility = Visibility.Visible;
                TicketPrice.Visibility = Visibility.Visible;

                CBCinemaName.Visibility = Visibility.Hidden;
                CBMovieName.Visibility = Visibility.Hidden;
                TBTime.Visibility = Visibility.Hidden;
                TBTickets.Visibility = Visibility.Hidden;
                TBTicketPrice.Visibility = Visibility.Hidden;

                if (!NoChanges && TimeParse)
                {
                    if(!TimeParse)
                        MessageBox.Show($"Время указано не правильно!", $"Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);

                    Time.Text = TBTime.Text;
                    Tickets.Text = TBTickets.Text;
                    CinemaName.Text = CBCinemaName.SelectedItem.ToString();
                    MovieName.Text = CBMovieName.SelectedItem.ToString();
                    TicketPrice.Text = TBTicketPrice.Text;

                    billboard.UpdateBillboard(CinemaName.Text, MovieName.Text, time, decimal.Parse(TicketPrice.Text, CultureInfo.InvariantCulture), int.Parse(Tickets.Text));
                }

                change = false;
            }
            else
            {
                TBTime.Text = Time.Text;
                TBTickets.Text = Tickets.Text;
                TBTicketPrice.Text = TicketPrice.Text;

                int CinemaIndex;
                int MovieIndex;

                for (CinemaIndex = 0; CinemaIndex < CBCinemaName.Items.Count; CinemaIndex++)
                    if ((string)CBCinemaName.Items[CinemaIndex] == CinemaName.Text)
                        break;

                for (MovieIndex = 0; MovieIndex < CBMovieName.Items.Count; MovieIndex++)
                    if ((string)CBMovieName.Items[MovieIndex] == MovieName.Text)
                        break;
                
                CBCinemaName.SelectedIndex = CinemaIndex;
                CBMovieName.SelectedIndex = MovieIndex;

                CinemaName.Visibility = Visibility.Hidden;
                MovieName.Visibility = Visibility.Hidden;
                Time.Visibility = Visibility.Hidden;
                Tickets.Visibility = Visibility.Hidden;
                TicketPrice.Visibility = Visibility.Hidden;

                CBCinemaName.Visibility = Visibility.Visible;
                CBMovieName.Visibility = Visibility.Visible;
                TBTime.Visibility = Visibility.Visible;
                TBTickets.Visibility = Visibility.Visible;
                TBTicketPrice.Visibility = Visibility.Visible;

                change = true;
            }
        }

        private void DeleteBillboard_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Visible;
            this.Height = 0;
            this.Width = 0;

            billboard.DeleteBillboard();
        }

        private void AddBillboard_Click(object sender, RoutedEventArgs e)
        {
            if(CBCinemaName.SelectedIndex != -1 & 
               CBMovieName.SelectedIndex != -1 & 
               !string.IsNullOrWhiteSpace(TBTime.Text) & 
               DateTime.TryParse(TBTime.Text, out DateTime time) &
               TBTickets.Text != "" & TBTicketPrice.Text != "")
            {

                Classes.BillboardContext.Insert(MainWindow.main.billboard[MainWindow.main.billboard.Count - 1].BillboardID + 1, 
                                                CBCinemaName.Text,
                                                CBMovieName.Text,
                                                time,
                                                decimal.Parse(TBTicketPrice.Text, CultureInfo.InvariantCulture),
                                                int.Parse(Tickets.Text));

                MainWindow.main.Billboard_Click(null, null);

            }
            else
                MessageBox.Show($"Заполните все данные правильно!", $"Внимание!", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void CancelBillboard_Click(object sender, RoutedEventArgs e)
        {
            CBCinemaName.SelectedIndex = -1;
            CBMovieName.SelectedIndex = -1;
            TBTime.Text = "";
            TBTime.Text = "";
            TBTickets.Text = "";
            TBTicketPrice.Text = "";

            AddGrid.Visibility = Visibility.Visible;
        }

        private void Add_Click(object sender, MouseButtonEventArgs e) => AddGrid.Visibility = Visibility.Hidden;

        private void DecimalCheck(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ".") && (!TBTicketPrice.Text.Contains(".") && TBTicketPrice.Text.Length != 0)))
                e.Handled = true;
        }

        private void DigitCheck(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }
    }
}
