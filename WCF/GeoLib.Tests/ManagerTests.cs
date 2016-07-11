using System;
using GeoLib.Contracts;
using GeoLib.Data;
using GeoLib.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace GeoLib.Tests
{
	[TestClass]
	public class ManagerTests
	{
		[TestMethod]
		public void test_zip_code_retrieval()
		{
			var mockZipCodeRepository = new Mock<IZipCodeRepository>();
			var zipCode = new ZipCode()
			{
				City = "lincoln park",
				State = new State() { Abbreviation = "NJ"},
				Zip = "07035"
			};

			mockZipCodeRepository.Setup(obj => obj.GetByZip("07035")).Returns(zipCode);

			IGeoService geoService = new GeoManager(mockZipCodeRepository.Object);

			ZipCodeData data = geoService.GetZipInfo("07035");

			Assert.IsTrue(data.City == "lincoln park");
			Assert.IsTrue(data.State == "NJ");
		}
	}
}
