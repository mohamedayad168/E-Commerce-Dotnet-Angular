using System.Linq.Expressions;

namespace Core.Spacifications
{
    public class BaseSpacification<T> : ISpecification<T>
    {
        public BaseSpacification()
        {
        }

        public BaseSpacification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> includes)
        {
            Includes.Add(includes);
        }

        protected void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDescending = orderByDesc;
        }

        protected void AddPagined(int skip, int take)
        {
            Take = take;
            Skip = skip;
            IsPagingEnabled = true;
        }
    }
}