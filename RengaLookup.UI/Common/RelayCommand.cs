using System;
using System.Windows.Input;

namespace RengaLookup.UI.Common
{
	public class RelayCommand : ICommand
	{
		private Action<object> _execute;
		private Predicate<object> _canExecute;

		public RelayCommand(Action<object> execute, Predicate<object> canExectute)
		{
			_execute = execute;
			_canExecute = canExectute;
		}

		public event EventHandler CanExecuteChanged
		{
			add
			{
				CommandManager.RequerySuggested += value;
			}
			remove
			{
				CommandManager.RequerySuggested -= value;
			}
		}

		public bool CanExecute(object parameter)
		{
			bool b = _canExecute == null ? true : _canExecute(parameter);

			return b;
		}

		public void Execute(object parameter)
		{
			_execute(parameter);
		}
	}
}
