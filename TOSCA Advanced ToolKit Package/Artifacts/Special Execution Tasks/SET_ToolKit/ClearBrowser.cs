using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Tricentis.Automation.AutomationInstructions.Dynamic.Values;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.SpecialExecutionTasks;
using Tricentis.Automation.Engines.SpecialExecutionTasks.Attributes;
using Tricentis.Automation.Execution.Results;

namespace ClearCookies.SET_Logic
{
    [SpecialExecutionTaskName("SETName_ClearBrowser")]
    class ClearBrowser : SpecialExecutionTaskEnhanced
    {
        public ClearBrowser(Validator validator) : base(validator)
        {
        }

        public override void ExecuteTask(ISpecialExecutionTaskTestAction testAction)
        {
            //The 2nd parameter of GetParameterAsInputValue Method defines if the argument is optional or not
            //and if we don't provide the second parameter by default it is false(Means argument is mandetory)
            IInputValue browserName = testAction.GetParameterAsInputValue("BrowserName", false);
            bool result = EraseTemporaryFiles(browserName.Value);
            if (result)
                testAction.SetResult(SpecialExecutionTaskResultState.Ok, string.Format("Cache for {0} is cleared.", browserName.Value));
            else
                testAction.SetResult(SpecialExecutionTaskResultState.Failed, string.Format("Unable to clear cache for {0}.", browserName.Value));
        }
        //Clears the Cache of the Browser
        public bool EraseTemporaryFiles(string BrowserName)
        {
            try
            {
                string batfileName = string.Empty;
                switch (BrowserName)
                {
                    case "Internet Explorer":
                        batfileName = "InternetExplorer.bat";
                        break;
                    case "Mozilla Firefox":
                        batfileName = "Firefox.bat";
                        break;
                    case "Google Chrome":
                        batfileName = "Chrome.bat";
                        break;
                    default: return false;
                }

                Process process = new Process();
                process.StartInfo.UseShellExecute = false;

                string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string normalizedPath = assemblyPath + "\\" + batfileName;

                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.FileName = normalizedPath;
                process.Start();
                process.WaitForExit();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
