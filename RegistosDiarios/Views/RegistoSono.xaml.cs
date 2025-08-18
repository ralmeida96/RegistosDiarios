using RegistosDiarios.Model;

namespace RegistosDiarios.Views;

public partial class RegistoSono : ContentPage
{
	RegistoSonoModel _model;
	public RegistoSono()
	{
		InitializeComponent();

		_model = new RegistoSonoModel();
		BindingContext = _model;

        Loaded += RegistoSono_Loaded;
	}

    private async void RegistoSono_Loaded(object? sender, EventArgs e)
    {
        try
        {
            if (_model == null) return;

            await _model.Carregar();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private async void btnGravar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (_model == null) return;

            await _model.Gravar();
            await DisplayAlert("Sucesso", "Registo gravado com sucesso!", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }
}