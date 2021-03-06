﻿using DataViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StudentTestsService
{
    [ServiceContract]
    public interface IStudentTestService
    {
        #region Read        
        [OperationContract]
        IEnumerable<StudentViewModel> GetStudents();
        [OperationContract]
        IEnumerable<GroupViewModel> GetGroups();
        [OperationContract]
        IEnumerable<TestViewModel> GetTests();
        [OperationContract]
        IEnumerable<TestViewModel> GetStudentTests(int studentId);
        [OperationContract]
        IEnumerable<QuestionViewModel> GetTestQuestions(int testId);
        [OperationContract]
        IEnumerable<AnswerViewModel> GetQuestionAnswers(int questionId);
        [OperationContract]
        IEnumerable<ResultViewModel> GetResults();
        [OperationContract]
        IEnumerable<QuestionViewModel> GetQuestions();
        [OperationContract]
        IEnumerable<GroupViewModel> GetTestGroups(int testId);
        [OperationContract]
        IEnumerable<ResultAnswerViewModel> GetResultAnswers(int resultId);
        [OperationContract]
        bool IsAcceess(int groupId, int testId);
        #endregion

        #region Create 
        [OperationContract]
        void CreateStudent(StudentViewModel student);
        [OperationContract]
        void CreateGroup(GroupViewModel group);
        [OperationContract]
        void CreateTest(TestViewModel test);
        [OperationContract]
        void CreateQuestion(QuestionViewModel question);
        [OperationContract]
        void CreateAnswer(AnswerViewModel answer);
        [OperationContract]
        void CreateAccess(int groupId, int testId);
        [OperationContract]
        int CreateResult(ResultViewModel result);
        [OperationContract]
        void CreateResultAnswer(ResultAnswerViewModel resultAnswer);
        #endregion

        #region Update 
        [OperationContract]
        void UpdateStudent(StudentViewModel student);
        [OperationContract]
        void UpdateGroup(GroupViewModel group);
        [OperationContract]
        void UpdateTest(TestViewModel test);
        [OperationContract]
        void UpdateQuestion(QuestionViewModel question);
        [OperationContract]
        void UpdateAnswer(AnswerViewModel answer);
        #endregion

        #region Delete 
        [OperationContract]
        void DeleteStudent(StudentViewModel student);
        [OperationContract]
        void DeleteGroup(GroupViewModel group);
        [OperationContract]
        void DeleteTest(TestViewModel test);
        [OperationContract]
        void DeleteQuestion(QuestionViewModel question);
        [OperationContract]
        void DeleteAnswer(AnswerViewModel answer);
        [OperationContract]
        void DeleteAccess(int groupId, int testId);
        #endregion
    }
}
