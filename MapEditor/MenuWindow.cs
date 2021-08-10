using CaveGenerator;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using TheGame.UI;

namespace MapEditor
{
  class MenuWindow : Window
  {
    UIControlManager UIManager;
    TileTextureMapper Mapper;

    private Action GetButtonAction(TileType type) => () => { Mouse.SetCursor(MouseCursor.FromTexture2D(Mapper.Get(type), 0, 0)); Info.Tile = type; };

    public MenuWindow(int x, int y, int width, int height, GraphicsDevice graphics, TileTextureMapper mapper) : base(x, y, width, height, graphics)
    {
      UIManager = new UIControlManager();
      Mapper = mapper;

      var MushroomButton      = new Button(new Vector2(32,      0),   image: Assets.MushroomTileTexture, offset: Position, click: GetButtonAction(new MushroomTile()));
      var MushroomFloorButton = new Button(new Vector2(128,     0),   image: Assets.MushroomFloorTileTexture, offset: Position, click: GetButtonAction(new MushroomFloorTile()));
      var LavaButton          = new Button(new Vector2(32,      64),  image: Assets.MagmaTileTexture, offset: Position, click: GetButtonAction(new MagmaTile()));
      var LavaFloorButton     = new Button(new Vector2(128,     64),  image: Assets.MagmaFloorTileTexture, offset: Position, click: GetButtonAction(new MagmaFloorTile()));
      var IceButton           = new Button(new Vector2(32,      128), image: Assets.IceTileTexture, offset: Position, click: GetButtonAction(new IceTile()));
      var IceFloorButton      = new Button(new Vector2(128,     128), image: Assets.IceFloorTileTexture, offset: Position, click: GetButtonAction(new IceFloorTile()));
      var FleshButton         = new Button(new Vector2(32,      192), image: Assets.FleshTileTexture, offset: Position, click: GetButtonAction(new FleshTile()));
      var FleshFloorButton    = new Button(new Vector2(128,     192), image: Assets.FleshFloorTileTexture, offset: Position, click: GetButtonAction(new FleshFloorTile()));
      var WallButton          = new Button(new Vector2(32,      256), image: Assets.WallTileTexture, offset: Position, click: GetButtonAction(new WallTile()));
      var FloorButton         = new Button(new Vector2(128,     256), image: Assets.FloorTileTexture, offset: Position, click: GetButtonAction(new FloorTile()));
      
      UIManager.AddControl(MushroomButton     );
      UIManager.AddControl(MushroomFloorButton);
      UIManager.AddControl(LavaButton         );
      UIManager.AddControl(LavaFloorButton    );
      UIManager.AddControl(IceButton          );
      UIManager.AddControl(IceFloorButton     );
      UIManager.AddControl(FleshButton        );
      UIManager.AddControl(FleshFloorButton   );
      UIManager.AddControl(WallButton         );
      UIManager.AddControl(FloorButton        );
    }

    public override void Update(float time)
    {
      UIManager.Update(time);
    }

    protected override void RenderSimple(SpriteBatch batch)
    {
      UIManager.Render(batch);
    }
  }
}
