using System.Diagnostics;
using System.Linq;
using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox.Engine;
using SiliconStudio.Paradox.Extensions;
using SiliconStudio.Paradox.Games;
using SiliconStudio.Paradox.Graphics;
using SiliconStudio.Paradox.Rendering.Sprites;

namespace SpeechRecognitionGame
{
    public class CastleController : SyncScript
    {
        int producedUnit;
        int unitSide;
        float unitPosition;
        int unitDistance;
        int animationTimer = 0;
        int workTimer = 0;
        int goldTimer = 0;
        protected bool dead = false;
        public string side;
        public string enemy;
        public int health = 1000;
        public int units = 0;
        public int pickaxes = 0;
        public int swords = 0;
        public int bowArrows = 0;
        public int wands;
        public int gold = 100;
        //modes: 0 - idle, 2 - work, 4 - take damage, 6 - die
        int mode = 0;

        public override void Update()
        {
            if (Game.IsRunning && !dead)
            {
                animationTimer++;
                goldTimer++;
                PlayAnimation();
                AddGold();
                StopWork();
                if (mode == 2)
                {
                    workTimer++;
                }
                if (health == 0)
                {
                    ChangeMode(6);
                    health = -1000;
                }
                if (mode == 6)
                {
                    Die();
                }
            }
            else if (Game.IsRunning && dead)
            {
                Defeat();
            }
        }

        void Defeat()
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
            if (!victory.Components.Get<BackgroundComponent>(BackgroundComponent.Key).Enabled)
            {
                defeat.Components.Get<BackgroundComponent>(BackgroundComponent.Key).Enabled = true;
            }
        }

        void PlayAnimation()
        {
            if (animationTimer == 20)
            {
                Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame++;
                if (Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame == 2 + mode)
                {
                    Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame = 0 + mode;
                }
                animationTimer = 0;
            }
        }

        void AddGold()
        {
            if (goldTimer == 30)
            {
                gold++;
                goldTimer = 0;
            }
        }

        void StopWork()
        {
            if (workTimer == 120)
            {
                ChangeMode(0);
                workTimer = 0;
                MakeUnit();
            }
        }

        void Die()
        {
            if (Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame == 7)
            {
                dead = true;
            }
        }

        public void StartWork(int unit, float position, int distance, int cost)
        {
            if (mode == 0 && gold >= cost)
            {
                ChangeMode(2);
                gold -= cost;
                producedUnit = unit;
                unitDistance = distance;
                unitPosition = position;
                if (Entity.Name.Contains("Enemy"))
                {
                    unitSide = 16;
                }
                else
                {
                    unitSide = 0;
                }
            }
        }

        void ChangeMode(int value)
        {
            mode = value;
            Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame = value;
        }

        void MakeUnit()
        {
            units++;
            SpriteController spriteController;
            string name;
            switch (producedUnit)
            {
                case 0:
                    pickaxes++;
                    name = side + "Pickaxe";
                    spriteController = new PickaxeController
                    {
                        mode = 4,
                        side = unitSide,
                        maxDistance = unitDistance,
                        castle = Entity
                    };
                    break;
                case 1:
                    swords++;
                    name = side + "Sword";
                    spriteController = new SwordController
                    {
                        mode = 4,
                        side = unitSide,
                        maxDistance = unitDistance,
                        castle = (from entities in SceneSystem.SceneInstance where entities.Name == enemy select entities).FirstOrDefault()
                    };
                    break;
                case 2:
                    bowArrows++;
                    name = side + "BowArrow";
                    spriteController = new BowArrowController
                    {
                        mode = 4,
                        side = unitSide,
                        maxDistance = unitDistance
                    };
                    break;
                case 3:
                    wands++;
                    name = side + "Wand";
                    spriteController = new WandController
                    {
                        mode = 4,
                        side = unitSide,
                        maxDistance = unitDistance
                    };
                    break;
                default:
                    name = side + "Unit";
                    spriteController = new SpriteController
                    {
                        mode = 0,
                        side = unitSide,
                        maxDistance = unitDistance
                    };
                    break;
            }
            //make entity with specific name and add proper sprite sheet and script to it
            Entity entity;
            if (Entity.Name.Contains("Enemy"))
            {
                entity = new Entity(new Vector3(37, unitPosition, 0), name);
            }
            else
            {
                entity = new Entity(new Vector3(3, unitPosition, 0), name);
            }
            SpriteComponent spriteComponent = new SpriteComponent();
            SpriteSheet spriteSheet = Asset.Get<SpriteSheet>(name.Replace("Enemy", ""));
            spriteComponent.SpriteProvider = new SpriteFromSheet
            {
                Sheet = spriteSheet
            };
            spriteComponent.CurrentFrame = 4 + unitSide;
            ScriptComponent scriptComponent = new ScriptComponent();
            scriptComponent.Scripts.Add(spriteController);
            entity.Add<SpriteComponent>(SpriteComponent.Key, spriteComponent);
            entity.Add<ScriptComponent>(ScriptComponent.Key, scriptComponent);
            SceneSystem.SceneInstance.Scene.AddChild(entity);
        }
    }
}
