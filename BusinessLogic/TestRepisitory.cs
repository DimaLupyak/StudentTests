using DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTestsDataBaseEntities;
using System.Data;

namespace BusinessLogic
{
    public class TestRepisitory : IRepository<TestViewModel>
    {
        #region Singleton
        protected static TestRepisitory instance = null;
        public static TestRepisitory Instance
        {
            get
            {
                if (instance == null)
                    instance = new TestRepisitory();
                return instance;

            }
        }
        #endregion
        #region Constructors
        protected TestRepisitory() { }
        #endregion
        #region CRUD
        public void Create(TestViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var test = new Test();
                test.TestName = item.Name;
                test.TestTime = item.Time;
                entities.Tests.Add(test);
                entities.SaveChanges();
                item.Id = test.TestId;
            }
        }

        public List<TestViewModel> Read()
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var test = (from a in entities.Tests
                            select new TestViewModel
                            {
                                Id = a.TestId,
                                Name = a.TestName,
                                Time = a.TestTime
                            }).ToList();
                return test;
            }
        }

        public void Update(TestViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var test = new Test();
                test.TestId = item.Id;
                test.TestName = item.Name;
                test.TestTime = item.Time;
                entities.Tests.Attach(test);
                entities.Entry(test).State = EntityState.Modified;
                entities.SaveChanges();
            }
        }

        public void Destroy(TestViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var test = new Test();
                test.TestId = item.Id;
                entities.Tests.Attach(test);
                entities.Tests.Remove(test);
                entities.SaveChanges();
            }
        }
        #endregion
    }
}