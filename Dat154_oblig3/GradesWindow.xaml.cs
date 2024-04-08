using Dat154_oblig3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dat154_oblig3
{
    /// <summary>
    /// Interaction logic for GradesWindow.xaml
    /// </summary>
    public partial class GradesWindow : Window
    {
        private readonly LocalView<Student> Students;
        private readonly LocalView<Grade> Grades;
        private readonly LocalView<Course> Courses;
        public Dat154Context dx = new();

        public GradesWindow(string grade)
        {
            InitializeComponent();
            Grades = dx.Grades.Local;
            //Grades = dx.Grades.Local;

            dx.Grades.Load();

            gradesList.DataContext = Grades.OrderBy(c => c.Student.Studentname);

            Students = dx.Students.Local;
            Grades = dx.Grades.Local;
            Courses = dx.Courses.Local;
            dx.Grades.Load();
            dx.Students.Load();
            dx.Courses.Load();
            //var filteredGrades = Grades.Where(c => c.Grade1 == "A");

            if (grade == "F") {

                var filteredGrades = from l1 in Students
                                     join l2 in Grades.Where(g => g.Grade1 == "F") on l1.Id equals l2.Studentid
                                     select new {
                                         Studentname = l1.Studentname,
                                         Studentid = l1.Id,
                                         Coursename = dx.Courses.FirstOrDefault(c => c.Coursecode == l2.Coursecode).Coursename,
                                         Coursecode = l2.Coursecode,
                                         Grade1 = l2.Grade1

                                     };
                                     gradesList.ItemsSource = filteredGrades;

            }

            else {
                var filteredGrades = from l1 in Students
                                     join l2 in Grades.Where(g => g.Grade1.CompareTo(grade) <= 0) on l1.Id equals l2.Studentid
                                     select new {
                                         Studentname = l1.Studentname,
                                         Studentid = l1.Id,
                                         Coursename = dx.Courses.FirstOrDefault(c => c.Coursecode == l2.Coursecode).Coursename,
                                         Coursecode = l2.Coursecode,
                                         Grade1 = l2.Grade1
                                     }; //.(l2.Studentid.Equals(l1.Id))
            
                /*var combinedList = from l1 in studentGradesCombined
                                   join l2 in Courses.Where(g => g.Coursecode.Contains(c)) on l1.Coursecode equals l2.Coursecode
                                   select new {Coursename = l2.Coursename};*/

                //, Grade1 = l1.Grades.Where(x => x.Studentid.Equals(l1.Id))
                gradesList.ItemsSource = filteredGrades;

            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e) {
            /*gradesList.DataContext = gradesList.Where(c => c.Student.Studentname.Contains(searchField.Text))
                .OrderBy(c => c.Student.Studentname);*/
        }



        private void gradeList_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            /*
                        Grade g = gradesList.SelectedItem as Grade;
                        string code = c.Coursecode;
                        string name = c.Coursename;
                        if (c != null) {

                            courseParticipants cp = new courseParticipants(code, name) {
                                dx = dx
                            };
                            cp.Show();

                        }*/
        }

        private void backButton_Click(object sender, RoutedEventArgs e) {
            //MainWindow mw = new MainWindow();
            //this.Content = null;

            /*mainPage mp = new mainPage();
            this.Content = mp;*/

            /*MainWindow mw = new MainWindow();
            mw.Show();*/

            MainWindow mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow.Visibility = Visibility.Visible;
            Window win = (Window)this.Parent;
            win.Close();
        }
    }
}
