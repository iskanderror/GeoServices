using System;
using System.Windows.Input;

namespace GeoServices.Classes
{
  public class DelegateCommand : ICommand
  {
    private readonly Action<object> _method;

    public DelegateCommand(Action<object> method)
    {
      _method = method;
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter)
    {
      return true;
    }

    public void Execute(object parameter)
    {
      _method.Invoke(parameter);
    }
  }
}