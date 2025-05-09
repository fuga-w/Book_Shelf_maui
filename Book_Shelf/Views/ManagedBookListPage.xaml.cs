using Book_Shelf.ViewModels;
namespace Book_Shelf.Views;

public partial class ManagedBookListPage : ContentPage
{
    private ManagedBooksViewModel _viewModel;
    public ManagedBookListPage(ManagedBooksViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadBooksAsync();
    }
}

