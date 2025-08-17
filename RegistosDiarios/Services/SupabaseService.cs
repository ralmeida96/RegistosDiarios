using Supabase;
using Supabase.Gotrue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistosDiarios.Services
{
    public static class SupabaseService
    {
        public static Supabase.Client? SupabaseClient { get; private set; }

        public static User? CurrentUser => SupabaseClient?.Auth?.CurrentUser;

        public static async Task Init()
        {
            if (SupabaseClient == null)
            {
                SupabaseClient = new Supabase.Client("https://nhuynqosganvriibbilk.supabase.co", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6Im5odXlucW9zZ2FudnJpaWJiaWxrIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NTU0NTQ1NTQsImV4cCI6MjA3MTAzMDU1NH0.x6TUbkDvJoOXybjzGn1x7gAqMh4AvtmpLu9wU2uGGTI");

                await SupabaseClient.InitializeAsync();
            }
        }

        public static async Task<Session?> SignUp(string email, string password)
        {
            if (SupabaseClient == null) return null;

            var _session = await SupabaseClient.Auth.SignUp(email, password);

            return _session;
        }

        public static async Task<Session?> SignIn(string email, string password)
        {
            if (SupabaseClient == null) return null;

            var _session = await SupabaseClient.Auth.SignIn(email, password);

            return _session;
        }

        public static async Task SignOut()
        {
            if (SupabaseClient == null) return;

            await SupabaseClient.Auth.SignOut();
        }
    }
}
