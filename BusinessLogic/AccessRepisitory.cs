using DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTestsDataBaseEntities;
using System.Data;

namespace BusinessLogic
{
    public class AccessRepisitory : IRepository<AccessViewModel>
    {
        #region Singleton
        protected static AccessRepisitory instance = null;
        public static AccessRepisitory Instance
        {
            get
            {
                if (instance == null)
                    instance = new AccessRepisitory();
                return instance;

            }
        }
        #endregion
        #region Constructors
        protected AccessRepisitory() { }
        #endregion
        #region CRUD
        public void Create(AccessViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var access = new Access();
                access.GroupId = item.GroupId;
                access.TestId = item.TestId;
                entities.Accesses.Add(access);
                entities.SaveChanges();
                item.Id = access.AccessId;
            }
        }

        public List<AccessViewModel> Read()
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var accesses = (from a in entities.Accesses
                            select new AccessViewModel
                            {
                                Id = a.AccessId,
                                GroupId = a.GroupId,
                                TestId = a.TestId
                            }).ToList();
                return accesses;
            }
        }

        public void Update(AccessViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var access = new Access();
                access.AccessId = item.Id;
                access.GroupId = item.GroupId;
                access.TestId = item.TestId;
                entities.Accesses.Attach(access);
                entities.Entry(access).State = EntityState.Modified;
                entities.SaveChanges();
            }
        }

        public void Destroy(AccessViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var access = new Access();
                access.AccessId = item.Id;
                entities.Accesses.Attach(access);
                entities.Accesses.Remove(access);
                entities.SaveChanges();
            }
        }
        #endregion
    }
}