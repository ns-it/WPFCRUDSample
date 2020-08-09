using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WPFCRUDSample.ViewModels;

namespace WPFCRUDSample.Models
{
    public partial class Student : ObservableObject
    {

        [Key]
        private int _id;
        private string _name;
        private string _address;

        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }

        }

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                    return "Unknown";
                return _name;
            }

            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }

        }
        public string Address
        {
            get
            {
                if (string.IsNullOrEmpty(_address))
                    return "Unknown";
                return _address;
            }

            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }

        }

        public override string ToString()
        {
            return "ID: " + Id + ", Name: " + Name + ", Address: " + Address;
        }
    }
}
