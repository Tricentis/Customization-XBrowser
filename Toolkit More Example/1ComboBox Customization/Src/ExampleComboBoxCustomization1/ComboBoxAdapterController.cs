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

namespace ExampleComboBoxCustomization1
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
            yield return new AlgorithmicAssociation("UKSVR");
        }
        protected override IEnumerable<ITechnical> SearchTechnicals(IAlgorithmicAssociation ownAlgorithmicAssociation)
        {
            ContextAdapter.Click();
            List<ITechnical> techs = new List<ITechnical>();
            if (ownAlgorithmicAssociation.AlgorithmName == "UKSVR")
            {
                IHtmlDivTechnical div = ContextAdapter.Technical.ParentNode.Get<IHtmlDivTechnical>().ParentNode.Get<IHtmlDivTechnical>().ParentNode.Get<IHtmlElementTechnical>().ParentNode.Get<IHtmlDivTechnical>().ParentNode.Get<IHtmlDivTechnical>();
                if (ContextAdapter.Technical.Id.Equals("hp-widget__depart"))
                {
                    div = div.Children.Get<IHtmlDivTechnical>().Last().Children.Get<IHtmlDivTechnical>().Last().Children.Get<IHtmlDivTechnical>().FirstOrDefault(x => x.ClassName.Equals("dateFilter hasDatepicker"));
                }
                else
                {
                    div = div.Children.Get<IHtmlDivTechnical>().Last().Children.Get<IHtmlDivTechnical>().Last().Children.Get<IHtmlDivTechnical>().FirstOrDefault(x => x.ClassName.Equals("dateFilterReturn hasDatepicker"));
                }
                for (int index = 0; index < 2; index++)
                {
                    IHtmlTableTechnical table = div.Children.Get<IHtmlDivTechnical>().Last().Children.Get<IHtmlDivTechnical>().ElementAt(index).Children.Get<IHtmlTableTechnical>().FirstOrDefault();
                    IHtmlElementTechnical element = table.Children.Get<IHtmlElementTechnical>().Last();
                    IEnumerable<IHtmlRowTechnical> rows = element.Children.Get<IHtmlRowTechnical>();
                    foreach (IHtmlRowTechnical row in rows)
                    {
                        foreach (IHtmlCellTechnical cell in row.Children.Get<IHtmlCellTechnical>())
                        {
                            IHtmlAnchorTechnical anchor = cell.Children.Get<IHtmlAnchorTechnical>().FirstOrDefault();
                            techs.Add(anchor);
                        }
                    }
                }
            }
            if (techs.Count > 0)
            {
                return techs;
            }
            else
            {
                return base.SearchTechnicals(ownAlgorithmicAssociation);
            }
        }
    }
}