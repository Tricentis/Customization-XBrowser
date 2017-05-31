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
    #region declare technical, override constructor and find technical for cells
    [SupportedTechnical(typeof(IHtmlDivTechnical))]
    public class Adapter_DivToTableCell : AbstractHtmlDomNodeAdapter<IHtmlDivTechnical>, ITableCellAdapter<IHtmlDivTechnical>
    {
        public Adapter_DivToTableCell(IHtmlDivTechnical technical, Validator validator) : base(technical, validator)
        {
            validator.AssertTrue(() => technical.Id.Equals("tablecell"));
        }
        #endregion

        #region table cells with unique names
        public string Text
        {

            get
            {
                string text = string.Empty;
                IEnumerable<IHtmlOptionTechnical> combo = Technical.Children.Get<IHtmlSelectTechnical>().FirstOrDefault().Children.Get<IHtmlOptionTechnical>();
                foreach (var item in combo)
                {
                    if (item.Selected == true)
                        text = item.Value;
                }
                return text;
            }
        }
        #endregion

        #region Row and Column span if any
        public int RowSpan
        {
            get { return 1; }
        }

        public int ColSpan
        {
            get { return 1; }
        }
        #endregion
    }

}
