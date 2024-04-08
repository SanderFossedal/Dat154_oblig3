using Azure;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dat154_oblig3
{
    /// <summary>
    /// Interaction logic for mainPage.xaml
    /// </summary>
    public partial class mainPage : Page
    {
        private readonly Dat154Context dx = new();

        private readonly LocalView<Student> Students;
        private readonly LocalView<Course> Courses;
        private readonly LocalView<Grade> Grades;
        public mainPage() {
            InitializeComponent();
            Students = dx.Students.Local;
            /*            Courses = dx.Courses.Local;
                        Grades = dx.Grades.Local;*/

            dx.Students.Load();

            studentList.DataContext = Students.OrderBy(s => s.Studentname);


            
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



            //courseParticipants cp = new courseParticipants();
            
            //this.Content = cp;

        }
    }
}
