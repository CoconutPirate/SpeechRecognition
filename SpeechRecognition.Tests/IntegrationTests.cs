using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SpeechRecognition.Tests
{
    [TestFixture]
    public class IntegrationTests
    {

        public void RunTask()
        {
            var soundTransform = new SoundTransform();
            var taskFactory = new TaskFactory();
            var startNew = taskFactory.StartNew(() => soundTransform.ListenForCommands(this, new EventArgs()));

            Thread.Sleep(TimeSpan.FromSeconds(10));
        }

        [Test]
        public void TestingProblem()
        {
            var soundTransform = new SoundTransform();
            var taskFactory = new TaskFactory();
            var startNew = taskFactory.StartNew(RunTask);

            startNew.Wait();

        }
    }
}