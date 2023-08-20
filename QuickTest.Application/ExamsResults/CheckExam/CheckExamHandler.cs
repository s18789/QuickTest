using MediatR;
using QuickTest.Core.Entities;
using QuickTest.Core.Entities.Enums;
using QuickTest.Infrastructure.Interfaces;
using QuickTest.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickTest.Application.ExamsResults.CheckExam
{
    public sealed class CheckExamHandler : IRequestHandler<CheckExamRequest, int>
    {
        private IExamResultRepository examResultRepository;

        public CheckExamHandler(IExamResultRepository examResultRepository) 
        {
            this.examResultRepository = examResultRepository;
        }

        public async Task<int> Handle(CheckExamRequest request, CancellationToken cancellationToken)
        {
            var examResult = await this.examResultRepository.GetExamResult(request.CheckedExam.ExamResultId);

            foreach(var question in request.CheckedExam.Questions.Where(x => x.Type == QuestionType.Open))
            {
                examResult.StudentAnswers.FirstOrDefault(x => x.QuestionId == question.QuestionId).Points = question.Score;
            }

            examResult.Score = examResult.StudentAnswers.Sum(x => x.Points);

            await this.examResultRepository.UpdateAsync(examResult);

            return request.CheckedExam.ExamResultId;
        }
    }
}
