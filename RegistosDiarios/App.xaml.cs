using RegistosDiarios.Services;

namespace RegistosDiarios
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SupabaseService.Init();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}