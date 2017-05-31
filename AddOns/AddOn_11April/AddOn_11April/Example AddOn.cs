using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.TCAddOns;

namespace AddOnTutorial_v1_0
{
    public class AddOn_Sample : TCAddOn
    {
        public override string UniqueName
        {
            get
            {
                return "TutorialAddOn_v1.0";
            }
        }
        public override string DisplayedName
        {
            get
            {
                return "Tutorial AddOn";
            }
        }
    }
}
