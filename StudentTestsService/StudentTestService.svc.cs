using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DataViewModels;
using BusinessLogic;

namespace StudentTestsService
{
    public class StudentTestService : IStudentTestService
    {
        public void CreateGroup(GroupViewModel group)
        {
            GroupRepisitory.Instance.Create(group);
        }

        public void CreateStudent(StudentViewModel student)
        {
            StudentRepisitory.Instance.Create(student);
        }

        public void CreateTest(TestViewModel test)
        {
            TestRepisitory.Instance.Create(test);
        }

        public void DeleteGroup(GroupViewModel group)
        {
            GroupRepisitory.Instance.Destroy(group);
        }

        public void DeleteStudent(StudentViewModel student)
        {
            StudentRepisitory.Instance.Destroy(student);
        }

        public void DeleteTest(TestViewModel test)
        {
            TestRepisitory.Instance.Destroy(test);
        }

        public IEnumerable<GroupViewModel> GetGroups()
        {
            return GroupRepisitory.Instance.Read();
        }

        public IEnumerable<AnswerViewModel> GetQuestionAnswers(int questionId)
        {
            return AnswerRepisitory.Instance.Read().Where(x => x.QuestionId == questionId);
        }

        public IEnumerable<ResultViewModel> GetStudentResults(int studentId)
        {
            return ResultRepisitory.Instance.Read().Where(x => x.StudentId == studentId);
        }

        public IEnumerable<StudentViewModel> GetStudents()
        {
            return StudentRepisitory.Instance.Read();
        }

        public IEnumerable<TestViewModel> GetStudentTests(int studentId)
        {
            return from t in TestRepisitory.Instance.Read()
                   join a in AccessRepisitory.Instance.Read() on t.Id equals a.TestId
                   join g in GroupRepisitory.Instance.Read() on a.GroupId equals g.Id
                   select t;
        }

        public IEnumerable<QuestionViewModel> GetTestQuestions(int testId)
        {
            return QuestionRepisitory.Instance.Read().Where(x => x.TestId == testId);
        }

        public IEnumerable<TestViewModel> GetTests()
        {
            return TestRepisitory.Instance.Read();
        }

        public void UpdateGroup(GroupViewModel group)
        {
            GroupRepisitory.Instance.Update(group);
        }

        public void UpdateStudent(StudentViewModel student)
        {
            StudentRepisitory.Instance.Update(student);
        }

        public void UpdateTest(TestViewModel test)
        {
            TestRepisitory.Instance.Update(test);
        }
    }
}
