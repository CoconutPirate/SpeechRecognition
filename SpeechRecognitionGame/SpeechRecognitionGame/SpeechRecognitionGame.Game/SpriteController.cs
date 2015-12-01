using System.Linq;
using SiliconStudio.Paradox.Engine;

namespace SpeechRecognitionGame
{
    public class SpriteController : SyncScript
    {
        //modes: 0 - idle, 4 - walk, 8 - action/fight, 12 - die
        public int mode = 0;
        public int side = 0;
        protected int workTimer = 0;
        int animationTimer = 0;
        public int liveTimer = 0;
        protected float speed = 0.02f;
        public int maxDistance;

        public override void Update()
        {
            if (Game.IsRunning)
            {
                if (mode == 12)
                {
                    Die();
                }
                animationTimer++;
                PlayAnimation();
                KillUnit();
            }
        }

        void PlayAnimation()
        {
            if (animationTimer == 10)
            {
                Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame++;
                if (Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame == 4 + mode + side)
                {
                    Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame = 0 + mode + side;
                }
                animationTimer = 0;
            }
        }

        void KillUnit()
        {
            if (liveTimer == 30)
            {
                ChangeMode(12);
                liveTimer = 0;
            }
        }

        void Die()
        {
            Entity castle;
            if (side == 0)
            {
                castle = (from entities in SceneSystem.SceneInstance where entities.Name == "Castle" select entities).FirstOrDefault();
            }
            else if (side == 16)
            {
                castle = (from entities in SceneSystem.SceneInstance where entities.Name == "EnemyCastle" select entities).FirstOrDefault();
            }
            else
            {
                castle = null;
            }
            if (Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame == 15 + side)
            {
                ((CastleController) castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).units--;
                switch (Entity.Name)
                {
                    case "Pickaxe":
                        ((CastleController) castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).pickaxes--;
                        break;
                    case "Sword":
                        ((CastleController) castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).swords--;
                        break;
                    case "BowArrow":
                        ((CastleController) castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).bowArrows--;
                        break;
                    case "Wand":
                        ((CastleController) castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).wands--;
                        break;
                }
                SceneSystem.SceneInstance.Scene.RemoveChild(Entity);
            }
        }

        protected void ChangeMode(int value)
        {
            mode = value;
            Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame = value + side;
        }

        protected void MoveRight(int select)
        {
            if (mode == 4)
            {
                Entity.Transform.Position.X += speed;
                if (Entity.Transform.Position.X > maxDistance)
                {
                    Entity.Transform.Position.X = maxDistance;
                    ChangeMode(select);
                }
            }
        }

        protected void MoveLeft(int select)
        {
            if (mode == 4)
            {
                Entity.Transform.Position.X -= speed;
                if (Entity.Transform.Position.X < maxDistance)
                {
                    Entity.Transform.Position.X = maxDistance;
                    ChangeMode(select);
                }
            }
        }
    }
}
