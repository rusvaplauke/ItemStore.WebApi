using System.Runtime.CompilerServices;
using ItemStore.WebApi.Models.Entities;
using FluentAssertions;
using FluentAssertions.Execution;

namespace ItemStore.WebApi.Comparer
{
    public static class ItemAssertions
    {
        public static void ShouldBeEqual(this ItemEntity actual, ItemEntity expected, ItemComparer comparer)
        {
            using (new AssertionScope())
                comparer.Compare(actual, expected).Should().Be(0, "because the objects should be equivalent");
        }
    }
}
