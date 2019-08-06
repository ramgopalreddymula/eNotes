using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FreshMvvm;
using PropertyChanged;
using Xamarin.Forms;

namespace eNote
{
    [AddINotifyPropertyChangedInterface]
    public class RandomGeneratorPageModel: FreshBasePageModel
    {
        public RandomGeneratorPageModel()
        {
           
            GetMatchItems = new ObservableCollection<RandomNumbers>();
        }
        public string Questions1 { get; set; }
        public string Questions2 { get; set; }
        public List<string> QuestionSet1 { get; set; }
        public List<string> QuestionSet2 { get; set; }
        public List<string> MatchedQuestionSet1 { get; set; }
        public List<string> MatchedQuestionSet2 { get; set; }
       public ObservableCollection<RandomNumbers> GetMatchItems { get; set; }
        public String GetRandomWord()
        {
            Random randomizer = new Random();
            string randomword = string.Empty;
            if (QuestionSet1 != null && QuestionSet1.Count > 0)
            {
                int index = randomizer.Next(QuestionSet1.Count);
                randomword = QuestionSet1[index]; 
            }
            return randomword;
        }
        public List<T> Shuffle<T>(List<T> list)
        {
            Random rnd = new Random();
            for (int i = 0; i < list.Count; i++)
            {
                int k = rnd.Next(0, i);
                T value = list[k];
                list[k] = list[i];
                list[i] = value;
            }
            return list;
        }
        public void MatchListhWords()
        {
            if (QuestionSet1 != null && QuestionSet2 != null && QuestionSet2.Count > 0 && QuestionSet1.Count > 0)
            {
                var joined = QuestionSet1.Zip(QuestionSet2, (x, y) => new { x, y });
                var shuffled = joined.OrderBy(x => Guid.NewGuid()).ToList();
                var MatchedQuestionSet11 = shuffled.Select(pair => pair.x).ToList();
                MatchedQuestionSet1 =  Shuffle(MatchedQuestionSet11);
                 MatchedQuestionSet2 = shuffled.Select(pair => pair.y).ToList();
                //MatchedQuestionSet2 = Shuffle(MatchedQuestionSet22);
                List<RandomNumbers> MainRandomList = new List<RandomNumbers>();
               
                for (int i=0;i< MatchedQuestionSet1.Count;i++)
                {
                    RandomNumbers randomList = new RandomNumbers();
                    randomList.Item1 = MatchedQuestionSet1[i];
                    randomList.Item2 = MatchedQuestionSet2[i];
                    MainRandomList.Add(randomList);
                }

                GetMatchItems = new ObservableCollection<RandomNumbers>(MainRandomList);
               

            }
            else
            {
                 CoreMethods.DisplayAlert("Both Editor boxes Should not be empty!", "Please Enter Something else", "Ok");
            }
            //return matchword;
        }
        public Command RandomCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if(!string.IsNullOrEmpty(Questions1) && !string.IsNullOrWhiteSpace(Questions1))
                    {
                        QuestionSet1 = new List<string>();
                        string[] result=Questions1.Split(new char[] { '\n',',' });
                        foreach(var resp in result)
                        {
                            if(!string.IsNullOrEmpty(resp) && !string.IsNullOrWhiteSpace(resp))
                              QuestionSet1.Add(resp);
                        }
                        var resps=GetRandomWord();
                        await CoreMethods.DisplayAlert("Random Picked Item!!", resps, "Ok");
                    }
                    else
                    {
                        await CoreMethods.DisplayAlert("Editor box!", "Please Enter Something else", "Ok");
                    }
                });
            }
        }
        public Command MatchedCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (!string.IsNullOrEmpty(Questions2) && !string.IsNullOrWhiteSpace(Questions2))
                    {

                        if (!string.IsNullOrEmpty(Questions1) && !string.IsNullOrWhiteSpace(Questions1))
                        {
                            QuestionSet1 = new List<string>();
                            string[] result = Questions1.Split(new char[] { '\n', ',' });
                            foreach (var resp in result)
                            {
                                if (!string.IsNullOrEmpty(resp) && !string.IsNullOrWhiteSpace(resp))
                                    QuestionSet1.Add(resp);
                            }
                            QuestionSet2 = new List<string>();
                            string[] result2 = Questions2.Split(new char[] { '\n', ',' });
                            foreach (var resp in result2)
                            {
                                if (!string.IsNullOrEmpty(resp) && !string.IsNullOrWhiteSpace(resp))
                                    QuestionSet2.Add(resp);
                            }
                            MatchListhWords();
                        }
                        else
                        {
                            // Please add words
                        }
                    }
                });
            }
        }
        public Command ClearCommand
        {
            get
            {
                return new Command(async () =>
                {
                    GetMatchItems = new ObservableCollection<RandomNumbers>();
                    Questions1 = string.Empty;
                    Questions2 = string.Empty;
                });
            }
        }
    }
}
