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
        public void CreateAnswer(AnswerViewModel answer)
        {
            AnswerRepisitory.Instance.Create(answer);
        }

        public void CreateGroup(GroupViewModel group)
        {
            GroupRepisitory.Instance.Create(group);
        }

        public void CreateQuestion(QuestionViewModel question)
        {
            QuestionRepisitory.Instance.Create(question);
        }

        public void CreateStudent(StudentViewModel student)
        {
            StudentRepisitory.Instance.Create(student);
        }

        public void CreateTest(TestViewModel test)
        {
            TestRepisitory.Instance.Create(test);
        }

        public void DeleteAnswer(AnswerViewModel answer)
        {
            AnswerRepisitory.Instance.Destroy(answer);
        }

        public void DeleteGroup(GroupViewModel group)
        {
            GroupRepisitory.Instance.Destroy(group);
        }

        public void DeleteQuestion(QuestionViewModel question)
        {
            QuestionRepisitory.Instance.Destroy(question);
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

        public void UpdateAnswer(AnswerViewModel answer)
        {
            AnswerRepisitory.Instance.Update(answer);
        }

        public void UpdateGroup(GroupViewModel group)
        {
            GroupRepisitory.Instance.Update(group);
        }

        public void UpdateQuestion(QuestionViewModel question)
        {
            QuestionRepisitory.Instance.Update(question);
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
