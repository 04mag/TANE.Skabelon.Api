using System.ComponentModel.DataAnnotations;

namespace TANE.Skabelon.Api.Dtos
{
    public class RejseplanSkabelonCreateDto
    {
        public string Titel { get; set; }
        public string Beskrivelse { get; set; }

        public List<TurSkabelonCreateDto>? Ture { get; set; } 

        
}
}
