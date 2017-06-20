using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tricentis.Automation.AutomationInstructions.Configuration;
using Tricentis.Automation.AutomationInstructions.Dynamic.Values;
using Tricentis.Automation.AutomationInstructions.TestActions;
using Tricentis.Automation.Creation;
using Tricentis.Automation.Engines.SpecialExecutionTasks;
using Tricentis.Automation.Engines.SpecialExecutionTasks.Attributes;
using Tricentis.Automation.Execution.Results;

namespace LanguageFind
{
    [SpecialExecutionTaskName("Language_fetch")]
    public class TextFileRead : SpecialExecutionTaskEnhanced
    {
        public const string ERROR_FILE_EMPTY = "Filepath contains an empty file!";
        public const string ERROR_FILE_NOT_FOUND = "No file found at given path!";
        public const string ERROR_INVALID_FILE = "File extension not supported!";
        public TextFileRead(Validator validator) : base(validator)
        {
        }

        public string ExtractString(string filePath)
        {
            string retString = string.Empty;
            string[] allLines = { };
            if (!File.Exists(filePath))
            {
                retString = ERROR_FILE_NOT_FOUND;
            }
            else
            {

                if (retString.Equals(string.Empty))
                {
                    switch (Path.GetExtension(filePath))
                    {
                        case ".txt":
                            allLines = ReadTextFile(filePath);
                            break;
                        default:
                            retString = ERROR_INVALID_FILE;
                            break;
                    }
                }

                foreach (string str in allLines)
                {
                    retString = retString + "," + str;
                }
                if (retString.Equals(string.Empty))
                {
                    retString = ERROR_FILE_EMPTY;
                }
                else
                {
                    retString = retString.Substring(1);
                }

            }

            return retString;
        }

        public string[] ReadTextFile(string filepath)
        {
            return File.ReadAllLines(filepath);
        }

        public override void ExecuteTask(ISpecialExecutionTaskTestAction testAction)
        {
            IInputValue path = testAction.GetParameterAsInputValue("Path");
            //IParameter result = testAction.GetParameter("Result");
            IParameter buffers = testAction.GetParameter("Buffers", true);
            IEnumerable<IParameter> arguments = buffers.GetChildParameters("Buffer");
            string returnValue = ExtractString(path.Value);
            string[] words = returnValue.Split(',');
            if (words.Length == arguments.Count())
            {
                int index = 0;
                string resultText = "Buffers Created:";
                foreach (IParameter argument in arguments)
                {
                    IInputValue arg = argument.Value as IInputValue;
                    Buffers.Instance.SetBuffer(arg.Value, words[index], false);
                    resultText = resultText + Environment.NewLine
                        + "Name: " + arg.Value + Environment.NewLine
                        + "Value: " + words[index];
                    index = index + 1;
                }
                testAction.SetResultForParameter(buffers, SpecialExecutionTaskResultState.Ok, string.Format(resultText));
            }
            else
            {
                testAction.SetResultForParameter(buffers, SpecialExecutionTaskResultState.Failed, string.Format("File does not have all the buffers!"));
            }
        }

    }
}
    
