namespace test_drive
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            Prompter prompter = new CommandLinePrompter();
            var result = prompter.AskEnum<Test>("How old are you?");


            Console.WriteLine("You typed in '{0}'", result);
            Console.ReadKey(true);
        }
    }

    public enum Test
    {
        Bob,
        Bill
    }
}
