﻿namespace StorEsc.Core.Data.Structs;

public struct Optional<T>
{
    public bool IsEmpty
    {
        get => _value == null;
        private set { }
    }
    private bool HasValue
    { 
        get => _value != null;
        set { }
    }

    private T _value;

    public T Value
    {
        get
        {
            if(IsEmpty)
                throw new InvalidOperationException();

            return _value;
        }
    }

    public Optional(T value)
    {
        _value = value;
    }

    public static explicit operator T(Optional<T> optional)
        => optional.Value;

    public static implicit operator Optional<T>(T value)
        => new Optional<T>(value);

    public override bool Equals(object obj)
    {
        if (obj is Optional<T>)
            return this.Equals((Optional<T>)obj);
        else
            return false;
    }
    public bool Equals(Optional<T> other)
    {
        if (HasValue && other.HasValue)
            return object.Equals(_value, other._value);
        else
            return HasValue == other.HasValue;
    }

    public static bool operator ==(Optional<T> left, Optional<T> right)
        => left.Equals(right);

    public static bool operator !=(Optional<T> left, Optional<T> right)
        => (left == right) is false;
}