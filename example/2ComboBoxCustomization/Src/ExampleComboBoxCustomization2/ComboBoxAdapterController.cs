using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.AutomationInstructions.TestActions.Associations;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters.Controllers;
using Tricentis.Automation.Engines.Representations.Attributes;
using Tricentis.Automation.Engines.Technicals;
using Tricentis.Automation.Engines.Technicals.Html;

namespace ExampleComboBoxCustomization2
{
    [SupportedAdapter(typeof(Adapter_ComboBox))]
    class Controller_ComboBox : ListAdapterController<Adapter_ComboBox>
    {
        public Controller_ComboBox(Adapter_ComboBox contextAdapter, ISearchQuery query, Validator validator) : base(contextAdapter, query, validator)
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

        protected override IEnumerable<IAssociation> ResolveAssociation(ListItemsBusinessAssociation businessAssociation)
        {
            yield return new AlgorithmicAssociation("ListItems");
        }
        protected override IEnumerable<ITechnical> SearchTechnicals(IAlgorithmicAssociation ownAlgorithmicAssociation)
        {
            if (ownAlgorithmicAssociation.AlgorithmName == "ListItems")
            {
                string Id_of_adapter = ContextAdapter.Technical.Id;
                string id_usediv = Id_of_adapter + "-menu";
                ContextAdapter.click();
                IHtmlDivTechnical div = ContextAdapter.Technical.Document.Get<IHtmlDocumentTechnical>().GetById(id_usediv).Get<IHtmlDivTechnical>();
                IHtmlRowTechnical row = div.Children.Get<IHtmlTableTechnical>().FirstOrDefault().Children.Get<IHtmlElementTechnical>().FirstOrDefault().Children.Get<IHtmlRowTechnical>().FirstOrDefault();
                IEnumerable<IHtmlCellTechnical> cells = row.Children.Get<IHtmlCellTechnical>();

                List<ITechnical> listTechnical = new List<ITechnical>();
                foreach (IHtmlCellTechnical cell in cells)
                {
                    listTechnical.AddRange(cell.Children.Get<IHtmlDivTechnical>().FirstOrDefault().Children.Get<IHtmlDivTechnical>());

                }
                return listTechnical;
            }
            else
            {
                return base.SearchTechnicals(ownAlgorithmicAssociation);
            }
        }
    }
}
