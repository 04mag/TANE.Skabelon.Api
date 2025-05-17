using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace TANE.Skabelon.Api.Models
{
    public class TurSkabelonModel : BaseEntity
    {
        
        [Required, MaxLength(100)]
        public string Titel { get; set; } = string.Empty;

        public string Beskrivelse { get; set; } = string.Empty;

        public double Pris { get; set; } = 0;

        public List<DagTurSkabelon> DagTurSkabelon { get; set; } = new List<DagTurSkabelon>();

        [JsonIgnore]
        public List<RejseplanTurSkabelon>? RejseplanTurSkabelon { get; set; } = new List<RejseplanTurSkabelon>();
    }
}
