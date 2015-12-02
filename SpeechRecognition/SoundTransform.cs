using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using Accord.Audio;
using Accord.Audio.Windows;
using Accord.DirectSound;

namespace SpeechRecognition
{
    public class SoundTransform
    {
        private IAudioSource source;
        private IWindow window;
        public IList<double[]> ListOfPowerSpectrum;
        public IList<int> soundsDetectedIndexes;
        public int soundsInAtakujCommandAfterCalibration;
        public int soundsInBronCommandAfterCalibration;
        public int soundsInIdzCommandAfterCalibration;

        public void StartListeningForCommands(int seconds)
        {
            // var taskFactory = new TaskFactory();
            // taskFactory.StartNew(() => ListenForCommands(this, new EventArgs()));
            Console.WriteLine("Start saying commands");
            ListenForCommands(this, new EventArgs(), false);

            Thread.Sleep(TimeSpan.FromSeconds(15));
            Console.WriteLine("Stop");
            source.Stop();
            DetectSounds();
        }

        public void StartCalibration()
        {
            Console.WriteLine("Calibration for atakuj");
            
            ListenForCommands(this, new EventArgs(), true);
            AdjustCommand(ref soundsInAtakujCommandAfterCalibration);

            Console.WriteLine("Calibration for bron");
            
            ListenForCommands(this, new EventArgs(), true);
            AdjustCommand(ref soundsInBronCommandAfterCalibration);

            Console.WriteLine("Calibration for idz");

            ListenForCommands(this, new EventArgs(), true);
            AdjustCommand(ref soundsInIdzCommandAfterCalibration);

            //  Console.WriteLine("gloski w atakuj {0}", soundsInAtakujCommandAfterCalibration);
            // Console.WriteLine("gloski w bron {0}", soundsInBronCommandAfterCalibration);
            //  Console.WriteLine("gloski w idz {0}", soundsInIdzCommandAfterCalibration);
        }

        public void AdjustCommand(ref int soundsInCommand)
        {
            IList<int> soundsDetectedInWords = FindSounds();

            int sum = 0;

            foreach (int sound in soundsDetectedInWords)
            {
                sum += sound;
            }

            soundsInCommand = sum / soundsDetectedInWords.Count;
            // Console.WriteLine("gloski w komenedzie {0}", soundsInCommand);
        }

        public void DetectSounds()
        {
            IList<int> soundsDetectedInWords = FindSounds();

            //Console.WriteLine(soundsDetectedInWords.Count);
            //  Console.WriteLine("gloski w atakuj {0}", soundsInAtakujCommandAfterCalibration);
            // Console.WriteLine("gloski w bron {0}", soundsInBronCommandAfterCalibration);
            // Console.WriteLine("gloski w idz {0}", soundsInIdzCommandAfterCalibration);

            for (int i = 0; i < soundsDetectedInWords.Count; i++)
            {
                int middleOfIdzBron = (soundsInBronCommandAfterCalibration + soundsInIdzCommandAfterCalibration) / 2;
                int middleOfBronAtakuj = (soundsInAtakujCommandAfterCalibration + soundsInBronCommandAfterCalibration) / 2;
                if (soundsDetectedInWords[i] <= middleOfIdzBron)
                {
                    //  Console.WriteLine(soundsDetectedInWords[i]);
                    Console.WriteLine("Word nr {0} is Idz", i);
                }
                else if (soundsDetectedInWords[i] > middleOfIdzBron && soundsDetectedInWords[i] <= middleOfBronAtakuj)
                {
                    //  Console.WriteLine(soundsDetectedInWords[i]);
                    Console.WriteLine("Word nr {0} is Bron", i);
                }
                else if (soundsDetectedInWords[i] > middleOfBronAtakuj)
                {
                    // Console.WriteLine(soundsDetectedInWords[i]);
                    Console.WriteLine("Word nr {0} is Atakuj", i);
                }
            }



            // Sort list and the word with the least amount of sounds is 'Idz', next 'Obrona' and last 'Atak'
            /*  IList<int> sortList = soundsDetectedInWords.OrderBy(v => v).ToList();
              Dictionary<int, string> recognizedWords = new Dictionary<int, string>();
              recognizedWords.Add(sortList[0], "Idz");
              recognizedWords.Add(sortList[1], "Bron");
              recognizedWords.Add(sortList[2], "Atakuj");

              Console.WriteLine("Word nr 1 is {0}", recognizedWords[soundsDetectedInWords[0]]);
              Console.WriteLine("Word nr 2 is {0}", recognizedWords[soundsDetectedInWords[1]]);
              Console.WriteLine("Word nr 3 is {0}", recognizedWords[soundsDetectedInWords[2]]);*/
        }

        public IList<int> FindSounds()
        {
            IList<int> soundsDetectedInWords = new List<int>();

            int soundsDetectedInWord = 0;

            for (int i = 0; i < soundsDetectedIndexes.Count; i++)
            {
                int soundsDetectedInWave = 0;
                foreach (double value in ListOfPowerSpectrum[soundsDetectedIndexes[i]])
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
                    // Console.WriteLine(soundsDetectedInWord);
                    // It should have at least 10 sounds to be treated as word 
                    if (soundsDetectedInWord > 150) soundsDetectedInWords.Add(soundsDetectedInWord);
                    soundsDetectedInWord = 0;

                }
            }

            return soundsDetectedInWords;
        }

        public void ListenForCommands(object sender, EventArgs e, bool forCalibration)
        {
            ListOfPowerSpectrum = new List<double[]>();
            soundsDetectedIndexes = new List<int>();

            var audioDevices = new AudioDeviceCollection(AudioDeviceCategory.Capture).First();

            source = new AudioCaptureDevice(audioDevices)
            {
                // Capture at 22050 Hz
                DesiredFrameSize = 2048,
                SampleRate = 22050
            };

            source.NewFrame += source_NewFrame;
            source.AudioSourceError += source_AudioSourceError;



            // Start it!
            Console.WriteLine("Start");
            source.Start();

            if (forCalibration)
            {
                Thread.Sleep(TimeSpan.FromSeconds(10));
                Console.WriteLine("Stop");
                source.Stop();
            }
        }


        void source_AudioSourceError(object sender, AudioSourceErrorEventArgs e)
        {
            throw new Exception(e.Description);
        }


        void source_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // We can start by converting the audio frame to a complex signal
            var signal = ComplexSignal.FromSignal(eventArgs.Signal);

            // If its needed,
            if (window != null)
            {
                // Apply the chosen audio window
                signal = window.Apply(signal, 0);
            }

            // Transform to the complex domain
            signal.ForwardFourierTransform();

            // Now we can get the power spectrum output and its
            // related frequency vector to plot our spectrometer.
            Complex[] channel = signal.GetChannel(0);


            double[] power = Accord.Audio.Tools.GetPowerSpectrum(channel);


            ListOfPowerSpectrum.Add(power);

            foreach (double value in power)
            {
                if (value != 0.0 && value > 1.0E-10)
                {
                    //Console.WriteLine("Index to {0}", ListOfPowerSpectrum.IndexOf(power));
                    soundsDetectedIndexes.Add(ListOfPowerSpectrum.IndexOf(power));
                    break;
                }
            }
        }
    }
}