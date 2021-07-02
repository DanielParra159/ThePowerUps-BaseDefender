using System;

namespace SystemUtilities
{
    public class NullOptional<T> : Optional<T>
    {
        public NullOptional(T value) : base(value)
        {
        }

        public override void Else(Action action)
        {
        }
    }

    public class Optional<T>
    {
        private readonly T _value;

        public T Get => _value == null ? throw new Exception("The element is null") : _value;

        public Optional(T value)
        {
            this._value = value;
        }

        public Optional()
        {
        }

        public bool IsPresent()
        {
            return _value != null;
        }

        public Optional<T> IfPresent(Action<T> consumer)
        {
            if (_value == null)
                return this;

            consumer(_value);
            return new NullOptional<T>(_value);
        }

        public virtual void Else(Action action)
        {
            action();
        }

        public T OrElse(T elseValue)
        {
            if (_value == null)
            {
                return elseValue;
            }

            return _value;
        }

        public T OrElseThrow(Exception exceptionToThrow)
        {
            if (_value == null)
            {
                throw exceptionToThrow;
            }

            return _value;
        }
    }
}
