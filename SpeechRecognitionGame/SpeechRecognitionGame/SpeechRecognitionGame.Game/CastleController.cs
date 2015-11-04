using System;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox.Engine;
using SiliconStudio.Paradox.Graphics;
using SiliconStudio.Paradox.Input;
using SiliconStudio.Paradox.Rendering.Sprites;

namespace SpeechRecognitionGame
{
    public class CastleController : SyncScript
    {
        int producedUnit;
        int animationTimer = 0;
        int workTimer = 0;
        //modes: 0 - idle, 2 - work, 4 - take damage, 6 - die
        int mode = 0;

        public override void Update()
        {
            if (Game.IsRunning)
            {
                animationTimer++;
                if (mode == 2)
                {
                    workTimer++;
                }
                //play animation
                if (animationTimer > 20)
                {
                    Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame++;
                    if (Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame == 2 + mode)
                    {
                        Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame = 0 + mode;
                    }
                    animationTimer = 0;
                }
                //stop work
                if (workTimer > 100)
                {
                    mode = 0;
                    Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame = 0;
                    workTimer = 0;
                    MakeUnit(producedUnit);
                }
            }
        }

        public void StartWork(int unit)
        {
            if (mode != 2)
            {
                mode = 2;
                Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame = 2;
                producedUnit = unit;
            }
        }

        void MakeUnit(int index)
        {
            string name;
            switch (index)
            {
                case 0:
                    name = "Pickaxe";
                    break;
                case 1:
                    name = "Sword";
                    break;
                case 2:
                    name = "BowArrow";
                    break;
                case 3:
                    name = "Wand";
                    break;
                default:
                    name = "Unit";
                    break;
            }
            //make entity with specific name and add proper sprite sheet and script to it
            Entity entity = new Entity(new Vector3(3, 0, 3), name);
            SpriteComponent spriteComponent = new SpriteComponent();
            SpriteSheet spriteSheet = Asset.Get<SpriteSheet>(name);
            spriteComponent.SpriteProvider = new SpriteFromSheet
            {
                Sheet = spriteSheet
            };
            spriteComponent.CurrentFrame = 4;
            ScriptComponent scriptComponent = new ScriptComponent();
            SpriteController spriteController = new SpriteController
            {
                mode = 4
            };
            scriptComponent.Scripts.Add(spriteController);
            entity.Add<SpriteComponent>(SpriteComponent.Key, spriteComponent);
            entity.Add<ScriptComponent>(ScriptComponent.Key, scriptComponent);
            SceneSystem.SceneInstance.Scene.AddChild(entity);
        }
    }
}
