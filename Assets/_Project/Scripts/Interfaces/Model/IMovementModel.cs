namespace _Project.Scripts.Interfaces.Model
{
    public interface IMovementModel
    {
        float MoveSpeed { get; set; }
        float RotationSpeed { get; set; }
        float Gravity { get; set; }
    }
}