using DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTestsDataBaseEntities;
using System.Data;

namespace BusinessLogic
{
    public class ResultAnswerRepisitory : IRepository<ResultAnswerViewModel>
    {
        #region Singleton
        protected static ResultAnswerRepisitory instance = null;
        public static ResultAnswerRepisitory Instance
        {
            get
            {
                if (instance == null)
                    instance = new ResultAnswerRepisitory();
                return instance;

            }
        }
        #endregion

        #region Constructors
        protected ResultAnswerRepisitory() { }
        #endregion

        #region CRUD
        public int Create(ResultAnswerViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var resultAnswer = new ResultAnswer();
                resultAnswer.ResultId = item.ResultId;
                resultAnswer.QuestionId = item.QuestionId;
                resultAnswer.AnswerId = item.AnswerId;
                entities.ResultAnswers.Add(resultAnswer);
                entities.SaveChanges();
                entities.Database.Connection.Close();
                return item.Id = resultAnswer.ResultAnswerId;
            }
        }

        public List<ResultAnswerViewModel> Read()
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var resultsAnswers = (from a in entities.ResultAnswers
                            select new ResultAnswerViewModel
                            {
                                ResultId = a.ResultId,
                                QuestionId = a.QuestionId,
                                AnswerId = a.AnswerId
                            }).ToList();
                entities.Database.Connection.Close();
                return resultsAnswers;
            }
        }

        public void Update(ResultAnswerViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var resultAnswer = new ResultAnswer();
                resultAnswer.ResultId = item.ResultId;
                resultAnswer.QuestionId = item.QuestionId;
                resultAnswer.AnswerId = item.AnswerId;
                entities.ResultAnswers.Attach(resultAnswer);
                entities.Entry(resultAnswer).State = EntityState.Modified;
                entities.SaveChanges();
                entities.Database.Connection.Close();
            }
        }

        public void Destroy(ResultAnswerViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var resultAnswer = new ResultAnswer();
                resultAnswer.ResultId = item.ResultId;
                resultAnswer.QuestionId = item.QuestionId;
                resultAnswer.AnswerId = item.AnswerId;
                entities.ResultAnswers.Attach(resultAnswer);
                entities.ResultAnswers.Remove(resultAnswer);
                entities.SaveChanges();
                entities.Database.Connection.Close();
            }
        }
        #endregion
    }
}