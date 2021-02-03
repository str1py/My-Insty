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
    class GeoLikePageViewModel : CommonViewModel
    {

        private static GeoLikePageViewModel geolikeinstance;
        public static GeoLikePageViewModel Instance
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

        private GeoLikeHelper geoHelper;

        public GeoLikePageViewModel()
        {
            geoHelper = new GeoLikeHelper(this);
        }

        public override async Task Start()
        {
            if (mainVars.IsGeoLikeInProgress == false)
            {
                LastActionTextHelper = "";
                ButtonContent = "Stop";
                await geoHelper.BeginLike(SelectedGeoItem);
            }
            else
            {
                geoHelper.Stop(this);
                ButtonContent = "Start";
            }
        }

        private Visibility loadingVisibility = Visibility.Hidden;
        public Visibility LoadingVisibility
        {
            get { return loadingVisibility; }
            set { loadingVisibility = value; OnPropertyChanged(); }
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
            if (list != null)
            {
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
            }else           
                logs.Add("Something went wrong. May be you dont select main account.", MessageType.Type.ERROR, this.GetType().Name);
            
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

        private ICommand selectGeoCommand;
        public ICommand SelectGeoCommand
        {
            get { return selectGeoCommand ?? (selectGeoCommand = new RelayCommand(p => SetSelectedGeo())); }
        }

        private void SetSelectedGeo()
        {
            logs.Add($"Selected geo : {SelectedGeoItem.Name}", MessageType.Type.DEBUGINFO, this.GetType().Name);
            var a = SearchResults.Where(x=>x.Name == SelectedGeoItem.Name).FirstOrDefault();
        }
        #endregion

        #region PANEL
        private Visibility stPanelHideVisibility = Visibility.Hidden;
        public Visibility StPanelHideVisibility
        {
            get { return stPanelHideVisibility; }
            set { stPanelHideVisibility = value; OnPropertyChanged(); }
        }
        private Visibility stPanelShowVisibility = Visibility.Visible;
        public Visibility StPanelShowVisibility
        {
            get { return stPanelShowVisibility; }
            set { stPanelShowVisibility = value; OnPropertyChanged(); }
        }

        private ICommand stPanelShowCommand;
        public ICommand StPanelShowCommand
        {
            get { return stPanelShowCommand ?? (stPanelShowCommand = new RelayCommand(p => ShowShowHideStPanel())); }
        }

        private ICommand stPanelHideCommand;
        public ICommand StPanelHideCommand
        {
            get { return stPanelHideCommand ?? (stPanelHideCommand = new RelayCommand(p => ShowShowHideStPanel())); }
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
