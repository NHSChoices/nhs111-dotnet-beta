using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NHS111.Web.Presentation.Models
{



    public class DosCase
    {

        public DosCase()
        {
            AgeFormat = AgeFormatType.Years;
        }

        public string CaseRef { get; set; }
        public string CaseId { get; set; }
        public string Postcode { get; set; }
        public string Surgery  { get; set; }
        public string Age { get; set; }
        public AgeFormatType AgeFormat { get; set; }
        public int Disposition { get; set; }
        public int SymptomGroup { get; set; }
        public int[] SymptomDiscriminatorList { get; set; }
        public bool SearchDistanceSpecified { get; set; }
        public GenderType Gender { get; set; }

    }

    public enum AgeFormatType
    {
        Years,
        AgeGroup,
    }

    public enum GenderType
    {

        //Male
        M,
        //Female
        F,
        //???
        I,
    }
}
