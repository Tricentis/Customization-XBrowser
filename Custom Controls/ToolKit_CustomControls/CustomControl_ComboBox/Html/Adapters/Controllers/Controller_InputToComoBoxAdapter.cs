using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.AutomationInstructions.TestActions.Associations;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters.Controllers;
using Tricentis.Automation.Engines.Representations.Attributes;
using Tricentis.Automation.Engines.Technicals;
using Tricentis.Automation.Engines.Technicals.Html;

namespace CustomControl_ComboBox
{
    [SupportedAdapter(typeof(Adapter_InputToComboBox))]
    class Controller_InputToComoBoxAdapter : ListAdapterController<Adapter_InputToComboBox>
    {
        public Controller_InputToComoBoxAdapter(Adapter_InputToComboBox contextAdapter, ISearchQuery query, Validator validator) : base(contextAdapter, query, validator)
        {
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(ParentBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("ParentNode");
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(DescendantsBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("All");
        }


        protected override IEnumerable<IAssociation> ResolveAssociation(ChildrenBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("Children");
        }

        #region New Association Algorithm Defined For mapping combobox items to combobox
        protected override IEnumerable<IAssociation> ResolveAssociation(ListItemsBusinessAssociation businessAssociation)
        {
            yield return new AlgorithmicAssociation("ListItems");
        }

        protected override IEnumerable<ITechnical> SearchTechnicals(IAlgorithmicAssociation ownAlgorithmicAssociation)
        {
            ContextAdapter.click();
            if (ownAlgorithmicAssociation.AlgorithmName == "ListItems")
                return GetComboDIVs<ITechnical>();
            return base.SearchTechnicals(ownAlgorithmicAssociation);
        }

        private IEnumerable<TSearchedTechnical> GetComboDIVs<TSearchedTechnical>() where TSearchedTechnical : class, ITechnical
        {
            IHtmlDivTechnical divTechnical = ContextAdapter.Technical.Document.Get<IHtmlDocumentTechnical>().GetByTag("div").Get<IHtmlDivTechnical>().FirstOrDefault(x => x.ClassName.Equals("combo-panel panel-body panel-body-noheader"));

            string firstDivText = divTechnical.InnerText;
            if (firstDivText.Contains("Java"))
            {
                return divTechnical.Children.Get<TSearchedTechnical>();
            }
            return new TSearchedTechnical[] { };
        }
        #endregion

    }
}
