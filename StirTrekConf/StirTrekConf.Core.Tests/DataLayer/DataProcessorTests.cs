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
            const string jsonData = @"{""TimeSlots"":[{""Id"":8,""StartTime"":""8:00 AM"",""EndTime"":""9:00 AM""}],
                                ""Speakers"":[{""Id"":65,""Name"":""Peter Parker"",""Bio"":""Peter is a superhero."",""ImageUrl"":""image1""},],
                                ""Sessions"":[{""Id"":74,""Name"":""Session Name"",""Abstract"":""Session Abstract"",""SpeakerIds"":[65],""TimeSlotId"":8,""TrackId"":30,""Tags"":[]}],
                                ""Sponsors"":[{""Id"":16,""Name"":""Sponsor Name"",""Description"":""Sponsor Description"",""LogoUrl"":""image2"",""URL"":""url Address"",""SponsorType"":""Inactive""}]}";

            var processor = new DataProcessor();

            var result = processor.DescerializeJsonFeed(jsonData);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.TimeSlots.Count, Is.EqualTo(1));
            Assert.That(result.Speakers.Count, Is.EqualTo(1));
            Assert.That(result.Sessions.Count, Is.EqualTo(1));
            Assert.That(result.Sponsors.Count, Is.EqualTo(1));

            Assert.That(result.TimeSlots[0].Id, Is.EqualTo(8));
            Assert.That(result.Speakers[0].Name, Is.EqualTo("Peter Parker"));
            Assert.That(result.Sessions[0].Id, Is.EqualTo(74));
            Assert.That(result.Sponsors[0].Description, Is.EqualTo("Sponsor Description"));
        }
    }
}
