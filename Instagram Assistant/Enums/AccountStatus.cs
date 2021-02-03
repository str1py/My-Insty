using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.Enums
{
    public class AccountStatus
    {
        public enum Type
        {
            REST,
            WORKING,
            BAN,
            NEEDSCHALLENGE,
            SPAM,
            NEEDSLOGIN,
            OFF
        }
    }
}
