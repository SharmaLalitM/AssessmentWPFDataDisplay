using AssessmentUI.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentUI.BusinessModel
{
    public class RootConfiguration
    {
        public Nullable<int> Id { get; set; }
        public string ConfigurationName { get; set; }
        public ObservableCollection<ConfigurationItem> ChildConfigurations { get; }

        public RootConfiguration(Configuration config, AssessmentEntities context)
        {
            Id = config.Id;
            ConfigurationName = config.ConfigurationName;
            ChildConfigurations = new ObservableCollection<ConfigurationItem>();
            ChildConfigurations.Add(new ConfigurationItem(config, context));
        }
    }
}
