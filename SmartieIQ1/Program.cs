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
        /* Real code :) */



        static Func<int?, List<int?>, int?> identityFunction = new Func<int?, List<int?>, int?>((value,list) => value);
      
        static List<Func<int?, List<int?>, int?>> basicFunctions =
                 new List<Func<int?, List<int?>, int?>>{
                                        
                     new Func<int?, List<int?>, int?>( //list[i]
                     (value, list) => 
                         (value==null || value<0 || value>=list.Count) ? 
                            null:list[value.GetValueOrDefault()]
                            //Value will never be null, so treat this as list[value] (It's a type thing)
                    ),
                    
                    new Func<int?, List<int?>, int?>( 
                        (value, list)=>1
                    ),

                    new Func<int?, List<int?>, int?>(
                        (value, list)=>-1
                    ),

                    new Func<int?, List<int?>, int?>( 
                        (value, list)=>value+1
                    ),

                    new Func<int?, List<int?>, int?>( 
                        (value, list)=>value*2
                    )
            };
        /* NOT YET
        static List<Func<int?, int?, int?>> basicOperators =
            new List<Func<int?, int?, int?>>{
                 new Func<int?, int?, int?>(
                    (i, j) =>(i==null||j==null)?null:i+j
                ),
                 new Func<int?, int?, int?>(
                     (i, j) =>(i==null||j==null)?null:i*j
                )
         };
        */
        private static List<int?> fillMissing(List<int?> sequenceWithNulls)
        {
            Func<int?, List<int?>, int?> itemGenerator = findItemGeneratorFunction(sequenceWithNulls);

            if (itemGenerator == null) throw new Exception("Could not find item generator function.");

            Console.WriteLine(itemGenerator);

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


        private static Func<int?, List<int?>, int?> findItemGeneratorFunction(List<int?> targetSequence){
            
            List<int?> identitySequence=new List<int?>();
            for(int i=0;i<targetSequence.Count;i++){
                identitySequence.Add(i);
            }

            return findItemGeneratorFunction(identitySequence, targetSequence, 5);
        }

        private static Func<int?, List<int?>, int?> findItemGeneratorFunction(List<int?> currentSequence, List<int?> targetSequence, int maxDepth)
        {
            Console.WriteLine(maxDepth);
            Console.WriteLine(makeHumanReadable(currentSequence));
            Console.WriteLine();
            
            if (isSameIgnoreNull(currentSequence,targetSequence))
            {
                return identityFunction;
            }

             foreach (Func<int?, List<int?>, int?> basicFunction in basicFunctions)
            {
                List<int?> mappedSequence = map(basicFunction, currentSequence);
                if (isSameIgnoreNull(mappedSequence,targetSequence))
            {
                return basicFunction;
                }
             }

            if (maxDepth<1) return null;

            foreach (Func<int?, List<int?>, int?> basicFunction in basicFunctions)
            {
                List<int?> mappedCurrentSequence = map(basicFunction, currentSequence);
                Func<int?, List<int?>, int?> subFunction = findItemGeneratorFunction(mappedCurrentSequence, targetSequence, maxDepth - 1);

                if (subFunction != null)
                {
                    Func<int?, List<int?>, int?> newFunction =
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

            return null;
        }

        private static List<int?> map(Func<int?, List<int?>, int?> function, List<int?> list)
        {
            List<int?> newList = new List<int?>();
            for (int i = 0; i < list.Count; i++)
            {
                newList.Add(function(list[i], list)); //This is right
            }
            return newList;
        }
        /*
        private static bool doesThisFunctionGenerateThisSequence(Func<int?, List<int?>, int?> function, List<int?> givenItems)
        {
            for (int i = 0; i < givenItems.Count; i++)
            {
                if (givenItems[i] != null) 
                    //If it's null then this item is deemed ok.
                    //(Since we don't know what it should be)
                {
                    if (function(i,givenItems) != givenItems[i])
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        */
    }
}
