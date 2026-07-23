namespace FrameDataApp.Services
{
    public class ServiceStore
    {
        private static ServiceStore? _instance;
        public static ServiceStore Instance => _instance ??= new ServiceStore();

        public CharacterService CharacterService { get; }
        public GameService GameService { get; }
        public MoveService MoveService { get; }

        public ServiceStore()
        {
            // 1. Instantiated once so character state is shared across Game and Move services
            CharacterService = new CharacterService();
            GameService = new GameService(CharacterService);
            MoveService = new MoveService(CharacterService);

            // Optional: Seed default data
            CharacterService.SeedDefaultData();
            MoveService.SeedDefaultMoveData();
        }
    }
}