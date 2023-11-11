using Microsoft.EntityFrameworkCore;
using Student.DTO.ViewModels.Paginations;

namespace Student.BLL.Extensions
{
    public static class PagingExtension
    {
        public static async Task<PagingResponse<TView>> ToPagingAsync<TView>(this IQueryable<TView> result, PagingRequest model) where TView : class
        {
            var count = await result.CountAsync();

            var page = Convert.ToInt32(Math.Ceiling(decimal.Divide(count, model.Count)));

            var data = await result.Skip((model.Page - 1) * model.Count).Take(model.Count).ToListAsync();
            return new PagingResponse<TView>
            {
                Data = data,
                ItemCount = count,
                PageCount = page
            };
        }
    }
}
