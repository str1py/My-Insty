using Instagram_Assistant.Helpers;
using Instagram_Assistant.Helpers.Like;
using Instagram_Assistant.Model;
using Instagram_Assistant.ViewModel.BaseModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Instagram_Assistant.ViewModel
{
    class HashtagLikePageViewModel : LikeViewModelBase
    {
        private static HashtagLikePageViewModel htlikeinstance;
        public static HashtagLikePageViewModel Instanse
        {
            get
            {
                if (htlikeinstance == null)
                {
                    htlikeinstance = new HashtagLikePageViewModel();
                }
                return htlikeinstance;
            }
        }

        private HashtagLikeHelper htHelper = new HashtagLikeHelper();

        public HashtagLikePageViewModel()
        {
            ButtonContent = "Start";
            LastActionTextHelper = "No actions yet";
            LikeStats = new StatsModelBase
            {
                Count = helper.BigNumbersCutting(0),
                SessionCount = "0",
                Status = "OFF",
                NextSessionIn = "00:00:00",
                TimeInWork = "00:00:00",
                NextIn = 0
            };
        }

        private Visibility _loadingVisibility = Visibility.Hidden;
        public Visibility LoadingVisibility
        {
            get { return _loadingVisibility; }
            set { _loadingVisibility = value; OnPropertyChanged(); }
        }

        private ICommand _startLikeCommand;
        public ICommand StartLikeCommand
        {
            get { return _startLikeCommand ?? (_startLikeCommand = new RelayCommand(p => StartLike())); }
        }

        public override async Task StartLike()
        {
            if (mainVars.IsHashtagLikeInProgres == false)
            {
                LastActionTextHelper = "";
                ButtonContent = "Stop";
                await htHelper.BeginLike(HashtagString);
            }
            else
            {
                htHelper.StopLike(this);
                ButtonContent = "Start";
            }
        }


        #region HashtagList
        private ObservableCollection<SearchResultModel> _searchResults;
        public ObservableCollection<SearchResultModel> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                OnPropertyChanged();
            }
        }

        private string _hashtagString;
        public string HashtagString
        {
            get { return _hashtagString; }
            set
            {
                _hashtagString = value;
                OnPropertyChanged();
              
              SetHashtagList();
            }
        }

        public async void SetHashtagList()
        {
            SearchResults = SearchResults ?? new ObservableCollection<SearchResultModel>();

            LoadingVisibility = Visibility.Visible;
            SearchResults?.Clear();
            var list = await htHelper.SearchHashtag(HashtagString.Replace('#',' '));
            foreach (var hashtag in list)
            {
                await App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                 {
                     SearchResults?.Add(new SearchResultModel
                     {
                         Name = hashtag.Name,
                         Count = helper.BigNumbersCutting(hashtag.MediaCount) + " posts"
                     });
                 });
            }
            LoadingVisibility = Visibility.Hidden;
        }
        #endregion

        #region SelectHashtag
        private SearchResultModel _selectedHashtagItem;
        public SearchResultModel SelectedHashtagItem
        {
            get { return _selectedHashtagItem; }
            set { _selectedHashtagItem = value; OnPropertyChanged(); }
        }

        private ICommand _selectHashtagCommand;
        public ICommand SelectHashtagCommand
        {
            get { return _selectHashtagCommand ?? (_selectHashtagCommand = new RelayCommand(p => SetSelectedHashtag())); }
        }

        private void SetSelectedHashtag()
        {
            logs.Add($"Selected geo : {SelectedHashtagItem.Name}", MessageType.Type.DEBUGINFO, this.GetType().Name);
            var a = SearchResults.Where(x => x.Name == SelectedHashtagItem.Name).FirstOrDefault();
            HashtagString = a.Name;
        }
        #endregion

        #region PANEL
        private Visibility _stPanelHideVisibility = Visibility.Hidden;
        public Visibility StPanelHideVisibility
        {
            get { return _stPanelHideVisibility; }
            set { _stPanelHideVisibility = value;OnPropertyChanged(); }
        }
        private Visibility _stPanelShowVisibility = Visibility.Visible;
        public Visibility StPanelShowVisibility
        {
            get { return _stPanelShowVisibility; }
            set { _stPanelShowVisibility = value; OnPropertyChanged(); }
        }

        private ICommand _stPanelShowCommand;
        public ICommand StPanelShowCommand
        {
            get { return _stPanelShowCommand ?? (_stPanelShowCommand = new RelayCommand(p => ShowShowHideStPanel())); }
        }

        private ICommand _stPanelHideCommand;
        public ICommand StPanelHideCommand
        {
            get { return _stPanelHideCommand ?? (_stPanelHideCommand = new RelayCommand(p => ShowShowHideStPanel())); }
        }

        private void ShowShowHideStPanel()
        {
            if(StPanelShowVisibility == Visibility.Visible)
            {
                StPanelShowVisibility = Visibility.Hidden;
                StPanelHideVisibility = Visibility.Visible;
            }
            else
            {
                StPanelShowVisibility = Visibility.Visible;
                StPanelHideVisibility = Visibility.Hidden;
            }

        }
        #endregion

    }
}
