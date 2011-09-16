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

        public static readonly int MAX_DEPTH=5;

        public class UnknownResultException : System.Exception
        {
            public UnknownResultException() : base() { }
        }

         public class CouldNotFindGeneratorFunctionException : System.Exception
        {
             public CouldNotFindGeneratorFunctionException() : base() { }
        }

        
        
        /* Console and testing */

        static void Main(string[] args)
        {
            //Nullable int
            List<int?> question = new List<int?> { 1, 2, 3, 4, 5, null };
            List<int> answer = new List<int> { 1, 2, 3, 4, 5, 6 };
            //isSameConsoleWrite(question, fillMissing(question), answer);

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
                attemptedAnswer = fillMissing(question);
            }
            catch (CouldNotFindGeneratorFunctionException){
                foundFunction = false;
            }

            if (foundFunction && isSame(attemptedAnswer, correctAnswer))
            {
                Console.WriteLine("PASS:");
                Console.WriteLine("  Question: " + makeHumanReadable(question));
                Console.WriteLine("  Answer:   " + makeHumanReadable(attemptedAnswer));
            }
            else if (foundFunction)
            {
                Console.WriteLine("FAIL:");
                Console.WriteLine("  Question:        " + makeHumanReadable(question));
                Console.WriteLine("  System's answer: " + makeHumanReadable(attemptedAnswer));
                Console.WriteLine("  Correct answer:  " + makeHumanReadable(correctAnswer));
            }
            else { //foundFunction==false
                Console.WriteLine("FAIL:");
                Console.WriteLine("  Question:        " + makeHumanReadable(question));
                Console.WriteLine("  System's answer: Couldn't determine pattern.");
                Console.WriteLine("  Correct answer:  " + makeHumanReadable(correctAnswer));
            }
            Console.WriteLine();
        }

        private static string makeHumanReadable(List<int> list) {
            List<int?> listWithNullables = list.ConvertAll<int?>(delegate(int i) { return i; });
            return makeHumanReadable(listWithNullables);
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

        private static bool isSame(List<int> list1, List<int> list2)
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



        static Func<int, List<int>, int> identityFunction = new Func<int, List<int>, int>((value,list) => value);
      
        static List<Func<int, List<int>, int>> basicFunctions =
                 new List<Func<int, List<int>, int>>{
                     
                    new Func<int, List<int>, int>( 
                        (value, list)=>1
                    ),

                    new Func<int, List<int>, int>(
                        (value, list)=>-1
                    ),

                    //list[i]
                    new Func<int, List<int>, int> ( 
                        delegate(int value, List<int>list) {
                             if (value<0 || value>=list.Count) throw new InvalidOperationException();
                             return list[value];
                        }
                   )
              };

        static List<Func<int, int, int>> basicOperators =
    new List<Func<int, int, int>>{
         new Func<int, int, int>(
            (i, j) =>i+j
        ),
         new Func<int, int, int>(
             (i, j) =>i*j
        )
    };

        private static List<int> fillMissing(List<int?> sequenceWithNulls)
        {
            Func<int, List<int>, int> itemGeneratorFunction = findItemGeneratorFunction(sequenceWithNulls);

            if (itemGeneratorFunction == null) throw new CouldNotFindGeneratorFunctionException();

            return create(itemGeneratorFunction,sequenceWithNulls.Count);
        }

        private static List<int> create(Func<int, List<int>, int> function,int lengthRequested)
        {
            List<int> newSequence = new List<int>();
            for (int i = 0; i < lengthRequested; i++)
            {
                int item = function(i, getIdentitySequence(lengthRequested));
                newSequence.Add(item);
            }
            return newSequence;
        }

        private static List<int> getIdentitySequence(int lengthRequested)
        {
            List<int> identitySequence = new List<int>();
            for (int i = 0; i < lengthRequested; i++)
            {
                identitySequence.Add(i);
            }
            return identitySequence;
        }
        
        private static Func<int, List<int>, int> findItemGeneratorFunction(List<int?> questionSequence){

            //Iterative breadth-first search
            for (int i = 0; i < MAX_DEPTH; i++)
            {
                Func<int,List<int>,int> itemGeneratorFunction = findItemGeneratorFunction(getIdentitySequence(questionSequence.Count), questionSequence, i);
                if (itemGeneratorFunction != null) return itemGeneratorFunction;
            }
            return null;
        }

        private static Func<int, List<int>, int> findItemGeneratorFunction(List<int> currentSequence, List<int?> questionSequence, int maxDepth)
        {
            //Console.WriteLine(maxDepth);
            //Console.WriteLine(makeHumanReadable(currentSequence));
            //Console.WriteLine();

            if (isASameBWhenIgnoreBNull(currentSequence, questionSequence))
            {
                return identityFunction;
            }

            foreach (Func<int, List<int>, int> basicFunction in basicFunctions)
            {
                try
                {
                    List<int> mappedSequence = map(basicFunction, currentSequence);
                    if (isASameBWhenIgnoreBNull(mappedSequence, questionSequence))
                    {
                        return basicFunction;
                    }
                }catch(InvalidOperationException){
                    //Bad function, ignore it.
                }
            }

            if (maxDepth<1) return null;

            //Basic (index, sequence) functions
            foreach (Func<int, List<int>, int> basicFunction in basicFunctions)
            {
                try
                {
                    List<int> mappedCurrentSequence = map(basicFunction, currentSequence);
                    Func<int, List<int>, int> subFunction = findItemGeneratorFunction(mappedCurrentSequence, questionSequence, maxDepth - 1);


                    if (subFunction != null)
                    {
                        Func<int, List<int>, int> newFunction =
                            (value, sequence) =>
                            {
                                return subFunction(
                                    basicFunction(
                                        value,
                                        sequence
                                        ),
                                    sequence
                                );
                            };
                        return newFunction;
                    }
                }
                catch (InvalidOperationException)
                {
                    //Function is bad, ignore it.
                }
            }
            
            //Basic (i, j) operators
            foreach (Func<int, int, int> basicOperator in basicOperators)
            {
                foreach (Func<int, List<int>, int> basicFunction1 in basicFunctions)
                {
                    foreach (Func<int, List<int>, int> basicFunction2 in basicFunctions)
                    {
                        try
                {
                   //Not sure
                    List<int> mapBasicFunc1 = map(basicFunction1, currentSequence);
                    List<int> mapBasicFunc2 = map(basicFunction2, currentSequence);

                    List<int> mapBasicOp = map(basicOperator, mapBasicFunc1, mapBasicFunc2);

                    Func<int, List<int>, int> subFunction = findItemGeneratorFunction(mapBasicOp, questionSequence, maxDepth - 1);

                    if (subFunction != null)
                    {
                        Func<int, List<int>, int> newFunction =
                            (value, sequence) =>
                            {
                                return
                                    subFunction(
                                        basicOperator(
                                            basicFunction1(
                                                value,
                                                sequence
                                            ),
                                            basicFunction2(
                                                value,
                                                sequence
                                            )
                                           ),
                                            sequence
                                        );
                                    
                            };
                       
                        return newFunction;
                    }
                }
                catch (InvalidOperationException)
                {
                    //Function is bad, ignore it.
                }
                    }
                }
            }
        
            return null;
        }



        private static bool isASameBWhenIgnoreBNull(List<int> a, List<int?> b)
        {
            if (a.Count != b.Count) return false;

            for (int i = 0; i < a.Count; i++)
            {
                if (b[i] != null)
                {
                    if (a[i] != b[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static List<int> map(Func<int, List<int>, int> function, List<int> list)
        {
            List<int> newList = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                newList.Add(function(list[i], list)); //This is right
            }
            return newList;
        }

        private static List<int> map(Func<int, int, int> op, List<int> list1, List<int> list2)
        {
            if (list1.Count != list2.Count) throw new Exception("This shouldn't happen!");
            
            List<int> newList = new List<int>();
            for (int i = 0; i < list1.Count; i++)
            {
                newList.Add(op(list1[i], list2[i])); 
            }
            return newList;
        }

    }
}
/* NOT YET

*/
/*
private static bool isSameIgnoreNull(List<int?> list1, List<int?> list2)
{
    if (list1.Count != list2.Count) return false;

    for (int i = 0; i < list1.Count; i++)
    {
        if (list1[i] != list2[i] && list1[i] != null && list2[i] != null)
        {
            return false;
        }
    }
    return true;
}
*/