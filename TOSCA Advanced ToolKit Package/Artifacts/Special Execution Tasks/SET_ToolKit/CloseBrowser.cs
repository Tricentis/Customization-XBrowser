using System;
using System.Diagnostics;
using Tricentis.Automation.AutomationInstructions.Dynamic.Values;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines;
using Tricentis.Automation.Engines.SpecialExecutionTasks;
using Tricentis.Automation.Engines.SpecialExecutionTasks.Attributes;

namespace SET_ToolKit
{
    [SpecialExecutionTaskName("SETName_CloseBrowser")]
    public class CloseBrowser : SpecialExecutionTask
    {
        private const string Name = "BrowserName";
        public const string ERROR_INVALID_BROWSER_NAME = "Browser name entered is invalid";
        public const string ERROR_BROWSER_NOTAVAILABLE = " Browser is not open";
        public CloseBrowser(Validator validator) : base(validator)
        {
        }

        public override ActionResult Execute(ISpecialExecutionTaskTestAction testAction)
        {
            IInputValue name = testAction.GetParameterAsInputValue(Name, false);
            string browserProcessName = string.Empty;
            string result = string.Empty;

            try
            {
                // Setting Browser process Name as per browserName 
                switch (name.Value)
                {
                    case "Internet Explorer":
                        browserProcessName = "iexplore";
                        break;
                    case "Google Chrome":
                        browserProcessName = "chrome";
                        break;
                    case "Mozilla Firefox":
                        browserProcessName = "firefox";
                        break;
                    default:
                        return new UnknownFailedActionResult("Could not kill program",string.Format("Failed while trying to kill {0}",name.Value),"");
                }
                Process[] procs = Process.GetProcessesByName(browserProcessName);
                if (procs.Length > 0)
                {
                    foreach (Process proc in procs)
                    {
                        proc.Kill();
                        result = name.Value+" Browser closed Successfully";
                    }
                }
                else
                {
                    result = name.Value+ERROR_BROWSER_NOTAVAILABLE;
                }
            }
            catch (Exception)
            {
                return new UnknownFailedActionResult("Could not kill program",
                                                     string.Format(
                                                         "Failed while trying to kill {0}",
                                                         name.Value),
                                                     "");
            }

            return new PassedActionResult(result);
        }
    }
}
