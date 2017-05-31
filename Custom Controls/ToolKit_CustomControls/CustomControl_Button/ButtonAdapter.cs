using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Technicals.Html;

namespace CustomControl_Button
{
    //This mentions the actual type of the HTML control we are customizing, a DIV element in this case. As this is the type of the actual control, it will always be a Technical.

    [SupportedTechnical(typeof(IHtmlDivTechnical))]

    class ButtonAdapter : AbstractHtmlDomNodeAdapter<IHtmlDivTechnical>, IPushButtonAdapter //These are the behaviour of the control we want to customize to (A Button, in this case).
                                                                                            //AbstractHtmlDomNodeAdapter is a generic adapter class. IPushButtonAdapter interface is implemented to derive more button specific behaviour.
                                                                                            //As this is the expected behaviour of the control, we need to implement Adapters only, in this place.
    {
        public ButtonAdapter(IHtmlDivTechnical technical, Validator validator) : base(technical, validator)
        {
            //This is the identifying criteria for the control (the DIV element).
            validator.AssertTrue(() => technical.Tag.ToLower().Equals("div") && technical.Id.Equals("submit"));
        }

        //This is the name of the control that will appear in XScan window while scanning.
        public override string DefaultName { get { return "DivButton"; } }

        public void Push()
        {
            Technical.Click();
        }

        public string Label { get { return "DivButton"; } }
    }
}
