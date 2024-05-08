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

        protected void AddInclude(Expression<Func<T, object>> includes)
        {
            Includes.Add(includes);
        }
    }
}