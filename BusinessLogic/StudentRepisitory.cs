﻿using DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTestsDataBaseEntities;
using System.Data;

namespace BusinessLogic
{
    public class StudentRepisitory : IRepository<StudentViewModel>
    {
        #region Singleton
        protected static StudentRepisitory instance = null;
        public static StudentRepisitory Instance
        {
            get
            {
                if (instance == null)
                    instance = new StudentRepisitory();
                return instance;

            }
        }
        #endregion

        #region Constructors
        protected StudentRepisitory() { }
        #endregion

        #region CRUD
        public int Create(StudentViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var student = new Student();
                student.StudentName = item.Name;
                student.GroupID = item.GroupID;
                entities.Students.Add(student);
                entities.SaveChanges();
                entities.Database.Connection.Close();
                return item.Id = student.StudentID;
            }
        }

        public List<StudentViewModel> Read()
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var students = (from a in entities.Students
                            select new StudentViewModel
                            {
                                Id = a.StudentID,
                                Name = a.StudentName,
                                GroupID = a.GroupID
                            }).ToList();
                entities.Database.Connection.Close();
                return students;
            }
        }

        public void Update(StudentViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var student = new Student();
                student.StudentID = item.Id;
                student.StudentName = item.Name;
                student.GroupID = item.GroupID;
                entities.Students.Attach(student);
                entities.Entry(student).State = EntityState.Modified;
                entities.SaveChanges();
                entities.Database.Connection.Close();
            }
        }

        public void Destroy(StudentViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var student = new Student();
                student.StudentID = item.Id;
                entities.Students.Attach(student);
                entities.Students.Remove(student);
                entities.SaveChanges();
                entities.Database.Connection.Close();
            }
        }
        #endregion
    }
}