using System.Linq;
using SiliconStudio.Paradox.Engine;
using SiliconStudio.Paradox.Input;
using SiliconStudio.Paradox.UI.Controls;

namespace SpeechRecognitionGame
{
    public class UIScript : SyncScript
    {
        Entity castle;

        public override void Update()
        {
            if (Game.IsRunning)
            {
                castle = (from entities in SceneSystem.SceneInstance where entities.Name == "Castle" select entities).FirstOrDefault();
                ((TextBlock) Entity.Components.Get<UIComponent>(UIComponent.Key).RootElement.VisualChildren.ElementAt(4)).Text = "Gold: " + ((CastleController) castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).gold.ToString();
                ((TextBlock) Entity.Components.Get<UIComponent>(UIComponent.Key).RootElement.VisualChildren.ElementAt(5)).Text = "Units: " + ((CastleController) castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).units.ToString();
                ((TextBlock) Entity.Components.Get<UIComponent>(UIComponent.Key).RootElement.VisualChildren.ElementAt(6)).Text = "Pickaxes: " + ((CastleController) castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).pickaxes.ToString();
                ((TextBlock) Entity.Components.Get<UIComponent>(UIComponent.Key).RootElement.VisualChildren.ElementAt(7)).Text = "Swords: " + ((CastleController) castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).swords.ToString();
                ((TextBlock) Entity.Components.Get<UIComponent>(UIComponent.Key).RootElement.VisualChildren.ElementAt(8)).Text = "BowArrows: " + ((CastleController) castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).bowArrows.ToString();
                ((TextBlock) Entity.Components.Get<UIComponent>(UIComponent.Key).RootElement.VisualChildren.ElementAt(9)).Text = "Wands: " + ((CastleController) castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).wands.ToString();
                //Keyboard Input
                if (Input.IsKeyReleased(Keys.Escape))
                {
                    ((Game) Game).Exit();
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
            if (((CastleController) castle.Get<ScriptComponent>().Scripts[0]).units < 12)
            {
                int index;
                float position;
                int distance;
                int cost;
                switch (name)
                {
                    case "PICKAXE":
                        if (((CastleController)castle.Get<ScriptComponent>().Scripts[0]).pickaxes == 3)
                        {
                            return;
                        }
                        index = 0;
                        position = 1.5f;
                        distance = 18;
                        cost = 50;
                        break;
                    case "SWORD":
                        index = 1;
                        position = -1.5f;
                        distance = 37;
                        cost = 100;
                        break;
                    case "BOWARROW":
                        index = 2;
                        position = 0f;
                        distance = 10;
                        cost = 100;
                        break;
                    case "WAND":
                        index = 3;
                        position = -1.5f;
                        distance = 35;
                        cost = 50;
                        break;
                    default:
                        index = -1;
                        position = 0f;
                        distance = 0;
                        cost = 0;
                        break;
                }
                ((CastleController) castle.Get<ScriptComponent>().Scripts[0]).StartWork(index, position, distance, cost);
            }
        }
    }
}
