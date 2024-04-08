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
using System.Windows.Navigation;
using System.Data;
using Microsoft.VisualBasic;


namespace Dat154_oblig3 {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private readonly Dat154Context dx = new();

        private readonly LocalView<Student> Students;
        private readonly LocalView<Course> Courses;
        private readonly LocalView<Grade> Grades;


        public MainWindow() {
            InitializeComponent();


            Students = dx.Students.Local;
            Courses = dx.Courses.Local;
            Grades = dx.Grades.Local;

            dx.Students.Load();

            studentList.DataContext = Students.OrderBy(s => s.Studentname);

            /*mainPage mp = new mainPage();
            this.Content = mp;*/



        }

        private void searchButton_Click(object sender, RoutedEventArgs e) {
            studentList.DataContext = Students.Where(s => s.Studentname.Contains(searchField.Text))
                .OrderBy(s => s.Studentname);
        }

        private void editButton_Click(object sender, RoutedEventArgs e) {

            Editor ed = new() {
                dx = dx
            };

            ed.Show();
        }

        private void studentList_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            Student s = studentList.SelectedItem as Student;
            

            if (s != null) {

                Editor ed = new(s) {
                    dx = dx
                };
                ed.Show();

            }
        }

        private void courseListButton_Click(object sender, RoutedEventArgs e) {



            /*CoursesPage cp = new CoursesPage();
            this.Content = cp;*/

            NavigationWindow window = new NavigationWindow();
            window.Source = new Uri("CoursesPage.xaml", UriKind.Relative);
            window.Show();
            this.Visibility = Visibility.Hidden;


        }


        private void comboboxCoursesButton_Click(object sender, RoutedEventArgs e) {
            string coursecode = comboboxCourses.Text;
            //string coursecode = Courses.Where(c => c.Coursename == coursename).Select(c => c.Coursecode).FirstOrDefault;
            var course = dx.Courses.FirstOrDefault(c => c.Coursecode == coursecode);
            string coursename = course.Coursename;
            

            courseParticipants cp = new courseParticipants(coursecode, coursename);
            cp.Show();



        }
        private void comboboxGradesButton_Click(object sender, RoutedEventArgs e) {
            string grade = comboboxGrades.Text;
            GradesWindow gp = new GradesWindow(grade);
            gp.Show();
            //this.Content = gp;

        }

        

    }
}
