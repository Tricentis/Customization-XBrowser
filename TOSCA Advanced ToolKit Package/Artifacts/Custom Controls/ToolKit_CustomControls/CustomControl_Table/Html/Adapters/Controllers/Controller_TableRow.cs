using System.Collections.Generic;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.AutomationInstructions.TestActions.Associations;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.Adapters.Controllers;
using Tricentis.Automation.Engines.Representations.Attributes;

namespace CustomControl_Table
{
    [SupportedAdapter(typeof(Adapter_DivToTableRow))]
    public class Controller_TableRow : TableRowContextAdapterController<Adapter_DivToTableRow>
    {
        public Controller_TableRow(Adapter_DivToTableRow contextAdapter, ISearchQuery query, Validator validator) : base(contextAdapter, query, validator)
        {
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(ChildrenBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("Children");
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(DescendantsBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("All");
        }

        protected override IEnumerable<IAssociation> ResolveAssociation(ParentBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("ParentNode");
        }

        #region Association algorithm for mapping cells with rows
        protected override IEnumerable<IAssociation> ResolveAssociation(CellsBusinessAssociation businessAssociation)
        {
            yield return new TechnicalAssociation("Children");
        }
        #endregion
    }
}

