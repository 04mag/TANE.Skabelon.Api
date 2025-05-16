using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TANE.Skabelon.Api.Models
{
    public class DagSkabelonModel : BaseEntity
    {
        public string Titel { get; set; } = string.Empty;
        public string Beskrivelse { get; set; } = string.Empty;

        public string Aktiviteter { get; set; } = string.Empty;
        public string Måltider { get; set; } = string.Empty;

        public string Overnatning { get; set; } = string.Empty;

        [JsonIgnore]
        public List<DagTurSkabelon> DagTurSkabelon { get; set; } = new List<DagTurSkabelon>();
    }
}
