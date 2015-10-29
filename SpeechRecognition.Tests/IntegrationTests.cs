using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SpeechRecognition.Tests
{
    [TestFixture]
    public class IntegrationTests
    {

        public SoundTransform RunTaskForNSec(int seconds)
        {
            var soundTransform = new SoundTransform();
            var taskFactory = new TaskFactory();
            var startNew = taskFactory.StartNew(() => soundTransform.ListenForCommands(this, new EventArgs()));

            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            return soundTransform;
        }

        [Test]
        public void TestingProblem()
        {
            for (int i = 1; i < 11; i++)
            {
                var soundTransform = RunTaskForNSec(i);
                var resultList = soundTransform.ListOfFfTedSamples;
                var complexses = resultList.ToList();
                Console.WriteLine(complexses.Count);
            }
            //var resultList = RunTaskForNSec(1).ListOfFfTedSamples;
            //var complexses = resultList.ToList();
            //Console.WriteLine(complexses.Count);
            //var resultList1 = RunTaskForNSec(2).ListOfFfTedSamples;
            //var complexses1 = resultList1.ToList();
            //Console.WriteLine(complexses1.Count);
            //var resultList2 = RunTaskForNSec(3).ListOfFfTedSamples;
            //var complexses2 = resultList2.ToList();
            //Console.WriteLine(complexses2.Count);
            //Assert.That(complexses.Count, Is.EqualTo(9));
        }
    }
}