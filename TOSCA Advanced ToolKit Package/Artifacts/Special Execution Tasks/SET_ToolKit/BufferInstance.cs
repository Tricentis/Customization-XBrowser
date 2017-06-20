using System;
using Tricentis.Automation.AutomationInstructions.Configuration;
using Tricentis.Automation.AutomationInstructions.Dynamic.Values;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines;
using Tricentis.Automation.Engines.SpecialExecutionTasks;
using Tricentis.Automation.Engines.SpecialExecutionTasks.Attributes;
using Tricentis.Automation.Execution.Results;

namespace Tutorial
{
    [SpecialExecutionTaskName("SETName_BufferInstance")]
    public class BufferInstance : SpecialExecutionTaskEnhanced
    {
        public BufferInstance(Validator validator) : base(validator)
        {
        }

        public override void ExecuteTask(ISpecialExecutionTaskTestAction testAction)
        {


            //Iterate over each TestStepValue
            foreach (IParameter parameter in testAction.Parameters)
            {
                try
                {
                    //ActionMode input means set the buffer
                    if (parameter.ActionMode == ActionMode.Input)
                    {
                        IInputValue inputValue = parameter.GetAsInputValue();
                        Buffers.Instance.SetBuffer(parameter.Name, inputValue.Value, false);
                        testAction.SetResultForParameter(parameter, SpecialExecutionTaskResultState.Ok, string.Format("Buffer {0} set to value {1}.", parameter.Name, inputValue.Value));
                    }
                    //Otherwise we let TBox handle the verification. Other ActionModes like WaitOn will lead to an exception.
                    else
                    {
                        //Don't need the return value of HandleActualValue in this case.
                        HandleActualValue(testAction, parameter, Buffers.Instance.GetBuffer(parameter.Name));
                    }
                }
                catch (Exception ex)
                {

                    testAction.SetResultForParameter(parameter, SpecialExecutionTaskResultState.Failed, string.Format(ex.Message));
                    break;
                }
            }

        }
    }
}
            

