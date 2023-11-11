namespace Student.DTO.ViewModels.Paginations
{
    public class PagingResponse<TView>
    {
        public List<TView> Data { get; set; }
        public int ItemCount { get; set; }
        public int PageCount { get; set; }
    }
}
