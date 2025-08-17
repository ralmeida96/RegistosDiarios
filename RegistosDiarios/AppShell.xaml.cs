using RegistosDiarios.Views;

namespace RegistosDiarios
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("loginPage", typeof(LoginPage));
        }
    }
}
