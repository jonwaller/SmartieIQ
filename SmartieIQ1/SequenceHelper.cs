using System;
using System.Collections.Generic;

namespace SmartieIQ
{
	static class SequenceHelper
	{

		public class CouldNotFindGeneratorFunctionException : System.Exception
		{
			public CouldNotFindGeneratorFunctionException() : base() { }
		}  

		public static bool isSame(List<int?> list1, List<int?> list2)
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

		public static bool isSame(List<int> list1, List<int> list2)
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



		private static Func<int, int> identityFunction = new Func<int, int>((i) => i);

		public static List<Func<int, int>> basic1ArgFuncs =

			new List<Func<int, int>>{
				new Func<int, int>((i) =>i), //important
				new Func<int, int>((i) =>-1),
				new Func<int, int>((i) =>1),
				new Func<int, int>((i) =>i%2)
		};

		static double goldenRatio = (1 + Math.Sqrt(5.0)) / 2.0;

		public static List<Func<int, int, int>> basic2ArgFuncs =
	new List<Func<int, int, int>>{
		new Func<int, int, int>(
            (i, j) =>i+j
        ),
         new Func<int, int, int>(
             (i, j) =>i*j
        ),
		new Func<int, int, int>(
             (i, j) =>(int)(Math.Pow(i,j))
        )/*,
        		new Func<int, int, int>(
             (i, j) =>(int)(Math.Pow(i*(1-goldenRatio),j))
        ),
				new Func<int, int, int>(
             (i, j) =>(int)(Math.Pow(i*goldenRatio,j))
        )*/
    };
		public  static List<int> fillMissing(List<int?> sequenceWithNulls)
		{
			Func<int, int> itemGeneratorFunction = findItemGeneratorFunction(sequenceWithNulls);

			if (itemGeneratorFunction == null) throw new CouldNotFindGeneratorFunctionException();

			return create(itemGeneratorFunction, sequenceWithNulls.Count);
		}

		public static List<int> create(Func<int, int> function, int lengthRequested)
		{
			return map(function, getIdentitySequence(lengthRequested));
		}

		public static List<int> getIdentitySequence(int lengthRequested)
		{
			List<int> identitySequence = new List<int>();
			for (int i = 0; i < lengthRequested; i++)
			{
				identitySequence.Add(i);
			}
			return identitySequence;
		}


		public static bool createMakesMatchingSequence(Func<int, int> function, List<int?> questionSequence)
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



	


		public  static Func<int, int> findItemGeneratorFunction(List<int?> questionSequence)
		{
			//foreach (Func<int, int> sf15 in basic1ArgFuncs)
			//{
			foreach (Func<int, int, int> sop7 in basic2ArgFuncs)
			{
				//foreach (Func<int, int> sf14 in basic1ArgFuncs)
				//{
				foreach (Func<int, int, int> sop6 in basic2ArgFuncs)
				{
					//foreach (Func<int, int> sf13 in basic1ArgFuncs)
					//{
					foreach (Func<int, int, int> sop5 in basic2ArgFuncs)
					{
						foreach (Func<int, int> sf12 in basic1ArgFuncs)
						{
							foreach (Func<int, int> sf11 in basic1ArgFuncs)
							{
								//foreach (Func<int, int> sf10 in basic1ArgFuncs)
								//{
								foreach (Func<int, int, int> sop4 in basic2ArgFuncs)
								{
									foreach (Func<int, int> sf9 in basic1ArgFuncs)
									{
										foreach (Func<int, int> sf8 in basic1ArgFuncs)
										{
											//foreach (Func<int, int> sf7 in basic1ArgFuncs)
											//{
											foreach (Func<int, int, int> sop3 in basic2ArgFuncs)
											{
												//foreach (Func<int, int> sf6 in basic1ArgFuncs)
												//{
												foreach (Func<int, int, int> sop2 in basic2ArgFuncs)
												{
													foreach (Func<int, int> sf5 in basic1ArgFuncs)
													{
														foreach (Func<int, int> sf4 in basic1ArgFuncs)
														{
															//foreach (Func<int, int> sf3 in basic1ArgFuncs)
															//{
															foreach (Func<int, int, int> sop1 in basic2ArgFuncs)
															{
																foreach (Func<int, int> sf2 in basic1ArgFuncs)
																{
																	foreach (Func<int, int> sf1 in basic1ArgFuncs)
																	{
																		//This functions need to be copied, or the previous items in the list will be "updated" to the new (wrong) function
																		Func<int, int, int> op1 = new Func<int, int, int>(sop1);
																		Func<int, int, int> op2 = new Func<int, int, int>(sop2);
																		Func<int, int, int> op3 = new Func<int, int, int>(sop3);
																		Func<int, int, int> op4 = new Func<int, int, int>(sop4);
																		Func<int, int, int> op5 = new Func<int, int, int>(sop5);
																		Func<int, int, int> op6 = new Func<int, int, int>(sop6);
																		Func<int, int, int> op7 = new Func<int, int, int>(sop7);

																		Func<int, int> f1 = new Func<int, int>(sf1);
																		Func<int, int> f2 = new Func<int, int>(sf2);

																		//Func<int, int> f3 = new Func<int, int>(sf3);
																		Func<int, int> f4 = new Func<int, int>(sf4);

																		Func<int, int> f5 = new Func<int, int>(sf5);
																		//Func<int, int> f6 = new Func<int, int>(sf6);

																		//Func<int, int> f7 = new Func<int, int>(sf7);
																		Func<int, int> f8 = new Func<int, int>(sf8);

																		Func<int, int> f9 = new Func<int, int>(sf9);
																		//Func<int, int> f10 = new Func<int, int>(sf10);

																		Func<int, int> f11 = new Func<int, int>(sf11);
																		Func<int, int> f12 = new Func<int, int>(sf12);

																		//Func<int, int> f13 = new Func<int, int>(sf13);
																		//Func<int, int> f14 = new Func<int, int>(sf14);

																		//Func<int, int> f15 = new Func<int, int>(sf15);

																		Func<int, int> function = new Func<int, int>(
																		(value) =>
																		(
																			op7(
																				(
																					op3(
																						(
																							op1(
																								f1(value), f2(value)
																							)
																						),
																						(
																							op2(
																								f4(value), f5(value)
																							)
																						)
																					)
																				),
																				(
																					op6(
																						(
																							op4(
																								f8(value), f9(value)
																							)
																						),
																						(
																							op5(
																								f11(value), f12(value)
																							)
																						)
																					)
																				)
																			)
																		)
																		);

																		//Console.WriteLine(makeHumanReadable(create(function, 5)));

																		if (createMakesMatchingSequence(function, questionSequence))
																		{
																			return function;
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			
			return null;
		}

		private static string makeHumanReadable(List<int> list)
		{
			List<int?> listWithNullables = list.ConvertAll<int?>(delegate(int i) { return i; });
			return makeHumanReadable(listWithNullables);
		}

		private static string makeHumanReadable(List<int?> list)
		{
			String outStr = "";

			outStr = "{";

			foreach (int? item in list)
			{
				string itemStr = (item == null) ? "NULL" : item.ToString();
				string extraSpace = (item != null && item >= 0) ? " " : "";
				outStr = outStr + extraSpace + itemStr + ", ";
			}

			if (outStr.Length > 1)
			{
				outStr = outStr.Substring(0, outStr.Length - 2); // To cut of the last ", "
			}

			outStr = outStr + "}";

			return outStr;
		}

		public  static List<int> map(Func<int, int> function, List<int> list)
		{
			List<int> newList = new List<int>();
			for (int i = 0; i < list.Count; i++)
			{
				newList.Add(function(list[i])); //This is right
			}
			return newList;
		}

		public static List<int> map(Func<int, int, int> op, List<int> list1, List<int> list2)
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
