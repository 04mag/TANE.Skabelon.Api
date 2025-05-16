using System.Text.Json.Serialization;

namespace TANE.Skabelon.Api.Models
{
    public class DagTurSkabelon
    {
        public int DagSkabelonId { get; set; }
        [JsonIgnore]
        public DagSkabelonModel? DagSkabelon { get; set; }
        public int TurSkabelonId { get; set; }

        [JsonIgnore]
        public TurSkabelonModel? TurSkabelon { get; set; }
        public int Order { get; set; } //tracks order
    }
}
