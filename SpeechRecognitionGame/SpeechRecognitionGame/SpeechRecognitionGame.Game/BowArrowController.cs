using System.Linq;
using SiliconStudio.Paradox.Engine;

namespace SpeechRecognitionGame
{
    public class BowArrowController : SpriteController
    {
        Entity[] swords;
        int index;

        public override void Update()
        {
            if (Game.IsRunning)
            {
                base.Update();
                if (Entity.Name.Contains("Enemy"))
                {
                    MoveLeft(0);
                }
                else
                {
                    MoveRight(0);
                }
                if (mode == 0)
                {
                    if (GetSwords())
                    {
                        SelectSword();
                    }
                }
                if (mode == 8)
                {
                    DoShoot();
                }
            }
        }

        bool GetSwords()
        {
            workTimer++;
            if (workTimer == 30)
            {
                liveTimer++;
                workTimer = 0;
            }
            if (Entity.Name.Contains("Enemy"))
            {
                swords = (from entities in SceneSystem.SceneInstance where entities.Name == "Sword" select entities).ToArray();
            }
            else
            {
                swords = (from entities in SceneSystem.SceneInstance where entities.Name == "EnemySword" select entities).ToArray();
            }
            return swords.Count() > 0;
        }

        void SelectSword()
        {
            if (mode == 0)
            {
                for (int i = 0; i < swords.Count(); i++)
                {
                    if (Entity.Name.Contains("Enemy"))
                    {
                        if (swords[i].Transform.Position.X > 20 && swords[i].Transform.Position.X < 30)
                        {
                            ChangeMode(8);
                            index = i;
                            break;
                        }
                    }
                    else
                    {
                        if (swords[i].Transform.Position.X < 20 && swords[i].Transform.Position.X > 10)
                        {
                            ChangeMode(8);
                            index = i;
                            break;
                        }
                    }
                }
            }
        }

        void DoShoot()
        {
            if (Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame == 11 + side)
            {
                ((SpriteController) swords[index].Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).liveTimer++;
                ChangeMode(0);
            }
        }
    }
}
