using System.ComponentModel.DataAnnotations;

namespace TANE.Skabelon.Api.Models
{
    public class RejseplanSkabelonModel : BaseEntity
    {
        public string Titel { get; set; }
        public string Beskrivelse { get; set; }
        // Relation til ture
        public List<RejseplanTurSkabelon>? RejseplanTurSkabelon { get; set; } = new List<RejseplanTurSkabelon>();
    }
}
