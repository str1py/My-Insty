using Instagram_Assistant.Model;
using Instagram_Assistant.Model.Stories;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Instagram_Assistant.Helpers.Story
{
    class StoriesCommon : CommonForAll
    {
        protected List<StoryModel> userstoriesfeed; //List For Feeds from user Instagram
        protected ObservableCollection<ActionModel> storiesactions; //What app doing 
        protected StatsModelBase _storiesstats; //for overview - likes and time stats
    }
}
