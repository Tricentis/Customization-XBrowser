using System;
using System.Drawing;
using System.Linq;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters;
using Tricentis.Automation.Engines.Adapters.Attributes;
using Tricentis.Automation.Engines.Adapters.Html.Generic;
using Tricentis.Automation.Engines.Technicals.Html;
using Tricentis.Automation.Simulation;

namespace CustomControl_Tree
{
    [SupportedTechnical(typeof(IHtmlDivTechnical))]
    class Adapter_TreeNodes : AbstractHtmlDomNodeAdapter<IHtmlDivTechnical>, ITreeNodeAdapter
    {
        public Adapter_TreeNodes(IHtmlDivTechnical technical, Validator validator) : base(technical, validator)
        {
            bool flag = false;
            try
            {
                flag= technical.ClassName.Contains("tree-node");
            }
            catch(Exception ex)
            {

            }
            validator.AssertTrue(() => flag);
        }
        public override string DefaultName
        {
            get
            {
                return TreeNodeName;
            }
        }
        public override bool IsSteerable
        {
            get
            {
                return true;
            }
        }
        public string Name
        {
            get
            {
                return TreeNodeName;
            }
        }
        private string TreeNodeName
        {
            get
            {
                return Technical.Children.Get<IHtmlSpanTechnical>().First(x=> x.ClassName.Equals("tree-title")).InnerText;
            }
        }
        public bool Expanded
        {
            get
            {
                return Technical.Children.Get<IHtmlSpanTechnical>().First(x => x.ClassName.Contains("tree-hit")).ClassName.Contains("expanded");
            }
        }
        public PointF? ExpandCollapsePoint
        {
            get
            {
                IHtmlSpanTechnical span = Technical.Children.Get<IHtmlSpanTechnical>().First(x => x.ClassName.Contains("tree-hit"));
                if (span != null)
                {
                    IGuiAdapter nodePoint = AdapterFactory.CreateAdapters<IGuiAdapter>(span, "Html").First();
                    return nodePoint.ActionPoint;
                }
                else
                {
                    return null;
                }
            }
        }
        public bool Selected
        {
            get
            {
                if (Technical.ClassName.Contains("selected"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void Collapse()
        {
            if (Expanded)
            {
                IHtmlSpanTechnical span = Technical.Children.Get<IHtmlSpanTechnical>().First(x => x.ClassName.Contains("tree-hit"));
                span.Focus();
                span.Click();
            }
        }
        public void Expand()
        {
            if (!Expanded)
            {
                IHtmlSpanTechnical span = Technical.Children.Get<IHtmlSpanTechnical>().First(x => x.ClassName.Contains("tree-hit"));
                span.Focus();
                span.Click();
            }
        }       
        public void Select()
        {
            IHtmlSpanTechnical span = Technical.Children.Get<IHtmlSpanTechnical>().First(x => x.ClassName.Contains("title"));
            span.Focus();
            span.Click();
        }
    }
}
