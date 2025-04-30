using System.ComponentModel.DataAnnotations;

namespace TANE.Skabelon.Api.Models
{
    public class RejseplanSkabelonModel : BaseEntity
    {
        

        // Relation til ture
        public List<TurSkabelonModel>? TurSkabeloner { get; set; }

    
  
    }
}
