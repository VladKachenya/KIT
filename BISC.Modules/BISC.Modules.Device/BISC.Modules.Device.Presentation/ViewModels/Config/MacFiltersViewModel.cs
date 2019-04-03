using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BISC.Presentation.BaseItems.ViewModels;
using BISC.Presentation.Infrastructure.Factories;

namespace BISC.Modules.Device.Presentation.ViewModels.Config
{
	public class MacFiltersViewModel : ViewModelBase
	{
		private string _title;

		public MacFiltersViewModel(ICommandFactory commandFactory)
		{
			MacAddresses = new ObservableCollection<MacAddressViewModel>();

			DeleteFilterCommand = commandFactory.CreatePresentationCommand<object>(OnDeleteFilter);
			AddFilterCommand = commandFactory.CreatePresentationCommand(OnAddFilter);
		}

		public ICommand DeleteFilterCommand { get; }

		public ICommand AddFilterCommand { get; }

		public string Title
		{
			get => _title;
			set => SetProperty(ref _title , value);
		}


		private void OnAddFilter()
		{
			MacAddresses.Add(new MacAddressViewModel());
		}

		private void OnDeleteFilter(object o)
		{
			if (o is MacAddressViewModel macAddress)
			{
				MacAddresses.Remove(macAddress);
			}
		}

		public void SetResultList(List<string> macList,string title)
		{
			MacAddresses.Clear();
			Title = title;
			macList.ForEach((s =>
			{
				MacAddresses.Add(new MacAddressViewModel()
				{
					Value = s,
				});
			}));
		}

		public List<string> GetResultList()
		{
			return MacAddresses.Select((model => model.Value)).ToList();
		}

		public ObservableCollection<MacAddressViewModel> MacAddresses { get; }
	}

	public class MacAddressViewModel : ViewModelBase
	{
		private string _value;

		public string Value
		{
			get => _value;
			set => SetProperty(ref _value, value);
		}
	}
}