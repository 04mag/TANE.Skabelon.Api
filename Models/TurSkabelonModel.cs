using System.ComponentModel.DataAnnotations;


namespace TANE.Skabelon.Api.Models
{
    public class TurSkabelonModel : BaseEntity
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Titel { get; set; }

        public string Beskrivelse { get; set; }

        // Fremmednøgle og relation
        public int RejseplanSkabelonId { get; set; }
        public virtual RejseplanSkabelonModel RejseplanSkabelon { get; set; }

        public virtual ICollection<DagSkabelonModel> DagSkabeloner { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
