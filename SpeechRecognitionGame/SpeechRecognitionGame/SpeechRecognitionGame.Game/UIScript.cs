using System.Linq;
using SiliconStudio.Core;
using SiliconStudio.Paradox.Engine;
using SiliconStudio.Paradox.Games;
using SiliconStudio.Paradox.Input;

namespace SpeechRecognitionGame
{
    public class UIScript : SyncScript
    {
        public override void Update()
        {
            if (Game.IsRunning)
            {
                //Keyboard Input
                if (Input.IsKeyReleased(Keys.Escape))
                {
                    ((Game)Game).Exit();
                }
                if (Input.IsKeyReleased(Keys.P))
                {
                    ProduceUnit("PICKAXE");
                }
                if (Input.IsKeyReleased(Keys.S))
                {
                    ProduceUnit("SWORD");
                }
                if (Input.IsKeyReleased(Keys.B))
                {
                    ProduceUnit("BOWARROW");
                }
                if (Input.IsKeyReleased(Keys.W))
                {
                    ProduceUnit("WAND");
                }
            }
        }

        //Order to make a unit
        public void ProduceUnit(string name)
        {
            int index;
            switch (name)
            {
                case "PICKAXE":
                    index = 0;
                    break;
                case "SWORD":
                    index = 1;
                    break;
                case "BOWARROW":
                    index = 2;
                    break;
                case "WAND":
                    index = 3;
                    break;
                default:
                    index = -1;
                    break;
            }
            Entity castle = (from entities in SceneSystem.SceneInstance where entities.Name == "Castle" select entities).FirstOrDefault();
            ((CastleController) castle.Get<ScriptComponent>().Scripts[0]).StartWork(index);
        }
    }
}
