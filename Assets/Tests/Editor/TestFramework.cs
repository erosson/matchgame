using NUnit.Framework;

// Make sure the test framework's working.
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