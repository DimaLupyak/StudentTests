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
    
    public partial class Test
    {
        public Test()
        {
            this.Accesses = new HashSet<Access>();
            this.Questions = new HashSet<Question>();
            this.Results = new HashSet<Result>();
        }
    
        public int TestId { get; set; }
        public string TestName { get; set; }
        public System.TimeSpan TestTime { get; set; }
    
        public virtual ICollection<Access> Accesses { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Result> Results { get; set; }
    }
}