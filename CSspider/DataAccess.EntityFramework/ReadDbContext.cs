using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EntityFramework
{
    /// <summary>
    /// R上下文
    /// </summary>
    public class ReadDbContext: BaseDbContext
    {
        public ReadDbContext(DbContextOptions<ReadDbContext> options)
         : base(options) { }
    }
}
