using NUnit.Framework;

// Make sure the test framework's working.
namespace GameTests {
	internal class TestFramework {
		[Test]
		public void testPass() {
			Assert.Pass();
		}

		[Test]
		[Ignore]
		public void testIgnore() {
			Assert.Fail();
		}
	}
}