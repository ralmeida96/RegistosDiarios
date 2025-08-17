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

            var session = await SupabaseService.SignIn(_model.Email ?? "", _model.Password ?? "");
            if (session != null)
            {
                var userId = SupabaseService.CurrentUser?.Id;
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

            var session = await SupabaseService.SignUp(_model.Email ?? "", _model.Password ?? "");
            if (session != null)
            {
                var userId = SupabaseService.CurrentUser?.Id;
            }
        }
        catch (Exception ex)
        {
            using JsonDocument doc = JsonDocument.Parse(ex.Message);
            JsonElement root = doc.RootElement;

            // Extrair o campo "msg"
            string msg = root.GetProperty("msg").GetString();
            await DisplayAlert("Erro", msg, "OK"); 
        }
    }
}