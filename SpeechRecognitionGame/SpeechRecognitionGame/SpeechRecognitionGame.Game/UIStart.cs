using SiliconStudio.Core.Mathematics;
using SiliconStudio.Paradox.Engine;
using SiliconStudio.Paradox.Graphics;
using SiliconStudio.Paradox.UI;
using SiliconStudio.Paradox.UI.Controls;
using SiliconStudio.Paradox.UI.Panels;

namespace SpeechRecognitionGame
{
    public class UIStart : StartupScript
    {
        public override void Start()
        {
            //Set buttons to create units
            float width = Entity.Get<UIComponent>().VirtualResolution.X;
            float height = Entity.Get<UIComponent>().VirtualResolution.Y;
            Button pickaxe = SetupButton("PICKAXE", (height - 100)/height);
            Button sword = SetupButton("SWORD", (height - 75)/height);
            Button bowArrow = SetupButton("BOWARROW", (height - 50)/height);
            Button wand = SetupButton("WAND", (height - 25)/height);
            TextBlock gold = SetupTextBlock("Gold: 0", 100/width, 0);
            TextBlock units = SetupTextBlock("Units: 0", 0, 0);
            TextBlock pickaxes = SetupTextBlock("Pickaxes: 0", 0, 25/height);
            TextBlock swords = SetupTextBlock("Swords: 0", 0, 50/height);
            TextBlock bowArrows = SetupTextBlock("BowArrorws: 0", 0, 75/height);
            TextBlock wands = SetupTextBlock("Wands: 0", 0, 100/height);
            Entity.Get<UIComponent>().RootElement = new Canvas
            {
                Children =
                {
                    pickaxe,
                    sword,
                    bowArrow,
                    wand,
                    gold,
                    units,
                    pickaxes,
                    swords,
                    bowArrows,
                    wands
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
            button.Click += delegate { ((UIScript) Entity.Get<ScriptComponent>().Scripts[1]).ProduceUnit(name); };
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

        //Textblock configuration
        TextBlock SetupTextBlock(string text, float x, float y)
        {
            TextBlock textBlock = new TextBlock
            {
                Height = 25,
                Width = 100,
                Font = Asset.Load<SpriteFont>("Font"),
                Text = text,
                TextAlignment = TextAlignment.Center
            };
            textBlock.SetCanvasRelativePosition(new Vector3(x, y, 0));
            return textBlock;
        }
    }
}
