using System.Text.RegularExpressions;
using Book_Shelf.Services;
using Book_Shelf.Models;
using Book_Shelf.Repositories;
namespace Book_Shelf.Views
{
    public partial class RegisterBookPage : ContentPage
    {
        private OpenDbService _openDbService;
        private ManagedBookRepository _managedBookRepository;
        private SearchedBook? book = null;
        public RegisterBookPage(OpenDbService openDbService, ManagedBookRepository managedBookRepository)
        {
            InitializeComponent();
            _openDbService = openDbService;
            _managedBookRepository = managedBookRepository;
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

            book = await LookupBookByIsbin(isbin);

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

        private async Task<SearchedBook?> LookupBookByIsbin(string isbin)
        {
            var result = await _openDbService.SearchBooks(isbin);

            return result == null ? null : result.FirstOrDefault();
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

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            _managedBookRepository.AddBook(new ManagedBook
            {
                Title = book?.Title,
                Author = book?.Author,
                CoverImage = book?.CoverImage,
                Isbn = book?.Isbn,
            });

            // TODO: 実際の登録処理（DB保存など）をここに実装
            // ここでは例としてメッセージ表示
            DisplayAlert("登録完了", $"「{book.Title}」を登録しました。", "OK");

            // オプション: 入力初期化や画面遷移
            IsbinEntry.Text = "";
            ResultCard.IsVisible = false;
        }
    }
}