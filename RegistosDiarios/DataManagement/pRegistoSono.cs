using RegistosDiarios.Entities;
using RegistosDiarios.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistosDiarios.DataManagement
{
    public class pRegistoSono
    {
        public async Task<List<RegistoSono>?> ObterRegistosSono(Guid userId, DateTime Data)
        {
            if (SupabaseService.SupabaseClient == null) return null;

            var response = await SupabaseService.SupabaseClient
                .From<RegistoSono>()
                .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, userId)
                .Filter("Data", Supabase.Postgrest.Constants.Operator.Equals, Data)
                .Get();

            return response.Models;
        }

        public async Task InserirRegistoSono(Guid userId, DateTime dataAcordar, DateTime? dataDeitar)
        {
            if (SupabaseService.SupabaseClient == null) return;

            var registo = new RegistoSono
            {
                UserId = userId,
                DataAcordar = dataAcordar,
                DataDeitar = dataDeitar,
            };

            await SupabaseService.SupabaseClient.From<RegistoSono>().Insert(registo);
        }
    }
}
