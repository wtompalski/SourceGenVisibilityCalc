using Models;
using RulesEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI.ViewModels;

namespace UI
{
    internal class VisibilityUpdater
    {
        private readonly NewPolicyDataViewModel _dataViewModel;
        private readonly NewPolicyVisibilityViewModel _visibilityViewModel;

        public VisibilityUpdater(NewPolicyDataViewModel dataViewModel, NewPolicyVisibilityViewModel visibilityViewModel)
        {
            _dataViewModel = dataViewModel;
            _visibilityViewModel = visibilityViewModel;
        }

        public void Attach()
        {
            _dataViewModel.PropertyChanged += ViewModelPropertyChanged;
        }

        public void Detach()
        {
            _dataViewModel.PropertyChanged -= ViewModelPropertyChanged;
        }

        private void ViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var rulesEngine = new RulesEngine.RulesEngine();

            var newPolicyModel = new NewPolicyModel
            {
                IsCompany = _dataViewModel.IsCompany,
                CompanyName = _dataViewModel.CompanyName,
                FirstName = _dataViewModel.FirstName,
                LastName = _dataViewModel.LastName,
                HasDriverLicence = _dataViewModel.HasDriverLicence,
                HasChildren = _dataViewModel.HasChildren,
                AnnualMilage = _dataViewModel.AnnualMilage,
            };

            var visibility = rulesEngine.CalculateVisibility(newPolicyModel);

            _visibilityViewModel.CompanyDetailsVisible = visibility.CompanyDetailsVisible;
            _visibilityViewModel.PersonDetailsVisible = visibility.PersonDetailsVisible;
            _visibilityViewModel.ChildrenDetailsVisible = visibility.ChildrenDetailsVisible;
            _visibilityViewModel.DriverDetailsVisible = visibility.DriverDetailsVisible;
        }
    }
}
