using System.Linq;
using SiliconStudio.Paradox.Engine;
using SiliconStudio.Paradox.Input;

namespace SpeechRecognitionGame
{
    public class MenuScript : SyncScript
    {
        Entity ui;

        public override void Update()
        {
            if (Game.IsRunning)
            {
                ui = (from entities in SceneSystem.SceneInstance where entities.Name == "UI" select entities).FirstOrDefault();
                //Keyboard Input
                if (Input.IsKeyReleased(Keys.Escape))
                {
                    Menu("WYJDŹ");
                }
                if (Input.IsKeyReleased(Keys.Space))
                {
                    Menu("GRAJ");
                }
            }
        }

        public void Menu(string name)
        {
            switch(name)
            {
                case "GRAJ":
                    ui.Components.Get<UIComponent>(UIComponent.Key).Enabled = true;
                    Entity.Components.Get<UIComponent>(UIComponent.Key).Enabled = false;
                    break;
                case "WYJDŹ":
                    ((Game)Game).Exit();
                    break;
            }
        }
    }
}
