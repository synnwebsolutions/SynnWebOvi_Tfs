using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SynnCore.Migration
{
    public class MigrationHandler
    {
        internal static void Perform(IdbMigration db)
        {
            string stepName = null;
            try
            {
                List<MethodInfo> methods = db.GetMethodsToPerform();
                List<string> finishedSteps = db.GetAlreadyFinishedSteps();
                var steps = methods.Where(x => !finishedSteps.Contains(x.Name)).ToList();
                
                foreach (var m in steps)
                {
                    stepName = m.Name;
                    m.Invoke(null, new object[] { db });
                    db.FinishMethod(stepName);
                }
            }
            catch (Exception ex)
            {
                string rd = ex.StackTrace;
            }
        }
    }
}
