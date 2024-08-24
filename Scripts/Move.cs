public Move
{
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");
    // To move a game object
    // method 1
    Vector3 move = transform.forward * z + transform.right * x;
    controller.Move(move * Time.deltaTime);

    //


}