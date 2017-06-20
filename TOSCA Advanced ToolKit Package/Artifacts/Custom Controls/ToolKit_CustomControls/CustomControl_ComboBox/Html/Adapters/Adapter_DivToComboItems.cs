using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Adapters.Lists;
using Tricentis.Automation.Engines.Technicals.Html;

namespace CustomControl_ComboBox
{
    #region Supported Technical by adapter and base class parameterized constructor
    [SupportedTechnical(typeof(IHtmlDivTechnical))]
    class Adapter_DivToComboItems : AbstractHtmlDomNodeAdapter<IHtmlDivTechnical>, IListItemAdapter
    {
        //private IHtmlInputElementTechnical inputTagMappedToCombo;
        public Adapter_DivToComboItems(IHtmlDivTechnical technical, Validator validator) : base(technical, validator)
        {
            bool flag = false;
            try
            {
                flag=FilterDivsForComboItem(technical);
            }
            catch(Exception e)
            {

            }
            validator.AssertTrue(() => flag);
        }
        #endregion

        #region Criteria for finding required DIVs 
        private bool FilterDivsForComboItem(IHtmlElementTechnical technical)
        {
            string id = technical.Id;
            string classNameOfTechnical = technical.ClassName;

            if ((classNameOfTechnical.Equals("combobox-item") || classNameOfTechnical.Equals("combobox-item combobox-item-selected")) && id.StartsWith("_easyui_combobox_"))
            {
                //inputTagMappedToCombo = Technical.Document.Get<IHtmlDocumentTechnical>().GetByTag("input").Get<IHtmlInputElementTechnical>().FirstOrDefault(x => x.ClassName.Equals("textbox-text validatebox-text"));
                return true;
            }
            else
                return false;
        }
        #endregion

        #region Making Combo Items Steerable

        public override bool IsSteerable
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region Selected Property for making combo item selection
        public bool Selected
        {
            get
            {
                string a = Technical.InnerText;
                string b = Technical.Document.Get<IHtmlDocumentTechnical>().GetByTag("div").Get<IHtmlDivTechnical>().FirstOrDefault(x => x.ClassName.Equals("combobox-item combobox-item-selected")).InnerText;
                if (a == b)
                    return true;
                else
                    return false;
            }

            set
            {
                Technical.Focus();
                Technical.Click();
            }
        }
        #endregion

        #region Combo Item text for representation layer
        public string Text
        {
            get
            {
                return Technical.InnerText;
            }
        }


        #endregion
    }
}
