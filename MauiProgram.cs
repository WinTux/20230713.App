using _20230713.ConexionDatos;
using _20230713.Paginas;

namespace _20230713;

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
		builder.Services.AddSingleton<IRestConexionDatos, RestConexionDatos>();
		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<GestionPlatosPage>();
        return builder.Build();
	}
}
