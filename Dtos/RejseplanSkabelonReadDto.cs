namespace TANE.Skabelon.Api.Dtos
{
    public class RejseplanSkabelonReadDto
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beskrivelse { get; set; }
        public List<TurSkabelonReadDto> Ture { get; set; } = new();
        public byte[] RowVersion { get; set; }  // **Til optimistic concurrency**
    }
}

