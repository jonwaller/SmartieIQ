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
            List<int?> answer = new List<int?> { 1, 2, 3, 4, 5, 6 };
            isSameConsoleWrite(fillMissing(question), answer);

            question = new List<int?> { 2, 4, 6, 8, null };
            answer = new List<int?> { 2, 4, 6, 8, 10 };
            isSameConsoleWrite(fillMissing(question), answer);

            question = new List<int?> { 2, -4, 8, -16, 32, null };
            answer = new List<int?> { 2, -4, 8, -16, 32, -64 };
            isSameConsoleWrite(fillMissing(question), answer);

            /*
            question = new List<int?> { 1, 3, 5, 7, 9, null };
            answer = new List<int?> { 1, 3, 5, 7, 9, 11 };
            isSameConsoleWrite(fillMissing(question), answer);

            question = new List<int?> { 1, 1, 2, 3, 5, 8, null };
            answer = new List<int?> { 1, 1, 2, 3, 5, 8, 13 }; //Fib
            isSameConsoleWrite(fillMissing(question), answer);

            question = new List<int?> { 154, 162, 170, 178, 186, null };
            answer = new List<int?> { 154, 162, 170, 178, 186, 194 }; //+8
            isSameConsoleWrite(fillMissing(question), answer);

            question = new List<int?> { 2, -4, 8, -16, 32, null };
            answer = new List<int?> { 2, -4, 8, -16, 32, -64 };
            isSameConsoleWrite(fillMissing(question), answer);

            question = new List<int?> { 1, 4, null, null, null, 36, null };
            answer = new List<int?> { 1, 4, 9, 16, 25, 36, 49 }; //Sqr
            isSameConsoleWrite(fillMissing(question), answer);
            */

            exitConsole();
        }

        private static void exitConsole()
        {
            Console.WriteLine("Finished.");
            Console.ReadLine();
        }
        
        private static void isSameConsoleWrite(List<int?> attemptedAnswer, List<int?> correctAnswer)
        {
            Console.WriteLine("Attempted answer: " + makeHumanReadable(attemptedAnswer));
            Console.WriteLine("Correct answer:   " + makeHumanReadable(correctAnswer));
            if (isSame(attemptedAnswer, correctAnswer))
            {
                Console.WriteLine("PASS");
            }
            else
            {
                Console.WriteLine("FAIL");

            }
            Console.WriteLine();
        }

        private static string makeHumanReadable(List<int?> list)
        {
            String outStr = "";

            outStr = "{";
            
            foreach(int? item in list){
                String itemStr = (item==null) ? "NULL" : item.ToString();
                outStr = outStr + itemStr + ", ";
            }

            if (outStr.Length > 1)
            {
                outStr = outStr.Substring(0, outStr.Length - 2); // To cut of the last ", "
            }

            outStr = outStr + "}";

            return outStr;
        }

        private static bool isSame(List<int?> list1, List<int?> list2)
        {
            if (list1.Count != list2.Count) return false;

            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i] != list2[i])
                {
                    return false;
                }
            }
            return true;
        }

        /* Real code :) */

        private static List<int?> fillMissing(List<int?> sequenceWithNulls)
        {
            Func<int, List<int?>, int> itemGenerator = findItemGenerator(sequenceWithNulls);
            
            List<int?> newSequence=new List<int?>();
            for (int i = 0; i < sequenceWithNulls.Count; i++)
            {
                int? item = sequenceWithNulls[i];
                if (item==null){
                    item = itemGenerator(i, sequenceWithNulls);
                }
                newSequence.Add(item);
            }
            return newSequence;
        }

        private static Func<int, List<int?>, int> findItemGenerator(List<int?> sequenceWithNulls)
        {
            //Not a very exciting function.
            return (indexOfItemWanted, sequenceKnown) => indexOfItemWanted+1;
        }

    }
}
