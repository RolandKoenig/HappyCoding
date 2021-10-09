using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HappyCoding.WinUISimple.Util;

namespace HappyCoding.WinUISimple.Views
{
    public class InputFormViewModel : PropertyChangedBase
    {
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    this.RaisePropertyChanged();
                    this.RaisePropertyChanged(nameof(this.FullName));
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    this.RaisePropertyChanged();
                    this.RaisePropertyChanged(nameof(this.FullName));
                }
            }
        }

        public string FullName
        {
            get
            {
                var strBuilder = new StringBuilder(_firstName.Length + _lastName.Length + 1);
                strBuilder.Append(_firstName);
                if (strBuilder.Length > 0) { strBuilder.Append(' '); }
                strBuilder.Append(_lastName);
                return strBuilder.ToString();
            }
        }

    }
}
