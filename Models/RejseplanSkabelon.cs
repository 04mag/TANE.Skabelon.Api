using System.ComponentModel.DataAnnotations;

namespace TANE.Skabelon.Api.Models
{
    public class RejseplanSkabelon
    {
        public int Id { get; set; }

        [Required]
        public int AntalDage { get; set; }

        // Relation til ture
        public virtual ICollection<TurSkabelon> TurSkabeloner { get; set; }

        // Concurrency token
        [Timestamp]
        public byte[] RowVersion { get; set; }

        
        public decimal GetTotalPris()
        {
            decimal sum = 0;
            foreach (var tur in TurSkabeloner)
                sum += tur.GetTotalPris();
            return sum;
        }
    }
}
