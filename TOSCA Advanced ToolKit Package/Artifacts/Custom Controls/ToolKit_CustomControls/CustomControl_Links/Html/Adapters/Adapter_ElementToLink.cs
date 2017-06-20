using System;

#region Customization specific imports
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Technicals.Html;
#endregion

namespace CustomControl_Links
{
    #region Declare supported technical and override base class constructor
    [SupportedTechnical(typeof(IHtmlElementTechnical))]
    class Adapter_ElementToLink : AbstractHtmlDomNodeAdapter<IHtmlElementTechnical>, ILinkAdapter
    {
        public Adapter_ElementToLink(IHtmlElementTechnical technical, Validator validator) : base(technical, validator)
        {
            bool flag = false;
            try
            {
               flag= technical.Tag.ToLower().Equals("h3") && technical.ClassName.StartsWith("ui-accordion-header");
            }
            catch(Exception e)
            {

            }
            validator.AssertTrue(() => flag); 

        }
        #endregion

        #region Define name for control in representation layer
        public override string DefaultName
        {
            get
            {
                return Technical.InnerText;
            }
        }

        string ILinkAdapter.Label
        {
            get
            {
                return Technical.InnerText;
            }
            #endregion
        }
        #region Define external url for link if required
        string ILinkAdapter.Url
        {
            get
            {
                return null;
            }
        }
        #endregion

        #region Follow Property of Link control
        void ILinkAdapter.Follow()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
