using Abstractions.Repositories;
using Contracts.QuizService;
using Models;
using Models.ResultTypes;

namespace App.QuizService;

public class QuizService : IQuizService
{
    private readonly IQuestionRepository _questionRepository;
    private IList<Answer> _answers;

    public QuizService(IQuestionRepository questionRepository, IList<Answer> answers)
    {
        _questionRepository = questionRepository;
        _answers = answers;
    }

    public AnswerResult CheckAnswer(int questionId, UserAnswer userAnswer)
    {
        throw new NotImplementedException();
    }

    public Question GetQuestion()
    {
        throw new NotImplementedException();
    }

    private void TakeNewQuestions()
    {
        throw new NotImplementedException();
    }
}