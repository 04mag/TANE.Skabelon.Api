using System.ComponentModel.DataAnnotations;

namespace TANE.Skabelon.Api.Models
{
    public class RejseplanSkabelonModel : BaseEntity
    {
        public string Titel { get; set; } = string.Empty;
        public string Beskrivelse { get; set; } = string.Empty;
        // Relation til ture
        public List<RejseplanTurSkabelon> RejseplanTurSkabelon { get; set; } = new List<RejseplanTurSkabelon>();
    }
}
