using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Technicals.Html;

namespace CustomControl_Table
{
    #region Declare Supported Technical, override base class constructor and filter technical for Table
    [SupportedTechnical(typeof(IHtmlDivTechnical))]
    public class Adapter_DivToTable : AbstractHtmlDomNodeAdapter<IHtmlDivTechnical>, ITableAdapter
    {
        public Adapter_DivToTable(IHtmlDivTechnical technical, Validator validator) : base(technical, validator)
        {
            validator.AssertTrue(() => technical.Id.Equals("table"));
        }
        #endregion

        #region Name of Table in representation layer
        public override string DefaultName { get { return "SampleTable"; } }
    }
    #endregion
}
