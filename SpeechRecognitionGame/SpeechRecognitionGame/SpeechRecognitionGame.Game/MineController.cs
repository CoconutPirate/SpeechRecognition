using SiliconStudio.Paradox.Engine;

namespace SpeechRecognitionGame
{
    public class MineController : SyncScript
    {
        int animationTimer = 0;

        public override void Update()
        {
            if (Game.IsRunning)
            {
                animationTimer++;
                PlayAnimation();
            }
        }

        void PlayAnimation()
        {
            if (animationTimer == 20)
            {
                Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame++;
                if (Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame == 2)
                {
                    Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame = 0;
                }
                animationTimer = 0;
            }
        }
    }
}
