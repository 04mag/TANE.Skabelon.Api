using System.ComponentModel.DataAnnotations;


namespace TANE.Skabelon.Api.Models
{
    public class TurSkabelonModel
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

        public decimal GetTotalPris()
        {
            decimal sum = 0;
            foreach (var dag in DagSkabeloner)
                sum += dag.Pris;
            return sum;
        }
    }
}
