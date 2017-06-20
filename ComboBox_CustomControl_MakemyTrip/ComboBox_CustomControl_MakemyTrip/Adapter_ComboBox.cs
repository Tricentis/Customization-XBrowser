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

namespace ComboBox_CustomControl_MakemyTrip
{
    [SupportedTechnical(typeof(IHtmlInputElementTechnical))]
    public class Adapter_ComboBox : AbstractHtmlDomNodeAdapter<IHtmlInputElementTechnical>, IComboBoxAdapter
    {
        string controlName = string.Empty;
        public Adapter_ComboBox(IHtmlInputElementTechnical technical, Validator validator) : base(technical, validator)
        {
            bool flag = false;

            if (technical.Id.Equals("hp-widget__depart") || technical.Id.Equals("hp-widget__return"))
            {
                flag = true;
                controlName = technical.Id.Contains("depart") ? "Depart" : "Return";
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
        public string Label
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
        public void Click()
        {
            IGuiAdapter clickAdapter = AdapterFactory.CreateAdapters<IGuiAdapter>(Technical, "Html").FirstOrDefault();
            Mouse.PerformMouseAction(MouseOperation.Click, clickAdapter.ActionPoint);
        }
    }
}
