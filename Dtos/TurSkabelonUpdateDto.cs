using System.ComponentModel.DataAnnotations;
using TANE.Skabelon.Api.Models;

namespace TANE.Skabelon.Api.Dtos
{
    public class TurSkabelonUpdateDto
    {
        public int Id { get; set; }
        //public ICollection<RejsePlan> RejsePlaner { get; set; } = new List<RejsePlan>();
        public List<int> RejseplanSkabelonIds { get; set; } = new();

        public string Titel { get; set; }
        public string Beskrivelse { get; set; }
        public double Pris { get; set; }
        public List<DagSkabelonUpdateDto> Dage { get; set; }

        public byte[] RowVersion { get; set; }  // **Til optimistic concurrency**
        public int Sekvens { get; set; }
    }
}
