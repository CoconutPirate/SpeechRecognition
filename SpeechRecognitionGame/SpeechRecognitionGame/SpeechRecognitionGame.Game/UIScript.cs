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
            if(Game.IsRunning && Entity.Components.Get<UIComponent>(UIComponent.Key).Enabled)
            {
                castle = (from entities in SceneSystem.SceneInstance where entities.Name == "Castle" select entities).FirstOrDefault();
                ((TextBlock)Entity.Components.Get<UIComponent>(UIComponent.Key).RootElement.VisualChildren.ElementAt(4)).Text = "Złoto: " + ((CastleController)castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).gold.ToString();
                ((TextBlock)Entity.Components.Get<UIComponent>(UIComponent.Key).RootElement.VisualChildren.ElementAt(5)).Text = "Zdrowie: " + ((CastleController)castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).health.ToString();
                ((TextBlock)Entity.Components.Get<UIComponent>(UIComponent.Key).RootElement.VisualChildren.ElementAt(6)).Text = "Jednostki: " + ((CastleController)castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).units.ToString();
                ((TextBlock)Entity.Components.Get<UIComponent>(UIComponent.Key).RootElement.VisualChildren.ElementAt(7)).Text = "Kilofy: " + ((CastleController)castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).pickaxes.ToString();
                ((TextBlock)Entity.Components.Get<UIComponent>(UIComponent.Key).RootElement.VisualChildren.ElementAt(8)).Text = "Miecze: " + ((CastleController)castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).swords.ToString();
                ((TextBlock)Entity.Components.Get<UIComponent>(UIComponent.Key).RootElement.VisualChildren.ElementAt(9)).Text = "Łuki: " + ((CastleController)castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).bowArrows.ToString();
                ((TextBlock)Entity.Components.Get<UIComponent>(UIComponent.Key).RootElement.VisualChildren.ElementAt(10)).Text = "Różdżki " + ((CastleController)castle.Get<ScriptComponent>(ScriptComponent.Key).Scripts[0]).wands.ToString();
                //Keyboard Input
                if(Input.IsKeyReleased(Keys.Escape))
                {
                    ((Game)Game).Exit();
                }
                if(Input.IsKeyReleased(Keys.K))
                {
                    ProduceUnit("KILOF");
                }
                if(Input.IsKeyReleased(Keys.M))
                {
                    ProduceUnit("MIECZ");
                }
                if(Input.IsKeyReleased(Keys.L))
                {
                    ProduceUnit("ŁUK");
                }
                if(Input.IsKeyReleased(Keys.R))
                {
                    ProduceUnit("RÓŻDŻKA");
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
                    case "KILOF":
                        if (((CastleController)castle.Get<ScriptComponent>().Scripts[0]).pickaxes == 3)
                        {
                            return;
                        }
                        index = 0;
                        position = 1.5f;
                        distance = 17;
                        cost = 50;
                        break;
                    case "MIECZ":
                        index = 1;
                        position = -1.5f;
                        distance = 37;
                        cost = 100;
                        break;
                    case "ŁUK":
                        index = 2;
                        position = 0f;
                        distance = 10;
                        cost = 100;
                        break;
                    case "RÓŻDŻKA":
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
