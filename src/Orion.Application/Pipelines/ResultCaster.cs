using OperationResult;

namespace Orion.Application.Pipelines
{
    public static class ResultCaster
    {
        public static TResult ErrorResult<TResult>(Exception ex)
        {
            var resultType = typeof(TResult);
            if (resultType.IsGenericType)
            {
                var resultError = typeof(Result).GetMethods().FirstOrDefault(x => x.Name == "Error" && x.IsGenericMethod);
                resultError = resultError.MakeGenericMethod(resultType.GetGenericArguments().First());

                var result = (TResult)Convert.ChangeType(resultError.Invoke(null, new object[] {
                    ex
                }), resultType);

                return result;
            }

            return (TResult)Convert.ChangeType(Result.Error(ex), resultType);
        }

        public static bool UsesOperationResult<TResult>()
        {
            var resultType = typeof(TResult);
            return resultType == typeof(Result) || (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>));
        }
    }
}
