using Common.Models;
using Common.Services;
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
using System.Windows.Shapes;

namespace Storefront.Views
{
    /// <summary>
    /// Interaction logic for EditStockAmount.xaml
    /// </summary>
    public partial class EditStockAmount : Window
    {
        public EditStockAmount()
        {
            InitializeComponent();
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var stockRepository = new StockRepository();
            var newstock = new StockModel
            {
                StoreId = Convert.ToInt32(StoreBox.Text),
                Isbnid = TitleBox.Text,
                NumberOfBooks = Convert.ToInt32(AmountBox.Text),
            };
            stockRepository.AddStock(newstock);
            MessageBox.Show("Edited stock balance!", "Operation successful", MessageBoxButton.OK);
            this.Close();
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
