namespace Construction.API.Error_Handling_Unit
{
    public class ErrorHandling
    {
        public static async Task<Out<T>> TryCatch<T>(Func<Task<T>> func, ILogger logger)
        {
            try
            {
                T res = await func();
                return Out<T>.Success(res);
            }catch (Exception ex)
            {
                logger.LogDebug(ex.Message);
                return Out<T>.Failure(ex);
            }
        }
    }
}
