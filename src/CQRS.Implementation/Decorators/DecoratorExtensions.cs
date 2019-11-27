using System.Collections.Generic;
using CQRS.Models;

namespace CQRS.Implementation.Decorators
{
    public static class DecoratorExtensions
    {
        public static IEnumerable<IDecorator<T>> DecoratorsChain<T>(this IDecorator<T> decorator)
        {
            while (decorator != null)
            {
                yield return decorator;

                decorator = decorator.Decorated as IDecorator<T>;
            }
        }
    }
}
