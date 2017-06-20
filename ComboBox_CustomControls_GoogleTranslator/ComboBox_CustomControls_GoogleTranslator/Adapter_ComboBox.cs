using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Adapters.Lists;
using Tricentis.Automation.Engines.Technicals.Html;

namespace ComboBox_CustomControls_GoogleTranslator
{
    [SupportedTechnical(typeof(IHtmlDivTechnical))]
    public class Adapter_ComboBox : AbstractHtmlDomNodeAdapter<IHtmlDivTechnical>, IComboBoxAdapter
    {
        string controlName = string.Empty;
        public Adapter_ComboBox(IHtmlDivTechnical technical, Validator validator) : base(technical, validator)
        {
            bool flag = false;
            if (technical.Id.Equals("gt-sl-gms") || technical.Id.Equals("gt-tl-gms"))
            {
                controlName = technical.Id.Equals("gt-sl-gms") ? "SourceCombo" : "TargetCombo";
                flag = true;

            }
            validator.AssertTrue(() => flag);
        }
        public override string DefaultName
        {
            get
            {
                return controlName;
            }
        }
        public void click()
        {
            Technical.Focus();
            Technical.Click();
            Technical.Click();
        }
    }
}
