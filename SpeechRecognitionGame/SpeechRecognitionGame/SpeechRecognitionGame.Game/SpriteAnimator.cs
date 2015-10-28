using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SiliconStudio.Paradox.Engine;

namespace MyGame3
{
    public class SpriteAnimator : SyncScript
    {
        int timer = 0;

        public override void Update()
        {
            if(Game.IsRunning)
            {
                timer++;
                if (timer > 10)
                {
                    Entity.Components.Get<SpriteComponent>(SpriteComponent.Key).CurrentFrame++;
                    timer = 0;
                }
            }
        }
    }
}
