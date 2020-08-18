using AssessmentUI.BusinessModel;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AssessmentUI
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChange(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        private ObservableCollection<RootConfiguration> _rootConfigurations;

        public ObservableCollection<RootConfiguration> RootConfigurations
        {
            get
            {
                return _rootConfigurations;
            }
        }

        public MainWindowViewModel()
        {
            _rootConfigurations = ConfigurationProvider.Instance.GetConfigurations();
            ExportJSONCommand = new RelayCommand(x=> ExportJSON());
        }

        public ICommand ExportJSONCommand
        {
            get; private set;
        }

        public void ExportJSON()
        {
            var configurationCollection = new List<ConfigurationItem>();
            foreach (var col in RootConfigurations)
            {
                configurationCollection.AddRange(col.ChildConfigurations);
            }

            string json = JsonConvert.SerializeObject(configurationCollection);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName, json);
        }
    }
}
