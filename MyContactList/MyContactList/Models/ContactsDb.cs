using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MyContactList.Models
{
    [Table("ContactsTable")]
   public class ContactsDb : INotifyPropertyChanged
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private string _firstName;
        [NotNull]
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                this._firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        private string _lastName;
        [MaxLength(50)]
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                this._lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        private string _emailAddress;
        public string EmailAddress
        {
            get
            {
                return _emailAddress;
            }
            set
            {
                _emailAddress = value;
                OnPropertyChanged(nameof(EmailAddress));
            }
        }

        private string _contactType;
        public string ContactType
        {
            get
            {
                return _contactType;
            }
            set
            {
                _contactType = value;
                OnPropertyChanged(nameof(ContactType));
            }
        }

        public string NameSort => _lastName[0].ToString();

        public string FirstLastName { get { return FirstName + " " + LastName; } }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}