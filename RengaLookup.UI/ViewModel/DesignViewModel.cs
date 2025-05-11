using RengaLookup.Model;
using RengaLookup.UI.Model.Implementations;
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
				new InterfaceInfo()
				{
					Name = "IInterface name",
					InfoSet = new List<IInfo>()
					{
						new Info()
						{
							Name = "Some name",
							Type = SyntaxType.Property,
							Value = "Value"
						}
					}
				}

			};
		}
	}
}
