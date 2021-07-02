using System;

namespace SystemUtilities
{
    public interface GetLazy<out T>
    {
        T Get { get; }
    }

    public class CustomLazy<T> : GetLazy<T>
    {
        private T _value;
        public T Get => _value == null ? throw new Exception("The element is null") : _value;

        public void SetValue(T value)
        {
            _value = value;
        }
    }
}
