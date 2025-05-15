using System.ComponentModel.DataAnnotations;


namespace TANE.Skabelon.Api.Models
{
    public class TurSkabelonModel : BaseEntity
    {
        
        [Required, MaxLength(100)]
        public string Titel { get; set; }

        public string Beskrivelse { get; set; }

        // Fremmednøgle og relation
        public List<DagSkabelonModel>? Dage { get; set; }    
        
        public List<RejseplanSkabelonModel>? RejseplanSkabeloner { get; set; }
        public int Sekvens { get; set; }
    }
}
