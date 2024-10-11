using UnityEngine;

public class groundCheck : MonoBehaviour
{
    public Rigidbody Grb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Grb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
