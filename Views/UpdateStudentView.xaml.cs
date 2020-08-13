using WPFCRUDSample.Models;
using WPFCRUDSample.ViewModels;
using WPFCRUDSample.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFCRUDSample.Views
{
    /// <summary>
    /// Interaction logic for UpdateStudentView.xaml
    /// </summary>
    public partial class UpdateStudentView : Window
    {
        public UpdateStudentView()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            StudentViewModel student = (StudentViewModel) DataContext;


            // ------- Edit Action

            if (!student.Id.Equals(0))
            {
                Student s = MainViewModel.db.Students.Find(student.Id);

                s.Name = student.Name;
                s.Address = student.Address;

                MainViewModel.db.SaveChanges();
                MainViewModel.StudentsListView.Refresh();
            }
            else
            {
                Student s = MainViewModel.db.Students.Add(new Student() { Name = student.Name, Address = student.Address }).Entity;
                MainViewModel.StudentsList.Add(new StudentViewModel(s));
                MainViewModel.db.SaveChanges();
                MainViewModel.StudentsListView.Refresh();
            }

            //{
            //    //    StudentViewModel vm =  new StudentViewModel() { Name = student.Name, Address = student.Address };
            //    //MainViewModel.StudentsList.Add(vm);
            //    //MainViewModel.StudentsList.Add(student);
            //    Student s = db.Students.Add(new Student() { Name = StudentRecord.Name, Address = StudentRecord.Address }).Entity;

            //    MainViewModel.StudentsList.Add(student);
            //    MainViewModel.db.SaveChanges();

            //    MainViewModel.StudentsListView.Refresh();
            //}

            //else
            //    MainViewModel.db.SaveChanges();
            DialogResult = true;
            Close();

        }
    }
}
