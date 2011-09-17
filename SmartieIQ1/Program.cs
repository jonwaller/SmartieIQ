using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace SmartieIQ
{
    class Program
    {

    
        
        /* Console and testing */

        static void Main(string[] args)
        {		
            	
			//Nullable int
            List<int?> question = new List<int?> { 1, 2, 3, 4, 5, null };
            List<int> answer = new List<int> { 1, 2, 3, 4, 5, 6 };
            writeResultToConsole(question,answer);

            question = new List<int?> { 1, 2, null, 4, 5 };
            answer = new List<int> { 1, 2, 3, 4, 5 };
            writeResultToConsole(question, answer);

            question = new List<int?> { 2, 4, 6, 8, null };
            answer = new List<int> { 2, 4, 6, 8, 10 };
            writeResultToConsole(question, answer);

            question = new List<int?> { 2, -4, 8, -16, 32, null };
            answer = new List<int> { 2, -4, 8, -16, 32, -64 };
            writeResultToConsole(question, answer);
                        
            question = new List<int?> { 1, 3, 5, 7, 9, null };
            answer = new List<int> { 1, 3, 5, 7, 9, 11 };
            writeResultToConsole(question, answer);
                        
            question = new List<int?> { 1, 1, 2, 3, 5, 8, null };
            answer = new List<int> { 1, 1, 2, 3, 5, 8, 13 }; //Fib
            writeResultToConsole(question, answer);

            question = new List<int?> { 154, 162, 170, 178, 186, null };
            answer = new List<int> { 154, 162, 170, 178, 186, 194 }; //+8
            writeResultToConsole(question, answer);

            question = new List<int?> { 2, -4, 8, -16, 32, null };
            answer = new List<int> { 2, -4, 8, -16, 32, -64 };
            writeResultToConsole(question, answer);

            question = new List<int?> { 1, 4, null, null, null, 36, null };
            answer = new List<int> { 1, 4, 9, 16, 25, 36, 49 }; //Sqr
            writeResultToConsole(question, answer);
            

            exitConsole();
        }

        private static void exitConsole()
        {
            Console.WriteLine("Finished.");
            Console.ReadLine();
        }
        
        private static void writeResultToConsole(List<int?> question, List<int> correctAnswer)
        {

            bool foundFunction;
            List<int> attemptedAnswer=new List<int>(); //Though this is never read
            try
            {
                foundFunction = true;
                attemptedAnswer = SequenceHelper.fillMissing(question);
            }
            catch (SequenceHelper.CouldNotFindGeneratorFunctionException){
                foundFunction = false;
            }

			if (foundFunction && SequenceHelper.isSame(attemptedAnswer, correctAnswer))
            {
                Console.WriteLine("PASS:");
                Console.WriteLine("  Question: " + getHumanReadable(question));
                Console.WriteLine("  Answer:   " + getHumanReadable(attemptedAnswer));
            }
            else if (foundFunction)
            {
                Console.WriteLine("FAIL:");
                Console.WriteLine("  Question:        " + getHumanReadable(question));
                Console.WriteLine("  System's answer: " + getHumanReadable(attemptedAnswer));
                Console.WriteLine("  Correct answer:  " + getHumanReadable(correctAnswer));
            }
            else { //foundFunction==false
                Console.WriteLine("FAIL:");
                Console.WriteLine("  Question:        " + getHumanReadable(question));
                Console.WriteLine("  System's answer: Couldn't determine pattern.");
                //Console.WriteLine("  Correct answer:  " + getHumanReadable(correctAnswer));
            }
            Console.WriteLine();
        }

        private static string getHumanReadable(List<int> list)
        {
            List<int?> listWithNullables = list.ConvertAll<int?>(delegate(int i) { return i; });
            return getHumanReadable(listWithNullables);
        }

        private static string getHumanReadable(List<int?> list)
        {
            String outStr = "";

            outStr = "{";
            
            foreach(int? item in list){
                string itemStr = (item==null) ? "NULL" : item.ToString();
                string extraSpace = (item!=null && item>=0)?" ":"";
                outStr = outStr + extraSpace + itemStr + ", ";
            }

            if (outStr.Length > 1)
            {
                outStr = outStr.Substring(0, outStr.Length - 2); // To cut of the last ", "
            }

            outStr = outStr + "}";

            return outStr;
        }
    }
}
