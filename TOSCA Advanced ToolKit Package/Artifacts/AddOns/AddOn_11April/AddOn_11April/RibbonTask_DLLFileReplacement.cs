using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects;
using Tricentis.TCAPIObjects.Objects;
using Tricentis.TCAPIObjects.DataObjects;
using Tricentis.TCAPI;

namespace AddOnTutorial_v1_0
{
    class RibbonItem : TCAddOnMenuItem
    {
        public override string ID
        {
            get
            {
                return "Replace DLL";
            }
        }

        public override string MenuText
        {
            get
            {
                return "Select DLL File";
            }
        }
        
        public override void Execute(TCAddOnTaskContext context)
        {
            string workSpaceName=TCAddOn.ActiveWorkspace.GetTCProject().Name;
            string ToscaProjectPath = Environment.GetEnvironmentVariable("Tricentis_Projects");
            string ToscaWorkspaces = ToscaProjectPath + "\\" + "Tosca_Workspaces";
            string currentWorkspacePath = ToscaWorkspaces + "\\"+ workSpaceName + "\\" + workSpaceName + ".tws";

            string home = Environment.GetEnvironmentVariable("Tricentis_Home");
            string fileToRun = home + "\\" + "DllReplacementUtility.exe";
            string initialDialogPath = "C:\\Tosca_Projects\\Tosca ToolKit";
            string fileToReplace = context.GetFilePath("upload dll file",true, initialDialogPath, true, "dll", false);
            ProcessStartInfo psi = new ProcessStartInfo();
            psi.FileName = fileToRun;
            psi.Arguments = fileToReplace.Replace(' ','?')+" "+ currentWorkspacePath.Replace(' ','?');
            Process.Start(psi);
        }
    }
}
