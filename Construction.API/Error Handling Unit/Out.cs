namespace Construction.API.Error_Handling_Unit
{
    public class Out<T>
    {
        public bool IsSuccess { get; set; }

        public T? Value { get; private set; }
        public Exception? Error { get; private set; }

        public static Out<T> Success(T value)
        {
            var s = new Out<T>()
            {
                IsSuccess = true,
                Value = value
            };
            return s;
        }
        public static Out<T> Failure(Exception ex)
        {
            var f = new Out<T>()
            {
                IsSuccess = false,
                Error = ex
            };
            return f;
        }
    }
}
