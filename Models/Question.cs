namespace Models;

public record Question(int Id, string Text, IList<string> Answers);