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

        public FinalExam(TimeOnly duration):base(TimeOnly.MinValue, 0, null) 
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

        public PracticalExam(TimeOnly duration): base(TimeOnly.MinValue, 0, null) 
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


        }
    }
}

