using System;
using System.Linq;
using System.Numerics;
using Accord.Audio;
using Accord.Audio.Windows;
using Accord.DirectSound;

namespace SpeechRecognition
{
    public class SoundTransform
    {
        private static IAudioSource source;
        private static IWindow window;

        public void ListenForCommands(object sender, EventArgs e)
        {

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
            source.Start();
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

            //double[] power = Accord.Audio.Tools.GetPowerSpectrum(channel);
            //double[] freqv = Accord.Audio.Tools.GetFrequencyVector(signal.Length, signal.SampleRate);

            //power[0] = 0; // zero DC
            //float[] g = new float[power.Length];
            //for (int i = 0; i < power.Length; i++)
            //    g[i] = (float)power[i];

            //// Adjust the zoom according to the horizontal and vertical scrollbars.
            //chart1.RangeX = new DoubleRange(freqv[0], freqv[freqv.Length - 1] / hScrollBar1.Value);
            //chart1.RangeY = new DoubleRange(0f, Math.Pow(10, -vScrollBar1.Value));

            //chart1.UpdateWaveform("fft", g);
        }
    }
}