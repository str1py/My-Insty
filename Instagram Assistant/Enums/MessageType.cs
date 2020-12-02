using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.Helpers
{
    public class MessageType
    {
        public enum Type
        {
            ERROR,
            DEBUGINFO,
            LIKE,
            FOLLOW,
            UNFOLLOW,
            STORY,
            AUDIENCE,
            HIDDEN
        }
    }
}
