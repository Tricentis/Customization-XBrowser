using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines;
using Tricentis.Automation.Engines.Adapters;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Technicals.Html;
using Tricentis.Automation.Simulation;

namespace CustomControl_CheckBox
{
    [SupportedTechnical(typeof(IHtmlDivTechnical))]
    public class CheckboxAdapter : AbstractHtmlDomNodeAdapter<IHtmlDivTechnical>, ICheckBoxAdapter
    {
        public CheckboxAdapter(IHtmlDivTechnical technical, Validator validator) : base(technical, validator)
        {
            bool flag = false;
            try
            {
                flag = technical.Children.Get<IHtmlDivTechnical>().First().Id.Equals("Yes");
            }
            catch (Exception e)
            {

            }
            validator.AssertTrue(() => flag);


        }

        public string Label
        {
            get
            {
                return "Yes-No-CheckBox";
            }
        }
        public override string DefaultName
        {
            get
            {
                return "Yes-No-CheckBox";
            }
        }
        public override bool IsSteerable
        {
            get
            {
                return true;
            }
        }
        public CheckState Selected
        {
            get
            {
                IHtmlLabelTechnical lbl = Technical.Document.Get<IHtmlDocumentTechnical>().GetById("ClickLabel").Get<IHtmlLabelTechnical>();
                if (lbl.InnerText.Contains("Yes"))
                {
                    return CheckState.True;
                }
                else
                {
                    return CheckState.False;
                }

            }

            set
            {
                IHtmlLabelTechnical lbl = Technical.Document.Get<IHtmlDocumentTechnical>().GetById("ClickLabel").Get<IHtmlLabelTechnical>();
                if (!value.Equals(Selected))
                {
                    lbl.Focus();
                    lbl.Click();
                }
            }
        }

        public bool TriState
        {
            get
            {
                if (Selected.Equals(CheckState.True))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
