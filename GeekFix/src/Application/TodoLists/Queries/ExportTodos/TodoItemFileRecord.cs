using GeekFix.Application.Common.Mappings;
using GeekFix.Domain.Entities;

namespace GeekFix.Application.TodoLists.Queries.ExportTodos
{
    public class TodoItemRecord : IMapFrom<TodoItem>
    {
        public string Title { get; set; }

        public bool Done { get; set; }
    }
}
