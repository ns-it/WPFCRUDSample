using Microsoft.EntityFrameworkCore;
using WPFCRUDSample.Models;
using WPFCRUDSample.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace WPFCRUDSample.ViewModels
{
    public class MainViewModel : INotifyCollectionChanged, INotifyPropertyChanged
    {
        private bool isEditModeOn;
        private bool isEditModeOff;

        private Student _studentRecord;
        //private string _idField;
        //private string _nameField;
        //private string _addressField;

        public Student StudentRecord {
            get { return _studentRecord; }
            set
            {
                _studentRecord = value;
                OnPropertyChanged("StudentRecord");
            }
        }

        public static WPFTestContext db = new WPFTestContext();

        public event PropertyChangedEventHandler PropertyChanged;

        public static ObservableCollection<Student> StudentsList { get; set; }
        public static ICollectionView StudentsListView { get; set; }


        public bool IsEditModeOn
        {
            get { return isEditModeOn; }
            set
            {
                isEditModeOn = value;
                OnPropertyChanged("IsEditModeOn");
            }
        }

        public bool IsEditModeOff
        {
            get { return isEditModeOff; }
            set
            {
                isEditModeOff = value;
                OnPropertyChanged("IsEditModeOff");
            }
        }

        //public string IdField {
        //    get { return _idField; }
        //    set
        //    {
        //        _idField = value;
        //        OnPropertyChanged("IdField");
        //    }
        //}
        //public string NameField
        //{
        //    get { return _nameField; }
        //    set
        //    {
        //        _nameField = value;
        //        OnPropertyChanged("NameField");
        //    }
        //}
        //public string AddressField
        //{
        //    get { return _addressField; }
        //    set
        //    {
        //        _addressField = value;
        //        OnPropertyChanged("AddressField");
        //    }
        //}

              //public string AddressField
        //{
        //    get { return _addressField; }
        //    set
        //    {
        //        _addressField = value;
        //        OnPropertyChanged("AddressField");
        //    }
        //}
        public string SearchField { get; set; }


        public NoParamsRelayCommand SaveCommand { get; private set; }
        public NoParamsRelayCommand AddCommand { get; private set; }
        public RelayCommand StartEditCommand { get; private set; }
        public NoParamsRelayCommand CancelCommand { get; private set; }
        public RelayCommand EditCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }




        public MainViewModel()
        {

            StudentRecord = new Student();

            //IdField = "0";
            //NameField = "nameTest";
            //AddressField = "addressTest";
            SearchField = "";

            IsEditModeOn = false;
            IsEditModeOff = true;

            db.Students.Load();
            StudentsList = db.Students.Local.ToObservableCollection();
            StudentsList.CollectionChanged += StudentsList_CollectionChanged;

            StudentsListView = CollectionViewSource.GetDefaultView(StudentsList);

            AddCommand = new NoParamsRelayCommand(AddAction);
            SaveCommand = new NoParamsRelayCommand(SaveAction);
            StartEditCommand = new RelayCommand(StartEditAction,null);
            CancelCommand = new NoParamsRelayCommand(StopEditAction);


            //EditCommand = new RelayCommand(EditAction, null);
            DeleteCommand = new RelayCommand(DeleteAction, null);

            SearchCommand = new RelayCommand(SearchAction, null);

        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add
            {
                ((INotifyCollectionChanged)StudentsList).CollectionChanged += value;
            }

            remove
            {
                ((INotifyCollectionChanged)StudentsList).CollectionChanged -= value;
            }
        }

        public void AddAction()
        {
            //StudentsList.Add(new Student() { Name = NameField, Address = AddressField });
            StudentsList.Add(new Student() { Name = StudentRecord.Name, Address = StudentRecord.Address });
        }

        //public void EditAction(object student)
        //{
        //    Student s = student as Student;

        //    s.Name = NameField;
        //    s.Address = AddressField;

        //    db.Students.Update(s);
        //    db.SaveChanges();

        //    StudentsListView.Refresh();

        //}


        public void SaveAction()
        {

            if (!StudentRecord.Id.Equals(0))
            {
                Student s = db.Students.Find(StudentRecord.Id);

                //s.Name = NameField;
                //s.Address = AddressField;

                s.Name = StudentRecord.Name;
                s.Address = StudentRecord.Address;

                db.Students.Update(s);
                db.SaveChanges();

                StudentsListView.Refresh();
            }
            else
            {
                //StudentsList.Add(new Student() { Name = NameField, Address = AddressField }); 
                StudentsList.Add(new Student() { Name = StudentRecord.Name, Address = StudentRecord.Address });
                StudentsListView.Refresh(); }

            IsEditModeOff = true;
            IsEditModeOn = false;

            //IdField = "";
            //NameField = "";
            //AddressField = "";

            //StudentRecord = new Student();



        }



        public void StartEditAction(object button)
        {

            if (button.ToString().Equals("Add"))
            {
                StudentRecord.Id = 0;
                StudentRecord.Name = "";
                StudentRecord.Address = "";
                //IdField = "";
                //NameField = "";
                //AddressField = "";
            }

            IsEditModeOff = false;
            IsEditModeOn = true;

        }

        public void StopEditAction()
        {

            IsEditModeOff = true;
            IsEditModeOn = false;

        }


        public void DeleteAction(object item)
        {
            Student student = item as Student;
            StudentsList.Remove(student);
        }



        public bool Contains(object de)
        {
            bool accepted = false;
            if (de is Student student)
            {
                accepted = student.Name.Contains(SearchField) || student.Address.Contains(SearchField);
            }
            return accepted;
        }


        public void SearchAction(object item)
        {
            StudentsListView.Filter = new Predicate<object>(Contains);
        }


        protected void OnPropertyChanged(string name)

        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }



        private void StudentsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MessageBox.Show("StudentsList_CollectionChanged");
            db.SaveChanges();
        }


    }



}
