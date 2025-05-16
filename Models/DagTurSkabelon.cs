namespace TANE.Skabelon.Api.Models
{
    public class DagTurSkabelon
    {
        public int DagSkabelonId { get; set; }
        public DagSkabelonModel DagSkabelon { get; set; }
        public int TurSkabelonId { get; set; }
        public TurSkabelonModel TurSkabelon { get; set; }
        public int Order { get; set; } //tracks order
    }
}
