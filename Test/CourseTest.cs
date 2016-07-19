using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace University
{
  public class CourseTest : IDisposable
  {
    public CourseTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=university_test;Integrated Security=SSPI;";
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int courseResult = Course.GetAll().Count;

      Assert.Equal(0, courseResult);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      Course firstCourse = new Course("Bio", "Bio01");
      Course secondCourse = new Course("Bio", "Bio01");

      Assert.Equal(firstCourse, secondCourse);
    }
    [Fact]
    public void Test_SavesCourseToDatabase()
    {
      Course newCourse = new Course("Bio", "Bio01");
      newCourse.Save();
      List<Course> result = Course.GetAll();
      List<Course> testList = new List<Course> {newCourse};
      Assert.Equal(result, testList);
    }
    [Fact]
    public void Test_FindsCourseInDatabase()
    {
      Course newCourse = new Course("Bio", "Bio01");
      newCourse.Save();
      Course foundCourse = Course.Find(newCourse.GetId());
      Assert.Equal(newCourse, foundCourse);
    }
    [Fact]
    public void Test_AddStudentToCourse()
    {
      Course newCourse = new Course("Bio", "Bio01");
      newCourse.Save();
      Student newStudent = new Student("Jack");
      newStudent.Save();

      newCourse.AddStudent(newStudent);

      List<Student> result = newCourse.GetStudents();
      List<Student> testList = new List<Student>{newStudent};

      Assert.Equal(result, testList);
    }
    public void Dispose()
    {
      Course.DeleteAll();
      Student.DeleteAll();
    }
  }
}
