using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    internal class NewPolicyDataViewModel : INotifyPropertyChanged
    {
        private bool _isCompany;
        public bool IsCompany
        {
            get => _isCompany;
            set 
            { 
                _isCompany = value;
                NotifyPropertyChanged();
            }
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                NotifyPropertyChanged();
            }
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                NotifyPropertyChanged();
            }
        }
        

        public string CompanyName { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
