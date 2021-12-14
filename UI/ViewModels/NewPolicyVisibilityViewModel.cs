using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    internal class NewPolicyVisibilityViewModel : INotifyPropertyChanged
    {
        private bool _personDetailsVisible;
        public bool PersonDetailsVisible
        {
            get => _personDetailsVisible;
            set
            {
                _personDetailsVisible = value;
                NotifyPropertyChanged();
            }
        }

        private bool _CompanyDetailsVisible;
        public bool CompanyDetailsVisible
        {
            get => _CompanyDetailsVisible;
            set
            {
                _CompanyDetailsVisible = value;
                NotifyPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
