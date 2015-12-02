using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox.Engine;
using SiliconStudio.Paradox.Graphics;
using SiliconStudio.Paradox.UI;
using SiliconStudio.Paradox.UI.Controls;
using SiliconStudio.Paradox.UI.Panels;

namespace SpeechRecognitionGame
{
    public class MenuStart : StartupScript
    {
        public override void Start()
        {
            //Set buttons to create units
            float height = Entity.Get<UIComponent>().VirtualResolution.Y;
            Button play = SetupButton("GRAJ", (height - 100) / height);
            Button exit = SetupButton("WYJDŹ", (height - 25) / height);
            Entity.Get<UIComponent>().RootElement = new Canvas
            {
                Children =
                {
                    play,
                    exit
                }
            };
        }

        //Button configuration
        Button SetupButton(string name, float location)
        {
            Button button = new Button
            {
                Height = 25,
                Width = 100
            };
            button.Click += delegate { ((MenuScript)Entity.Get<ScriptComponent>().Scripts[1]).Menu(name); };
            TextBlock text = new TextBlock
            {
                Height = 20,
                Width = 80,
                Font = Asset.Load<SpriteFont>("Font"),
                Text = name,
                TextAlignment = TextAlignment.Center
            };
            text.SetCanvasRelativePosition(new Vector3(0, 0, 0));
            button.Content = new Canvas
            {
                Children =
                {
                    text
                }
            };
            button.SetCanvasRelativePosition(new Vector3(0, location, 0));
            return button;
        }
    }
}
