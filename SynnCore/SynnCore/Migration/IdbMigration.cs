using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.Migration
{
    public interface IdbMigration
    {
        List<MethodInfo> GetMethodsToPerform();
        List<string> GetAlreadyFinishedSteps();
        void FinishMethod(string stepName);
    }
}
