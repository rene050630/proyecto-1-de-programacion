    public enum Powers{sprint, fortress, clock, ghost, jump, recoil, trapper, destroyer, backwards, exchange};
    public class Token
    {
        public string name {get; set;}
        public int Speed {get; set;}
        public int Cooldown {get;set;}
        public Powers TokenPower;
        public bool IsActive;
        public (int, int) SafePos;
        public int Recharge;
        public int Number;
        public (int, int) position;
        public string Emoji;
        public Token (string name, int Speed, int Cooldown, Powers TokenPower ,int Number, string Emoji)
        {
            this.name = name;
            this.Speed = Speed;
            this.Cooldown = Cooldown;
            this.Number = Number;
            this.TokenPower = TokenPower;
            Recharge = 0;
            position = (0, 0);
            SafePos = (0,0);
            this.Emoji = Emoji;
        }
        public bool EstaEnPos(int x, int y)
        {
            if (position.Item1 == x && position.Item2 == y) return true;
            return false;
        }
        public void SetToken(int x, int y)
        {
            position.Item1 = x;
            position.Item2 = y;
        }
    }

