namespace SpeechRecognitionGame
{
    class SpeechRecognitionGameApp
    {
        static void Main(string[] args)
        {
            using (var game = new SRGame())
            {
                game.Run();
            }
        }
    }
}
