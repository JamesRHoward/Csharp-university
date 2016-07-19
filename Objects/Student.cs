using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace University
{
  public class Student
  {
    private int _id;
    private string _name;

    public Student(string name, int id = 0)
    {
      _id = id;
      _name = name;
    }
    public override bool Equals(System.Object otherStudent)
    {
        if (!(otherStudent is Student))
        {
          return false;
        }
        else
        {
          Student newStudent = (Student) otherStudent;
          bool idEquality = this.GetId() == newStudent.GetId();
          bool nameEquality = this.GetName() == newStudent.GetName();
          return (idEquality && nameEquality);
        }
    }
    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public static List<Student> GetAll()
    {
      List<Student> allStudents = new List<Student>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        Student newStudent = new Student(studentName, studentId);
        allStudents.Add(newStudent);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allStudents;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO students (name) OUTPUT INSERTED.id VALUES (@StudentName);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@StudentName";
      nameParameter.Value = this.GetName();
      cmd.Parameters.Add(nameParameter);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }
    public static Student Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE id = @StudentId;", conn);
      SqlParameter studentIdParameter = new SqlParameter();
      studentIdParameter.ParameterName = "@StudentId";
      studentIdParameter.Value = id.ToString();
      cmd.Parameters.Add(studentIdParameter);
      rdr = cmd.ExecuteReader();

      int foundStudentId = 0;
      string foundStudentName = null;

      while(rdr.Read())
      {
        foundStudentId = rdr.GetInt32(0);
        foundStudentName = rdr.GetString(1);
      }
      Student foundStudent = new Student(foundStudentName, foundStudentId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundStudent;
    }

    public static void DeleteAll()
   {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlCommand cmd = new SqlCommand("DELETE FROM students;", conn);
     cmd.ExecuteNonQuery();
   }
   public void AddCourse(Course newCourse)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO courses_students (course_id, student_id) VALUES (@CourseId, @StudentId);", conn);

      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@CourseId";
      courseIdParameter.Value = newCourse.GetId();
      cmd.Parameters.Add(courseIdParameter);

      SqlParameter studentIdParameter = new SqlParameter();
      studentIdParameter.ParameterName = "@StudentId";
      studentIdParameter.Value = this.GetId();
      cmd.Parameters.Add(studentIdParameter);

      cmd.ExecuteNonQuery();

      if (conn != null)
      {
        conn.Close();
      }
    }

    public List<Course> GetCourses()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT course_id FROM courses_students WHERE student_id = @StudentId;", conn);

      SqlParameter studentIdParameter = new SqlParameter();
      studentIdParameter.ParameterName = "@StudentId";
      studentIdParameter.Value = this.GetId();
      cmd.Parameters.Add(studentIdParameter);

      rdr = cmd.ExecuteReader();

      List<int> courseIds = new List<int> {};

      while (rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        courseIds.Add(courseId);
      }
      if (rdr != null)
      {
        rdr.Close();
      }

      List<Course> course = new List<Course> {};

      foreach (int courseId in courseIds)
      {
        SqlDataReader queryReader = null;
        SqlCommand courseQuery = new SqlCommand("SELECT * FROM courses WHERE id = @CourseId;", conn);

        SqlParameter courseIdParameter = new SqlParameter();
        courseIdParameter.ParameterName = "@CourseId";
        courseIdParameter.Value = courseId;
        courseQuery.Parameters.Add(courseIdParameter);

        queryReader = courseQuery.ExecuteReader();
        while (queryReader.Read())
        {
          int thisCourseId = queryReader.GetInt32(0);
          string courseName = queryReader.GetString(1);
          string courseNumber = queryReader.GetString(2);
          Course foundCourse = new Course(courseName, courseNumber, thisCourseId);
          course.Add(foundCourse);
        }
        if (queryReader != null)
        {
          queryReader.Close();
        }
      }
      if (conn != null)
      {
        conn.Close();
      }
      return course;
    }
  }
}
