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
            var soundTransform = RunTaskForNSec(10);
            IList<double[]> complexses= soundTransform.ListOfPowerSpectrum;

            IList<int> soundsDetectedInWords = new List<int>();
            IList<int> soundsDetectedIndexes = soundTransform.soundsDetectedIndexes;

            int soundsDetectedInWord = 0;
              
            for (int i = 0; i < soundsDetectedIndexes.Count; i++)
            {
                int soundsDetectedInWave = 0;
                foreach (double value in complexses[soundsDetectedIndexes[i]])
                {
                    if (value != 0.0 && value > 1.0E-10)
                    {
                        soundsDetectedInWave++;
                    }
                }

                soundsDetectedInWord += soundsDetectedInWave;

                // Detect next word when reach the end of loop or following index value is not next to previous one   
                if ((i + 1) == soundsDetectedIndexes.Count || soundsDetectedIndexes[i + 1] != soundsDetectedIndexes[i] + 1)
                {
                    // It should have at least 10 sounds to be treated as word 
                    if (soundsDetectedInWord > 10) soundsDetectedInWords.Add(soundsDetectedInWord);
                    soundsDetectedInWord = 0;
                }               
            }

            // Sort list and the word with the least amount of sounds is 'Idz', next 'Obrona' and last 'Atak'
            IList<int> sortList = soundsDetectedInWords.OrderBy(v => v).ToList();
            Dictionary<int, string> recognizedWords =  new Dictionary<int, string>();
            recognizedWords.Add(sortList[0], "Idz");
            recognizedWords.Add(sortList[1], "Obrona");
            recognizedWords.Add(sortList[2], "Atak");
        
            Console.WriteLine("Word nr 1 is {0}", recognizedWords[soundsDetectedInWords[0]]);
            Console.WriteLine("Word nr 2 is {0}", recognizedWords[soundsDetectedInWords[1]]);
            Console.WriteLine("Word nr 3 is {0}", recognizedWords[soundsDetectedInWords[2]]);
            
                     
            //var resultList = RunTaskForNSec(1).ListOfPowerSpectrum;
            //var complexses = resultList.ToList();
            //Console.WriteLine(complexses.Count);
            //var resultList1 = RunTaskForNSec(2).ListOfPowerSpectrum;
            //var complexses1 = resultList1.ToList();
            //Console.WriteLine(complexses1.Count);
            //var resultList2 = RunTaskForNSec(3).ListOfPowerSpectrum;
            //var complexses2 = resultList2.ToList();
            //Console.WriteLine(complexses2.Count);
            //Assert.That(complexses.Count, Is.EqualTo(9));
        }
    }
}