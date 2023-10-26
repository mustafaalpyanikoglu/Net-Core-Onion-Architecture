using Core.Application.Requests;
using Core.Persistence.Dynamic;

namespace Entities.Concrete
{
    public class QueryModel
    {
        public PageRequest PageRequest { get; set; }
        public DynamicQuery? DynamicQuery { get; set; }
    }
}
