//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StudentTestsDataBaseEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Group
    {
        public Group()
        {
            this.Accesses = new HashSet<Access>();
            this.Students = new HashSet<Student>();
        }
    
        public int GroupID { get; set; }
        public string GroupName { get; set; }
    
        public virtual ICollection<Access> Accesses { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
