using GeekFix.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace GeekFix.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
