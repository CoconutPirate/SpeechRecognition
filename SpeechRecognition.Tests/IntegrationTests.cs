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

        [Test]
        public void TestingProblem()
        {
            var soundTransform = new SoundTransform();
            soundTransform.StartCalibration();
            soundTransform.StartListeningForCommands(10);
        }
    }
}