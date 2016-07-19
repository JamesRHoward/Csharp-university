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
        Course getCourse = Course.Find(parameter.id);
        List<Student> courseStudent = getCourse.GetStudents();
        List<Student> AllStudents = Student.GetAll();
        course_student.Add("course", getCourse);
        course_student.Add("courseStudent", courseStudent);
        course_student.Add("allStudents", AllStudents);
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
      Get["/students/{id}"]= parameter =>{
        Dictionary<string, object> student_course = new Dictionary<string, object>();
        Student getStudent = Student.Find(parameter.id);
        List<Course> studentCourse = getStudent.GetCourses();
        List<Course> AllCourses = Course.GetAll();
        student_course.Add("student", getStudent);
        student_course.Add("studentCourse", studentCourse);
        student_course.Add("allCourses", AllCourses);
        return View ["course_list.cshtml", student_course];
      };
      Post["/courses/add"] = _ =>{
        Student foundStudent = Student.Find(Request.Form["student_id"]);
        Course addedCourse = new Course(Request.Form["course_name_add"], Request.Form["course_number_add"]);
        addedCourse.Save();
        foundStudent.AddCourse(addedCourse);
        List<Course> allCourses = Course.GetAll();
        return View["success.cshtml", allCourses];
      };
    }
  }
}
