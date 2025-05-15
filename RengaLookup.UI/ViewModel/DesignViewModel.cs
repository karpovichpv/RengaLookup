using RengaLookup.Model.Contracts;
using RengaLookup.Model.Implementations;
using System.Collections.Generic;

namespace RengaLookup.UI.ViewModel
{
	public class DesignViewModel : ViewModelBase
	{
		public override IEnumerable<IInterfaceInfo> InfoSet
		{
			get => GetInfoSet();
			set
			{
				RaisePropertyChange(nameof(InfoSet));
			}
		}


		private IEnumerable<IInterfaceInfo> GetInfoSet()
		{
			return new List<IInterfaceInfo>
			{
				GetInterfaceInfo("1"),
				GetInterfaceInfo("2"),
			};
		}

		private static IInterfaceInfo GetInterfaceInfo(string n)
		{
			return new InterfaceInfo()
			{
				Name = $"IInterface name {n}",
				InfoSet = new List<IInfo>()
					{
						new Info()
						{
							Name = $"Some name {1}",
							Type = SyntaxType.Property,
							Value = $"Value {1}"
						},
						new Info()
						{
							Name = $"Some name {2}",
							Type = SyntaxType.Property,
							Value = $"Value {2}"
						}
					}
			};
		}
	}
}
