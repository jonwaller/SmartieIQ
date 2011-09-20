using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace SmartieIQ
{
	[TestFixture]
	public class TestSequenceHelper
	{

		private static List<int?> a1 = new List<int?> { 1, 2, 3, null };
		private static List<int?> b1 = new List<int?> { 1, 2, 3, null, 5 };
		private static List<int?> c1 = new List<int?> { 2, 4, 6, null, 10 };

		private static List<int> a2 = new List<int> { 1, 2, 3, 4 };
		private static List<int> b2 = new List<int> { 1, 2, 3, 4, 5 };
		private static List<int> c2 = new List<int> { 2, 4, 6, 8, 10 };

		[Test]
		public static void isSame()
		{

			//int?
			Assert.True(SequenceHelper.isSame(a1, a1));
			Assert.True(SequenceHelper.isSame(b1, b1));
			Assert.True(SequenceHelper.isSame(c1, c1));

			Assert.False(SequenceHelper.isSame(a1, b1));
			Assert.False(SequenceHelper.isSame(a1, c1));
			Assert.False(SequenceHelper.isSame(b1, a1));
			Assert.False(SequenceHelper.isSame(b1, c1));
			Assert.False(SequenceHelper.isSame(c1, a1));
			Assert.False(SequenceHelper.isSame(c1, b1));


			//int
			Assert.True(SequenceHelper.isSame(a2, a2));
			Assert.True(SequenceHelper.isSame(b2, b2));
			Assert.True(SequenceHelper.isSame(c2, c2));

			Assert.False(SequenceHelper.isSame(a2, b2));
			Assert.False(SequenceHelper.isSame(a2, c2));
			Assert.False(SequenceHelper.isSame(b2, a2));
			Assert.False(SequenceHelper.isSame(b2, c2));
			Assert.False(SequenceHelper.isSame(c2, a2));
			Assert.False(SequenceHelper.isSame(c2, b2));
		}

		[Test]
		public static void fillMissing()
		{
			Assert.True(SequenceHelper.isSame(a2, SequenceHelper.fillMissing(a1)));
			Assert.True(SequenceHelper.isSame(b2, SequenceHelper.fillMissing(b1)));
			Assert.True(SequenceHelper.isSame(c2, SequenceHelper.fillMissing(c1)));

			Assert.False(SequenceHelper.isSame(a2, SequenceHelper.fillMissing(b1)));
			Assert.False(SequenceHelper.isSame(b2, SequenceHelper.fillMissing(c1)));
			Assert.False(SequenceHelper.isSame(c2, SequenceHelper.fillMissing(a1)));
		}

		[Test]
		public static void create()
		{

			Func<int, int> identityPlus1 = (value) => value + 1;
			Func<int, int> identityPlus1Times2 = (value) => (value + 1) * 2;

			Assert.True(SequenceHelper.isSame(a2, SequenceHelper.create(identityPlus1, 4)));
			Assert.True(SequenceHelper.isSame(b2, SequenceHelper.create(identityPlus1, 5)));
			Assert.True(SequenceHelper.isSame(c2, SequenceHelper.create(identityPlus1Times2, 5)));

			Assert.False(SequenceHelper.isSame(c2, SequenceHelper.create(identityPlus1, 4)));
			Assert.False(SequenceHelper.isSame(c2, SequenceHelper.create(identityPlus1, 5)));
			Assert.False(SequenceHelper.isSame(a2, SequenceHelper.create(identityPlus1Times2, 5)));
			Assert.False(SequenceHelper.isSame(b2, SequenceHelper.create(identityPlus1Times2, 5)));
		}

		[Test]
		public static void getIdentitySequence()
		{
			Assert.True(SequenceHelper.isSame(new List<int> { 0 }, SequenceHelper.getIdentitySequence(1)));
			Assert.True(SequenceHelper.isSame(new List<int> { 0, 1 }, SequenceHelper.getIdentitySequence(2)));
			Assert.True(SequenceHelper.isSame(new List<int> { 0, 1, 2 }, SequenceHelper.getIdentitySequence(3)));
			Assert.True(SequenceHelper.isSame(new List<int> { 0, 1, 2, 3 }, SequenceHelper.getIdentitySequence(4)));
			Assert.True(SequenceHelper.isSame(new List<int> { 0, 1, 2, 3, 4 }, SequenceHelper.getIdentitySequence(5)));

			Assert.False(SequenceHelper.isSame(new List<int> { 0, 1, 1000, 3, 4 }, SequenceHelper.getIdentitySequence(5)));
		}

		[Test]
		public static void findItemGeneratorFunction()
		{
			Assert.True(SequenceHelper.isSame(a2, SequenceHelper.create(SequenceHelper.findItemGeneratorFunction(a1), a2.Count)));
			Assert.True(SequenceHelper.isSame(b2, SequenceHelper.create(SequenceHelper.findItemGeneratorFunction(b1), b2.Count)));
			Assert.True(SequenceHelper.isSame(c2, SequenceHelper.create(SequenceHelper.findItemGeneratorFunction(c1), c2.Count)));
		}

		[Test]
		public static void createMakesMatchingSequence()
		{
			Func<int, int> identityPlus1 = (value) => value + 1;
			Func<int, int> identityPlus1Times2 = (value) => (value + 1) * 2;

			Assert.True(SequenceHelper.createMakesMatchingSequence(identityPlus1, a1));
			Assert.True(SequenceHelper.createMakesMatchingSequence(identityPlus1, b1));
			Assert.True(SequenceHelper.createMakesMatchingSequence(identityPlus1Times2, c1));

			Assert.False(SequenceHelper.createMakesMatchingSequence(identityPlus1Times2, b1));
			Assert.False(SequenceHelper.createMakesMatchingSequence(identityPlus1, c1));

		}


		/*
		[Test]
		public static void getFunctionTree()
		{
			//General function math
			Func<int, int, int> add = new Func<int, int, int>((i, j) => i + j);
			Func<int, int> one = new Func<int, int>((i) => 1);
			Func<int, int> identity = new Func<int, int>((i) => i);

			Func<int, int> twoiFunc = new Func<int, int>((value) => add(value, value));
			Func<int, int> twoFunc = new Func<int, int>((value) => add(one(value), one(value)));
			Func<int, int> addOneFunc = new Func<int, int>((value) => add(one(value), identity(value)));

			Assert.True(SequenceHelper.createMakesMatchingSequence(twoiFunc, new List<int?> { 0, 2, 4, 6 }));
			Assert.True(SequenceHelper.createMakesMatchingSequence(twoFunc, new List<int?> { 2, 2, 2, 2 }));
			Assert.True(SequenceHelper.createMakesMatchingSequence(addOneFunc, new List<int?> { 1, 2, 3, 4 }));
			//End - General function math
			

			
			
			List<Func<int, int>> functionTree = SequenceHelper.getFunctionTree(0);
			Assert.AreEqual(1, functionTree.Count); //Identity only
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[0], new List<int?> { 0, 1, 2, 3 }));
			
			functionTree = SequenceHelper.getFunctionTree(1);
			Assert.AreEqual(SequenceHelper.basic1ArgFuncs.Count, functionTree.Count);
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[0], new List<int?>{0,1,2,3}));
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[1], new List<int?> { 0, 0, 0, 0 }));
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[2], new List<int?> { 1,1,1,1}));
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[3], new List<int?> { -1,-1,-1,-1 }));


			functionTree = SequenceHelper.getFunctionTree(2);
			//Assert.AreEqual(SequenceHelper.basic1ArgFuncs.Count, functionTree.Count);
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[0], new List<int?> { 0, 1, 2, 3 }));
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[1], new List<int?> { 0, 0, 0, 0 }));
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[2], new List<int?> { 1, 1, 1, 1 }));
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[3], new List<int?> { -1, -1, -1, -1 }));
			

			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[4], new List<int?> { 0, 2, 4, 6 }));//f1 f1 +
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[5], new List<int?> { 0, 1, 2, 3 }));//f2 f1 +
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[6], new List<int?> { 1, 2, 3, 4 }));//f3 f1 +
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[7], new List<int?> { -1, 0, 1, 2 }));//f4 f1 + 

			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[8], new List<int?> { 0, 1, 2, 3 }));//f1 f2 +
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[9], new List<int?> { 0, 0, 0, 0 }));//f2 f2 +
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[10], new List<int?> { 1, 1, 1, 1 }));//f3 f2 +
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[11], new List<int?> { -1, -1, -1, -1 }));//f4 f2 + 

			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[12], new List<int?> { 1, 2, 3, 4 }));//f1 f3 +
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[13], new List<int?> { 1,1, 1, 1 }));//f2 f3 +
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[14], new List<int?> { 2, 2, 2, 2 }));//f3 f3 +
			Assert.True(SequenceHelper.createMakesMatchingSequence(functionTree[15], new List<int?> { 0, 0, 0, 0 }));//f4 f3 + 

	}
*/


		[Test]
		public static void map()
		{

			Func<int, int> identityPlus1 = (value) => value + 1;
			Func<int, int> identityPlus1Times2 = (value) => (value + 1) * 2;

			Assert.True(SequenceHelper.isSame(a2, SequenceHelper.map(identityPlus1, SequenceHelper.getIdentitySequence(a2.Count))));
			Assert.True(SequenceHelper.isSame(b2, SequenceHelper.map(identityPlus1, SequenceHelper.getIdentitySequence(b2.Count))));
			Assert.True(SequenceHelper.isSame(c2, SequenceHelper.map(identityPlus1Times2, SequenceHelper.getIdentitySequence(c2.Count))));

			Assert.False(SequenceHelper.isSame(a2, SequenceHelper.map(identityPlus1Times2, SequenceHelper.getIdentitySequence(a2.Count))));
			Assert.False(SequenceHelper.isSame(b2, SequenceHelper.map(identityPlus1Times2, SequenceHelper.getIdentitySequence(b2.Count))));
			Assert.False(SequenceHelper.isSame(c2, SequenceHelper.map(identityPlus1, SequenceHelper.getIdentitySequence(c2.Count))));


			Func<int, int, int> add = (value1, value2) => value1 + value2;
			Func<int, int, int> mult = (value1, value2) => value1 * value2;

			List<int> a3 = new List<int> { 2, 2, 2, 2 };
			List<int> b3 = new List<int> { 1, 2, 3, 4 };

			List<int> aAddB = new List<int> { 3, 4, 5, 6 };
			List<int> aMultB = new List<int> { 2, 4, 6, 8 };

			Assert.True(SequenceHelper.isSame(aAddB, SequenceHelper.map(add, a3, b3)));
			Assert.True(SequenceHelper.isSame(aMultB, SequenceHelper.map(mult, a3, b3)));

			Assert.False(SequenceHelper.isSame(aMultB, SequenceHelper.map(add, a3, b3)));
			Assert.False(SequenceHelper.isSame(aAddB, SequenceHelper.map(mult, a3, b3)));

		}
	}
}