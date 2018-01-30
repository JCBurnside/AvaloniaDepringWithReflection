using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace ReflectionDerping
{
    class BasicViewModel : INotifyPropertyChanged
    {
        #region PrivateVars
        private bool? _IsChecked;
        private double _X;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        #region PublicProps
        public bool? IsChecked
        {
            get => _IsChecked;
            set
            {
                if (value != _IsChecked)
                {
                    _IsChecked = value;
                    OnPropertyChanged();
                }
            }
        }

        public double X 
        {
            get => _X;
            set {
                if(value!=X){
                    _X = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        private void OnPropertyChanged([CallerMemberName]string name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
