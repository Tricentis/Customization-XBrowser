using System;
using System.Diagnostics;
using Tricentis.Automation.AutomationInstructions.Dynamic.Values;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines;
using Tricentis.Automation.Engines.SpecialExecutionTasks;
using Tricentis.Automation.Engines.SpecialExecutionTasks.Attributes;
using SHDocVw;
namespace Tutorial
{
    [SpecialExecutionTaskName("SETName_OpenUrl")]
    public class OpenUrl : SpecialExecutionTask
    {
        private const string URL = "URL";
        private const string isNewTab = "IsNewTab";


        public OpenUrl(Validator validator) : base(validator)
        {
        }

        public override ActionResult Execute(ISpecialExecutionTaskTestAction testAction)
        {
            bool completed = false;
            string returnMsg = "";
            IInputValue paramURL = testAction.GetParameterAsInputValue(URL, false);
            IInputValue paramIsNewTab = testAction.GetParameterAsInputValue(isNewTab, false);

            if (paramURL == null || string.IsNullOrEmpty(paramURL.Value))
                throw new ArgumentException(string.Format("Mandatory parameter '{0}' not set.", URL));

            if (paramIsNewTab == null || string.IsNullOrEmpty(paramIsNewTab.Value))
                throw new ArgumentException(string.Format("Mandatory parameter '{0}' not set.", isNewTab));

            try
            {
                if (paramIsNewTab.Value == "True")
                {
                    ShellWindows shellWindows = new ShellWindows();  //Uses SHDocVw to get the Internet Explorer Instances
                    foreach (SHDocVw.InternetExplorer internetExplorerInstance in shellWindows)
                    {
                        string url = internetExplorerInstance.LocationURL;
                        if (!url.Contains("file:///"))
                        {
                            internetExplorerInstance.Navigate(paramURL.Value, 0x800);
                            completed = true;
                            returnMsg = "URL opened in new tab";
                            break;
                        }
                    }
                }

                if (!completed)   // To open url in new window
                {
                    Process procInNewWndow = new Process();
                    procInNewWndow.StartInfo.FileName = "C:\\Program Files\\Internet Explorer\\iexplore.exe";
                    procInNewWndow.StartInfo.Arguments = paramURL.Value;
                    procInNewWndow.StartInfo.UseShellExecute = true;
                    procInNewWndow.StartInfo.CreateNoWindow = false;
                    procInNewWndow.Start();
                    returnMsg = "URL opened in new window";
                }
            }
            catch (Exception)
            {
                return new UnknownFailedActionResult("Could not start program",
                                                     string.Format(
                                                         "Failed while trying to start:\nURL: {0}\r\nIsNewTab: {1}",
                                                         paramURL.Value, paramIsNewTab.Value),
                                                     "");
            }

            return new PassedActionResult(returnMsg);
        }
    }
}