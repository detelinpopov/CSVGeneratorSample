using System.Collections.Generic;
using System.Globalization;

using CSVGenerator.Core;

using NUnit.Framework;

namespace CSVGenerator.Tester
{
	[TestFixture]
	public class CSVGeneratorFixture
	{
		[TestCase]
		public void GenerateToCsv_GeneratesValidString()
		{
			// Arrange
			IList<TestModel> testModels = CreateTestModelCollection(5);

			// Act
			string csvContent = testModels.GenerateToCsv(',');

			// Assert
			Assert.IsNotNull(csvContent);
			Assert.IsNotEmpty(csvContent);

			foreach (TestModel model in testModels)
			{
				Assert.IsTrue(csvContent.Contains(model.FirstName));
				Assert.IsTrue(csvContent.Contains(model.LastName));
				Assert.IsTrue(csvContent.Contains(model.Age.ToString(CultureInfo.InvariantCulture)));
			}
		}

		[TestCase]
		public void GenerateToCsv_GeneratesValidHeader()
		{
			// Arrange
			IList<TestModel> testModels = CreateTestModelCollection(5);

			// Act
			string csvContent = testModels.GenerateToCsv(',');

			// Assert		
			TestModel model;
			Assert.IsTrue(csvContent.Contains(nameof(model.FirstName)));
			Assert.IsTrue(csvContent.Contains(nameof(model.LastName)));
			Assert.IsTrue(csvContent.Contains(nameof(model.Age)));
		}

		[TestCase]
		public void GenerateToCsv_DoesNotGeneratedHeader_WhenIncludeHeaderIsFalse()
		{
			// Arrange
			IList<TestModel> testModels = CreateTestModelCollection(5);

			// Act
			string csvContent = testModels.GenerateToCsv(',', false);

			// Assert		
			TestModel model;
			Assert.IsFalse(csvContent.Contains(nameof(model.FirstName)));
			Assert.IsFalse(csvContent.Contains(nameof(model.LastName)));
			Assert.IsFalse(csvContent.Contains(nameof(model.Age)));
		}

		private static TestModel CreateTestModel(int identifier)
		{
			TestModel testModel = new TestModel();
			testModel.FirstName = $"First name {identifier}";
			testModel.LastName = $"Last name {identifier}";
			testModel.Age = identifier + 25;
			return testModel;
		}

		private static IList<TestModel> CreateTestModelCollection(int count)
		{
			List<TestModel> models = new List<TestModel>();
			for (int i = 0; i < count; i++)
			{
				int identifier = i++;
				TestModel model = CreateTestModel(identifier);
				models.Add(model);
			}
			return models;
		}

		private class TestModel
		{
			public int Age { get; set; }

			public string FirstName { get; set; }

			public string LastName { get; set; }
		}
	}
}
