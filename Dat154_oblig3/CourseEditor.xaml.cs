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
    /// Interaction logic for CourseEditor.xaml
    /// </summary>
    public partial class CourseEditor : Window
    {
        public Dat154Context dx = new();

        private readonly LocalView<Student> Students;
        private readonly LocalView<Grade> Grades;
        private readonly LocalView<Course> Courses;
        public CourseEditor() {
            InitializeComponent();
        }
            public CourseEditor(Student s)
        {
            InitializeComponent();
            Students = dx.Students.Local;
            Grades = dx.Grades.Local;
            Courses = dx.Courses.Local;
            dx.Grades.Load();
            dx.Students.Load();
            var studentGradesCombined = from l1 in Students
                                        join l2 in Grades on l1.Id equals l2.Studentid
                                        select new { Studentname = l1.Studentname, Studentid = l1.Id, Coursecode = l2.Coursecode, Grade1 = l2.Grade1 }; //.(l2.Studentid.Equals(l1.Id))
            string studentiD = s.Id.ToString();
            /*int sage = s.Studentage;
            string sname = s.Studentname;
            string scoursecode = studentGradesCombined.FirstOrDefault(a => a.Studentid.Equals(studentiD)).Coursecode;
            string sgrade = studentGradesCombined.FirstOrDefault(b => b.Studentid.Equals(studentiD)).Grade1;*/

            sAge.Text = s.Studentage.ToString();
            sID.Text = s.Id.ToString();
            sName.Text = s.Studentname;
            sCoursecode.Text = studentGradesCombined.FirstOrDefault(a => a.Studentid.Equals(studentiD)).Coursecode;
            sGrade.Text = studentGradesCombined.FirstOrDefault(b => b.Studentid.Equals(studentiD)).Grade1;
        }

        //Legg til feilsjekk try catch
        private void bAdd_Click(object sender, RoutedEventArgs e) {
            Student s = new() {
                Studentname = sName.Text,
                Studentage = int.Parse(sAge.Text),
                Id = int.Parse(sID.Text)
            };

            dx.Students.Add(s);


            Grade g = new Grade() {
                Studentid = s.Id,
                Coursecode = sCoursecode.Text,
                Grade1 = sGrade.Text
                //CoursecodeNavigation = Courses.FirstOrDefault(c => c.Coursecode.Equals(sCoursecode.Text)),
                //Student = s


            };

            dx.Grades.Add(g);
            dx.SaveChanges();
            sName.Text = sAge.Text = sID.Text = sCoursecode.Text = sGrade.Text = "";
        }

        private void bDel_Click(object sender, RoutedEventArgs e) {

            int id = int.Parse(sID.Text);
            Student s = dx.Students.Where(st => st.Id == id).First();

            if (s != null) {
                //dx.Students.Remove(s);
                //dx.SaveChanges();
            }
            sName.Text = sAge.Text = sID.Text = "";

            Grade g = dx.Grades.Where(gr => gr.Studentid == id).First();
            if (g != null) {
                dx.Grades.Remove(g);
                dx.SaveChanges();
            }
            sName.Text = sAge.Text = sID.Text = sCoursecode.Text = sGrade.Text = "";

        }

        private void bUpdate_Click(object sender, RoutedEventArgs e) {

            int id = int.Parse(sID.Text);
            Student s = dx.Students.Where(st => st.Id == id).First();

            if (s != null) {

                if (sName.Text.Length > 0) s.Studentname = sName.Text;
                if (sAge.Text.Length > 0) s.Studentage = int.Parse(sAge.Text);

                //dx.SaveChanges();
            }

            Grade g = dx.Grades.Where(st => st.Studentid == id).First();
            if (g != null) {
                if (sID.Text.Length > 0) g.Studentid = int.Parse(sID.Text);
                if (sCoursecode.Text.Length > 0) g.Coursecode = sCoursecode.Text;
                if (sGrade.Text.Length > 0) g.Grade1 = sGrade.Text;
                dx.SaveChanges();
            }
        }
    }
}
