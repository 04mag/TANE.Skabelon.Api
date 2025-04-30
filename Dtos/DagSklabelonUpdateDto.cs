using System.ComponentModel.DataAnnotations;

namespace TANE.Skabelon.Api.Dtos
{
    public class DagSklabelonUpdateDto
    {
        public int Id { get; set; }
        public List<int> TurSkabelonIds { get; set; } = new();
        public string Titel { get; set; }
        public string Beskrivelse { get; set; }
        public List<string> Aktiviteter { get; set; } = new();
        public List<string> Måltider { get; set; } = new();
        public string Overnatning { get; set; }
        public double Pris { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }  // **Til optimistic concurrency**
    }
}
