using UnityEngine;

public class Movement : MonoBehaviour
{


    public float forwardAccel = 8f;
    public float reverseAccel = 4f;
    public float maxSpeed = 50f;
    public float turnStrength = 80f;
    public float driftturnStrength = 180f;
    public float jumpForce = 500f;
    public int jumps = 1;
    public bool isGrounded = false;
    private float speedInput;
    private float turnInput;
    public float boostForce = 1000f;


    public float driftMultiplier = 1.5f;
    public float driftForce = 5f;
    private bool isDrifting = false;


    public Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.transform.parent = null;
        rb.linearDamping = 2f;
        
    }

    // Update is called once per frame
    void Update()
    {
        speedInput = 0f;
        transform.position = rb.transform.position;
        jumping();
        if (isGrounded == true)
        {
            
        }
        inputs();
        Debug.Log(speedInput);
       


    }
    private void FixedUpdate()
    {
        if(Mathf.Abs(speedInput)> 0)
        {
            rb.AddForce(transform.forward * speedInput * Time.deltaTime);
        }

        if (isDrifting)
        {
            Vector3 driftDirection = transform.right * turnInput;
            rb.AddForce(driftDirection * driftForce * Time.deltaTime, ForceMode.Acceleration);
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
           isGrounded = true;
            jumps = 1;
        }

        if (other.CompareTag("BoostPad"))
        {
            rb.AddForce(transform.forward * boostForce * 50 * Time.deltaTime);
            
        }
    }
    void jumping()
    {
       
         if (jumps > 0 && Input.GetKeyDown(KeyCode.Space)) 
         {
            rb.AddForce(Vector3.up * jumpForce);
            jumps = 0;
        
         }

       
        
           
    }
    void inputs()
    {
        
        if(Input.GetAxis("Vertical") > 0)
        {
            speedInput = Input.GetAxis("Vertical") * forwardAccel * 1000f;

        }else if (Input.GetAxis("Vertical") < 0)
        {
            speedInput = Input.GetAxis("Vertical") * reverseAccel * 1000f;
        }
       


        turnInput = Input.GetAxis("Horizontal");
        
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, turnInput * turnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0));


        if (Input.GetKey(KeyCode.LeftShift)) 
        {
            isDrifting = true;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, turnInput * driftturnStrength * driftMultiplier * Time.deltaTime));

        }
        else
        {
            isDrifting = false;
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, turnInput * turnStrength * Time.deltaTime, 0));
        }

    }

   

}
