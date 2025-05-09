using Microsoft.Extensions.Logging;
using Book_Shelf.Services;
using Book_Shelf.Repositories;
using Book_Shelf.ViewModels;
namespace Book_Shelf;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton(new HttpClient());
		builder.Services.AddTransient<OpenDbService>();
		builder.Services.AddSingleton<IManagedBookService, DummyManagedBookService>();
		builder.Services.AddSingleton<ManagedBookRepository>();
		builder.Services.AddTransient<ManagedBooksViewModel>();
		builder.Services.AddTransient<RegisterBookPageViewModel>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
