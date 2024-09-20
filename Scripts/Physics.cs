float gravityForce = -9.81f;
float fallSpeed;
float jumpHeight = 2f;
bool isGrounded = true;
float velocity = new Vector3(0, 0, 0);
bool jumpInput = true;

class Physics
{
    Gravity();
}

void Gravity()
{
    if (isGrounded && velocity.Y < 0)
    {
        velocity.y = -2f;
    }

    // Finding Velocity When Only Given Height [v = sqrt(2gh)]
    if (isGrounded) // JUMP ACTION
    {
        velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityForce);
    }

    // y = 1/2 * g * t^2 [FREE FALL MOTION]
    velocity.y += gravityForce * fallSpeed * Time.deltaTime;

    controller.Move(velocity * Time.deltaTime);
}