using Dat154_oblig3.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
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
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace Dat154_oblig3 {
    /// <summary>
    /// Interaction logic for courseParticipants.xaml
    /// </summary>
    public partial class courseParticipants : Window {
        private readonly LocalView<Student> Students;
        private readonly LocalView<Grade> Grades;
        private readonly LocalView<Course> Courses;


        public Dat154Context dx = new();


        public courseParticipants(string code, string name) {
            InitializeComponent();

            Students = dx.Students.Local;
            Grades = dx.Grades.Local;
            Courses = dx.Courses.Local;
            dx.Grades.Load();
            dx.Students.Load();

            

            var studentGradesCombined = from l1 in Students
                                        join l2 in Grades.Where(g => g.Coursecode.Contains(code)) on l1.Id equals l2.Studentid
                                        select new { Studentname = l1.Studentname, Studentid = l1.Id, Coursename = name, 
                                        Coursecode = l2.Coursecode, Grade1 = l2.Grade1}; 

            /*var combinedList = from l1 in studentGradesCombined
                               join l2 in Courses.Where(g => g.Coursecode.Contains(c)) on l1.Coursecode equals l2.Coursecode
                               select new {Coursename = l2.Coursename};*/
            
                               //, Grade1 = l1.Grades.Where(x => x.Studentid.Equals(l1.Id))

            courseParticipantlist.ItemsSource = studentGradesCombined;

            //courseParticipantlist.DataContext = combinedList.Where(g => g.Coursecode.Contains(c));//.OrderBy(s => s.Student.Studentname);
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e) {
            courseParticipantlist.DataContext = Students.Where(s => s.Studentname.Contains(searchField.Text))
                .OrderBy(s => s.Studentname);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) {

            CourseEditor ed = new() {
                dx = dx
            };

            ed.Show();
        }

        private void courseParticipantList_MouseDoubleClick(object sender, MouseButtonEventArgs e) {
            Student s = courseParticipantlist.SelectedItem as Student;


            if (s != null) {

                CourseEditor ed = new(s) {
                    dx = dx
                };
                ed.Show();

            }
        }

    }
}
