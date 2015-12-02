using SiliconStudio.Paradox.Engine;

namespace SpeechRecognitionGame
{
    public class SwordController : SpriteController
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
                DoAttack();
            }
        }

        void DoAttack()
        {if (mode == 8)
            {
                workTimer++;
                if (workTimer == 30)
                {
                    liveTimer++;
                    workTimer = 0;
                    if (((CastleController) castle.Components.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).health > 0)
                    {
                        ((CastleController) castle.Components.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).health -= 10;
                    }
                    else
                    {
                        ChangeMode(0);
                    }
                }
            }
        }
    }
}
