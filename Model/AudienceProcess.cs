using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.Model
{
    class AudienceProcessModel
    {
        public AudienceProcessModel(string action, double? percent)
        {
            Message = action;
            Percent = percent;
        }
        public string Message { get; set; }
        public double? Percent { get; set; }
    }
}
