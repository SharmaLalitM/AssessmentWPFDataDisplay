using AssessmentUI.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentUI.BusinessModel
{
    public class ConfigurationProvider
    {
        private static ConfigurationProvider _instance;
        private static readonly object _lockObj = new object();
        private AssessmentEntities _context;

        private ConfigurationProvider()
        {
            _context = new AssessmentEntities();
        }

        public static ConfigurationProvider Instance
        {
            get
            {
                lock(_lockObj)
                {
                    if (_instance == null)
                        _instance = new ConfigurationProvider();
                }

                return _instance;
            }
        }

        public ObservableCollection<RootConfiguration> GetConfigurations()
        {
            var items = _context.Configurations.Where(x => x.IsRoot.HasValue && x.IsRoot.Value && !string.IsNullOrEmpty(x.ConfigurationName));
            var rootItems = new ObservableCollection<RootConfiguration>();

            foreach(var item in items)
            {
                rootItems.Add(new RootConfiguration(item, _context));
            }

            return rootItems;
        }
    }
}
