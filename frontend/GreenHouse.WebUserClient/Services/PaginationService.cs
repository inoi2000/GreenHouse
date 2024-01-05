namespace GreenHouse.WebUserClient.Services
{
    public class PaginationService<T>
    {
        public IReadOnlyList<T> Pagination(int take, int skip, IReadOnlyList<T> list)
        {
            if (list.Count() > take)
            {
                return list.Skip(skip).Take(take).ToList();
            }
            else
            {
                return list.ToList();
            }
        }
    }
}
