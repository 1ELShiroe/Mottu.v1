using Microsoft.AspNetCore.Mvc;
using Shared.Abstracts;

namespace Api.Extensions;

public static class ControllerExtensions
{
    public static IActionResult ToActionResult<T>(this ControllerBase controller, UseCaseResult<T> result)
    {
        if (result.Success)
        {
            return controller.StatusCode(result.StatusCode, new
            {
                sucesso = true,
                mensagem = result.Message,
                dados = result.Data
            });
        }

        return controller.BadRequest(new
        {
            sucesso = false,
            mensagem = result.Message,
        });
    }
}