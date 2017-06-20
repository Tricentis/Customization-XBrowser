using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Generic;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Technicals.Html;

namespace CustomControl_Table
{
    #region Declare supported technical, override constructor and find technicals for table row
    [SupportedTechnical(typeof(IHtmlDivTechnical))]
    public class Adapter_DivToTableRow : AbstractHtmlDomNodeAdapter<IHtmlDivTechnical>, ITableRowAdapter<IHtmlDivTechnical>
    {
        public Adapter_DivToTableRow(IHtmlDivTechnical technical, Validator validator) : base(technical, validator)
        {
            bool flag = false;
            try
            {
              flag=technical.Id.Equals("tablerow");
            }
            catch(Exception e)
            {

            }
            validator.AssertTrue(() => flag);
        }
        #endregion
    }

}