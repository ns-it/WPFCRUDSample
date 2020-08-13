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

        private StudentViewModel _studentRecord;
        private StudentViewModel _selectedStudent;

        public StudentViewModel StudentRecord
        {
            get { return _studentRecord; }
            set { _studentRecord = value; OnPropertyChanged("StudentRecord"); }
        }

        public StudentViewModel SelectedStudent
        {
            get { return _selectedStudent; }
            set { _selectedStudent = value; OnPropertyChanged("SelectedStudent"); }
        }


        public static WPFTestContext db = new WPFTestContext();
        public event PropertyChangedEventHandler PropertyChanged;
        public static ObservableCollection<StudentViewModel> StudentsList { get; set; }
        public static ICollectionView StudentsListView { get; set; }

        public bool IsEditModeOn
        {
            get { return isEditModeOn; }
            set { isEditModeOn = value; OnPropertyChanged("IsEditModeOn"); }
        }

        public bool IsEditModeOff
        {
            get { return isEditModeOff; }
            set { isEditModeOff = value; OnPropertyChanged("IsEditModeOff"); }
        }

        public string SearchField { get; set; }

        public RelayCommand SaveCommand { get; private set; }
        //public NoParamsRelayCommand AddCommand { get; private set; }
        public RelayCommand StartEditCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }
        //public RelayCommand EditCommand { get; private set; }
        public RelayCommand DeleteCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand SelectedItemChangedCommand { get; private set; }


        public MainViewModel()
        {

            StudentRecord = new StudentViewModel() { Id = 0, Name = "", Address = "" };

            SearchField = "";

            IsEditModeOn = false;
            IsEditModeOff = true;

            db.Students.Load();

            StudentsList = new ObservableCollection<StudentViewModel>();

            foreach (Student s in db.Students)
            {
                StudentsList.Add(new StudentViewModel(s));
            }

            //StudentsList = 

            //db.Students.Local.ToObservableCollection();



            StudentsList.CollectionChanged += StudentsList_CollectionChanged;

            StudentsListView = CollectionViewSource.GetDefaultView(StudentsList);

            //AddCommand = new NoParamsRelayCommand(AddAction);

            SaveCommand = new RelayCommand(SaveAction, null);
            StartEditCommand = new RelayCommand(StartEditAction, null);
            CancelCommand = new RelayCommand(StopEditAction, null);


            //EditCommand = new RelayCommand(EditAction, null);
            DeleteCommand = new RelayCommand(DeleteAction, null);

            SearchCommand = new RelayCommand(SearchAction, null);
            SelectedItemChangedCommand = new RelayCommand(SelectedItemChangedAction, null);

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



        public void SelectedItemChangedAction(object student)
        {
            //StudentRecord = student as StudentViewModel;


            if (SelectedStudent!=null)
            {
                StudentRecord.Id = SelectedStudent.Id;
                StudentRecord.Name = SelectedStudent.Name;
                StudentRecord.Address = SelectedStudent.Address;
            }

            else
            {
                StudentRecord.Id = 0;
                StudentRecord.Name = "";
                StudentRecord.Address = "";
            }


            //StudentsListView.Refresh();

        }

        //public void AddAction()
        //{
        //    //StudentsList.Add(new StudentViewModel() { Name = NameField, Address = AddressField });
        //    StudentsList.Add(new StudentViewModel() { Name = StudentRecord.Name, Address = StudentRecord.Address });
        //}

        public void SaveAction(object selectedItem)
        {

            //Edit Action
            if (!StudentRecord.Id.Equals(0))
            {
                //StudentRecord.UpdateStudent();

                Student s = db.Students.Find(StudentRecord.Id);



                //StudentViewModel vm = new StudentViewModel(s);

                //StudentsList.Remove(vm);

                //s.Name = NameField;
                //s.Address = AddressField;

                s.Name = StudentRecord.Name;
                s.Address = StudentRecord.Address;


                //StudentsList.Remove(vm);
                //StudentsList.Add(StudentRecord);



                db.Students.Update(s);
                db.SaveChanges();


                StudentsListView.Refresh();


            }
            else
            {
                //StudentsList.Add(new StudentViewModel() { Name = NameField, Address = AddressField }); 


                //db.Students.Add(new Student() { Name = StudentRecord.Name, Address = StudentRecord.Address });

                //StudentViewModel vm = new StudentViewModel() { Name = StudentRecord.Name, Address = StudentRecord.Address };

                //StudentsList.Add();
                Student s = db.Students.Add(new Student() { Name = StudentRecord.Name, Address = StudentRecord.Address }).Entity;
                StudentsList.Add(new StudentViewModel(s));
                db.SaveChanges();

                StudentsListView.Refresh();

                SelectedStudent = null;
            }

            IsEditModeOff = true;
            IsEditModeOn = false;

            //StudentRecord.Id = 0;
            //StudentRecord.Name = "";
            //StudentRecord.Address = "";

            //StudentViewModel student = selectedItem as StudentViewModel;

            //StudentRecord.Id = student.Id;
            //StudentRecord.Name = student.Name;
            //StudentRecord.Address = student.Address;

            //SelectedStudent = null;

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

        public void StopEditAction(object selectedItem)
        {
            IsEditModeOff = true;
            IsEditModeOn = false;

            //StudentViewModel student = selectedItem as StudentViewModel;

            //StudentRecord.Id = student.Id;
            //StudentRecord.Name = student.Name;
            //StudentRecord.Address = student.Address;

            StudentRecord.Id = SelectedStudent.Id;
            StudentRecord.Name = SelectedStudent.Name;
            StudentRecord.Address = SelectedStudent.Address;
        }


        public void DeleteAction(object item)
        {
            //StudentViewModel student = item as StudentViewModel;
            //StudentsList.Remove(student);
            //db.Students.Remove(student.Model);

            StudentsList.Remove(SelectedStudent);
            db.Students.Remove(SelectedStudent.Model);
            db.SaveChanges();
        }

        //public bool CanModify(object item)
        //{
        //    if (item != null)
        //    {
        //        if (item.ToString() == "Add") return true;
        //    }
        //    return SelectedStudent != null;
        //}

        public bool Contains(object de)
        {
            bool accepted = false;
            if (de is StudentViewModel student)
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }



        private void StudentsList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MessageBox.Show("StudentsList_CollectionChanged");
            //db.SaveChanges();
        }


    }



}
