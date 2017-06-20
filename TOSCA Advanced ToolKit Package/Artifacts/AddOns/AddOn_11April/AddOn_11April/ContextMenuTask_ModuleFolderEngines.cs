using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace AddOnTutorial_v1_0
{
    class ContextMenuTask : TCAddOnTask
    {
        public override Type ApplicableType
        {
            get
            {
                return typeof(TCFolder);
            }
        }
        public override bool IsTaskPossible(TCObject obj)
        {
            bool flag = false;
            if ((obj as TCFolder).Search("=>SUBPARTS:XModule").Count > 0)
            {
                flag = true;
            }
            return flag;
        }
        public override string Name
        {
            get
            {
                return "View Engines";
            }
        }
        public override TCObject Execute(TCObject objectToExecuteOn, TCAddOnTaskContext taskContext)
        {
            List<TCObject> modules = (objectToExecuteOn as TCFolder).Search("=>SUBPARTS:XModule");
            List<string> engineNames = new List<string>();
            List<string> moduleNames = new List<string>();
            string engineInfo = string.Empty;
            foreach (XModule module in modules)
            {
                IEnumerable<XParam> parms = (module as XModule).XParams;
                foreach (XParam parm in parms)
                {
                    if (parm.Name.Equals("Engine"))
                    {
                        if (!engineNames.Contains(parm.Value))
                        {
                            engineNames.Add(parm.Value);
                            moduleNames.Add(string.Empty);                          
                        }
                        moduleNames[engineNames.IndexOf(parm.Value)]
                            = moduleNames[engineNames.IndexOf(parm.Value)] + "      " + module.Name + Environment.NewLine;
                        break;
                    }
                }
            }
            foreach (string engine in engineNames)
            {
                engineInfo = engineInfo +
                    "Engine " + engine + " in Modules: "
                    + Environment.NewLine 
                    + moduleNames[engineNames.IndexOf(engine)]
                    + Environment.NewLine;  
            }
            taskContext.ShowMessageBox("All Engines in Module Folder " + objectToExecuteOn.DisplayedName, engineInfo);            
            return objectToExecuteOn;
        }
    }
}
