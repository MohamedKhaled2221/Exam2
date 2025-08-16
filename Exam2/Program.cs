namespace Exam2
{
    public abstract class Question
    {
        public string? Header { get; set; }
        public string? Body { get; set; }
        public int Mark { get; set; }
        public Answers[] AnswerList { get; set; }
        public abstract void DispalyQuestions();
        public abstract void DisplayCorrectAnswers();

    }
    public enum Typeofexam { Final, Practical }
    public class TrueFalseQuestion : Question
    {
        public List<int> NumofQuestion { get; set; } = new List<int>();
        public List<bool> Answer { get; set; } = new List<bool>();
        
        public override void DispalyQuestions()
        {
            Console.WriteLine($"Header: {Header}, Body: {Body}, Mark: {Mark}");
            foreach (var answer in Answer)
            {

                Console.WriteLine(answer ? "True" : "False");
            }
        }
        public override void DisplayCorrectAnswers()
        {
            for (int i = 0; i < NumofQuestion.Count; i++)
            {
                Console.WriteLine($"Question {NumofQuestion[i]}: {AnswerList[i].AnswerText}");
            }
        }

    }
   public class McqQuestion : Question
    {
        public List<int> NumofQuestion { get; set; } = new List<int>();
        public List<char> CorrectChoicesIndices { get; set; } = new List<char>();
       
        public override void DispalyQuestions()
        {
            Console.WriteLine($"Header: {Header}, Body: {Body}, Mark: {Mark}");
            foreach (var answer in AnswerList)
            {
                Console.WriteLine($"AnswerId: {answer.AnswerId}, AnswerText: {answer.AnswerText}");
            }
        }
        public override void DisplayCorrectAnswers()
        {
            for (int i = 0; i < NumofQuestion.Count; i++)
            {
                Console.WriteLine($"Question {NumofQuestion[i]}: {AnswerList[i].AnswerText}");
            }
        }

    }
   public class Answers :IComparable<Answers> , ICloneable
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public Answers(int answerId, string answerText)
        {
            AnswerId = answerId;
            AnswerText = answerText;
        }
        public int CompareTo(Answers other)
        {
            return AnswerId.CompareTo(other?.AnswerId) ;
        }

        public object Clone()
        {
            return new Answers( AnswerId,AnswerText).Clone();
        }
        override public string ToString()
        {
            return $"AnswerId: {AnswerId}, AnswerText: {AnswerText}";
        }
    }

   public abstract  class Exam 
    {
        public List<Question> Questions { get; set; } = new List<Question>();

        public TimeOnly TimeofExam { get; set; }
        public int NumberOfQuestions { get; set; }
        public abstract string ExamName { get; }
        public Subject AssociatedSubject { get; set; }
        protected Exam(TimeOnly TimeofExam, int NumberofQuestions,Subject AssociatedSubject)
        {
            this.TimeofExam = TimeofExam;
            this.NumberOfQuestions = NumberofQuestions;
            this.AssociatedSubject = AssociatedSubject;

        }
        public abstract void DisplayExamDetails();
        public abstract void ShowResults();

    }
    class FinalExam : Exam
    {
        
        public int Grade { get;  }
        public override string ExamName => "Final Exam";

        public TimeOnly Duration { get; }

        public FinalExam(TimeOnly timeofExam,int NumberofQuestions,Subject Associated,int Grade):base(timeofExam,NumberofQuestions,Associated)
        {
           this.Grade = Grade;
        }

        public FinalExam(TimeOnly duration):base(duration, 0, null) 
        {
            Duration = duration;
        }

        public override void DisplayExamDetails()
        {
            Console.WriteLine($"Exam Name: {ExamName}, Time: {TimeofExam}");
        }
        public override  void  ShowResults()
        {
            
             Console.WriteLine($"Grade: {Grade}/100");
            Console.WriteLine("\nQuestions and Answers:");

            foreach (var question in Questions)
            {
                question.DispalyQuestions();
                question.DisplayCorrectAnswers();
                Console.WriteLine();
            }
        }

    }

    public class Subject
    {

        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Exam ExamofSubject { get; set; }
        public Subject(int subjectId, string subjectName)
        {
            SubjectId = subjectId;
            SubjectName = subjectName;
        }


        public void CreateExam(Typeofexam Type, TimeOnly duration)
        {
            ExamofSubject = Type switch
            {
                Typeofexam.Final => new FinalExam(duration),
                Typeofexam.Practical => new PracticalExam(duration),
                _ => throw new ArgumentException("Invalid exam type")
            };

        }

        public override string ToString()
        {
            return $"SubjectId: {SubjectId}, SubjectName: {SubjectName}, Exam: {ExamofSubject?.ExamName}";

        }
    }
    public class PracticalExam : Exam
    {
        public int Grade { get; }
        public override string ExamName => "Practical Exam";

        public TimeOnly Duration { get; }

        public PracticalExam(TimeOnly timeofExam, int NumberofQuestions, Subject Associated, int Grade) : base(timeofExam, NumberofQuestions, Associated)
        {
            this.Grade = Grade;
        }

        public PracticalExam(TimeOnly duration): base(duration, 0, null) 
        {
            Duration = duration;
        }

        public override void DisplayExamDetails()
        {
            Console.WriteLine($"Exam Name: {ExamName}, Time: {TimeofExam}");
        }
        public override void ShowResults()
        {
            Console.WriteLine($"Grade: {Grade}/100");
            Console.WriteLine("\nQuestions and Answers:");
            foreach (var question in Questions)
            {
                question.DispalyQuestions();
                question.DisplayCorrectAnswers();
                Console.WriteLine();
            }
        }
    }
   
    internal class Program
    {
        static void Main(string[] args)
        {
         
            var Subject = new Subject(1, "C# Fundamentals");

            Subject.CreateExam(Typeofexam.Final, new TimeOnly(3, 0)); 

            
            var answers01 = new Answers[]
            {
            new Answers(1, "True"),
            new Answers(2, "False")
            };

            var answers02 = new Answers[]
            {
            new Answers(1, " a=5"),
            new Answers(2, "b= 2"),
            new Answers(3, "c= 25"),
            new Answers(4, "d= 53")
            };

            
            var truefalseQuestion = new TrueFalseQuestion
            {
                Header = "True OR False Question",
                Body = "OPP",
                Mark = 10,
                AnswerList = answers01,
                NumofQuestion = new List<int> { 1 },
                Answer = new List<bool> { true }
            };
            var mcqQuestion = new McqQuestion
            {
                Header = "MCQ Question",
                Body = "Which is the correct Value bigger than 50",
                Mark = 10,
                AnswerList = answers02,
                NumofQuestion = new List<int> { 4 },
                CorrectChoicesIndices = new List<char> { 'd' } 
            };

            if (Subject.ExamofSubject is FinalExam finalExam)
            {
                finalExam.Questions.Add(truefalseQuestion);
                finalExam.Questions.Add(mcqQuestion);
                finalExam.NumberOfQuestions = finalExam.Questions.Count;
            }

            Subject.ExamofSubject.DisplayExamDetails();
           Subject.ExamofSubject.ShowResults();  
            var OperatingsysSubject = new Subject(102, "OPerating SYS");
            OperatingsysSubject.CreateExam(Typeofexam.Practical, new TimeOnly(2, 30)); 
            var practicalQuestion = new McqQuestion
            {
                Header = "Practical Question",
                Body = "Which is True solution",
                Mark = 20,
                AnswerList = new Answers[]
                {
                new Answers(1, "a"),
                new Answers(2, "b"),
                new Answers(3, "c"),
                new Answers(4, "d")
                },
                NumofQuestion = new List<int> { 1 },
                CorrectChoicesIndices = new List<char> { 'b' } 
            };
            if (OperatingsysSubject.ExamofSubject is PracticalExam practicalExam)
            {
                practicalExam.Questions.Add(practicalQuestion);
                practicalExam.NumberOfQuestions = practicalExam.Questions.Count;
            }
            Console.WriteLine(" The Results of Particular Exam");
            OperatingsysSubject.ExamofSubject.DisplayExamDetails();
            Console.WriteLine("\nTaking the exam...\n");
            OperatingsysSubject.ExamofSubject.ShowResults();
            Console.WriteLine("================================================");
            Console.WriteLine("info about subject");
            Console.WriteLine(Subject.ToString());
            Console.WriteLine(OperatingsysSubject.ToString());

          
        }
    }

}
    


