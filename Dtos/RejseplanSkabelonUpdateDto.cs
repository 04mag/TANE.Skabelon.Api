using System.ComponentModel.DataAnnotations;

namespace TANE.Skabelon.Api.Dtos
{
    public class RejseplanSkabelonUpdateDto
    {
        public int Id { get; set; }
        public List<TurSkabelonCreateDto> Ture { get; set; } = new();
        [Required]
        public byte[] RowVersion { get; set; }  // **Til optimistic concurrency**
    }
}
