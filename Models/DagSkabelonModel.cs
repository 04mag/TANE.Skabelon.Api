using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TANE.Skabelon.Api.Models
{
    public class DagSkabelonModel : BaseEntity
    {
        public string Titel { get; set; }
        public string Beskrivelse { get; set; }

        public String Aktiviteter { get; set; }
        public String Måltider { get; set; }

        public string Overnatning { get; set; }

        public int Sekvens { get; set; }
        //[JsonIgnore]
        //  public List<TurSkabelonModel>? Tur { get; set; }


    }
}
