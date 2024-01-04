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
    /// Логика взаимодействия для Billboard.xaml
    /// </summary>
    public partial class Billboard : UserControl
    {
        Classes.BillboardContext billboard;
        public Billboard(Classes.BillboardContext billboard, string movie)
        {
            InitializeComponent();
            this.billboard = billboard;
            ID.Text = $"{billboard.BillboardID}";
            Movie.Text = $"{movie}";
            Time.Text = $"{billboard.ShowTime.ToString("dd.MM.yyy HH:mm")}";
            Tickets.Text = $"{billboard.TicketPrice}";
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
