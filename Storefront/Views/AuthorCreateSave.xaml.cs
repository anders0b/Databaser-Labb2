using System.Windows;
using Common.Models;
using Common.Services;

namespace Storefront.Views
{
    /// <summary>
    /// Interaction logic for AuthorCreateSave.xaml
    /// </summary>
    public partial class AuthorCreateSave : Window
    {
        public AuthorCreateSave()
        {
            InitializeComponent();
            var titleRepository = new TitleRepository();
            var titles = titleRepository.GetAllBooks();
            foreach (var title in titles)
            {
                TitleList.Items.Add(title);
            }
        }
        public List<TitleModel> AddTitleModels { get; set; } = new();

        private void AddTitleBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (TitleList.SelectedItem is TitleModel selectedTitle)
            {
                AddTitleModels.Add(selectedTitle);
                NewTitleList.Items.Clear();
                foreach (var title in AddTitleModels)
                {
                    NewTitleList.Items.Add(title);
                }
            }
        }

        private void RemoveTitleBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (TitleList.SelectedItem is TitleModel selectedTitle)
            {
                AddTitleModels.Remove(selectedTitle);
                NewTitleList.Items.Clear();
                foreach (var title in AddTitleModels)
                {
                    NewTitleList.Items.Add(title);
                }
            }
        }

        private void SaveBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var authorRepository = new AuthorRepository();
            var date = DateBox.SelectedDate;
            var dateofbirth = DateOnly.FromDateTime((DateTime)date);
            var newAuthor = new AuthorModel();
            if (AuthorID.Text == "")
            {
                newAuthor.FirstName = FirstNameBox.Text;
                newAuthor.LastName = LastNameBox.Text;
                newAuthor.DateOfBirth = dateofbirth;
                newAuthor.Isbns = AddTitleModels;
                authorRepository.AddAuthor(newAuthor);
                MessageBox.Show("Successfully added " + newAuthor.FirstName + " " + newAuthor.LastName);
                this.Close();
            }
            else
            {
                newAuthor.Id = Convert.ToInt32(AuthorID.Text);
                newAuthor.FirstName = FirstNameBox.Text;
                newAuthor.LastName = LastNameBox.Text;
                newAuthor.DateOfBirth = dateofbirth;
                newAuthor.Isbns = AddTitleModels;
                authorRepository.UpdateAuthor(newAuthor);
                MessageBox.Show("Successfully updated " + newAuthor.FirstName + " " + newAuthor.LastName);
                this.Close();
            }

        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
