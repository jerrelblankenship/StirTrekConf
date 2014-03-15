namespace StirTrekConf.Core.Tests.DataLayer
{
    using Core.DataLayer;
    using NUnit.Framework;

    [TestFixture]
    public class DataProcessorTests
    {
        [Test]
        public void DescerializrJsonFeed_Success()
        {
            var jsonData = @"{""TimeSlots"":[{""Id"":8,""StartTime"":""8:00 AM"",""EndTime"":""9:00 AM""}],""Speakers"":[{""Id"":65,""Name"":""Jerrel  Blankenship"",""Bio"":""Jerrel is a software craftsman."",""ImageUrl"":""image1""},],""Sessions"":[{""Id"":74,""Name"":""Session Name"",""Abstract"":""Session Abstract"",""SpeakerIds"":[65],""TimeSlotId"":8,""TrackId"":30,""Tags"":[]}],""Sponsors"":[{""Id"":16,""Name"":""Sponsor Name"",""Description"":""Sponsor Description"",""LogoUrl"":""image2"",""URL"":""url Address"",""SponsorType"":""Inactive""}]}";
            var processor = new DataProcessor();

            var result = processor.DescerializeJsonFeed(jsonData);

            Assert.That(result, Is.Not.Null);
        }
    }
}
