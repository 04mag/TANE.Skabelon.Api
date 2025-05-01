namespace TANE.Skabelon.Api.Dtos
{
    public class TurSkabelonCreateDto
    {
        public List<int> RejseplanSkabelonIds { get; set; } = new();

        public string Titel { get; set; }
        public string Beskrivelse { get; set; }
        public double Pris { get; set; }
      
        public List<DagSkabelonCreateDto> Dage { get; set; } = new();
    }
}
