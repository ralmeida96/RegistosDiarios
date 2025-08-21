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
        public DateTime DataAcordar { get; set; }
        public TimeSpan? HoraAcordar { get; set; }
        public TimeSpan? HoraDeitar { get; set; }
        public bool IsBusy { get; set; }

        public RegistoSonoModel()
        {
            DataAcordar = DateTime.Today;
        }

        public async Task Carregar()
        {
            if (SupabaseService.SupabaseClient == null) return;

            var userId = SupabaseService.CurrentUser?.Id;

            var _resultado = await SupabaseService.SupabaseClient
                .From<RegistoSono>()
                .Filter("user_id", Supabase.Postgrest.Constants.Operator.Equals, userId)
                .Filter("data_acordar", Supabase.Postgrest.Constants.Operator.Equals, DataAcordar.Date.ToString("yyyy-MM-dd"))
                .Get();

            var _registo = _resultado.Models.FirstOrDefault();
            if (_registo != null)
            {
                DataAcordar = _registo.DataAcordar.Date;
                HoraAcordar = _registo.DataAcordar.TimeOfDay;
                if (_registo.DataDeitar.HasValue)
                    HoraDeitar = _registo.DataDeitar.Value.TimeOfDay;
            }
        }

        public async Task Gravar()
        {
            if (SupabaseService.SupabaseClient == null) return;

            NotifyValue(nameof(IsBusy), true);

            var userId = SupabaseService.CurrentUser?.Id;

            DateTime _acordar = DataAcordar.Date.Add(HoraAcordar.GetValueOrDefault(new TimeSpan()));
            DateTime? _deitar = null;

            if (HoraDeitar.HasValue)
            {
                if (HoraDeitar.Value < HoraAcordar.GetValueOrDefault(new TimeSpan()))
                    _deitar = _acordar.Date.AddDays(1).Add(HoraDeitar.Value);
                else
                    _deitar = DataAcordar.Date.Add(HoraDeitar.Value);
            }
            
            //TODO:: Está a gravar com um  dia antes do selecionado

            var registo = new RegistoSono
            {
                UserId = Guid.Parse(userId),
                DataAcordar = _acordar.Date,
                HoraAcordar = _acordar.TimeOfDay,
                DataDeitar = _deitar.HasValue ? _deitar.Value.Date : null,
                HoraDeitar = _deitar.HasValue ? _deitar.Value.TimeOfDay : null,
            };

            await SupabaseService.SupabaseClient.From<RegistoSono>().Insert(registo);
            NotifyValue(nameof(IsBusy), false);
        }
    }
}
