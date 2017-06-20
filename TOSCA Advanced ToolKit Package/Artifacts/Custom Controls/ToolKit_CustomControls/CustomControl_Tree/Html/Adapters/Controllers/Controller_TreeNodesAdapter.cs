using System;
using System.Collections.Generic;
using System.Linq;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.AutomationInstructions.TestActions.Associations;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters.Controllers;
using Tricentis.Automation.Engines.Representations.Attributes;
using Tricentis.Automation.Engines.Technicals;
using Tricentis.Automation.Engines.Technicals.Html;

namespace CustomControl_Tree
{
    [SupportedAdapter(typeof(Adapter_TreeNodes))]
    class Controller_TreeNodesAdapter : TreeNodeContextAdapterController<Adapter_TreeNodes>
    {
        public Controller_TreeNodesAdapter(Adapter_TreeNodes contextAdapter, ISearchQuery query, Validator validator) : base(contextAdapter, query, validator)
        {
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(ParentBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("ParentNode");
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(ChildrenBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("Children");
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(TreeNodeBusinessAssociation businessAssociation)
        {

            yield return new AlgorithmicAssociation("SubNodes");
        }

        protected override IEnumerable<ITechnical> SearchTechnicals(IAlgorithmicAssociation ownAlgorithmicAssociation)
        {
            if (ownAlgorithmicAssociation.AlgorithmName == "SubNodes")
                return GetSubNodes();
            return base.SearchTechnicals(ownAlgorithmicAssociation);
        }

        private IEnumerable<ITechnical> GetSubNodes() 
        {
            try
            {
                List<ITechnical> nodes = new List<ITechnical>();
                IHtmlElementTechnical nodeIdentification = ContextAdapter.Technical.ParentNode.Get<IHtmlElementTechnical>().Children.Get<IHtmlElementTechnical>().FirstOrDefault(x => x.Tag.ToLower().Equals("ul"));
                if (nodeIdentification != null)
                {
                    foreach (IHtmlElementTechnical tech in nodeIdentification.Children.Get<IHtmlElementTechnical>())
                    {
                        nodes.Add(tech.Children.Get<IHtmlDivTechnical>().First());
                    }
                }
                return nodes;       
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
