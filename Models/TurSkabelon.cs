using System.ComponentModel.DataAnnotations;


namespace TANE.Skabelon.Api.Models
{
    public class TurSkabelon
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Titel { get; set; }

        public string Beskrivelse { get; set; }

        // Fremmednøgle og relation
        public int RejseplanSkabelonId { get; set; }
        public virtual RejseplanSkabelon RejseplanSkabelon { get; set; }

        public virtual ICollection<DagSkabelon> DagSkabeloner { get; set; }

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
