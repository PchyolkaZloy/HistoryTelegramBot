﻿using Models;
using Models.ResultTypes;

namespace Contracts.QuizService;

public interface IQuizService
{
    AnswerResult CheckAnswer(UserAnswer userAnswer);

    Task<Question> GetQuestion();

    Task<List<string>> GetAnswersToCurrentQuestion();
}