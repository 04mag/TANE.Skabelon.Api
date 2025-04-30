using System.ComponentModel.DataAnnotations;

namespace TANE.Skabelon.Api.Dtos
{
    public class RejseplanSkabelonCreateDto
    {
        public int Id { get; set; }
        
        public List<TurSkabelonCreateDto> Ture { get; set; } = new();

        
}
}
