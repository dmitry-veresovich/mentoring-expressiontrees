using System;
using System.Linq;
using System.Linq.Expressions;

namespace ClassMaper
{
    public class MappingGenerator
    {
        public Mapper<TSource, TDestination> Generate<TSource, TDestination>()
        {
            var sourceParam = Expression.Parameter(typeof(TSource));
            var destination = Expression.New(typeof(TDestination));
            Func<TSource, TDestination, TDestination> mapProperties = Map;
            var map = Expression.Call(mapProperties.Method, sourceParam, destination);

            var mapFunction = Expression.Lambda<Func<TSource, TDestination>>(map, sourceParam);
            return new Mapper<TSource, TDestination>(mapFunction.Compile());
        }

        private static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            var sourceProperties = typeof(TSource).GetProperties();
            var destinationProperties = typeof(TDestination).GetProperties();
            foreach (var sourceProperty in sourceProperties)
            {
                var value = sourceProperty.GetValue(source);
                var destinationProperty = destinationProperties.FirstOrDefault(info => info.Name == sourceProperty.Name);

                destinationProperty?.SetValue(destination, value);
            }

            return destination;
        }
    }
}
