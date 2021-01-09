using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseableObject : MonoBehaviour
{
    private bool active;
    private bool isGrabbed;
    private Camera mainCam;
    private Rigidbody2D ObjectRigidbody;


    public Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        active = true;
        mainCam = Camera.main;
        isGrabbed = false;
        ObjectRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
            if (isGrabbed)
            {
                transform.position = new Vector2(mousePos.x, mousePos.y);
            }
        }
    }

    private void OnMouseDown()
    {
        if (active)
        {
            isGrabbed = true;
        }
    }

    private void OnMouseUp()
    {
        if (active)
        {
            isGrabbed = false;
        }
    }

    public void SetActive(bool value)
    {
        active = value;
    }

    public void ChangeRigidBodyType(string newType)
    {
        if (newType == "Dynamic")
        {
            ObjectRigidbody.bodyType = RigidbodyType2D.Dynamic;
        }
        else if (newType == "Kinematic")
        {
            ObjectRigidbody.bodyType = RigidbodyType2D.Kinematic;
        }
        else if (newType == "Static")
        {
            ObjectRigidbody.bodyType = RigidbodyType2D.Static;
        }
    }
}
