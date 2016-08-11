using DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTestsDataBaseEntities;
using System.Data;

namespace BusinessLogic
{
    public class QuestionRepisitory : IRepository<QuestionViewModel>
    {
        #region Singleton
        protected static QuestionRepisitory instance = null;
        public static QuestionRepisitory Instance
        {
            get
            {
                if (instance == null)
                    instance = new QuestionRepisitory();
                return instance;

            }
        }
        #endregion
        #region Constructors
        protected QuestionRepisitory() { }
        #endregion
        #region CRUD
        public void Create(QuestionViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var question = new Question();
                question.QuestionImage = item.Image;
                question.TestId = item.TestId;
                question.QuestionText = item.Text;
                entities.Questions.Add(question);
                entities.SaveChanges();
                item.Id = question.QuestionId;
            }
        }

        public List<QuestionViewModel> Read()
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var questions = (from a in entities.Questions
                            select new QuestionViewModel
                            {
                                Id = a.QuestionId,
                                Image = a.QuestionImage,
                                TestId = a.TestId,
                                Text = a.QuestionText
                            }).ToList();
                return questions;
            }
        }

        public void Update(QuestionViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var question = new Question();
                question.QuestionId = item.Id;
                question.QuestionImage = item.Image;
                question.TestId = item.TestId;
                question.QuestionText = item.Text;
                entities.Questions.Attach(question);
                entities.Entry(question).State = EntityState.Modified;
                entities.SaveChanges();
            }
        }

        public void Destroy(QuestionViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var question = new Question();
                question.QuestionId = item.Id;
                entities.Questions.Attach(question);
                entities.Questions.Remove(question);
                entities.SaveChanges();
            }
        }
        #endregion
    }
}