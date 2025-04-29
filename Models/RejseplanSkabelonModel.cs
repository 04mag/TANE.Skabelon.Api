using System.ComponentModel.DataAnnotations;

namespace TANE.Skabelon.Api.Models
{
    public class RejseplanSkabelonModel : BaseEntity
    {
        public int Id { get; set; }

        // Relation til ture
        public List<TurSkabelonModel> TurSkabeloner { get; set; }

        // Concurrency token
        [Timestamp]
        public byte[] RowVersion { get; set; }


    }
}
