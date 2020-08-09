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
            Student student = (Student)DataContext;
            if (Int32.Parse(stuID.Text) == 0)
            {
                

                MainViewModel.StudentsList.Add(new Student() { Name = student.Name, Address = student.Address });
                MainViewModel.StudentsListView.Refresh();
            }

            else
                MainViewModel.db.SaveChanges();
            Close();

        }
    }
}
