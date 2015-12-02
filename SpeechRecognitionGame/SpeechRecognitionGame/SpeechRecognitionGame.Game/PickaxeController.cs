using SiliconStudio.Paradox.Engine;

namespace SpeechRecognitionGame
{
    public class PickaxeController : SpriteController
    {
        public Entity castle;

        public override void Update()
        {
            if (Game.IsRunning)
            {
                base.Update();
                if (Entity.Name.Contains("Enemy"))
                {
                    MoveLeft(8);
                }
                else
                {
                    MoveRight(8);
                }
                DoWork();
            }
        }

        void DoWork()
        {
            if (mode == 8)
            {
                workTimer++;
                if (workTimer == 30)
                {
                    ((CastleController) castle.Get<ScriptComponent>().Scripts[0]).gold += 10;
                    liveTimer++;
                    workTimer = 0;
                }
            }
        }
    }
}
