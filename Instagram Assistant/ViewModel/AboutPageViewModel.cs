using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Instagram_Assistant.ViewModel
{
    class AboutPageViewModel :ViewModelBase
    {
  
        public AboutPageViewModel()
        {
            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            License = "Developer";
        }
        private string _programState = "Alpha";
        public string ProgramState
        {
            get { return _programState; }
            set { _programState = value; OnPropertyChanged(); }
        }


        private string _version;
        public string Version
        {
            get { return _version; }
            set { _version = value; OnPropertyChanged(); }
        }

        private string _license;
        public string License
        {
            get { return _license; }
            set { _license = value; OnPropertyChanged(); }
        }

    }
}
