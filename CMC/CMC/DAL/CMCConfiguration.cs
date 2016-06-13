using System.Data.Entity;
using System.Data.Entity.SqlServer;

namespace CMC.DAL
{
    public class ProjectConfiguration : DbConfiguration
    {
        public ProjectConfiguration()
        {
            SetExecutionStrategy("System.Data.SqlClient", () => new SqlAzureExecutionStrategy());

        }
    }
}