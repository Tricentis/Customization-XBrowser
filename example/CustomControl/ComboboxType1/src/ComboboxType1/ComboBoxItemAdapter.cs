using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Adapters.Lists;
using Tricentis.Automation.Engines.Technicals.Html;
using Tricentis.Automation.Simulation;

namespace ComboboxType1
{
    [SupportedTechnical(typeof(IHtmlAnchorTechnical))]
    class Adapter_ComboItem : AbstractHtmlDomNodeAdapter<IHtmlAnchorTechnical>, IListItemAdapter
    {
        public Adapter_ComboItem(IHtmlAnchorTechnical technical, Validator validator) : base(technical, validator)
        {
            bool flag = false;

            if (technical.ClassName.Contains("ui-state-default"))
            {
                IHtmlDivTechnical div = technical.ParentNode.Get<IHtmlCellTechnical>().ParentNode.Get<IHtmlRowTechnical>().ParentNode.Get<IHtmlElementTechnical>().ParentNode.Get<IHtmlTableTechnical>().ParentNode.Get<IHtmlDivTechnical>().ParentNode.Get<IHtmlDivTechnical>().ParentNode.Get<IHtmlDivTechnical>();
                if (div.ClassName.Equals("dateFilter hasDatepicker") || div.ClassName.Equals("dateFilterReturn hasDatepicker"))
                {
                    flag = true;
                }
            }
            validator.AssertTrue(() => flag);
        }

        public bool Selected
        {
            get
            {
                return Technical.ClassName.Contains("ui-state-active");
            }

            set
            {
                IGuiAdapter clickAdapter = AdapterFactory.CreateAdapters<IGuiAdapter>(Technical, "Html").FirstOrDefault();
                Mouse.PerformMouseAction(MouseOperation.Click, clickAdapter.ActionPoint);
            }
        }

        public string Text
        {
            get
            {
                string date = Technical.InnerText;
                IHtmlDivTechnical div = Technical.ParentNode.Get<IHtmlCellTechnical>().ParentNode.Get<IHtmlRowTechnical>().ParentNode.Get<IHtmlElementTechnical>().ParentNode.Get<IHtmlTableTechnical>().ParentNode.Get<IHtmlDivTechnical>();
                string month = div.Children.Get<IHtmlDivTechnical>().First().Children.Get<IHtmlDivTechnical>().Last().Children.Get<IHtmlSpanTechnical>().First().InnerText;
                string year = Technical.ParentNode.Get<IHtmlCellTechnical>().GetAttribute("data-year").ToString();
                return date + month + year;
            }
        }

        bool IListItemBaseAdapter.Selected
        {
            get
            {
                return Selected;
            }
        }
        public override bool IsSteerable
        {
            get
            {
                return true;
            }
        }
    }
}
