using EmployeeManagement.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Test
{
    public class CourseTests
    {
        [Fact]
        public void CourseContructor_ConstructCourse_IsNewMustBeTrue()
        {
            //Arrrange 
            //Nothing to arrange

            //Act
            var course = new Course("Physique Chimie & Technologie");

            //Assert
            Assert.True(course.IsNew);
        }
    }
}
