using System.Text.RegularExpressions;
using Book_Shelf.Services;
using Book_Shelf.Models;
using Book_Shelf.Repositories;
using Book_Shelf.ViewModels;
using System.Threading.Tasks;
namespace Book_Shelf.Views
{
    public partial class RegisterBookPage : ContentPage
    {
        private SearchedBook? book = null;
        private RegisterBookPageViewModel _viewModel;
        public RegisterBookPage(RegisterBookPageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
        }
        private async void OnSearchClicked(object sender, EventArgs e)
        {
            var isbin = IsbinEntry.Text?.Trim();

            if (string.IsNullOrEmpty(isbin) || !IsValidIsbin(isbin))
            {
                IsbinBorder.Stroke = Colors.Red;
                ErrorLabel.Text = "ISBINが空か、形式が正しくありません。";
                ErrorLabel.IsVisible = true;
                ResultCard.IsVisible = false;
                return;
            }

            IsbinBorder.Stroke = Colors.Gray;
            ErrorLabel.IsVisible = false;

            book = await _viewModel.LookupBookByIsbin(isbin);

            if (book == null)
            {
                DisplayAlert("検索失敗", "書籍が見つかりませんでした。", "OK");
                ResultCard.IsVisible = false;
                return;
            }

            // 表示更新
            TitleLabel.Text = book.Title;
            AuthorLabel.Text = book.Author;
            CoverImage.Source = string.IsNullOrEmpty(book.CoverImage)
                ? "placeholder.png"
                : book.CoverImage;

            ResultCard.IsVisible = true;
        }

        private bool IsValidIsbin(string isbin)
        {
            return Regex.IsMatch(isbin, @"^\d{13}$");
        }

        private void OnIsbinChanged(object sender, TextChangedEventArgs e)
        {
            IsbinBorder.Stroke = Colors.Gray;
            ErrorLabel.IsVisible = false;
            ResultCard.IsVisible = false;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await _viewModel.RegisterBook(new ManagedBook
            {
                Title = book?.Title,
                Author = book?.Author,
                CoverImage = book?.CoverImage,
                Isbn = book?.Isbn,
            });

            // ここでは例としてメッセージ表示
            DisplayAlert("登録完了", $"「{book.Title}」を登録しました。", "OK");

            // オプション: 入力初期化や画面遷移
            IsbinEntry.Text = "";
            ResultCard.IsVisible = false;
        }
    }
}