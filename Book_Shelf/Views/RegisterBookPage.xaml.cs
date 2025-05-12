using System.Text.RegularExpressions;
using Book_Shelf.Models;
using Book_Shelf.ViewModels;
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
            var isbinInput = _viewModel.Isbin;
            var isbin = Isbin.TryCreate(isbinInput);

            if (!isbin.IsValid)
            {
                IsbinBorder.Stroke = Colors.Red;
                ErrorLabel.Text = "ISBINが空か、形式が正しくありません。";
                ErrorLabel.IsVisible = true;
                _viewModel.InDisplaySearchResult = false;
                return;
            }

            IsbinBorder.Stroke = Colors.Gray;
            ErrorLabel.IsVisible = false;

            var isbinValue = isbin.Value?.Value.ToString();
            if (string.IsNullOrEmpty(isbinValue))
            {
                IsbinBorder.Stroke = Colors.Red;
                ErrorLabel.Text = "ISBINが空か、形式が正しくありません。";
                ErrorLabel.IsVisible = true;
                _viewModel.InDisplaySearchResult = false;
                return;
            }

            book = await _viewModel.LookupBookByIsbin(isbinValue);

            if (book == null)
            {
                await DisplayAlert("検索失敗", "書籍が見つかりませんでした。", "OK");
                ResultCard.IsVisible = false;
                return;
            }

            // 書籍が登録済みかどうかをチェック
            await _viewModel.CheckBookRegistered(isbinValue);

            // 表示更新
            _viewModel.SearchedBookTitle = book.Title;
            _viewModel.SearchedBookAuthor = book.Author;
            _viewModel.SearchedBookCoverImage = book.CoverImage;
            _viewModel.InDisplaySearchResult = true;
        }

        private void OnIsbinChanged(object sender, TextChangedEventArgs e)
        {
            IsbinBorder.Stroke = Colors.Gray;
            ErrorLabel.IsVisible = false;
            _viewModel.InDisplaySearchResult = false;

            _viewModel.Isbin = e.NewTextValue?.Trim() ?? string.Empty;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await _viewModel.RegisterBook(new ManagedBook
            {
                Title = _viewModel.SearchedBookTitle,
                Author = _viewModel.SearchedBookAuthor,
                CoverImage = _viewModel.SearchedBookCoverImage,
                Isbn = _viewModel.Isbin,
            });

            // ここでは例としてメッセージ表示
            await DisplayAlert("登録完了", $"「{book.Title}」を登録しました。", "OK");

            // オプション: 入力初期化や画面遷移
            _viewModel.Isbin = string.Empty;
            _viewModel.InDisplaySearchResult = false;
        }

        private async void OnUnregisterClicked(object sender, EventArgs e)
        {
            await _viewModel.UnregisterBook(_viewModel.Isbin);
            await DisplayAlert("登録解除", $"「{_viewModel.SearchedBookTitle}」の登録を解除しました。", "OK");
            _viewModel.Isbin = string.Empty;
            _viewModel.InDisplaySearchResult = false;
        }
    }
}