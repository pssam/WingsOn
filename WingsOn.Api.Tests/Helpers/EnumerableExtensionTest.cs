using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WingsOn.Api.Helpers;

namespace WingsOn.Api.Tests.Helpers
{
    [TestClass]
    public class EnumerableExtensionTest
    {
        [TestMethod]
        public void Distinct_RemovesDuplicateEntities()
        {
            var source = new[] { (Id: 1, "val1"), (Id: 1, "val2") };

            var actual = source.Distinct(x => x.Id);

            actual.Should().BeEquivalentTo((Id: 1, "val1"));
        }
    }
}