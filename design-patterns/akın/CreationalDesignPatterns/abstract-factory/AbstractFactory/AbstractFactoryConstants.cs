namespace AbstractFactory;
public static class AbstractFactoryConstants {

    public struct Elf {
        public const String KingName = "Thranduil";
        public const String CastleDescription = "A majestic palace built amidst the trees, shimmering with ethereal light.";
        public const Int32 ArmySize = 8000;
    }

    public struct Human {
        public const String KingName = "Thorin Oakenshield";
        public const String CastleDescription = "A grand fortress of stone and steel, its banners unfurling proudly.";
        public const Int32 ArmySize = 10000;
    }

    public struct Orc {
        public const String KingName = "Bolg";
        public const String CastleDescription = "A dark and brutal stronghold, reeking of iron and blood.";
        public const Int32 ArmySize = 15000;
    }
}