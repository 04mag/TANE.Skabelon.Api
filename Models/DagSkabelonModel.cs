using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TANE.Skabelon.Api.Models
{
    public class DagSkabelonModel : BaseEntity
    {
        public string Beskrivelse { get; set; }

        public List <String> Aktiviteter { get; set; }
        public List <String> Måltider { get; set; }

        public string Overnatning { get; set; }

        [Required]
        public double Pris { get; set; }
        [JsonIgnore]
        public List<TurSkabelonModel>? Tur { get; set; }

      
    }
}
