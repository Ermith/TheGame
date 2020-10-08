using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveGenerator {
    public class GeneratorOption {

        public GeneratorOption(int mooreNeighborhood, int treshold, int percentage, int caIterations, TileType primaryTile, TileType secondaryTile) {
            MooreNeighborhood = mooreNeighborhood;
            Treshold = treshold;
            Percentage = percentage;
            CAIterations = caIterations;
            PrimaryTile = primaryTile;
            SecondaryTile = secondaryTile;
        }

        // Static options
        //   Special
        public static GeneratorOption FewMushrooms { get; } = new GeneratorOption(1, 3, 60, 1, TileType.MushroomFloor, TileType.Mushroom);
        public static GeneratorOption Mushrooms { get; } = new GeneratorOption(1, 4, 60, 1, TileType.MushroomFloor, TileType.Mushroom);
        public static GeneratorOption ManyMushrooms { get; } = new GeneratorOption(1, 6, 60, 1, TileType.MushroomFloor, TileType.Mushroom);
        public static GeneratorOption LittleMoss { get; } = new GeneratorOption(1, 4, 70, 2, TileType.MossFloor, TileType.Moss);
        public static GeneratorOption Moss { get; } = new GeneratorOption(1, 6, 80, 1, TileType.MossFloor, TileType.Moss);
        public static GeneratorOption LotOfMoss { get; } = new GeneratorOption(1, 7, 80, 1, TileType.MossFloor, TileType.Moss);
        public static GeneratorOption LittleIce { get; } = new GeneratorOption(2, 14, 50, 2, TileType.Ice, TileType.IceFloor);
        public static GeneratorOption Ice { get; } = new GeneratorOption(2, 17, 80, 3, TileType.Ice, TileType.IceFloor);
        public static GeneratorOption LotOfIce { get; } = new GeneratorOption(1, 6, 80, 1, TileType.Ice, TileType.IceFloor);
        public static GeneratorOption FewMagmaPools { get; } = new GeneratorOption(2, 10, 28, 2, TileType.Magma, TileType.MagmaFloor);
        public static GeneratorOption MagmaPools { get; } = new GeneratorOption(2, 16, 80, 3, TileType.Magma, TileType.MagmaFloor);
        public static GeneratorOption BigMagmaPools { get; } = new GeneratorOption(2, 11, 48, 3, TileType.Magma, TileType.MagmaFloor);
        public static GeneratorOption HUGEMagmamaPools { get; } = new GeneratorOption(2, 10, 50, 3, TileType.Magma, TileType.MagmaFloor);
        public static GeneratorOption ComfortFlesh { get; } = new GeneratorOption(1, 1, 5, 0, TileType.Flesh, TileType.FleshFloor);
        public static GeneratorOption DangeraousFlesh { get; } = new GeneratorOption(1, 1, 20, 0, TileType.Flesh, TileType.FleshFloor);
        public static GeneratorOption NightmareFlesh { get; } = new GeneratorOption(1, 1, 30, 0, TileType.Flesh, TileType.FleshFloor);
        //   Basic
        public static GeneratorOption BasicRock1 { get; } = new GeneratorOption(3, 23, 47, 3, TileType.Rock, TileType.Floor);
        public static GeneratorOption BasicRock2 { get; } = new GeneratorOption(2, 11, 43, 4, TileType.Rock, TileType.Floor);

        // Actual Options
        public int MooreNeighborhood { get; set; }
        public int Treshold { get; set; }
        public int Percentage { get; set; }
        public int CAIterations { get; set; }
        public TileType PrimaryTile { get; set; }
        public TileType SecondaryTile { get; set; }
    }
}
