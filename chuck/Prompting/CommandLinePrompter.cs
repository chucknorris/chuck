namespace test_drive
{
    using System;
    using System.Collections.Generic;
    using Magnum.Binding;
    using Magnum.ValueProviders;

    public class CommandLinePrompter :
        Prompter
    {

        public T Ask<T>(string question)
        {
            return AskInner<T>(question);
        }

        public E AskEnum<E>(string question) where E : struct, IConvertible
        {
            if (!typeof(E).IsEnum) throw new ArgumentException("E must be an enum type");

            var options = Enum.GetNames(typeof(E));

            return AskInner<E>(question, options);
        }

        T AskInner<T>(string question, params string[] options)
        {
            Console.WriteLine(question);
            bool unanswered = true;


            for (int i = 0; i < options.Length; i++)
                Console.WriteLine("{0}. {1}", i, options[i]);


            Console.Write("Answer:");

            Silly<T> silly = null;
            do
            {
                try
                {
                    string result = Console.ReadLine();

                    var dict = new Dictionary<string, object>() { { "answer", result } };
                    var fmd = new FastModelBinder();
                    var cxt = new AskContext(dict);
                    silly = fmd.Bind<Silly<T>>(cxt);

                    unanswered = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Whoops. Looks like something went wrong.");
                }

            } while (unanswered);

            return silly.answer;

        }

        class Silly<T>
        {
            public T answer { get; set; }
        }

        class AskContext :
            ModelBinderContext
        {
            readonly DictionaryValueProvider _provider;

            public AskContext(Dictionary<string, object> dict)
            {
                _provider = new DictionaryValueProvider(dict);
            }

            public bool GetValue(string key, Func<object, bool> matchingValueAction)
            {
                return _provider.GetValue(key, matchingValueAction);
            }

            public bool GetValue(string key, Func<object, bool> matchingValueAction, Action missingValueAction)
            {
                return _provider.GetValue(key, matchingValueAction, missingValueAction);
            }

            public void GetAll(Action<string, object> valueAction)
            {
                _provider.GetAll(valueAction);
            }
        }
    }
}