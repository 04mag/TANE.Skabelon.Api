using System.ComponentModel.DataAnnotations;

namespace TANE.Skabelon.Api.Models
{
    public class DagSkabelon
    {

        public int Id { get; set; }

        public string Beskrivelse { get; set; }

        public virtual ICollection<string> Aktiviteter { get; set; }
        public virtual ICollection<string> Måltider { get; set; }

        public string Overnatning { get; set; }

        [Required]
        public double Pris { get; set; }

        // Fremmednøgle
        public int TurSkabelonId { get; set; }
        public virtual TurSkabelon TurSkabelon { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
