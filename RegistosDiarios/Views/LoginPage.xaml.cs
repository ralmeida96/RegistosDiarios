using Newtonsoft.Json;
using RegistosDiarios.Model;
using RegistosDiarios.Services;
using System.Text.Json;

namespace RegistosDiarios.Views;

public partial class LoginPage : ContentPage
{
    LoginModel _model;

	public LoginPage()
	{
		InitializeComponent();

        _model = new LoginModel();
        BindingContext = _model;
	}

    private async void btnEntrar_Clicked(object sender, EventArgs e)
    {
		try
		{
            if (_model == null) return;

            _model.NotifyValue(nameof(_model.IsBusy), true);
            var session = await SupabaseService.SignIn(_model.Email ?? "", _model.Password ?? "");
            if (session != null)
            {
                _model.NotifyValue(nameof(_model.IsBusy), false);
                await Shell.Current.GoToAsync("//RegistosSono");
            }
        }
		catch (Exception ex)
		{
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private async void btnRegistar_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (_model == null) return;

            _model.NotifyValue(nameof(_model.IsBusy), true);
            var session = await SupabaseService.SignUp(_model.Email ?? "", _model.Password ?? "");
            if (session != null)
            {
                _model.NotifyValue(nameof(_model.IsBusy), false);
                await DisplayAlert("Atenção!", "Deves confirmar o registo no teu email antes de continuar.", "OK");
                _model = new LoginModel();
                BindingContext = _model;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK"); 
        }
    }
}