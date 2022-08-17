using System;
using System.Collections.Generic;

namespace L4686
{
    public class AnswerList
    {
        private List<Answer> Answers { get; set; }

        public AnswerList()
        {
            Answers = new List<Answer>();
        }

        public void Add(int number)
        {
            bool found = false;
            foreach (var work in Answers)
            {
                if (work.GetValue() == number)
                    found = true;
            }
            if (!found)
                Answers.Add(new Answer(number));
        }

        public List<Answer> GetAnswers()
        {
            return Answers;
        }

        public override string ToString()
        {
            return $"Count {Answers.Count} " + (Answers.Count == 1 ? $"=> {Answers[0].GetValue()}" : "");
        }

        internal void OnlyIncludeDigitAtPosition(int digit, int pos)
        {
            int i = Answers.Count;
            while (i > 0)
            {
                i--;
                var answer = Answers[i];
                if (Program.digitAtPosition(answer.GetValue(), pos) != digit)
                {
                    Answers.Remove(answer);
                }
            }
        }
        internal void OnlyIncludeDigitAtPosition(HashSet<int> digits, int pos)
        {
            var work = new List<Answer>();
            foreach (var dig in digits)
            {
                int i = Answers.Count;
                while (i > 0)
                {
                    i--;
                    var answer = Answers[i];
                    if (Program.digitAtPosition(answer.GetValue(), pos) == dig)
                    {
                        work.Add(answer);
                    }
                }
            }
            Answers = work;
        }
        internal void OnlyIncludeDigitAtPosition(int[] digits, int pos)
        {
            var work = new List<Answer>();
            foreach (var dig in digits)
            {
                int i = Answers.Count;
                while (i > 0)
                {
                    i--;
                    var answer = Answers[i];
                    if (Program.digitAtPosition(answer.GetValue(), pos) == dig)
                    {
                        work.Add(answer);
                    }
                }
            }
            Answers = work;
        }
    }
}
