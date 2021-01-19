using Instagram_Assistant.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Instagram_Assistant.Helpers.Like;
using System.Collections.ObjectModel;
using Instagram_Assistant.Helpers;
using Instagram_Assistant.ViewModel.BaseModels;

namespace Instagram_Assistant.ViewModel
{
    class GeoLikePageViewModel : LikeViewModelBase
    {

        private static GeoLikePageViewModel geolikeinstance;
        public static GeoLikePageViewModel Instanse
        {
            get
            {
                if (geolikeinstance == null)
                {
                    geolikeinstance = new GeoLikePageViewModel();
                }
                return geolikeinstance;
            }
        }

        private GeoLikeHelper geoHelper = new GeoLikeHelper();

        public GeoLikePageViewModel()
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
            if (mainVars.IsGeoLikeInProgress == false)
            {
                LastActionTextHelper = "";
                ButtonContent = "Stop";
                await geoHelper.BeginLike(SelectedGeoItem);
            }
            else
            {
                geoHelper.StopLike(this);
                ButtonContent = "Start";
            }
        }

        #region GeoList

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

        private string _geoString;
        public string GeoString
        {
            get { return _geoString; }
            set {
                _geoString = value; 
                OnPropertyChanged();
                SetGeoList();
            }
        }

        public async void SetGeoList()
        {
            SearchResults = SearchResults ?? new ObservableCollection<SearchResultModel>();

            LoadingVisibility = Visibility.Visible;
            SearchResults?.Clear();

            var list = await geoHelper.SearchGeo(_geoString);

            foreach (var geo in list)
            {

                await App.Current.Dispatcher.BeginInvoke((Action)delegate ()
                {
                    SearchResults?.Add(new SearchResultModel
                    {
                        Name = geo.Name,
                        Id = geo.ExternalId
                    });
                });
            }
            LoadingVisibility = Visibility.Hidden;
        }
        #endregion

        #region SelectGeo
        private SearchResultModel _selectedGeoItem;
        public SearchResultModel SelectedGeoItem
        {
            get { return _selectedGeoItem; }
            set { _selectedGeoItem = value;  OnPropertyChanged();}
        }

        private ICommand _selectGeoCommand;
        public ICommand SelectGeoCommand
        {
            get { return _selectGeoCommand ?? (_selectGeoCommand = new RelayCommand(p => SetSelectedGeo())); }
        }

        private void SetSelectedGeo()
        {
            logs.Add($"Selected geo : {SelectedGeoItem.Name}", MessageType.Type.DEBUGINFO, this.GetType().Name);
            var a = SearchResults.Where(x=>x.Name == SelectedGeoItem.Name).FirstOrDefault();
        }
        #endregion

        #region PANEL
        private Visibility _stPanelHideVisibility = Visibility.Hidden;
        public Visibility StPanelHideVisibility
        {
            get { return _stPanelHideVisibility; }
            set { _stPanelHideVisibility = value; OnPropertyChanged(); }
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
            if (StPanelShowVisibility == Visibility.Visible)
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
