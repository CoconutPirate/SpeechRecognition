using System;
using System.Linq;
using SiliconStudio.Paradox.Engine;

namespace SpeechRecognitionGame
{
    public class WandController : SpriteController
    {
        Entity sword;
        Random random = new Random();

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
                    SelectSword();
                }
                if (mode == 8)
                {
                    DoHeal();
                }
            }
        }

        void SelectSword()
        {
            workTimer++;
            if (workTimer == 25)
            {
                liveTimer++;
                workTimer = 0;
            }
                Entity[] swords = (from entities in SceneSystem.SceneInstance where entities.Name == Entity.Name.Replace("Wand", "Sword") select entities).ToArray();
            if (swords.Count() > 0)
            {
                int index = random.Next(swords.Count());
                sword = swords[index];
                if (((SpriteController) sword.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).liveTimer > 0)
                {
                    ChangeMode(8);
                }
            }
        }

        void DoHeal()
        {
            if (Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame == 11 + side)
            {
                ((SpriteController) sword.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).liveTimer--;
                ChangeMode(0);
            }
        }
    }
}
