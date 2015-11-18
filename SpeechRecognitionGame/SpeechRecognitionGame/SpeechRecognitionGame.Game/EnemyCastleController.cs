using System;

namespace SpeechRecognitionGame
{
    public class EnemyCastleController : CastleController
    {
        Random random = new Random();

        public override void Update()
        {
            if (Game.IsRunning && !dead)
            {
                base.Update();
                RandomUnit();
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
                        distance = 22;
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
