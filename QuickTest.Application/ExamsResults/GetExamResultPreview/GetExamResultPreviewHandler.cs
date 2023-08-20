using MediatR;
using QuickTest.Application.Common;
using QuickTest.Core.Entities;
using QuickTest.Core.Entities.Enums;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.ExamsResults.GetExamResultPreview
{
    public sealed class GetExamResultPreviewHandler : IRequestHandler<GetExamResultPreviewRequest, ExamPreviewDTO>
    {
        private readonly IExamResultRepository examResultRepository;

        public GetExamResultPreviewHandler(IExamResultRepository examResultRepository) 
        {
            this.examResultRepository = examResultRepository;
        }

        public async Task<ExamPreviewDTO> Handle(GetExamResultPreviewRequest request, CancellationToken cancellationToken)
        {
            var examResult = await this.examResultRepository.GetExamResultPreview(request.ExamResultId);

            return new ExamPreviewDTO
            {
                title = examResult.Exam.Title,
                Questions = examResult.Exam.Questions.Select((q, i) => new QuestionPreviewDTO
                {
                    QuestionId = q.Id,
                    Content = q.QuestionContent,
                    Type = q.Type,
                    Points = q.Points,
                    Score = examResult.StudentAnswers?.ElementAt(i)?.Points,
                    AnswerContent = q.Type != QuestionType.Open ? null : examResult.StudentAnswers?.Where(x => x.QuestionId == q.Id).FirstOrDefault()?.Content,
                    Answers = q.Type == QuestionType.Open ? null : q.PredefinedAnswers?.Select(a => new AnswerPreviewDTO
                    {
                        AnswerId = a.Id,
                        Content = a.Content,
                        IsCorrect = a.IsCorrect,
                        IsSelected = a.SelectedStudentAnswers
                            .Where(sta => sta.PredefinedAnswerId == a.Id)
                            .Where(sa => sa.StudentAnswer.ExamResultId == examResult.Id)
                            .FirstOrDefault()?.IsSelected
                    })
                })
            };
        }
    }
}
