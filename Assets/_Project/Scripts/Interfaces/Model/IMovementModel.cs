namespace _Project.Scripts.Interfaces
{
    public interface IMovementModel
    {
        float MoveSpeed { get; set; }
        float RotationSpeed { get; set; }
        float Gravity { get; set; }
    }
}