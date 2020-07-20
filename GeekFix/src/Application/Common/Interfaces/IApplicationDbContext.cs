using GeekFix.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace GeekFix.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }

        // DbSet<Result> Results { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
