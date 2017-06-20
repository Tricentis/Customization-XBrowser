using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Adapters.Lists;
using Tricentis.Automation.Engines.Technicals.Html;
using Tricentis.Automation.Simulation;

namespace ComboBox_CustomControls_GoogleTranslator
{
    [SupportedTechnical(typeof(IHtmlDivTechnical))]
    class Adapter_ComboItem : AbstractHtmlDomNodeAdapter<IHtmlDivTechnical>, IListItemAdapter
    {
        string controlName = string.Empty;
        public Adapter_ComboItem(IHtmlDivTechnical technical, Validator validator) : base(technical, validator)
        {
            bool flag = false;
            if (technical.ClassName.Contains("goog-menuitem goog-option"))
            {
                controlName = technical.Children.Get<IHtmlDivTechnical>().FirstOrDefault().InnerText;
                controlName = Regex.Replace(controlName, @"^\s*$\n", string.Empty, RegexOptions.Multiline);
                flag = true;

            }
            validator.AssertTrue(() => flag);
        }

        public bool Selected
        {
            get
            {
                return Technical.ClassName.Contains("goog-option-selected") ? true : false;
            }

            set
            {
                IHtmlDivTechnical div = Technical.ParentNode.Get<IHtmlDivTechnical>().ParentNode.Get<IHtmlCellTechnical>().ParentNode.Get<IHtmlRowTechnical>().ParentNode.Get<IHtmlElementTechnical>().ParentNode.Get<IHtmlTableTechnical>().ParentNode.Get<IHtmlDivTechnical>();
                string id = div.Id.Replace("-menu", "");
                div = Technical.Document.Get<IHtmlDocumentTechnical>().GetById(id).Get<IHtmlDivTechnical>();
                div.Focus();
                div.Click();

                IGuiAdapter clickAdapter = AdapterFactory.CreateAdapters<IGuiAdapter>(Technical, "Html").FirstOrDefault();
                Mouse.PerformMouseAction(MouseOperation.Click, clickAdapter.ActionPoint);
            }
        }

        public string Text
        {
            get
            {
                return controlName;
            }
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
                return controlName;
            }
        }

        bool IListItemBaseAdapter.Selected
        {
            get
            {
                return Selected;
            }
        }
    }
}
