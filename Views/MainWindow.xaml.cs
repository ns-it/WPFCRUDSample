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
using System.Threading;
using System.Globalization;

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


            //Thread.CurrentThread.CurrentCulture = new CultureInfo("ar-SY");

            string currentLanguage =
                System.Globalization.CultureInfo.CurrentUICulture.IetfLanguageTag;

            ////MainUI.Language = System.Windows.Markup.XmlLanguage.GetLanguage("ar-SY");

            if (currentLanguage.ToLower().StartsWith("ar"))
            {
                MainUI.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                MainUI.FlowDirection = FlowDirection.LeftToRight;
            }

            //MessageBox.Show(System.Globalization.CultureInfo.CurrentCulture.IetfLanguageTag);
            //MessageBox.Show(WPFCRUDSample.Properties.Resources.Title);

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
