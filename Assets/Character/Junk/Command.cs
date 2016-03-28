namespace Character.Command
{
    using System;

    interface Command
    {
        void Execute();
    }

    interface Invoker
    {
        void Invoke(Command command);
    }

    class ConcreteCommand<T> : Command
    {
        private T target;
        private Action<T> command;

        public ConcreteCommand(T target, Action<T> command)
        {
            this.target = target;
            this.command = command;
        }

        public void Execute()
        {
            command(target);
        }
    }

    class ConcreteInvoker : Invoker
    {
        public void Invoke(Command command)
        {
            command.Execute();
        }
    }

    class Receiver
    {
        public void FirstCommand()
        {
            Console.WriteLine("First Command");
        }

        public void SecondCommand()
        {
            Console.WriteLine("Second Command");
        }
    }
}