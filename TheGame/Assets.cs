using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TheGame
{
  class Assets
  {
    public static Texture2D placeHolder;
    public static SpriteFont testFont;
    public static SoundEffect Click;

    static public void Load(ContentManager content)
    {
      placeHolder = content.Load<Texture2D>("placeHolder");
      testFont = content.Load<SpriteFont>("Font");
      Click = content.Load<SoundEffect>("Click");
    }
  }
}
