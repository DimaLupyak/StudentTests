﻿using DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTestsDataBaseEntities;
using System.Data;

namespace BusinessLogic
{
    public class ResultRepisitory : IRepository<ResultViewModel>
    {
        #region Singleton
        protected static ResultRepisitory instance = null;
        public static ResultRepisitory Instance
        {
            get
            {
                if (instance == null)
                    instance = new ResultRepisitory();
                return instance;

            }
        }
        #endregion

        #region Constructors
        protected ResultRepisitory() { }
        #endregion

        #region CRUD
        public int Create(ResultViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var result = new Result();
                result.StudentId = item.StudentId;
                result.TestId = item.TestId;
                result.CorrectCount = item.CorrectCount;
                result.SpentTime = item.SpentTime;
                result.ResultDate = item.ResultDate;
                entities.Results.Add(result);
                entities.SaveChanges();
                item.Id = result.TestResultId;
                entities.Database.Connection.Close();
                return item.Id;
            }
        }

        public List<ResultViewModel> Read()
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var results = (from a in entities.Results
                            select new ResultViewModel
                            {
                                Id = a.TestResultId,
                                StudentId = a.StudentId,
                                TestId = a.TestId,
                                CorrectCount = a.CorrectCount,
                                CorrectPercent = 100 / (float)entities.Questions.Where(x => x.TestId == a.TestId).Count() * a.CorrectCount,
                                SpentTime = a.SpentTime,
                                ResultDate = a.ResultDate
                            }).ToList();
                entities.Database.Connection.Close();
                return results;
            }
        }

        public void Update(ResultViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var result = new Result();
                result.TestResultId = item.Id;
                result.StudentId = item.StudentId;
                result.TestId = item.TestId;
                result.CorrectCount = item.CorrectCount;
                result.SpentTime = item.SpentTime;
                result.ResultDate = item.ResultDate;
                entities.Results.Attach(result);
                entities.Entry(result).State = EntityState.Modified;
                entities.SaveChanges();
                entities.Database.Connection.Close();
            }
        }

        public void Destroy(ResultViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var result = new Result();
                result.TestResultId = item.Id;
                entities.Results.Attach(result);
                entities.Results.Remove(result);
                entities.SaveChanges();
                entities.Database.Connection.Close();
            }
        }
        #endregion
    }
}