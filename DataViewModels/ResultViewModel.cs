using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataViewModels
{
    public class ResultViewModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int TestId { get; set; }
        public int CorrectCount { get; set; }
        public System.TimeSpan SpentTime { get; set; }
        public System.DateTime ResultDate { get; set; }
        public float CorrectPercent { get; set; }
    }
}
