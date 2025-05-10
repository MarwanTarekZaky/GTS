using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

public class Todo : Controller
{
    private readonly HttpClient _httpClient;

    public Todo(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Action to list all todos
    public async Task<IActionResult> ListAllTodos(string? status = null, string? priority = null, DateTime? startDate = null, DateTime? endDate = null, string? orderBy = "Title", string? sortOrder = "asc")
    {
        var queryString = $"?status={status}&priority={priority}&startDate={startDate}&endDate={endDate}&orderBy={orderBy}&sortOrder={sortOrder}";
        var todos = await _httpClient.GetFromJsonAsync<List<TodoDto>>("http://localhost:5257/api/todo" + queryString);

        return View("Index", todos); // Passing the todos to the Index view
    }
}