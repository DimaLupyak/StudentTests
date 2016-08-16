using DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StudentTestsDataBaseEntities;
using System.Data;

namespace BusinessLogic
{
    public class GroupRepisitory : IRepository<GroupViewModel>
    {
        #region Singleton
        protected static GroupRepisitory instance = null;
        public static GroupRepisitory Instance
        {
            get
            {
                if (instance == null)
                    instance = new GroupRepisitory();
                return instance;

            }
        }
        #endregion

        #region Constructors
        protected GroupRepisitory() { }
        #endregion

        #region CRUD
        public int Create(GroupViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var group = new Group();
                group.GroupName = item.Name;
                entities.Groups.Add(group);
                entities.SaveChanges();
                return item.Id = group.GroupID;
            }
        }

        public List<GroupViewModel> Read()
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var groups = (from a in entities.Groups
                            select new GroupViewModel
                            {
                                Id = a.GroupID,
                                Name = a.GroupName
                            }).ToList();
                return groups;
            }
        }

        public void Update(GroupViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var group = new Group();
                group.GroupID = item.Id;
                group.GroupName = item.Name;
                entities.Groups.Attach(group);
                entities.Entry(group).State = EntityState.Modified;
                entities.SaveChanges();
            }
        }

        public void Destroy(GroupViewModel item)
        {
            using (StudentTestDBEntities entities = new StudentTestDBEntities())
            {
                var group = new Group();
                group.GroupID = item.Id;
                entities.Groups.Attach(group);
                entities.Groups.Remove(group);
                entities.SaveChanges();
            }
        }
        #endregion
    }
}