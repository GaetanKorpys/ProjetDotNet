using ProjetDotNet.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjetDotNet.Commands
{
    internal class UpdateViewCommand : ICommand
    {
        private MainViewModel viewModel;

        public UpdateViewCommand(MainViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(parameter.ToString() == "Investigation")
                viewModel.SelectedViewModel = new InvestigationViewModel();
            else if(parameter.ToString() == "Investigator")
                viewModel.SelectedViewModel = new InvestigatorViewModel();
            else if (parameter.ToString() == "Suspect")
                viewModel.SelectedViewModel = new SuspectViewModel();
            else if (parameter.ToString() == "Complainant")
                viewModel.SelectedViewModel = new ComplainantViewModel();
            else if (parameter.ToString() == "Visit")
                viewModel.SelectedViewModel = new VisitViewModel();
        }   
    }
}
