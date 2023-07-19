using _20230713.ConexionDatos;
using _20230713.Models;
using _20230713.Paginas;
using System.Diagnostics;
using System.Linq;

namespace _20230713;

public partial class MainPage : ContentPage
{

	private readonly IRestConexionDatos restConexionDatos;
	public MainPage(IRestConexionDatos restConexionDatos)
	{
		InitializeComponent();
		this.restConexionDatos = restConexionDatos;
	}
	protected async override void OnAppearing() { 
		base.OnAppearing();
		coleccionPlatosView.ItemsSource = await restConexionDatos.GetPaltosAsync();//aun no existe en la view.
	}
	async void OnAddPlatoClic(object sender, EventArgs e )
    {
		Debug.WriteLine("[EVENTO] Botón AddPlato presionado");
		var param = new Dictionary<string, object> {
            { nameof(Plato), new Plato()}
        };
		await Shell.Current.GoToAsync(nameof(GestionPlatosPage), param);
    }
    async void OnElementoCambiado(object sender, SelectionChangedEventArgs e)
    {
        Debug.WriteLine("[EVENTO] Botón ElementoCambiado presionado");
        var param = new Dictionary<string, object> {
            { nameof(Plato), e.CurrentSelection.FirstOrDefault() as Plato}
        };
        await Shell.Current.GoToAsync(nameof(GestionPlatosPage), param);
    }
}
