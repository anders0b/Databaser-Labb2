using Common.Models;
using System.Windows;
using Common.Services;

namespace Storefront.Views
{
    /// <summary>
    /// Interaction logic for StoreCreateSave.xaml
    /// </summary>
    public partial class StoreCreateSave : Window
    {
        public StoreCreateSave()
        {
            InitializeComponent();
        }
        public List<EmployeeModel> AddEmployeeModels { get; set; } = new();

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var storeRepository = new StoreRepository();
            var newStore = new StoreModel();
            if (StoreID.Text == "")
            {
                newStore.Name = NameBox.Text;
                newStore.StreetAddress = AddressBox.Text;
                newStore.PostalCode = Convert.ToInt32(PostalCodeBox.Text);
                newStore.City = CityBox.Text;
                newStore.Employees = AddEmployeeModels;
                storeRepository.AddStore(newStore);
                MessageBox.Show("Successfully added " + newStore.Name);
                this.Close();
            }
            else
            {
                newStore.Id = Convert.ToInt32(StoreID.Text);
                newStore.Name = NameBox.Text;
                newStore.StreetAddress = AddressBox.Text;
                newStore.PostalCode = Convert.ToInt32(PostalCodeBox.Text);
                newStore.City = CityBox.Text;
                newStore.Employees = AddEmployeeModels;
                storeRepository.UpdateStore(newStore);
                MessageBox.Show("Successfully updated " + newStore.Name);
                this.Close();
            }
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
