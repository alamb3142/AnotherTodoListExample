using Microsoft.AspNetCore.Mvc;
using Domain.Common.Errors;
using FluentResults;

namespace Api.Http.Controllers;

internal static class ControllerBaseExtensions
{
    public static ActionResult FromResult(this ControllerBase controller, Result result)
    {
        return result switch
        {
            { IsSuccess: true } => controller.Accepted(),
            { IsSuccess: false } => controller.ProcessErrors(result.Errors)
        };
    }

    public static ActionResult<T> FromResult<T>(this ControllerBase controller, Result<T> result)
    {
        return result switch
        {
            { IsSuccess: true } => controller.Ok(result.Value),
            { IsSuccess: false } => controller.ProcessErrors(result.Errors)
        };
    }

    private static ActionResult ProcessErrors(this ControllerBase controller, List<IError> errors)
    {
        var error = errors.FirstOrDefault() ?? new Error("Failure cause not specified");
        return error switch
        {
            NotFoundError => controller.NotFound(error.Message),
            BusinessRuleError => controller.BadRequest(error.Message),
            _ => controller.Problem("Something went wrong")
        };
    }
}
