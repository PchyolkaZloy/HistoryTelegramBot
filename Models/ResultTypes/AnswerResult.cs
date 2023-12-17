namespace Models.ResultTypes;

public abstract record AnswerResult
{
    public sealed record Correct : AnswerResult;

    public sealed record Incorrect : AnswerResult;
}