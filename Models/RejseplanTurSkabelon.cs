using System.Text.Json.Serialization;

namespace TANE.Skabelon.Api.Models
{
    public class RejseplanTurSkabelon
    {
        public int TurSkabelonId { get; set; }
        public TurSkabelonModel? TurSkabelon { get; set; }
        public int RejseplanSkabelonId { get; set; }
        [JsonIgnore]
        public RejseplanSkabelonModel? RejseplanSkabelon { get; set; }
        public int Order { get; set; } //tracks order
    }
}
