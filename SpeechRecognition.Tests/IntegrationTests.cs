using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SpeechRecognition.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void TestingProblem()
        {
            var soundTransform = new SoundTransform();
            var taskFactory = new TaskFactory();
            var eventHandler = new EventHandler(soundTransform.ListenForCommands);


            Assert.DoesNotThrow(() => eventHandler.Invoke(this, new EventArgs()));


        }
    }
}