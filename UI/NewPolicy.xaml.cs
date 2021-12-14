using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UI.ViewModels;

namespace UI
{
    /// <summary>
    /// Interaction logic for NewPolicy.xaml
    /// </summary>
    public partial class NewPolicy : Window
    {
        private VisibilityUpdater _visibilityUpdater;

        public NewPolicy()
        {
            InitializeComponent();
        }

        private void NewPolicy_Initialized(object sender, EventArgs e)
        {
            var dataViewModel = new NewPolicyDataViewModel();
            var visibilityViewModel = new NewPolicyVisibilityViewModel();
            DataContext = new
            {
                Data = dataViewModel,
                Visibility = visibilityViewModel,

            };

            _visibilityUpdater = new VisibilityUpdater(dataViewModel, visibilityViewModel);
            _visibilityUpdater.Attach();
        }

        private void NewPolicy_Closing(object sender, CancelEventArgs e)
        {
            _visibilityUpdater?.Detach();
        }
    }
}
