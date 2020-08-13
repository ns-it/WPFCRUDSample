using System;
using System.Collections.Generic;
using System.Text;
using WPFCRUDSample.Models;

namespace WPFCRUDSample.ViewModels
{
    public class StudentViewModel : ObservableObject
    {
        private Student model;
        private WPFTestContext db = new WPFTestContext();

        public StudentViewModel(Student student) { Model = student; }
        public StudentViewModel() : this(new Student()) { }
        public Student Model { get => model; set => model = value; }

        public int Id
        {
            get { return Model.Id; }
            set { Model.Id = value; OnPropertyChanged("Id"); }
        }

        public string Name
        {
            get { return Model.Name; }
            set { Model.Name = value; OnPropertyChanged("Name"); }
        }
        public string Address
        {
            get { return Model.Address; }
            set { Model.Address = value; OnPropertyChanged("Address"); }
        }


        public override string ToString()
        {
            return "ID: " + Id + ", Name: " + Name + ", Address: " + Address;
        }

        public StudentViewModel Clone()
        {
          return  this.MemberwiseClone() as StudentViewModel;

            //return new StudentViewModel() { Model = this.Model };
        }








    }
}
