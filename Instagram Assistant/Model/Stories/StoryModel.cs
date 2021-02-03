using Instagram_Assistant.Model.Base;
using System;

namespace Instagram_Assistant.Model.Stories
{
    class StoryModel : BaseModel
    {
        public DateTime ExpiringAt { get; set; }
        public string StoryId { get; set; }
        public DateTime DeviceTimestamp { get; set; }
    }
}
