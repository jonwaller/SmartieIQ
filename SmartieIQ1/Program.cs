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

        public static readonly int MAX_DEPTH=4;
		public static List<Func<int, int>> FUNCTION_TREE = new List<Func<int,int>>();

        public class CouldNotFindGeneratorFunctionException : System.Exception
        {
             public CouldNotFindGeneratorFunctionException() : base() { }
        }      
        
        /* Console and testing */

        static void Main(string[] args)
        {
			Console.WriteLine("Calculating tree...");
			FUNCTION_TREE = getFunctionTree(MAX_DEPTH);
			Console.WriteLine("Done. (Size=" + FUNCTION_TREE.Count+ ")");
			Console.WriteLine();
			
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
                attemptedAnswer = fillMissing(question);
            }
            catch (CouldNotFindGeneratorFunctionException){
                foundFunction = false;
            }

            if (foundFunction && isSame(attemptedAnswer, correctAnswer))
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



        static Func<int,int> identityFunction = new Func<int,int> ((i)=>i);

       static List<Func<int, int>> basic1ArgFuncs =
            
           new List<Func<int, int>>{
                new Func<int, int>((i) => 0),
				new Func<int, int>((i) => 1),
				new Func<int, int>((i) => -1)
    };

        static List<Func<int, int, int>> basic2ArgFuncs =
    new List<Func<int, int, int>>{
         new Func<int, int, int>(
            (i, j) =>i+j
        ),
         new Func<int, int, int>(
             (i, j) =>i*j
        )
    };
        /*//Need?
                             //list[i]
                            new Func<int, List<int>, int> ( 
                                delegate(int value, List<int>list) {
                                     if (value<0 || value>=list.Count) throw new InvalidOperationException();
                                     return list[value];
                                }
                           )
         */
        private static List<int> fillMissing(List<int?> sequenceWithNulls)
        {
            Func<int, int> itemGeneratorFunction = findItemGeneratorFunction(sequenceWithNulls);

            if (itemGeneratorFunction == null) throw new CouldNotFindGeneratorFunctionException();

            return create(itemGeneratorFunction,sequenceWithNulls.Count);
        }

        private static List<int> create(Func<int, int> function,int lengthRequested)
        {
            return map(function, getIdentitySequence(lengthRequested));
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
        
          private static Func<int, int> findItemGeneratorFunction(List<int?> questionSequence){

			  //Just in case someone is giving me something very easy.
			  if (isASameBWhenIgnoreBNull(getIdentitySequence(questionSequence.Count), questionSequence))
			  {
				  return identityFunction;
			  }

			  
			  foreach (Func<int, int> function in FUNCTION_TREE)
			  {				  
				  if (createMakesMatchingSequence(function, questionSequence))
				  {
					  return function;
				  }
			  }

            return null;
        }

		  private static bool createMakesMatchingSequence(Func<int, int> function, List<int?> questionSequence)
		  {
			  for (int i = 0; i < questionSequence.Count; i++)
			  {
				  if (questionSequence[i] != null)
				  {
					  if (function(i) != questionSequence[i])
					  {
						  return false;
					  }
				  }
			  }
			  return true;
		  }

          private static List<Func<int, int>> getFunctionTree(int currentDepth)
          {
              List<Func<int, int>> functionTree=new List<Func<int,int>>();

			  foreach (Func<int,int> basic1ArgFunc in basic1ArgFuncs){
			   	  functionTree.Add(basic1ArgFunc);                      
			  }

			  if (currentDepth > 0)
			  {
				  List<Func<int, int>> subFunctionTree = getFunctionTree(currentDepth - 1);

				  //Chains
				  foreach (Func<int, int> basic1ArgFunc in basic1ArgFuncs)
				  {
					  foreach (Func<int, int> complex1ArgFunc in subFunctionTree)
					  {
						  functionTree.Add(
							  (value) =>
							  {
								  return basic1ArgFunc(
									  complex1ArgFunc(value)
								  );
							  }
							  );
					  }
				  }


				  //Operators
				  foreach (Func<int, int, int> basic2ArgFunc in basic2ArgFuncs)
				  {
					  foreach (Func<int, int> complex1ArgFuncA in subFunctionTree)
					  {
						  foreach (Func<int, int> complex1ArgFuncB in subFunctionTree)
						  {
							  functionTree.Add(
								  (value) =>
								  {
									  return basic2ArgFunc(
										  complex1ArgFuncA(value),
										  complex1ArgFuncB(value)
									  );
								  }
								  );
						  }
					  }
				  } //Operators

			  } //If (currentDepth > 0)

              return functionTree;
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

        private static List<int> map(Func<int, int> function, List<int> list)
        {
            List<int> newList = new List<int>();
            for (int i = 0; i < list.Count; i++)
            {
                newList.Add(function(list[i])); //This is right
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
