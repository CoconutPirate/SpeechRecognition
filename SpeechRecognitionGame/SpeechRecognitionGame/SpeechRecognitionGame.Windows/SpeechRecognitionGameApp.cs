using SiliconStudio.Paradox.Engine;

namespace SpeechRecognitionGame
{
    class SpeechRecognitionGameApp
    {
        static void Main(string[] args)
        {
            using (var game = new Game())
            {
                game.Run();
            }
        }
    }
}
