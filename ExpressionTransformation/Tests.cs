using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;

namespace ExpressionTransformation
{
    [TestFixture]
    class Tests
    {
        [Test]
        public void TestIncDec()
        {
            Expression<Func<int, int, int>> exp = (a, b) => (a + 1) * (b - 1) / 13 + (a + 1) + (b - 1);
            var res = exp.Compile().Invoke(2, 3);
            Console.WriteLine($"res: {res}");

            var newExp = ExpressionTransformer.ReplaceIncDec(exp);
            var newRes = newExp.Compile().Invoke(2, 3);
            Console.WriteLine($"newRes: {newRes}");

            Assert.AreEqual(res, newRes);
        }

        [Test]
        public void TestReplacingAllParams()
        {
            Expression<Func<int, int, int>> exp = (a, b) => (a + 1) * (b - 1) / 13 + (a + 1) + (b - 1);
            var res = exp.Compile().Invoke(2, 3);
            Console.WriteLine($"res: {res}");

            var newExp = ExpressionTransformer.ReplaceParams(exp, new Dictionary<string, object>() { ["a"] = 2, ["b"] = 3 });
            var newRes = newExp.Compile().DynamicInvoke(); // a = 2, b = 3.
            Console.WriteLine($"newRes: {newRes}");

            Assert.AreEqual(res, newRes);
        }

        [Test]
        public void TestReplacingOneParam()
        {
            Expression<Func<int, int, int>> exp = (a, b) => (a + 1) * (b - 1) / 13 + (a + 1) + (b - 1);
            var res = exp.Compile().Invoke(2, 3);
            Console.WriteLine($"res: {res}");

            var newExp = ExpressionTransformer.ReplaceParams(exp, new Dictionary<string, object>() { ["a"] = 2 });
            var newRes = newExp.Compile().DynamicInvoke(3); // b = 3.
            Console.WriteLine($"newRes: {newRes}");

            Assert.AreEqual(res, newRes);
        }
    }
}
