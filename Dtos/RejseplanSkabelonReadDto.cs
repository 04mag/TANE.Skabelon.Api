namespace TANE.Skabelon.Api.Dtos
{
    public class RejseplanSkabelonReadDto
    {
        public int Id { get; set; }
        public List<TurSkabelonReadDto> Ture { get; set; } = new();
    }
}
