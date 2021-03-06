using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace University
{
  public class StudentTest : IDisposable
  {
    public StudentTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=university_test;Integrated Security=SSPI;";
    }
    public void Dispose()
    {
      Student.DeleteAll();
      Course.DeleteAll();
    }
    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
      int result = Student.GetAll().Count;

      Assert.Equal(0, result);
    }
    [Fact]
    public void Test_SaveStudentToDatabase()
    {
      Student testStudent = new Student("Jake");

      testStudent.Save();
      List<Student> result = Student.GetAll();
      List<Student> testList = new List<Student>{testStudent};

      Assert.Equal(testList, result);
    }
    [Fact]
    public void Test_Equal_ReturnsTrueIfDescriptionsAreTheSame()
    {
      //Arrange, Act
      Student firstStudent = new Student("Jake");
      Student secondStudent = new Student("Jake");

      //Assert
      Assert.Equal(firstStudent, secondStudent);
    }
    [Fact]
    public void Test_FindFindsStudentInDatabase()
    {
      //Arrange
      Student testStudent = new Student("Mike");
      testStudent.Save();

      //Act
      Student foundStudent = Student.Find(testStudent.GetId());

      //Assert
      Assert.Equal(testStudent, foundStudent);
    }
    [Fact]
    public void Test_AddCourseToStudent()
    {
      Course newCourse = new Course("Bio", "Bio01");
      newCourse.Save();
      Student newStudent = new Student("Jack");
      newStudent.Save();

      newStudent.AddCourse(newCourse);

      List<Course> result = newStudent.GetCourses();
      List<Course> testList = new List<Course>{newCourse};

      Assert.Equal(result, testList);
    }
    [Fact]
    public void Test_Delete_RemovesStudentFromDatabase()
    {
      List<Student> TestStudents = new List<Student>{};

      Student testStudent1 = new Student("Dave");
      testStudent1.Save();
      Student testStudent2 = new Student("Morphumax");
      testStudent2.Save();

      Course TestCourse1 = new Course("Gym", "101", testStudent1.GetId());
      TestCourse1.Save();
      Course TestCourse2 = new Course("Music", "101", testStudent2.GetId());
      TestCourse2.Save();

      testStudent1.Delete();

      List<Student> resultStudents = Student.GetAll();
      List<Student> testStudents = new List<Student> {testStudent2};

      List<Course> resultCourses = Course.GetAll();
      List<Course> testCourses = new List<Course> {TestCourse1, TestCourse2};

      Assert.Equal(resultStudents, testStudents);
      Assert.Equal(resultCourses, testCourses);
    }
  }
}
