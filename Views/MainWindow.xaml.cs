using WPFCRUDSample.Models;
using WPFCRUDSample.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DataGridRow_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (DataGridRow)sender;
            Student student = (Student)item.DataContext;

            stuID.Text = student.Id.ToString();
            stuName.Text = student.Name.ToString();
            stuAddress.Text = student.Address.ToString();

        }

        private void AddModalButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateStudentView updateStudentView = new UpdateStudentView();
            updateStudentView.ShowDialog();
        }

        private void EditModalButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateStudentView updateStudentView = new UpdateStudentView
            {
                DataContext = StudentsGrid.SelectedItem
            };
            updateStudentView.ShowDialog();
        }

    }
}
