using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace MPL.Common.Reflection
{
    /// <summary>
    /// A class that defines static functions for executing dynamic code.
    /// </summary>
    public static class DynamicCodeExecutor
    {
        #region Methods
        #region _Public_
        /// <summary>
        /// Gets the output from executing the specified dynamic code.
        /// </summary>
        /// <param name="code">A string containing the code to execute.</param>
        /// <param name="result">An object that will be set to the value of the code output.</param>
        /// <param name="warningText">A string that will be set to any warning associated with the attempt.</param>
        /// <returns>A bool indicating success.</returns>
        public static bool TryGetDynamicCodeOutput(string code, out object result, out string warningText)
        {
            bool ReturnValue = false;

            // Defaults
            result = null;
            warningText = null;

            try
            {
                string ClassName;
                string Code;
                CSharpCodeProvider CodeProvider;
                CompilerParameters Parameters;
                CompilerResults Results;
                string SourceCode;
                Type TargetType;
                string UniqueID;

                // Build parameters
                UniqueID = Guid.NewGuid().ToString().Replace("-", "");
                ClassName = $"DynamicClass{UniqueID}";
                Code = $"return {code};";
                SourceCode = @"public class " + ClassName + " { public static object GetValue() { " + Code + "; } }";

                // Compile the code
                Parameters = new CompilerParameters { GenerateExecutable = false, GenerateInMemory = true };
                CodeProvider = new CSharpCodeProvider();
                Results = CodeProvider.CompileAssemblyFromSource(Parameters, SourceCode);

                // Get the type and output
                TargetType = Results.CompiledAssembly.GetType(ClassName);
                result = TargetType.InvokeMember("GetValue", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, null);
                ReturnValue = true;
            }
            catch (Exception ex)
            {
                warningText = ex.Message;
            }

            return ReturnValue;
        }

        #endregion
        #endregion
    }
}