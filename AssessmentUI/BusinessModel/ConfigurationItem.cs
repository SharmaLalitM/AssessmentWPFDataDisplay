using AssessmentUI.DataModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace AssessmentUI.BusinessModel
{
    public class ConfigurationItem
    {
        public int C__id { get; set; }
        public Nullable<int> Id { get; set; }
        public string PartNo { get; set; }
        public string Description { get; set; }
        public string Revision { get; set; }
        public string FilePath { get; set; }
        public string ConfigurationName { get; set; }
        public string BomPartNumber { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsRoot { get; set; }
        public byte[] Preview { get; set; }
        public double? Quantity { get; set; }
        public ObservableCollection<ConfigurationItem> ChildConfigurations { get; }


        public ConfigurationItem(Configuration config, AssessmentEntities context)
        {
            C__id = config.C__id;
            Id = config.Id;
            PartNo = config.PartNo;
            Description = config.Description;
            Revision = config.Revision;
            FilePath = config.FilePath;
            ConfigurationName = config.ConfigurationName;
            BomPartNumber = config.BomPartNumber;
            IsActive = config.IsActive;
            IsRoot = config.IsRoot;
            Preview = config.Preview;
            Quantity = 1;

            ChildConfigurations = new ObservableCollection<ConfigurationItem>();

            var bomChildEntries = context.Boms.Where(x => x.ParentConfigIndex == config.Id);
            foreach(var entry in bomChildEntries)
            {
                ChildConfigurations.Add(
                    new ConfigurationItem(
                        context.Configurations.Where(x => x.Id == entry.ChildConfigIndex).First(), context
                        )
                    { Quantity = entry.Qty });
            }
        }
    }
}
