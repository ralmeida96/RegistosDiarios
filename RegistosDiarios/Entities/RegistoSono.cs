using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColumnAttribute = Supabase.Postgrest.Attributes.ColumnAttribute;
using TableAttribute = Supabase.Postgrest.Attributes.TableAttribute;

namespace RegistosDiarios.Entities
{
    /// <summary>
    /// Classe entidade para registos de sono
    /// </summary>
    [Table("registos_sono")]
    public class RegistoSono : BaseModel
    {
        [PrimaryKey("id", false)]
        public int Id { get; set; }
        [Column("user_id")]
        public Guid UserId { get; set; }
        [Column("data_acordar")]
        public DateTime DataAcordar { get; set; }
        [Column("hora_acordar")]
        public TimeSpan? HoraAcordar { get; set; }
        [Column("data_deitar")]
        public DateTime? DataDeitar { get; set; }
        [Column("hora_deitar")]
        public TimeSpan? HoraDeitar { get; set; }
    }
}
