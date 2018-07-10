using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BISC.Presentation.Infrastructure.Factories;
using BISC.Presentation.Infrastructure.Services;

namespace BISC.Presentation.Services
{
    public class TreeItemViewModelFactoryRegistryService : ITreeItemViewModelFactoryRegistryService
    {
        private Dictionary<string, ITreeItemViewModelFactory> _modelFactoriesDictionary = new Dictionary<string, ITreeItemViewModelFactory>();

        public void RegisterFactory(ITreeItemViewModelFactory treeItemViewModelFactory, string key)
        {
            if (_modelFactoriesDictionary.ContainsKey(key))
            {
                throw new ArgumentException($"Tree item factory with key {key} already exists");
            }
            _modelFactoriesDictionary.Add(key, treeItemViewModelFactory);
        }

        public bool GetIsFactoryRegistered(string elementName)
        {
            return _modelFactoriesDictionary.ContainsKey(elementName);
        }

        public ITreeItemViewModelFactory GetFactoryByKey(string key)
        {
            if (!_modelFactoriesDictionary.ContainsKey(key)){

                throw new ArgumentException($"Tree item factory with key {key} is not added");

            }
            return _modelFactoriesDictionary[key];
        }
    }
}
