using Abstractions.Repositories;
using Contracts.QuizService;
using Models;
using Models.ResultTypes;

namespace App.QuizServices;

public class QuizService : IQuizService
{
    private readonly IQuestionRepository _questionRepository;
    private Stack<Question?> _questions;
    private Question? _currentQuestion;

    public QuizService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
        _questions = new Stack<Question?>();
    }

    public AnswerResult CheckAnswer(UserAnswer userAnswer)
    {
        if (_currentQuestion is null) return new AnswerResult.Incorrect();

        if (_currentQuestion.Answers.Any(answer =>
                answer.Equals(userAnswer.Answer, StringComparison.OrdinalIgnoreCase)))
        {
            return new AnswerResult.Correct();
        }

        return new AnswerResult.Incorrect();
    }

    public async Task<Question> GetQuestion()
    {
        const int defaultLimit = 5;
        if (_questions.Count == 0)
        {
            _questions = await _questionRepository.FindLimitQuestionsAsync(defaultLimit).ConfigureAwait(false);
        }

        _currentQuestion = _questions.Pop();

        return _currentQuestion ?? new Question(-1, "no questions in data base", new List<string>());
    }

    public string GetFirstAnswerToCurrentQuestion()
    {
        return _currentQuestion is null
            ? string.Empty
            : _currentQuestion.Answers.First();
    }
}