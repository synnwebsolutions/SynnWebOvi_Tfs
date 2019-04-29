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
        public static void Perform(string connectionString, string table, List<MethodInfo> methods)
        {
            string stepName = null;
            try
            {
                var db = new SqlDbMigrationExecuter(connectionString, table);
                List<string> finishedSteps = new List<string>();
                try
                {
                    finishedSteps = db.GetAlreadyFinishedSteps();
                }
                catch (Exception)
                {
                    finishedSteps = new List<string>();
                }
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
