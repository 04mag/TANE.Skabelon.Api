using System.ComponentModel.DataAnnotations;

namespace TANE.Skabelon.Api.Models
{
    public class RejseplanSkabelonModel : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public int AntalDage { get; set; }

        // Relation til ture
        public virtual ICollection<TurSkabelonModel> TurSkabeloner { get; set; }

        // Concurrency token
        [Timestamp]
        public byte[] RowVersion { get; set; }


    }
}
