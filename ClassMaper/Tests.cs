using NUnit.Framework;

namespace ClassMaper
{
    [TestFixture]
    class Tests
    {
        [Test]
        public void TestMappingGenerator()
        {
            var mapGenerator = new MappingGenerator();
            var mapper = mapGenerator.Generate<Foo, Bar>();

            var foo = new Foo {Id = 1, Name = "test"};
            var res = mapper.Map(foo);

            Assert.NotNull(res);
            Assert.AreEqual(foo.Id, res.Id);
            Assert.AreEqual(foo.Name, res.Name);
        }
    }
}
