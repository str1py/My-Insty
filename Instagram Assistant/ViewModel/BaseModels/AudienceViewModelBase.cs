using Instagram_Assistant.Helpers;
using Instagram_Assistant.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Instagram_Assistant.ViewModel.BaseModels
{
    class AudienceViewModelBase : CommonViewModel
    {
        private AudienceStatsModel _stats;
        public new AudienceStatsModel Stats
        {
            get { return _stats; }
            set { _stats = value; OnPropertyChanged(); }
        }

        private AudienceProcessModel audienceProcess;
        public AudienceProcessModel AudienceProcess
        {
            get { return audienceProcess; }
            set
            {
                audienceProcess = value;
                OnPropertyChanged();
            }
        }

        private ICommand _startAudienceCommand;
        public ICommand StartAudienceCommand
        {
            get { return _startAudienceCommand ?? (_startAudienceCommand = new RelayCommand(async p => await StartAudience())); }
        }

        private int comboBoxSelectedIndex;
        public int ComboBoxSelectedIndex
        {
            get { return comboBoxSelectedIndex; }
            set { comboBoxSelectedIndex = value; OnPropertyChanged(); }
        }

        private bool comboBoxEnable = true;
        public bool ComboBoxEnable
        {
            get { return comboBoxEnable; }
            set { comboBoxEnable = value; OnPropertyChanged(); }
        }

        private string collectFrom;
        public string CollectFrom
        {
            get { return collectFrom; }
            set
            {
                collectFrom = value;
                OnPropertyChanged();
                CollectFromList = CollectFromToList(collectFrom);
            }
        }

        private string[] collectFromList;
        public string[] CollectFromList
        {
            get { return collectFromList; }
            set { collectFromList = value; OnPropertyChanged(); }
        }

        public string[] CollectFromToList(string srt)
        {
            char[] delimiterChars = { '\r', '\t', ' ' };
            srt = srt.Replace('\n', ' ');

            string[] massive = srt.Split(delimiterChars);

            foreach (var line in massive)
                line.Trim();

            massive = massive.Where(x => x != "").ToArray();

            return massive;
        }

        public virtual Task StartAudience() { return null; }

    }
}
