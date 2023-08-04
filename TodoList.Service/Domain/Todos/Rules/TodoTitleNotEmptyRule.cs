namespace Domain.Todos.BusinessRules;

public class TodoTitleNotEmptyRule
{
	public static string Message => "Todo title must not be empty";

	public static bool IsBroken(string title)
	{
		return string.IsNullOrWhiteSpace(title);
	}
}
