using System.Windows;
using Common.Models;
using Common.Services;
using Labb2_DbFirst_Template.Entities;
using Microsoft.EntityFrameworkCore;

namespace Storefront.Views
{
    /// <summary>
    /// Interaction logic for EditStoreStock.xaml
    /// </summary>
    public partial class EditStoreStock : Window
    {
        public EditStoreStock()
        {
            InitializeComponent();
            var stockRepository = new StockRepository();
            var stocks = stockRepository.GetAllStock();
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
            MessageBox.Show("Added stock!", "Operation successful", MessageBoxButton.OK);
            this.Close();
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
