using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.TCAddOns;
using Tricentis.TCAPIObjects.Objects;

namespace AddOnTutorial_v1_0
{
    class ModuleFolderToTestCaseFolder : TCAddOnDropTask
    {
        public override Type ApplicableType
        {
            get
            {
                return typeof(TCFolder);
            }
        }

        public override Type DropObjectType
        {
            get
            {
                return typeof(TCFolder);
            }
        }

        public override string Name
        {
            get
            {
                return "Create Test Cases";
            }
        }

        public override TCObject Execute(TCObject obj, List<TCObject> dropObjects, bool copy, TCAddOnTaskContext context)
        {
            TCFolder folder = obj as TCFolder;
            TestCase tc = folder.CreateTestCase();
            tc.Name = "AddOn Drag-drop Test Cases";
            foreach (TCObject objDrop in dropObjects)
            {
                TCFolder moduleFolder = objDrop as TCFolder;
                List<TCObject> modules = moduleFolder.Search("=>SUBPARTS:XModule");
                tc.CreateXTestStepFromXModule(modules);
            }
            return null;
        }
        public override bool IsTaskPossible(TCObject targetObject, TCObject sourceObject)
        {
            return base.IsTaskPossible(targetObject, sourceObject);
        }
    }
}
