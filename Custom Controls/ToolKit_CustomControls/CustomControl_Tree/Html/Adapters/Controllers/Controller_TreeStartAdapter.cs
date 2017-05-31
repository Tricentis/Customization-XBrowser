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
    [SupportedAdapter(typeof(Adapter_TreeStart))]
    class Controller_TreeStartAdapter : TreeContextAdapterController<Adapter_TreeStart>
    {
        public Controller_TreeStartAdapter(Adapter_TreeStart contextAdapter, ISearchQuery query, Validator validator) : base(contextAdapter, query, validator)
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

        protected override IEnumerable<IAssociation> ResolveAssociation(DescendantsBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("All");
        }

        #region Association Algorithm for mapping tree nodes with tree
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
                return ContextAdapter.Technical.Children.Get<IHtmlElementTechnical>().First().Children.Get<IHtmlDivTechnical>();
           }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
