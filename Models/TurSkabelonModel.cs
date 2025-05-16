using System.ComponentModel.DataAnnotations;


namespace TANE.Skabelon.Api.Models
{
    public class TurSkabelonModel : BaseEntity
    {
        
        [Required, MaxLength(100)]
        public string Titel { get; set; }

        public string Beskrivelse { get; set; }

        public double Pris { get; set; }

        public ICollection<DagTurSkabelon> DagTurSkabelon { get; set; }

        public ICollection<RejseplanTurSkabelon>? RejseplanTurSkabelon { get; set; }
    }
}
