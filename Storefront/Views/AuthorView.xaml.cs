using System.Windows;
using Common.Models;
using Common.Services;
using System.Windows.Controls;

namespace Storefront.Views
{
    /// <summary>
    /// Interaction logic for AuthorView.xaml
    /// </summary>
    public partial class AuthorView : UserControl
    {
        public AuthorView()
        {
            InitializeComponent();
            var authorRepository = new AuthorRepository();
            var authors = authorRepository.GetAllAuthors();
            foreach (var author in authors)
            {
                AuthorList.Items.Add(author);
            }
            AuthorRepository.AuthorListChanged += AuthorRepository_AuthorListChanged;

        }
        private void AuthorRepository_AuthorListChanged()
        {
            AuthorList.Items.Clear();
            var authorRepository = new AuthorRepository();
            var authors = authorRepository.GetAllAuthors();
            foreach (var author in authors)
            {
                AuthorList.Items.Add(author);
            }
        }

        private void AuthorList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AuthorTitleList.Items.Clear();
            if (AuthorList.SelectedItem is AuthorModel selectedAuthor)
            {
                foreach (var title in selectedAuthor.Isbns)
                {
                    AuthorTitleList.Items.Add(title);
                }
            }
        }

        private void CreateSaveAuthorBtn_OnClick(object sender, RoutedEventArgs e)
        {
            AuthorCreateSave window = new AuthorCreateSave();
            window.Show();
        }

        private void RemoveAuthorBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (AuthorList.SelectedItem is AuthorModel selectedAuthor)
            {
                selectedAuthor.Isbns.Clear();
                var authorRepository = new AuthorRepository();
                authorRepository.RemoveAuthor(selectedAuthor);
                MessageBox.Show("Successfully removed author " + selectedAuthor.FirstName + " " + selectedAuthor.LastName + ". Please take note of any books missing authors", "Operation completed", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void EditAuthorBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (AuthorList.SelectedItem is AuthorModel selectedAuthor)
            {
                var date = selectedAuthor.DateOfBirth.ToString();
                var parseddate = DateTime.Parse(date);

                AuthorCreateSave window = new AuthorCreateSave();
                window.AuthorID.Text = selectedAuthor.Id.ToString();
                window.FirstNameBox.Text = selectedAuthor.FirstName;
                window.LastNameBox.Text = selectedAuthor.LastName;
                window.DateBox.SelectedDate = parseddate;
                foreach (var title in selectedAuthor.Isbns)
                {
                    window.AddTitleModels.Add(title);
                    window.NewTitleList.Items.Add(title);
                }
                window.Show();
            }
        }

        private void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            AuthorList.Items.Clear();
            var authorRepository = new AuthorRepository();
            var authors = authorRepository.GetAllAuthors();
            foreach (var author in authors)
            {
                AuthorList.Items.Add(author);
            }
        }
    }
}
