using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace OctopusPathfinder.Manager
{
    public enum ETexture2D
    {
        // Name of Enum Map
        Rect2,
        Exit,
        Background,
        MenuBG,
        Fissure,
        Land1,
        Land2,
        Land3,
        Rock,
        Rect,
        Waves,
        Tentacle,
        Name,
        Curtain,
        Star,
        Crossroad,
        Jamb,

        // Name of Enum Button
        Button,
        LevelButton,
        Song,
        Sound,
        HelpButton,
        
        // Name of Enum Logo
        CSLogo,
        GameUITLogo,

        // Name of Enum Octopus
        OctopusStay,
        OctopusDead,
        OctopusDown,
        OctopusInWater,
        OctopusLeft,
        OctopusRight,
        OctopusUp,

        //Name of Enum for Help
        HelpWin,
        HelpDied,
        HelpStart,
        HelpRock,
        KeyBoard,

        CircularShape

    }

    public enum ESpriteFont
    {
        FontMenu,
        FontNormal,
        FontHeading
    }

    public static class TextureManager
    {
        // Texture Map
        private static Texture2D Rect2;
        private static Texture2D exit;
        private static Texture2D background;
        private static Texture2D MenuBG;
        private static Texture2D fissure;
        private static Texture2D land1;
        private static Texture2D land2;
        private static Texture2D land3;
        private static Texture2D rock;
        private static Texture2D Rect;
        private static Texture2D Waves;
        private static Texture2D Tentacle;
        private static Texture2D Name;
        private static Texture2D Curtain;
        private static Texture2D Star;
        private static Texture2D Crossroad;
        private static Texture2D Jamb;

        //Texture Button
        private static Texture2D button;
        private static Texture2D LevelButton;
        private static Texture2D Song;
        private static Texture2D Sound;
        private static Texture2D helpButton;

        //Texture Logo
        static Texture2D CSLogo;

        //Texture Octopus
        private static Texture2D octopusDead;
        private static Texture2D octopusDown;
        private static Texture2D octopusInWater;
        private static Texture2D octopusLeft;
        private static Texture2D octopusRight;
        private static Texture2D octopusStay;
        private static Texture2D octopusUp;

        //Texture Help
        static Texture2D HelpStart;
        static Texture2D HelpRock;
        static Texture2D HelpDied;
        static Texture2D HelpWin;
        static Texture2D KeyBoard;

        static Texture2D circularShape;

        //Font
        static SpriteFont fontMenu;
        static SpriteFont fontChar;
        static SpriteFont fontHeading;


        public static void LoadContent(ContentManager Content)
        {            
            Rect2 = Content.Load<Texture2D>(@"Map\Rect2");
            exit = Content.Load<Texture2D>(@"Map\Exit");
            background = Content.Load<Texture2D>(@"Map\Background");
            MenuBG = Content.Load<Texture2D>(@"Map\MenuBG");
            fissure = Content.Load<Texture2D>(@"Map\Fissure");
            land1 = Content.Load<Texture2D>(@"Map\Land1");
            land2 = Content.Load<Texture2D>(@"Map\Land2");
            land3 = Content.Load<Texture2D>(@"Map\Land3");
            rock = Content.Load<Texture2D>(@"Map\Rock");
            Rect = Content.Load<Texture2D>(@"Map\Rect");
            Waves = Content.Load<Texture2D>(@"Map\Waves");
            Tentacle = Content.Load<Texture2D>(@"Map\Tentacle");
            Name = Content.Load<Texture2D>(@"Map\Name");
            Curtain = Content.Load<Texture2D>(@"Map\Curtain");
            Star = Content.Load<Texture2D>(@"Map\Star");
            Crossroad = Content.Load<Texture2D>(@"Map\Crossroad");
            Jamb = Content.Load<Texture2D>(@"Map\Jamb");

            button = Content.Load<Texture2D>(@"Button\Button");
            LevelButton = Content.Load<Texture2D>(@"Button\LevelButton");
            Song = Content.Load<Texture2D>(@"Button\Song");
            Sound = Content.Load<Texture2D>(@"Button\Sound");
            helpButton = Content.Load<Texture2D>(@"Button\HelpButton");

            CSLogo = Content.Load<Texture2D>(@"Logo\CSLogo");

            octopusDead = Content.Load<Texture2D>(@"Octopus\Dead");
            octopusDown = Content.Load<Texture2D>(@"Octopus\Down");
            octopusInWater = Content.Load<Texture2D>(@"Octopus\InWater");
            octopusLeft = Content.Load<Texture2D>(@"Octopus\Left");
            octopusRight = Content.Load<Texture2D>(@"Octopus\Right");
            octopusStay = Content.Load<Texture2D>(@"Octopus\Stay");
            octopusUp = Content.Load<Texture2D>(@"Octopus\Up");

            HelpDied = Content.Load<Texture2D>(@"Helps\HelpDied");
            HelpRock = Content.Load<Texture2D>(@"Helps\Help1");
            HelpStart = Content.Load<Texture2D>(@"Helps\Help01");
            HelpWin = Content.Load<Texture2D>(@"Helps\HelpWin");
            KeyBoard = Content.Load<Texture2D>(@"Helps\KeyBoard");

            circularShape = Content.Load<Texture2D>(@"Octopus\CircularShape");

            fontMenu = Content.Load<SpriteFont>(@"Font\Menu");
            fontChar = Content.Load<SpriteFont>(@"Font\Normal");
            fontHeading = Content.Load<SpriteFont>(@"Font\Large");

        }

        public static Texture2D GetTexture2D(ETexture2D id)
        {
            switch (id)
            {
                case ETexture2D.Rect2 :
                    return Rect2;
                case ETexture2D.Exit :
                    return exit;
                case ETexture2D.Background :
                    return background;
                case ETexture2D.MenuBG:
                    return MenuBG;
                case ETexture2D.Fissure :
                    return fissure;
                case ETexture2D.Land1 :
                    return land1;
                case ETexture2D.Land2 :
                    return land2;
                case ETexture2D.Land3 :
                    return land3;
                case ETexture2D.Rock :
                    return rock;
                case ETexture2D.Rect:
                    return Rect;
                case ETexture2D.Waves:
                    return Waves;
                case ETexture2D.Name:
                    return Name;
                case ETexture2D.Tentacle:
                    return Tentacle;
                case ETexture2D.Star:
                    return Star;
                case ETexture2D.Crossroad :
                    return Crossroad;
                case ETexture2D.Jamb :
                    return Jamb;

                case ETexture2D.Curtain:
                    return Curtain;

                case ETexture2D.Button:
                    return button;
                case ETexture2D.LevelButton:
                    return LevelButton;
                case ETexture2D.Song:
                    return Song;
                case ETexture2D.Sound:
                    return Sound;
                case ETexture2D.HelpButton:
                    return helpButton;

                case ETexture2D.CSLogo:
                    return CSLogo;

                case ETexture2D.OctopusDead :
                    return octopusDead;
                case ETexture2D.OctopusDown :
                    return octopusDown;
                case ETexture2D.OctopusInWater :
                    return octopusInWater;
                case ETexture2D.OctopusLeft :
                    return octopusLeft;
                case ETexture2D.OctopusRight :
                    return octopusRight;
                case ETexture2D.OctopusStay :
                    return octopusStay;
                case ETexture2D.OctopusUp :
                    return octopusUp;

                case ETexture2D.HelpDied:
                    return HelpDied;
                case ETexture2D.HelpRock:
                    return HelpRock;
                case ETexture2D.HelpStart:
                    return HelpStart;
                case ETexture2D.HelpWin:
                    return HelpWin;
                case ETexture2D.KeyBoard:
                    return KeyBoard;


                case ETexture2D.CircularShape:
                    return circularShape;

                default :
                    return null;
            }
        }

        public static SpriteFont GetSpriteFont(ESpriteFont id)
        {
            switch (id)
            {
                case ESpriteFont.FontMenu:
                    return fontMenu;

                case ESpriteFont.FontNormal:
                    return fontChar;
                case ESpriteFont.FontHeading:
                    return fontHeading;

                default:
                    return null;
            }
        }
    }
}
