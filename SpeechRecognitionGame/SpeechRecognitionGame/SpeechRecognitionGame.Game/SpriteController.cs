using SiliconStudio.Paradox.Engine;

namespace SpeechRecognitionGame
{
    public class SpriteController : SyncScript
    {
        //modes: 0 - idle, 4 - walk, 8 - action/fight, 12 - die
        public int mode = 0;
        int animationTimer = 0;
        int liveTimer = 0;

        public override void Update()
        {
            if (Game.IsRunning)
            {
                animationTimer++;
                liveTimer++;
                //play animation
                if (animationTimer > 10)
                {
                    Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame++;
                    if (Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame == 4 + mode)
                    {
                        Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame = 0 + mode;
                    }
                    animationTimer = 0;
                }
                //kill unit
                if (liveTimer > 500)
                {
                    SceneSystem.SceneInstance.Scene.RemoveChild(Entity);
                }
                //move right
                if (mode == 4)
                {
                    Entity.Transform.Position.X += 0.01f;
                }
            }
        }
    }
}
