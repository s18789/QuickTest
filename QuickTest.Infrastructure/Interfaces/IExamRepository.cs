﻿using QuickTest.Core.Entities;

namespace QuickTest.Infrastructure.Interfaces;
public interface IExamRepository : IAsyncRepository<Exam>
{
    Task<IEnumerable<Exam>> GetAllExams();

    Task<Exam> GetExamIncludeExamResultsAndQuestions(int id);

    Task<Exam> GetExamPreview(int id);

    Task<IEnumerable<Exam>> GetCreatedExam(int teacherId);

    Task<IEnumerable<Exam>> GetExamsForMonth(int teacherId, int month, int year);

    Task<IEnumerable<Exam>> GetExamsForMonth(int month, int year);
}