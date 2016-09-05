using DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTestsDataBaseEntities;
using System.Data;

namespace BusinessLogic
{
    public class AnswerRepisitory : IRepository<AnswerViewModel>
    {
        #region Singleton
        protected static AnswerRepisitory instance = null;
        public static AnswerRepisitory Instance
        {
            get
            {
                if (instance == null)
                    instance = new AnswerRepisitory();
                return instance;

            }
        }
        #endregion

        #region Constructors
        protected AnswerRepisitory() { }
        #endregion

        #region CRUD
        public int Create(AnswerViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var answer = new Answer();
                answer.AnswerImage = item.Image;
                answer.AnswerIsCorrect = item.IsCorrect;
                answer.AnswerText = item.Text;
                answer.QuestionId = item.QuestionId;
                entities.Answers.Add(answer);
                entities.SaveChanges();
                entities.Database.Connection.Close();
                return item.Id = answer.AnswerId;
            }
        }

        public List<AnswerViewModel> Read()
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var answer = (from a in entities.Answers
                            select new AnswerViewModel
                            {
                                Id = a.AnswerId,
                                Image = a.AnswerImage,
                                IsCorrect = a.AnswerIsCorrect,
                                Text = a.AnswerText,
                                QuestionId = a.QuestionId
                            }).ToList();
                entities.Database.Connection.Close();
                return answer;
            }
        }

        public void Update(AnswerViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var answer = new Answer();
                answer.AnswerId = item.Id;
                answer.AnswerImage = item.Image;
                answer.AnswerIsCorrect = item.IsCorrect;
                answer.AnswerText = item.Text;
                answer.QuestionId = item.QuestionId;
                entities.Answers.Attach(answer);
                entities.Entry(answer).State = EntityState.Modified;
                entities.SaveChanges();
                entities.Database.Connection.Close();
            }
        }

        public void Destroy(AnswerViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var answer = new Answer();
                answer.AnswerId = item.Id;
                entities.Answers.Attach(answer);
                entities.Answers.Remove(answer);
                entities.SaveChanges();
                entities.Database.Connection.Close();
            }
        }
        #endregion
    }
}