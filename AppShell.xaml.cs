using _20230713.Paginas;

namespace _20230713;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(GestionPlatosPage), typeof(GestionPlatosPage));
	}
}
