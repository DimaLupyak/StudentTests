using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataViewModels
{
    public class ResultAnswerViewModel
    {
        public int Id { get; set; }
        public int ResultId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
    }
}
