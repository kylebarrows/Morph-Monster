public class FormMovementInfo
{
    public float moveSpeed;

    public float jumpHeight;

    public void Copy(FormMovementInfo MovementInfo)
    {
        moveSpeed = MovementInfo.moveSpeed;
        jumpHeight = MovementInfo.jumpHeight;
    }
}
