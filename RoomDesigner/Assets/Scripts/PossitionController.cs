using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PossitionController : MonoBehaviour
{
    Vector3 pos;
    Rigidbody rig;
    bool isColliding = false;
    public Vector3 startPosition;
    public Vector3 objectRotation;
    GameObject collisionObject;
    public int colorCode = 1;

    private void Awake()
    {
        colorCode = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
        {
            if (gameObject.tag.Equals("Furniture"))
            {
                rig.constraints = RigidbodyConstraints.None;
                rig.constraints = RigidbodyConstraints.FreezeRotation;
            }
            else if(gameObject.tag.Equals("WallLight"))
            {
                rig.constraints = RigidbodyConstraints.None;
                rig.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
            }
            else if (gameObject.tag.Equals("Rigid"))
            {
                rig.constraints = RigidbodyConstraints.FreezeAll;
            }
            if (!isColliding)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPosition, 1f);
            }
            else
            {

                if (Vector3.Distance(transform.position, collisionObject.transform.position) > 6f)
                {
                    isColliding = false;
                    collisionObject = null;


                }
            }
            transform.rotation = Quaternion.Euler(objectRotation);
            pos = transform.position;
            if (gameObject.tag.Equals("Furniture"))
            {
                
                if (transform.position.y < 2f)
                {
                    pos.y = 2f;
                    transform.position = pos;
                }
                if (transform.position.y > 2f)
                {
                    pos.y = 2f;
                    transform.position = pos;
                }
            }
            if (gameObject.tag.Equals("CellingLight"))
            {
                if(transform.position.y> 6.778f || transform.position.y < 6.778f)
                {
                    pos.y = 6.778f;
                    transform.position = pos;
                }
            }


            
        }
        else
        {
            if (gameObject.tag.Equals("Rigid"))
            {
                rig.constraints = RigidbodyConstraints.None;
            }
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Wall"))
        {
            isColliding = true;
            collisionObject = collision.gameObject;
        }
    }

}
