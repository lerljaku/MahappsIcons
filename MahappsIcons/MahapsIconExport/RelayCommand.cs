using System;
using System.Windows.Input;

namespace MahapsIconExport
{
    public class RelayCommand : ICommand
    {
        private readonly Action m_action;
        private readonly Action<object> m_paramAction;

        public RelayCommand(Action<object> paramAction)
        {
            m_paramAction = paramAction;
        }

        public RelayCommand(Action action)
        {
            m_action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            m_action?.Invoke();
            m_paramAction?.Invoke(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}