using System.Windows;
using Common.Models;
using Common.Services;

namespace Storefront.Views
{
    /// <summary>
    /// Interaction logic for TitleCreateSave.xaml
    /// </summary>
    public partial class TitleCreateSave : Window
    {
        public TitleCreateSave()
        {
            InitializeComponent();
            var authorRepository = new AuthorRepository();
            var authors = authorRepository.GetAllAuthors();
            foreach (var author in authors)
            {
                AuthorList.Items.Add(author);
            }
        }

        public List<AuthorModel> AddAuthorModels { get; set; } = new();

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var titleRepository = new TitleRepository();
            var date = DateBox.SelectedDate;
            var yearofpublication = DateOnly.FromDateTime((DateTime)date);
            var newTitle = new TitleModel
            {
                Isbn = IsbnBox.Text,
                Title = TitleBox.Text,
                Language = LanguageBox.Text,
                Pages = Convert.ToInt32(PagesBox.Text),
                Price = Convert.ToDouble(PriceBox.Text),
                YearOfPublication = yearofpublication,
                OrderDetails = null,
                Stock = null,
                Authors = AddAuthorModels
            };
            if (IsbnBox.Text.Length != 13)
            {
                MessageBox.Show("Please enter an ISBN sequence containing 13 digits");
                return;
            }
            var books = titleRepository.GetAllBooks();
            var checkBooks = books.FirstOrDefault(book => book.Isbn == newTitle.Isbn);
            if (checkBooks != null)
            {
                titleRepository.UpdateTitle(newTitle);
                MessageBox.Show("Successfully updated " + newTitle.Title, "Operation completed", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                titleRepository.AddBookTitle(newTitle);
                MessageBox.Show("Successfully added " + newTitle.Title, "Operation completed", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void AddAuthorBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (AuthorList.SelectedItem is AuthorModel selectedAuthor)
            {
                AddAuthorModels.Add(selectedAuthor);
                NewAuthorList.Items.Clear();
                foreach (var author in AddAuthorModels)
                {
                    NewAuthorList.Items.Add(author);
                }
            }
        }

        private void RemoveAuthorBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (NewAuthorList.SelectedItem is AuthorModel selectedAuthor)
            {
                AddAuthorModels.Remove(selectedAuthor);
                NewAuthorList.Items.Clear();
                foreach (var author in AddAuthorModels)
                {
                    NewAuthorList.Items.Add(author);
                }
            }
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
