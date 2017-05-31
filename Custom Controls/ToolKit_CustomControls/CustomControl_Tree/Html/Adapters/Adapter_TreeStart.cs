using System.Linq;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Technicals.Html;

namespace CustomControl_Tree
{
    [SupportedTechnical(typeof(IHtmlElementTechnical))]
    class Adapter_TreeStart : AbstractHtmlDomNodeAdapter<IHtmlElementTechnical>, ITreeAdapter
    {
        public Adapter_TreeStart(IHtmlElementTechnical technical, Validator validator) : base(technical, validator)
        {
            validator.AssertTrue(() => technical.ClassName.Equals("easyui-tree tree"));
        }
        public override bool IsSteerable
        {
            get
            {
                return true;
            }
        }       
        public override string DefaultName
        {
            get
            {
                return "Customized_Tree";
            }
        }
    }
}
