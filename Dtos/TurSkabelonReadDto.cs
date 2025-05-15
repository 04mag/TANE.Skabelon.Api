


namespace TANE.Skabelon.Api.Dtos
{
    public class TurSkabelonReadDto
    {
        public int Id { get; set; }

        public string Titel { get; set; }
        public string Beskrivelse { get; set; }
        public double Pris { get; set; }
        public List<DagSkabelonReadDto> Dage { get; set; } = new();
        //public ICollection<RejsePlan> RejsePlaner { get; set; } = new List<RejsePlan>();
        public List<int> RejsePlanIds { get; set; } = new();
        public byte[] RowVersion { get; set; }  // **Til optimistic concurrency**
        public int Sekvens { get; set; }
    }
}

