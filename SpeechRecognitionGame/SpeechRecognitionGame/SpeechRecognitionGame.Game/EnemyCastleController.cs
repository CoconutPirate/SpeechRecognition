using System;
using System.Linq;
using SiliconStudio.Paradox.Engine;
using SiliconStudio.Paradox.Extensions;

namespace SpeechRecognitionGame
{
    public class EnemyCastleController : CastleController
    {
        Random random = new Random();
        Entity ui;

        public override void Update()
        {
            if (Game.IsRunning && !dead)
            {
                base.Update();
                ui = (from entities in SceneSystem.SceneInstance where entities.Name == "UI" select entities).FirstOrDefault();
                if(ui.Components.Get<UIComponent>(UIComponent.Key).Enabled)
                {
                    RandomUnit();
                }
            }
            else if (Game.IsRunning && dead)
            {
                Victory();
            }
        }

        void Victory()
        {
            SpriteComponent spriteComponent;
            foreach (Entity entity in SceneSystem.SceneInstance.Scene.GetChildren())
            {
                spriteComponent = entity.Components.Get<SpriteComponent>(SpriteComponent.Key);
                if (spriteComponent != null)
                {
                    spriteComponent.Enabled = false;
                }
            }
            Entity ui = (from entities in SceneSystem.SceneInstance where entities.Name == "UI" select entities).FirstOrDefault();
            ui.Components.Get<UIComponent>(UIComponent.Key).Enabled = false;
            Entity victory = (from entities in SceneSystem.SceneInstance where entities.Name == "Victory" select entities).FirstOrDefault();
            Entity defeat = (from entities in SceneSystem.SceneInstance where entities.Name == "Defeat" select entities).FirstOrDefault();
            if (!defeat.Components.Get<BackgroundComponent>(BackgroundComponent.Key).Enabled)
            {
                victory.Components.Get<BackgroundComponent>(BackgroundComponent.Key).Enabled = true;
            }
        }

        void RandomUnit()
        {
            if (units < 12)
            {
                int value = random.Next(4);
                int index;
                if (gold <= 100 || pickaxes == 0)
                {
                    index = 0;
                }
                else if(value == 3 && swords > wands)
                {
                    index = 3;
                }
                else if(value == 1 || value == 3)
                {
                    index = 1;
                }
                else
                {
                    index = value;
                }
                float position;
                int distance;
                int cost;
                switch (index)
                {
                    case 0:
                        if (pickaxes == 3)
                        {
                            return;
                        }
                        position = 1.5f;
                        distance = 23;
                        cost = 50;
                        break;
                    case 1:
                        position = 0f;
                        distance = 3;
                        cost = 100;
                        break;
                    case 2:
                        position = -1.5f;
                        distance = 30;
                        cost = 100;
                        break;
                    case 3:
                        position = 0f;
                        distance = 5;
                        cost = 50;
                        break;
                    default:
                        position = 0f;
                        distance = 0;
                        cost = 0;
                        break;
                }
                StartWork(index, position, distance, cost);
            }
        }
    }
}
