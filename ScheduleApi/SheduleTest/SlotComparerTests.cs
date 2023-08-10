using AutoFixture;
using ScheduleApi.Infrastructure;
using ScheduleApi.Infrastructure.Entitys;
using Xunit;

namespace SheduleTest
{
    public class SlotComparerTests
    {
        [Fact]
        public void Equals_ShouldReturn_True()
        {
            Fixture fixture = new Fixture();

            var firstSlot = fixture.Create<Slot>();
            var secondSlot = fixture.Create<Slot>();

            var comparer = new SlotComparer();

            var result = comparer.Equals(firstSlot, firstSlot);

            Assert.True(result);
        }

        [Fact]
        public void Equals_ShouldReturn_False()
        {
            Fixture fixture = new Fixture();

            var firstSlot = fixture.Create<Slot>();
            var secondSlot = fixture.Create<Slot>();

            var comparer = new SlotComparer();

            var result = comparer.Equals(firstSlot, secondSlot);

            Assert.False(result);
        }

        [Fact]
        public void GetHashCode_ShouldReturn_True()
        {
            Fixture fixture = new Fixture();

            var slot = fixture.Create<Slot>();

            var slotComparer = new SlotComparer();

            var result = slotComparer.GetHashCode(slot);

            Assert.NotNull(result);
        }
    }
}
