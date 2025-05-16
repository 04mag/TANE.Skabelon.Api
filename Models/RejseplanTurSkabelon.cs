namespace TANE.Skabelon.Api.Models
{
    public class RejseplanTurSkabelon
    {
        public int RejseplanSkabelonId { get; set; }
        public RejseplanSkabelonModel RejseplanSkabelon { get; set; }
        public int TurSkabelonId { get; set; }
        public TurSkabelonModel TurSkabelon { get; set; }

        public int Order { get; set; } //tracks order
    }
}
