using RegistosDiarios.Entities;
using RegistosDiarios.Helpers;
using RegistosDiarios.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistosDiarios.Model
{
    public class RegistoSonoModel : NotifyObject
    {
        public DateTime Data { get; set; }
        public TimeSpan? HoraAcordar { get; set; }
        public TimeSpan? HoraDeitar { get; set; }
        public bool IsBusy { get; set; }

        public RegistoSonoModel()
        {
            Data = DateTime.Today;
        }

        public async Task Carregar()
        {
            if (SupabaseService.SupabaseClient == null) return;

            var userId = SupabaseService.CurrentUser?.Id;

            var _resultado = await SupabaseService.SupabaseClient
                .From<RegistoSono>()
                .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, userId)
                .Filter("data", Supabase.Postgrest.Constants.Operator.Equals, Data.ToString("yyyy-MM-dd"))
                .Get();

            var _registo = _resultado.Models.FirstOrDefault();
            if (_registo != null)
            {
                Data = _registo.Data;
                HoraAcordar = _registo.HoraAcordar;
                HoraDeitar = _registo.HoraDeitar;
            }
        }

        public async Task Gravar()
        {
            if (SupabaseService.SupabaseClient == null) return;

            NotifyValue(nameof(IsBusy), true);

            var userId = SupabaseService.CurrentUser?.Id;

            var registo = new RegistoSono
            {
                UserId = Guid.Parse(userId),
                Data = Data,
                HoraDeitar = HoraDeitar,
                HoraAcordar = HoraAcordar
            };

            await SupabaseService.SupabaseClient.From<RegistoSono>().Insert(registo);
            NotifyValue(nameof(IsBusy), false);
        }
    }
}
