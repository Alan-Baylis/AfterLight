using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //MonoBehaviour has functions such as Awake() and FixedUpdate()
    public float speed = 6f;
    Vector3 movement; // used to store the type of movement to the player
   // Animator anim;
    Rigidbody playerRigidBody;
    int floorMask;
    public bool isHasMouseControl = true;
    float camRayLength = 100; //length of the ray we cast from the camera

    // called regardless if script is enabled or not
    void Awake()
    {
        //setup floor mask
        floorMask = LayerMask.GetMask("Floor"); //"Floor" is the name of the layer were going to get
        // get reference to the animator
        //anim = GetComponent<Animator>();
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // automatically called every phisics update or "physics step"
    void FixedUpdate()
    {
        //get input from the horizontal and vertical axis
        float h = Input.GetAxisRaw("Horizontal"); // "Horizontal being the name of the input, default name in unity, get there by edit/projectsettings/input
        float v = Input.GetAxisRaw("Vertical");
        //call all the functions we created
        Move(h, v);
        if(isHasMouseControl)Turning();
        Animating(h, v);


    }
    void Move(float h, float v)
    {
        //set our Vector3 variable
        movement.Set(h, 0f, v); // X and Z are flat on the ground, 0f for Y
        // using the normalized function it will prevent the character from walking faster when combining the X and Z (diagnonally)
        // it will set the max the veteor can be to 1 times our speed variable
        // delta time is the time between each update call, we need to multiply by this or else this function would make our character
        // move 50 times a second!
        movement = movement.normalized * speed * Time.deltaTime;
        // apply the movement to the player
        //rigidbody function MovePosition moves the rigidbody to a position in world space
        //transform.position means were moving realative to where the character is
   
        playerRigidBody.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;
        //now cast the ray we created, it will return true or false depending if it hit something
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) // out means were going to get information "out" of this function
                                                                            //;camRayLenght is how far the ray will extend, last variable is the layerMask that the ray will hit
        {

            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f; //make sure the character does not move on the Y axis!!!
            //use a quaternian to store the rotation of the character, you cannot use a Vector 3
            Quaternion newRotation = Quaternion.LookRotation(playerToMouse); //LookRotation sets the Quaternian to the Vector3 location playerToMouse
            //now apply the rotation
            //MoveRotation rotates the rigidbody to rotation
            playerRigidBody.MoveRotation(newRotation);
        }
    }

    void Animating(float h, float v)
    {
        bool walking = h != 0f || v != 0f; // walking is true if the players h or v input does not equal 0
        //pass this variable to the animator component
        //anim.SetBool("IsWalking", walking);
    }
   
}
