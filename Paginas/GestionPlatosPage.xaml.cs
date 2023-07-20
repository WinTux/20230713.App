using _20230713.ConexionDatos;
using _20230713.Models;

namespace _20230713.Paginas;
[QueryProperty(nameof(plato),"Plato")]
public partial class GestionPlatosPage : ContentPage
{
    private readonly IRestConexionDatos restConexionDatos;
    private Plato _plato;
    public Plato plato {
        get => _plato;
        set {
            _esNuevo = esNuevo(value);
            _plato = value;
            OnPropertyChanged();
        }
    }
    private bool _esNuevo;

    public GestionPlatosPage(IRestConexionDatos restConexionDatos)
	{
		InitializeComponent();
		this.restConexionDatos = restConexionDatos;
        BindingContext = this;
    }
    bool esNuevo(Plato plato) { 
        if(plato.id == 0)
            return true;
        return false;
    }

    async void OnCancelClic(object sender, EventArgs e) {
        await Shell.Current.GoToAsync("..");
    }

    async void OnGuardarClic(object sender, EventArgs e)
    {
        if (_esNuevo) 
            await restConexionDatos.AddPlatoAsync(plato);
        else
            await restConexionDatos.UpdatePlatoAsync(plato);
        await Shell.Current.GoToAsync("..");
    }
    async void OnEliminarClic(object sender, EventArgs e)
    {
        await restConexionDatos.DeletePlatoAsync(plato.id);
        await Shell.Current.GoToAsync("..");
    }
}