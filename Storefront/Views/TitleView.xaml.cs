using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Common.Models;
using Common.Services;

namespace Storefront.Views
{
    /// <summary>
    /// Interaction logic for TitleView.xaml
    /// </summary>
    public partial class TitleView : UserControl
    {
        public TitleView()
        {
            InitializeComponent();
            var titleRepository = new TitleRepository();
            var titles = titleRepository.GetAllBooks();
            foreach (var title in titles)
            {
                TitleList.Items.Add(title);
            }

            TitleRepository.TitleListChanged += titleRepository_TitleListChanged;
        }
        private void titleRepository_TitleListChanged()
        {
            TitleList.Items.Clear();
            var titleRepository = new TitleRepository();
            var titles = titleRepository.GetAllBooks();
            foreach (var title in titles)
            {
                TitleList.Items.Add(title);
            }
        }

        private void TitleList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AuthorListTitle.Items.Clear();
            if (TitleList.SelectedItem is TitleModel selectedTitle)
            {
                foreach (var author in selectedTitle.Authors)
                {
                    AuthorListTitle.Items.Add(author);
                }
            }
        }

        private void CreateSaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            TitleCreateSave window = new TitleCreateSave();
            window.Show();
        }

        private void ViewBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (TitleList.SelectedItem is TitleModel selectedTitle)
            {
                var date = selectedTitle.YearOfPublication.ToString();
                var parseddate = DateTime.Parse(date);

                TitleCreateSave window = new TitleCreateSave();
                window.IsbnBox.Text = selectedTitle.Isbn;
                window.TitleBox.Text = selectedTitle.Title;
                window.LanguageBox.Text = selectedTitle.Language;
                window.PagesBox.Text = selectedTitle.Pages.ToString();
                window.PriceBox.Text = selectedTitle.Price.ToString();
                window.DateBox.SelectedDate = parseddate;
                foreach (var author in selectedTitle.Authors)
                {
                    window.AddAuthorModels.Add(author);
                    window.NewAuthorList.Items.Add(author);
                }
                window.Show();
            }
        }
        private void RemoveTitleBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (TitleList.SelectedItem is TitleModel selectedTitle)
            {
                if (selectedTitle.Stock.Count > 0)
                {
                    MessageBox.Show("Title with ISBN: " + selectedTitle.Isbn + " is still in stock, please remove stock entries in Store before deleting", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                selectedTitle.Authors.Clear();
                var titleRepository = new TitleRepository();
                titleRepository.RemoveTitle(selectedTitle);
                MessageBox.Show("Successfully removed " + selectedTitle.Title, "Operation completed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            TitleList.Items.Clear();
            var titleRepository = new TitleRepository();
            var titles = titleRepository.GetAllBooks();
            foreach (var title in titles)
            {
                TitleList.Items.Add(title);
            }
        }
    }
}
