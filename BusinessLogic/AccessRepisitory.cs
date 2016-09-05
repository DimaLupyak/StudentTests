using DataViewModels;
using System.Collections.Generic;
using System.Linq;
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
        public int Create(AccessViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();                
                var access = new Access();
                access.GroupId = item.GroupId;
                access.TestId = item.TestId;
                entities.Accesses.Add(access);
                entities.SaveChanges();
                entities.Database.Connection.Close();
                return item.Id = access.AccessId;
            }
        }

        public List<AccessViewModel> Read()
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var accesses = (from a in entities.Accesses
                            select new AccessViewModel
                            {
                                Id = a.AccessId,
                                GroupId = a.GroupId,
                                TestId = a.TestId
                            }).ToList();
                entities.Database.Connection.Close();
                return accesses;
            }
        }

        public void Update(AccessViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var access = new Access();
                access.AccessId = item.Id;
                access.GroupId = item.GroupId;
                access.TestId = item.TestId;
                entities.Accesses.Attach(access);
                entities.Entry(access).State = EntityState.Modified;
                entities.SaveChanges();
                entities.Database.Connection.Close();
            }
        }

        public void Destroy(AccessViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                entities.Database.Connection.Open();
                var access = new Access();
                access.AccessId = item.Id;
                entities.Accesses.Attach(access);
                entities.Accesses.Remove(access);
                entities.SaveChanges();
                entities.Database.Connection.Close();
            }
        }
        #endregion
    }
}