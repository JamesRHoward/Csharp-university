using System;
using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace University
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get ["/"]= _ =>{
        return View ["index.cshtml"];
      };
      Get ["/courses/new"]= _ =>{
        List<Course> allCourses = Course.GetAll();
        return View ["courses.cshtml", allCourses];
      };
      Post ["/courses/new"]= _ =>{
        Course newCourse = new Course(Request.Form["course_name"], Request.Form["course_number"]);
        newCourse.Save();
        List<Course> allCourses = Course.GetAll();
        return View ["courses.cshtml", allCourses];
      };
      Get["/students/new"] = _ => {
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };
      Post["/students/new"] = _ => {
        Student newStudent = new Student(Request.Form["student_name"]);
        newStudent.Save();
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };
      Get["courses/{id}"]= parameter =>{
        Dictionary<string, object> course_student = new Dictionary<string, object>();
        var getCourse = Course.Find(parameter.id);
        var getStudent = getCourse.GetStudents();
        course_student.Add("course", getCourse);
        course_student.Add("student", getStudent);
        return View["student_list.cshtml", course_student];
      };
      Post["/students/add"] = _ =>{
        Course foundCourse = Course.Find(Request.Form["course_id"]);
        Student addedStudent = new Student(Request.Form["student_name_add"]);
        addedStudent.Save();
        foundCourse.AddStudent(addedStudent);
        List<Student> allStudents = Student.GetAll();
        return View["success.cshtml", allStudents];
      };
    }
  }
}
