using Instagram_Assistant.Model;
using System;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Instagram_Assistant.ViewModel
{
    class SettingsPageViewModel : ViewModelBase
    {
        Properties.Settings settings = Properties.Settings.Default;

        public SettingsPageViewModel()
        {
            DelaySliderValue = settings.DelayValue;
            RandomDelaySliderValue = settings.RandomDelayValue;
            SkipSliderValue = settings.SkipChance;

            DelayStoriesSliderValue = settings.StoriesDelay;
            RandomDelayStoriesSliderValue = settings.RandomStoriesDelay;
            SkipStorySliderValue = settings.StoriesSkipChance;

            DelayUnfollowSliderValue = settings.UnfollowDelay;
            RandomDelayUnfollowSliderValue = settings.RandomUnfollowDelay;
            UnfollowIsChecked = settings.IsUnfollowIfFollowing;

            NotWorkToHour = settings.RestHoursTo;
            NotWorkFromHour = settings.RestHoursFrom;
            RestTimeMinutes = settings.RestTimeMinutes;

            MaxLikesPerHour = settings.MaxUnfollowPerHour;
            MaxStoriesPerHour = settings.MaxStoriesPerHour;
            MaxUnfollowPerHour = settings.MaxUnfollowPerHour;

            StopWordsPath = Properties.Settings.Default.StopWordsPath;
            AllowWordsPath = Properties.Settings.Default.GoWordsPath;
            WordsInAccNamePath = Properties.Settings.Default.WordsInNamePath;
            SaveFilterAudiencePath = Properties.Settings.Default.SaveFilteredAudiencePath;
            SaveAudiencePath = Properties.Settings.Default.SaveAudiencePath;

            IsNameSave = Properties.Settings.Default.IsNameSave;
            IsIdSave = Properties.Settings.Default.IsIdSave;
            IsPhoneSave = Properties.Settings.Default.IsPhoneSave;
            IsEmailSave = Properties.Settings.Default.IsEmailSave;
            MinFollowersCount = Properties.Settings.Default.MinFollowersCount;
            MinMediaCount = Properties.Settings.Default.MinMediaCount;
            AccountType = Properties.Settings.Default.AccountTypeFilter;

            HasBioCheck = Properties.Settings.Default.HasBioParameter;
            HasHLCheck = Properties.Settings.Default.HasHLparameter;
            HasImageCheck = Properties.Settings.Default.HasImageParameter;
            CategoriesToCheck = Properties.Settings.Default.AccountCategoriesFilter;
        }

        #region GENERAL SETTINGS
        private int _notWorkFromHour;
        public int NotWorkFromHour
        {
            get { return _notWorkFromHour; }
            set
            {
                _notWorkFromHour = HourControl(value);
                OnPropertyChanged();

                settings.RestHoursFrom = _notWorkFromHour;
                settings.Save();
            }
        }

        private int _notWorkToHour;
        public int NotWorkToHour
        {
            get { return _notWorkToHour; }
            set
            {
                _notWorkToHour = HourControl(value);
                OnPropertyChanged();

                settings.RestHoursTo = _notWorkToHour;
                settings.Save();
            }
        }

        private int _restTimeMinutes;
        public int RestTimeMinutes
        {
            get { return _restTimeMinutes; }
            set
            {
                if (value.ToString().Length > 3)
                    value = Int32.Parse(value.ToString().Remove(3, 1));
                _restTimeMinutes = value;
                OnPropertyChanged();

                settings.RestTimeMinutes = _restTimeMinutes;
                settings.Save();
            }
        }
        

        private int HourControl(int hour)
        {
            var _hour = MaxValueChecker(hour, 2);
            if (_hour > 24)
                _hour = 24;
            return _hour;
        }
        #endregion

        #region LIKE SETTINGS
        private int _skipSliderValue;
        public int SkipSliderValue
        {
            get { return _skipSliderValue; }
            set {
                _skipSliderValue = value; OnPropertyChanged();

                if (value > 70)
                    _skipSliderValue = 70;

                settings.SkipChance = _skipSliderValue;
             
                AverageLikesCount = $"~{CalculateAverage(SkipSliderValue,DelaySliderValue,RandomDelaySliderValue)}"; //Cant 
                settings.Save();
            }
        }
        private int _delaySliderValue; 
        public int DelaySliderValue
        {
            get { return _delaySliderValue; }
            set
            {
                _delaySliderValue = value; OnPropertyChanged();
                settings.DelayValue = (int)value;
                settings.Save();
                AverageLikesCount = $"~{CalculateAverage(SkipSliderValue, DelaySliderValue, RandomDelaySliderValue)}";
            }
        }
        private int _randomDelaySliderValue;
        public int RandomDelaySliderValue
        {
            get { return _randomDelaySliderValue; }
            set
            {
                _randomDelaySliderValue = value; OnPropertyChanged();
                settings.RandomDelayValue = (int)value;
                settings.Save();
                AverageLikesCount = $"~{CalculateAverage(SkipSliderValue, DelaySliderValue, RandomDelaySliderValue)}";
            }
        }
        private string _averageLikesCount;
        public string AverageLikesCount
        {
            get { return _averageLikesCount; }
            set
            {
                _averageLikesCount = value; OnPropertyChanged();
            }
        }
        #endregion

        #region STORIES SETTINGS
        private int _skipStorySliderValue;
        public int SkipStorySliderValue
        {
            get { return _skipStorySliderValue; }
            set
            {
                _skipStorySliderValue = value; OnPropertyChanged();
                settings.StoriesSkipChance = value;
                settings.Save();
                AverageStoriesCount = $"~{CalculateAverage(SkipStorySliderValue, DelayStoriesSliderValue, RandomDelayStoriesSliderValue)}";
            }
        }
        private int _delayStoriesSliderValue;
        public int DelayStoriesSliderValue
        {
            get { return _delayStoriesSliderValue; }
            set
            {
                _delayStoriesSliderValue = value; OnPropertyChanged();
                settings.StoriesDelay = (int)value;
                settings.Save();
                AverageStoriesCount = $"~{CalculateAverage(SkipStorySliderValue, DelayStoriesSliderValue, RandomDelayStoriesSliderValue)}";
            }
        }
        private int _randomDelayStoriesSliderValue;
        public int RandomDelayStoriesSliderValue
        {
            get { return _randomDelayStoriesSliderValue; }
            set
            {
                _randomDelayStoriesSliderValue = value; OnPropertyChanged();
                settings.RandomStoriesDelay = (int)value;
                settings.Save();
                AverageStoriesCount = $"~{CalculateAverage(SkipStorySliderValue, DelayStoriesSliderValue, RandomDelayStoriesSliderValue)}";
            }
        }

        private string _averageStoriesCount;
        public string AverageStoriesCount
        {
            get { return _averageStoriesCount; }
            set
            {
                _averageStoriesCount = value; OnPropertyChanged();
            }
        }
        #endregion

        #region UNFOLLOW SETTINGS
        private bool _unfollowIsChecked;
        public bool UnfollowIsChecked
        {
            get { return _unfollowIsChecked; }
            set
            {
                _unfollowIsChecked = value; OnPropertyChanged();
                settings.IsUnfollowIfFollowing = value;
                settings.Save();
            }
        }
        private int _delayUnfollowSliderValue;
        public int DelayUnfollowSliderValue
        {
            get { return _delayUnfollowSliderValue; }
            set
            {
                _delayUnfollowSliderValue = value; OnPropertyChanged();
                settings.UnfollowDelay = (int)value;
                settings.Save();
                AverageUnfollowCount = $"~{CalculateAverage( DelayUnfollowSliderValue, RandomDelayUnfollowSliderValue)}";
            }
        }
        private int _randomDelayUnfollowSliderValue;
        public int RandomDelayUnfollowSliderValue
        {
            get { return _randomDelayUnfollowSliderValue; }
            set
            {
                _randomDelayUnfollowSliderValue = value; OnPropertyChanged();
                settings.RandomUnfollowDelay = (int)value;
                settings.Save();
                AverageUnfollowCount = $"~{CalculateAverage(DelayUnfollowSliderValue, RandomDelayUnfollowSliderValue)}";
            }
        }

        private string _averageUnfollowCount;
        public string AverageUnfollowCount
        {
            get { return _averageUnfollowCount; }
            set
            {
                _averageUnfollowCount = value; OnPropertyChanged();
            }
        }
        #endregion

        #region Maximum

        private int _maxLikesPerHour;
        public int MaxLikesPerHour
        {
            get { return _maxLikesPerHour; }
            set
            {
                _maxLikesPerHour = MaxValueChecker(value, 3); OnPropertyChanged();
                settings.MaxLikePerHour = _maxLikesPerHour;
                settings.Save();
            }
        }

        private int _maxStoriesPerHour;
        public int MaxStoriesPerHour
        {
            get { return _maxStoriesPerHour; }
            set
            {
                _maxStoriesPerHour = MaxValueChecker(value, 3); OnPropertyChanged();
                settings.MaxStoriesPerHour = _maxStoriesPerHour;
                settings.Save();
            }
        }

        private int _maxUnfollowPerHour;
        public int MaxUnfollowPerHour
        {
            get { return _maxUnfollowPerHour; }
            set
            {
                _maxUnfollowPerHour = MaxValueChecker(value,3); OnPropertyChanged();
                settings.MaxUnfollowPerHour = _maxUnfollowPerHour;
                settings.Save();
            }
        }


        #endregion

        #region FILTER REGION
        private string _stopWordsPath;
        public string StopWordsPath
        {
            get { return _stopWordsPath; }
            set { _stopWordsPath = value; OnPropertyChanged(); Properties.Settings.Default.StopWordsPath = _stopWordsPath; Properties.Settings.Default.Save(); }
        }

        private string _allowWordsPath;
        public string AllowWordsPath
        {
            get { return _allowWordsPath; }
            set { _allowWordsPath = value; OnPropertyChanged(); Properties.Settings.Default.GoWordsPath = _allowWordsPath; Properties.Settings.Default.Save(); }
        }

        private string _wordsInAccNamePath;
        public string WordsInAccNamePath
        {
            get { return _wordsInAccNamePath; }
            set { _wordsInAccNamePath = value; OnPropertyChanged(); Properties.Settings.Default.WordsInNamePath = _wordsInAccNamePath; Properties.Settings.Default.Save(); }
        }

        private string _saveAudiencePath;
        public string SaveAudiencePath
        {
            get { return _saveAudiencePath; }
            set { _saveAudiencePath = value; OnPropertyChanged(); Properties.Settings.Default.SaveAudiencePath = _saveAudiencePath; Properties.Settings.Default.Save(); }
        }

        private string _saveFilterAudiencePath;
        public string SaveFilterAudiencePath
        {
            get { return _saveFilterAudiencePath; }
            set { _saveFilterAudiencePath = value; OnPropertyChanged(); Properties.Settings.Default.SaveFilteredAudiencePath = _saveFilterAudiencePath; Properties.Settings.Default.Save(); }
        }

        private bool _isNameSave;
        public bool IsNameSave
        {
            get { return _isNameSave; }
            set { _isNameSave = value; OnPropertyChanged(); Properties.Settings.Default.IsNameSave = _isNameSave; Properties.Settings.Default.Save(); }
        }

        private bool _isIdSave;
        public bool IsIdSave
        {
            get { return _isIdSave; }
            set { _isIdSave = value; OnPropertyChanged(); Properties.Settings.Default.IsIdSave = _isIdSave; Properties.Settings.Default.Save(); }
        }

        private bool _isPhoneSave;
        public bool IsPhoneSave
        {
            get { return _isPhoneSave; }
            set { _isPhoneSave = value; OnPropertyChanged(); Properties.Settings.Default.IsPhoneSave = _isPhoneSave; Properties.Settings.Default.Save(); }
        }

        private bool _isEmailSave;
        public bool IsEmailSave
        {
            get { return _isEmailSave; }
            set { _isEmailSave = value; OnPropertyChanged(); Properties.Settings.Default.IsEmailSave = _isEmailSave; Properties.Settings.Default.Save(); }
        }

        private int _minFollowersCount;
        public int MinFollowersCount
        {
            get { return _minFollowersCount; }
            set { _minFollowersCount = value; OnPropertyChanged(); Properties.Settings.Default.MinFollowersCount = _minFollowersCount; Properties.Settings.Default.Save(); }
        }

        private int _minMediaCount;
        public int MinMediaCount
        {
            get { return _minMediaCount; }
            set { _minMediaCount = value; OnPropertyChanged(); Properties.Settings.Default.MinMediaCount = _minMediaCount; Properties.Settings.Default.Save(); }
        }

        private int _accountType;
        public int AccountType
        {
            get { return _accountType; }
            set { _accountType = value; OnPropertyChanged();  Properties.Settings.Default.AccountTypeFilter = _accountType; Properties.Settings.Default.Save(); }
        }

        private bool _hasBioCheck;
        public bool HasBioCheck
        {
            get { return _hasBioCheck; }
            set { _hasBioCheck = value; OnPropertyChanged(); Properties.Settings.Default.HasBioParameter = _hasBioCheck; Properties.Settings.Default.Save(); }
        }

        private bool _hasHLCheck;
        public bool HasHLCheck
        {
            get { return _hasHLCheck; }
            set { _hasHLCheck = value; OnPropertyChanged(); Properties.Settings.Default.HasHLparameter = _hasHLCheck; Properties.Settings.Default.Save(); }
        }

        private bool _hasImageCheck;
        public bool HasImageCheck
        {
            get { return _hasImageCheck; }
            set { _hasImageCheck = value; OnPropertyChanged(); Properties.Settings.Default.HasImageParameter = _hasImageCheck; Properties.Settings.Default.Save(); }
        }

        private string _categoriesToCheck;
        public string CategoriesToCheck
        {
            get { return _categoriesToCheck; }
            set { _categoriesToCheck = value; OnPropertyChanged(); Properties.Settings.Default.AccountCategoriesFilter = _categoriesToCheck; Properties.Settings.Default.Save(); }
        }


        #endregion



        private ICommand _setStopWordsCommand;
        public ICommand SetStopWordsCommand
        {
            get { return _setStopWordsCommand ?? (_setStopWordsCommand = new RelayCommand(p => SetStopWordsPath())); }
        }
        private void SetStopWordsPath()
        {
            CommonOpenFileDialog fileDialog =  new CommonOpenFileDialog();
            fileDialog.Title = "Select stop words path";

            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (fileDialog.FileName.Contains(".txt"))
                    StopWordsPath = fileDialog.FileName;
                else MessageBox.Show("Please select text file (.txt)");
            }
        }

        private ICommand _setGoWordsPathCommand;
        public ICommand SetGoWordsPathCommand
        {
            get { return _setGoWordsPathCommand ?? (_setGoWordsPathCommand = new RelayCommand(p => SetGoWordsPath())); }
        }
        private void SetGoWordsPath()
        {
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog();
            fileDialog.Title = "Select Allow Words Path";
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (fileDialog.FileName.Contains(".txt"))
                    AllowWordsPath = fileDialog.FileName;
                else MessageBox.Show("Please select text file (.txt)");
            }
        }

        private ICommand _setInNameWordsCommand;
        public ICommand SetInNameWordsCommand
        {
            get { return _setInNameWordsCommand ?? (_setInNameWordsCommand = new RelayCommand(p => SetInNameWordsPath())); }
        }
        private void SetInNameWordsPath()
        {
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog();
            fileDialog.Title = "Select allow words in account name path";
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (fileDialog.FileName.Contains(".txt"))
                    WordsInAccNamePath = fileDialog.FileName;
                else MessageBox.Show("Please select text file (.txt)");
            }
        }

        private ICommand _setFilterAudienceCommand;
        public ICommand SetFilterAudienceCommand
        {
            get { return _setFilterAudienceCommand ?? (_setFilterAudienceCommand = new RelayCommand(p => SetFilterAudiencePath())); }
        }
        private void SetFilterAudiencePath()
        {
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog();
            fileDialog.Title = "Select save filter audience path";
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (fileDialog.FileName.Contains(".txt"))
                    SaveFilterAudiencePath = fileDialog.FileName;
                else MessageBox.Show("Please select text file (.txt)");
            }
        }

        private ICommand _setAudienceCommand;
        public ICommand SetAudienceCommand
        {
            get { return _setAudienceCommand ?? (_setAudienceCommand = new RelayCommand(p => SetAudiencePath())); }
        }
        private void SetAudiencePath()
        { 
            CommonOpenFileDialog fileDialog = new CommonOpenFileDialog();
            fileDialog.Title = "Select save audience path";
            if (fileDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (fileDialog.FileName.Contains(".txt"))
                    SaveAudiencePath = fileDialog.FileName;
                else MessageBox.Show("Please select text file (.txt)");
            }
        }

        private int MaxValueChecker(int value, int length)
        {
            var _value = value;
            if (_value.ToString().Length > length)
                return Int32.Parse(_value.ToString().Remove(length, 1));
            else return value;
        }
        private int CalculateAverage(double _skipvalue, double _delay, double _randomdelay)
        {
            if (_delay != 0)
            {
                int value = 0;
                double _time = 3600;
                Random random = new Random();
                do
                {
                    if (random.Next(0, 100) < _skipvalue)
                    {
                        _time -= 5;
                    }
                    else
                    {
                        int a = random.Next(0, 1);
                        if (a == 0)
                            _time = _time - (_delay + _randomdelay);
                        else
                            _time = _time - (_delay - _randomdelay);
                        value++;
                    }
                } while (_time > 0);
                return value;
            }
            else return 0;
        }

        private int CalculateAverage(double _delay, double _randomdelay)
        {
            if (_delay != 0)
            {
                int value = 0;
                double _time = 3600;
                Random random = new Random();
                do
                {
                    int a = random.Next(0, 1);
                    if (a == 0)
                        _time = _time - (_delay + _randomdelay);
                    else
                        _time = _time - (_delay - _randomdelay);
                    value++;
                } while (_time > 0);
                return value;
            }
            else return 0;
        }

        
    }
}
