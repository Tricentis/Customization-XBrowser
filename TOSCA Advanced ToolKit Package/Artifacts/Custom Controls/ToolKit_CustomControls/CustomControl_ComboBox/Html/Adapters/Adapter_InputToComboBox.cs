using System;
using System.Linq;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Adapters.Lists;
using Tricentis.Automation.Engines.Technicals.Html;

namespace CustomControl_ComboBox
{
    #region Defining supported technical and overriding base class constructor
    [SupportedTechnical(typeof(IHtmlInputElementTechnical))]
    class Adapter_InputToComboBox : AbstractHtmlDomNodeAdapter<IHtmlInputElementTechnical>, IComboBoxAdapter
    {
        public Adapter_InputToComboBox(IHtmlInputElementTechnical technical, Validator validator) : base(technical, validator)
        {
            bool flag = false;
            try
            {
               flag= FilterInputForCombo(technical);
            }
            catch(Exception e)
            {

            }
            validator.AssertTrue(() => flag);
        }
        #endregion

        #region Finding desired technical for combo
        private bool FilterInputForCombo(IHtmlInputElementTechnical technical)
        {
            string className = technical.ClassName;

            if (className.Equals("textbox-text validatebox-text") && !string.IsNullOrEmpty(className))
                return true;
            return false;
        }
        #endregion

        #region Name of combo control in representation layer
        public override string DefaultName
        {
            get
            {
                IHtmlInputElementTechnical inputTechnicalForName = Technical.ParentNode.Get<IHtmlSpanTechnical>().
                    Children.Get<IHtmlInputElementTechnical>().
                    FirstOrDefault(x => x.ClassName.Equals("textbox-value"));

                string comboName = inputTechnicalForName.GetAttribute("name");
                return comboName;
            }
        }
        #endregion
        public void click()
        {
            Technical.ParentNode.Get<IHtmlSpanTechnical>().Children.Get<IHtmlSpanTechnical>().First().Children.Get<IHtmlAnchorTechnical>().First().Focus();
            Technical.ParentNode.Get<IHtmlSpanTechnical>().Children.Get<IHtmlSpanTechnical>().First().Children.Get<IHtmlAnchorTechnical>().First().Click();
        }
    }
}
