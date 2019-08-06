using System;
using System.Collections.Generic;
using System.Linq;

namespace eNotesConsole
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            QuestionSet1 = new List<string>();
            QuestionSet2 = new List<string>();
            QuestionSet1.Add("Ram1");
            QuestionSet1.Add("Ram2");
            QuestionSet1.Add("Ram3");
            QuestionSet1.Add("Ram4");
            QuestionSet2.Add("Test1");
            QuestionSet2.Add("Test2");
            QuestionSet2.Add("Test3");
            // for (int i = 0; i <8; i++)
            {
                Console.WriteLine(GetRandomWord());
                MatchListhWords();
            }
            string Questions1 = "word1,word2,word3 test1";
            string[] result = Questions1.Split(new char[] { '\n', ',' });
            foreach (var resp in result)
            {
                QuestionSet1.Add(resp);
            }

            Console.ReadLine();
        }
        public static List<string> QuestionSet1 { get; set; }
        public static List<string> QuestionSet2 { get; set; }
        public static List<string> MatchedQuestionSet1 { get; set; }
        public static List<string> MatchedQuestionSet2 { get; set; }

        public static String GetRandomWord()
        {
            Random randomizer = new Random();
            int index = randomizer.Next(QuestionSet1.Count);
            string randomword = QuestionSet1[index]; //<---- this is the fix

            return randomword;
        }

        public static void  MatchListhWords()
        {
            var joined = QuestionSet1.Zip(QuestionSet2, (x, y) => new { x, y });
            var shuffled = joined.OrderBy(x => Guid.NewGuid()).ToList();
            MatchedQuestionSet1 = shuffled.Select(pair => pair.x).ToList();
            MatchedQuestionSet2 = shuffled.Select(pair => pair.y).ToList();
            //return matchword;
        }
    }
}
