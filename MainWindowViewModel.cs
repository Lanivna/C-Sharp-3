using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace C_Sharp_2
{
    class MainWindowViewModel: INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _dateOfBirth;
    
        private string _birthday;
        private string _adult;
        private string _sun;
        private string _chinese;

        private RelayCommand _proceedCommand;
        private bool _canExecute;
        private RelayCommand _ageCalc;
        private readonly Action<bool> _showLoaderAction;
        private readonly Action _closeAction;

        public MainWindowViewModel(Action close, Action<bool> showLoader)
        {
            _closeAction = close;
            _showLoaderAction = showLoader;
            CanExecute = true;
        }

        public string FName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged();
            }
        }

        public string Date
        {
            get { return Date; }
            set
            {
                Date = _dateOfBirth.ToShortDateString(); 
                OnPropertyChanged();
            }
        }

        public string Adult
        {
            get { return _adult; }
            set
            {
                _adult = value;
                OnPropertyChanged();
            }
        }

        public string SunSign
        {
            get { return _sun; }
            set
            {
                _sun = value;
                OnPropertyChanged();
            }
        }

        public string ChineseSign
        {
            get { return _chinese; }
            set
            {
                _chinese = value;
                OnPropertyChanged();
            }
        }

        public string Birthday
        {
            get { return _birthday; }
            set
            {
                _birthday = value;
                OnPropertyChanged();
            }
        }


        public bool CanExecute
        {
            get { return _canExecute; }
            private set
            {
                _canExecute = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand CountAge
        {
            get { return _ageCalc ?? (_ageCalc = new RelayCommand(CalcImpl)); }
        }

        private async void CalcImpl(object o)
        {
            _showLoaderAction.Invoke(true);
            CanExecute = false;
            await Task.Run((() =>
            {
                StationManager.CurrentPerson = PersonAdapter.CreateUser(_firstName, _lastName, _email, _dateOfBirth);
                Thread.Sleep(600);
            }));

            if (StationManager.CurrentPerson == null)
                MessageBox.Show($"Wrong input!");
            else
            {
                if (_dateOfBirth.DayOfYear == DateTime.Today.DayOfYear)
                { MessageBox.Show($"Happy b-day to you! Happy b-day, dear {FName}!");}

                Adult = $"{StationManager.CurrentPerson.IsAdult}";
                SunSign = $"{StationManager.CurrentPerson.SunSign}";
                ChineseSign = $"{StationManager.CurrentPerson.ChineseSign}";
                Birthday = $"{StationManager.CurrentPerson.IsBirthday}";
                _closeAction.Invoke();
            }

            CanExecute = true;

            _showLoaderAction.Invoke(false);
        }

        public RelayCommand ProceedCommand
        {
            get
            {
                return _proceedCommand ?? (_proceedCommand = new RelayCommand( CalcImpl,
                           o => !String.IsNullOrWhiteSpace(_firstName) &&
                                !String.IsNullOrWhiteSpace(_lastName) &&
                                !String.IsNullOrWhiteSpace(_email) &&
                                DateCheck(_dateOfBirth)));
            }
        }

        private bool DateCheck(DateTime dateOfBirth)
        {
            return dateOfBirth < DateTime.Today && dateOfBirth.Year > 1883;
        }

        #region Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


    }
}
