using Models;
using Models.ResultTypes;

namespace Contracts.QuizService;

public interface IQuizService
{
    AnswerResult CheckAnswer(int questionId, UserAnswer userAnswer);

    Question GetQuestion();
}