using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace University
{
  public class Course
  {
    private int _id;
    private string _course_name;
    private string _course_number;

    public Course(string courseName, string courseNumber, int id = 0)
    {
      _id = id;
      _course_name = courseName;
      _course_number = courseNumber;
    }
    public int GetId()
    {
      return _id;
    }
    public string GetCourseName()
    {
      return _course_name;
    }
    public void SetCourseName(string newCourseName)
    {
      _course_name = newCourseName;
    }
    public string GetCourseNumber()
    {
      return _course_number;
    }
    public void SetCourseNumber(string newCourseNumber)
    {
      _course_number = newCourseNumber;
    }
    public override bool Equals(System.Object otherCourse)
    {
      if (!(otherCourse is Course))
      {
        return false;
      }
      else
      {
        Course newCourse = (Course) otherCourse;
        bool idEquality = this.GetId() == newCourse.GetId();
        bool nameEquality = this.GetCourseName() == newCourse.GetCourseName();
        bool numberEquality = this.GetCourseNumber() == newCourse.GetCourseNumber();
        return (idEquality && nameEquality && numberEquality);
      }
    }
    public static List<Course> GetAll()
    {
      List<Course> allCourses = new List<Course>{};

      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses;", conn);
      rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int courseId = rdr.GetInt32(0);
        string courseName = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        Course newCourse = new Course(courseName, courseNumber, courseId);
        allCourses.Add(newCourse);
      }

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return allCourses;
    }
    public void Save()
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr;
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO courses (course_name, course_number) OUTPUT INSERTED.id VALUES (@CourseName, @CourseNumber);", conn);

      SqlParameter nameParameter = new SqlParameter();
      nameParameter.ParameterName = "@CourseName";
      nameParameter.Value = this.GetCourseName();
      cmd.Parameters.Add(nameParameter);

      SqlParameter numberParameter = new SqlParameter();
      numberParameter.ParameterName = "@CourseNumber";
      numberParameter.Value = this.GetCourseNumber();
      cmd.Parameters.Add(numberParameter);
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
    public static Course Find(int id)
    {
      SqlConnection conn = DB.Connection();
      SqlDataReader rdr = null;
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE id = @CourseId;", conn);
      SqlParameter courseIdParameter = new SqlParameter();
      courseIdParameter.ParameterName = "@CourseId";
      courseIdParameter.Value = id.ToString();
      cmd.Parameters.Add(courseIdParameter);
      rdr = cmd.ExecuteReader();

      int foundCourseId = 0;
      string foundCourseName = null;
      string foundCourseNumber = null;

      while(rdr.Read())
      {
        foundCourseId = rdr.GetInt32(0);
        foundCourseName = rdr.GetString(1);
        foundCourseNumber = rdr.GetString(2);
      }
      Course foundCourse = new Course(foundCourseName, foundCourseNumber, foundCourseId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundCourse;
    }
    public void AddStudent(Student newStudent)
  {
    SqlConnection conn = DB.Connection();
    conn.Open();

    SqlCommand cmd = new SqlCommand("INSERT INTO courses_students (student_id, course_id) VALUES (@StudentId, @CourseId);", conn);

    SqlParameter studentIdParameter = new SqlParameter();
    studentIdParameter.ParameterName = "@StudentId";
    studentIdParameter.Value = newStudent.GetId();
    cmd.Parameters.Add(studentIdParameter);

    SqlParameter courseIdParameter = new SqlParameter();
    courseIdParameter.ParameterName = "@CourseId";
    courseIdParameter.Value = this.GetId();
    cmd.Parameters.Add(courseIdParameter);

    cmd.ExecuteNonQuery();

    if (conn != null)
    {
      conn.Close();
    }
  }

  public List<Student> GetStudents()
  {
    SqlConnection conn = DB.Connection();
    SqlDataReader rdr = null;
    conn.Open();

    SqlCommand cmd = new SqlCommand("SELECT student_id FROM courses_students WHERE course_id = @CourseId;", conn);

    SqlParameter courseIdParameter = new SqlParameter();
    courseIdParameter.ParameterName = "@CourseId";
    courseIdParameter.Value = this.GetId();
    cmd.Parameters.Add(courseIdParameter);

    rdr = cmd.ExecuteReader();

    List<int> studentIds = new List<int> {};

    while (rdr.Read())
    {
      int studentId = rdr.GetInt32(0);
      studentIds.Add(studentId);
    }
    if (rdr != null)
    {
      rdr.Close();
    }

    List<Student> students = new List<Student> {};

    foreach (int studentId in studentIds)
    {
      SqlDataReader queryReader = null;
      SqlCommand studentQuery = new SqlCommand("SELECT * FROM students WHERE id = @StudentId;", conn);

      SqlParameter studentIdParameter = new SqlParameter();
      studentIdParameter.ParameterName = "@StudentId";
      studentIdParameter.Value = studentId;
      studentQuery.Parameters.Add(studentIdParameter);

      queryReader = studentQuery.ExecuteReader();
      while (queryReader.Read())
      {
        int thisStudentId = queryReader.GetInt32(0);
        string studentName = queryReader.GetString(1);
        Student foundStudent = new Student(studentName, thisStudentId);
        students.Add(foundStudent);
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
    return students;
  }
    public static void DeleteAll()
     {
       SqlConnection conn = DB.Connection();
       conn.Open();
       SqlCommand cmd = new SqlCommand("DELETE FROM courses;", conn);
       cmd.ExecuteNonQuery();
     }
  }
}
