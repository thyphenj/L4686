using System;
namespace L4686
{
    public class Answer
    {
        private int Value { get; set; }

        public Answer(int value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
        public int GetValue()
        {
            return Value;
        }
    }
}

