using Instagram_Assistant.Model.Base;
using System.Collections.Generic;

namespace Instagram_Assistant.Model.Spy
{
    class SpyModel : BaseModel
    {
        public List<string> Posts { get; set; }
        public List<string> Stories { get; set; }
    }
}
