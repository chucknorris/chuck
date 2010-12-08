namespace test_drive
{
    using System;

    public interface Prompter
    {
        T Ask<T>(string question);
        E AskEnum<E>(string question) where E : struct, IConvertible;
    }
}