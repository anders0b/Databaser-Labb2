using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Common.Models;
using Common.Services;
using static System.Net.Mime.MediaTypeNames;

namespace Storefront.Views
{
    /// <summary>
    /// Interaction logic for StoreView.xaml
    /// </summary>
    public partial class StoreView : UserControl
    {
        public StoreView()
        {
            InitializeComponent();
            var storeRepository = new StoreRepository();
            var stores = storeRepository.GetAllStores();
            foreach (var store in stores)
            {
                StoreList.Items.Add(store);
            }
            StoreRepository.StoreListChanged += storeRepository_StoreListChanged;
            StockRepository.StockListChanged += stockRepository_StockListChanged;
        }
        private void storeRepository_StoreListChanged()
        {
            StoreList.Items.Clear();
            var storeRepository = new StoreRepository();
            var stores = storeRepository.GetAllStores();
            foreach (var store in stores)
            {
                StoreList.Items.Add(store);
            }
        }
        private void stockRepository_StockListChanged()
        {
            storeRepository_StoreListChanged();
        }

        private void StoreList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StockList.Items.Clear();
            EmployeeList.Items.Clear();
            if (StoreList.SelectedItem is StoreModel selectedStore)
            { 
                foreach (var employee in selectedStore.Employees)
                {
                    EmployeeList.Items.Add(employee);
                }
                foreach (var stock in selectedStore.Stocks)
                {
                    StockList.Items.Add(stock);
                }
            }
            
        }

        private void CreateSaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            StoreCreateSave window = new StoreCreateSave();
            window.Show();
        }

        private void RemoveStoreBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (StoreList.SelectedItem is StoreModel selectedStore)
            {
                if (selectedStore.Stocks.Count > 0)
                {
                    MessageBox.Show("The store you are trying to remove still has stock balance, please remove stock before removing the store.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var storeRepository = new StoreRepository();
                storeRepository.RemoveStore(selectedStore);
                MessageBox.Show("Successfully removed " + selectedStore.Name, "Operation completed", MessageBoxButton.OK, MessageBoxImage.Information);

            }
        }

        private void ViewBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (StoreList.SelectedItem is StoreModel selectedStore)
            {

                StoreCreateSave window = new StoreCreateSave();
                window.StoreID.Text = selectedStore.Id.ToString();
                window.NameBox.Text = selectedStore.Name;
                window.AddressBox.Text = selectedStore.StreetAddress;
                window.PostalCodeBox.Text = selectedStore.PostalCode.ToString();
                window.CityBox.Text = selectedStore.City;                
                foreach (var employee in selectedStore.Employees)
                {
                    window.AddEmployeeModels.Add(employee);
                    window.EmployeeList.Items.Add(employee);
                }
                window.Show();
            }
        }

        private void EditStockBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (StockList.SelectedItem is StockModel selectedStock)
            {
                EditStockAmount window = new EditStockAmount();
                window.StoreBox.Text = selectedStock.StoreId.ToString();
                window.TitleBox.Text = selectedStock.Isbnid;
                window.AmountBox.Text = selectedStock.NumberOfBooks.ToString();
                window.Show();
            }
        }

        private void NewStockBtn_OnClick(object sender, RoutedEventArgs e)
        {
            EditStoreStock window = new EditStoreStock();
            window.Show();
        }

        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            StoreList.Items.Clear();
            var storeRepository = new StoreRepository();
            var stores = storeRepository.GetAllStores();
            foreach (var store in stores)
            {
                StoreList.Items.Add(store);
            }
        }

        private void DeleteStockBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (StockList.SelectedItem is StockModel selectedStock)
            {
                var stockRepository = new StockRepository();
                stockRepository.DeleteStock(selectedStock);
                MessageBox.Show(selectedStock.Isbnid + " has been removed from store", "Operation completed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
