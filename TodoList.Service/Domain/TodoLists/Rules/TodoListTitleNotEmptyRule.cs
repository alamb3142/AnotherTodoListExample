namespace Domain.TodoLists.Rules;

public class TodoListTitleNotEmptyRule
{
    public static string Message => "Todo List title must not be empty";

	public static bool IsBroken(string title)
	{
		return string.IsNullOrWhiteSpace(title);
	}
}
