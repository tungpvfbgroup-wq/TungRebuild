namespace Tung.Modules.Player.Application
{
    public readonly struct PlayerReadModel
    {
        public float MaxHealth { get; }
        public float CurrentHealth { get; }
        public float MoveVelocityX { get; }
        public float MoveVelocityY { get; }
        public bool IsMoving { get; }
        public bool IsDead { get; }
        public PlayerReadModel(float maxHealth, float currentHealth, float moveVelocityX,
         float moveVelocityY, bool isMoving, bool isDead)
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
            MoveVelocityX = moveVelocityX;
            MoveVelocityY = moveVelocityY;
            IsMoving = isMoving;
            IsDead = isDead;
        }
    }
}