using System;
using System.Collections.Generic;
using System.Text;

namespace StockOnline.Models
{
    public class Person : ObservableObject
    {

        Int32 _countryId;
        String _firstName;
        String _lastName;
        String _nextVacationSpot;
        Sex _sex;
        String _state;

        public Int32 CountryId
        {
            get { return _countryId; }
            set
            {
                _countryId = value;
                RaisePropertyChanged();
            }
        }

        public String FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                RaisePropertyChanged();
            }
        }

        public String LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                RaisePropertyChanged();
            }
        }

        public String NextVacationSpot
        {
            get { return _nextVacationSpot; }
            set
            {
                _nextVacationSpot = value;
                RaisePropertyChanged();
            }
        }

        public Sex Sex
        {
            get { return _sex; }
            set
            {
                _sex = value;
                RaisePropertyChanged();
            }
        }

        public String State
        {
            get { return _state; }
            set
            {
                _state = value;
                RaisePropertyChanged();
            }
        }

        public Person()
        {
        }

    }
}
