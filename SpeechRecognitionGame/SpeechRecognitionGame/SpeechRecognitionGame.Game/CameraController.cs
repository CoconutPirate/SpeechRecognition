using SiliconStudio.Paradox.Engine;
using SiliconStudio.Paradox.Input;

namespace SpeechRecognitionGame
{
    public class CameraController : SyncScript
    {
        float speed = 0.1f;

        public override void Update()
        {
            if (Game.IsRunning)
            {
                if (Input.IsKeyDown(Keys.Right))
                {
                    MoveRight();
                }
                if (Input.IsKeyDown(Keys.Left))
                {
                    MoveLeft();
                }
                if (Input.MousePosition.X >= 0.999f)
                {
                    MoveRight();
                }
                if (Input.MousePosition.X <= 0.001f)
                {
                    MoveLeft();
                }
            }
        }

        void MoveRight()
        {
            Entity.Transform.Position.X += speed;
            if (Entity.Transform.Position.X > 40)
            {
                Entity.Transform.Position.X = 40;
            }
        }

        void MoveLeft()
        {
            Entity.Transform.Position.X -= speed;
            if (Entity.Transform.Position.X < 0)
            {
                Entity.Transform.Position.X = 0;
            }
        }
    }
}
