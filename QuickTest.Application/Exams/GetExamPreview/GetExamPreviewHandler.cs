using MediatR;
using QuickTest.Application.Common;
using QuickTest.Infrastructure.Interfaces;

namespace QuickTest.Application.Exams.GetExamPreview
{
    public sealed class GetExamPreviewHandler : IRequestHandler<GetExamPreviewRequest, ExamPreviewDTO>
    {
        private readonly IExamRepository examRepository;

        public GetExamPreviewHandler(IExamRepository examRepository)
        {
            this.examRepository = examRepository;
        }

        public async Task<ExamPreviewDTO> Handle(GetExamPreviewRequest request, CancellationToken cancellationToken)
        {
            var exam = await this.examRepository.GetExamPreview(request.ExamId);

            return new ExamPreviewDTO
            {
                title = exam.Title,
                Questions = exam.Questions.Select(q => new QuestionPreviewDTO
                {
                    QuestionId = q.Id,
                    Content = q.QuestionContent,
                    Type = q.Type,
                    Points = q.Points,
                    Answers = q.PredefinedAnswers.Select(a => new AnswerPreviewDTO
                    {
                        AnswerId = a.Id,
                        Content = a.Content,
                        IsCorrect = a.IsCorrect,
                    })
                })
            };
        }
    }
}
