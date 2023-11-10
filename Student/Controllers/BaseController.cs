using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Student.Controllers
{
    [Authorize(Policy = "CheckUnauthorized")]
    [Route("api/[controller]/[action]/")]
    public class BaseController : Controller
    {
        //protected string GetModelStateErrors()
        //{
        //    return string.Join("; ", ModelState.Values
        //        .SelectMany(x => x.Errors)
        //        .Select(x => x.ErrorMessage));
        //}
        //protected void CreateErrorResult(ServiceResult serviceResult, Exception ex)
        //{
        //    serviceResult.Success = false;
        //    serviceResult.Messages.AddMessage(MessageType.Error, ex.Message);
        //    serviceResult.Messages.AddMessage(MessageType.Error, ex.InnerException?.Message);
        //}
        //protected async Task<IActionResult> MakeActionCallWithModelAsync<TResult, TModel>(Func<Task<TResult>> action, TModel model) where TModel : IViewModel
        //{
        //    var serviceResult = new ServiceResult();
        //    try
        //    {
        //        ValidateModel(model);
        //        var result = await action();
        //        CreateSuccessResult(serviceResult, result, "OK");
        //    }
        //    catch (SmartException e)
        //    {
        //        CreateErrorResult(serviceResult, e);
        //    }
        //    catch (Exception e)
        //    {
        //        CreateErrorResult(serviceResult, e);
        //    }
        //    return Json(serviceResult);
        //}

        //protected async Task<IActionResult> MakeActionCallAsync<TResult>(Func<Task<TResult>> action)
        //{
        //    var serviceResult = new ServiceResult();
        //    try
        //    {
        //        var result = await action();
        //        CreateSuccessResult(serviceResult, result, "OK");
        //    }
        //    catch (SmartException e)
        //    {
        //        CreateErrorResult(serviceResult, e);
        //    }
        //    catch (Exception e)
        //    {
        //        CreateErrorResult(serviceResult, e);
        //    }
        //    return Json(serviceResult);
        //}

        //protected void ValidateModel<TModel>(TModel model) where TModel : IViewModel
        //{
        //    if (model == null)
        //        throw new Exception($"No field in {typeof(TModel).Name} model was initialized");
        //    if (!ModelState.IsValid)
        //        throw new Exception(GetModelStateErrors());
        //}

        //protected void CreateSuccessResult(ServiceResult serviceResult, object data, string message)
        //{
        //    serviceResult.Success = true;
        //    serviceResult.Data = data;
        //    serviceResult.Messages.AddMessage(MessageType.Info, message);
        //}
    }
}
