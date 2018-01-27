using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    public Vector3 velocity;
    public float xForce = 0.0F;
    public float yForce = 0.0F;
    public float maxForce = 30.0F;
    string b_state = "Flying";
    public GameObject colliderSpawn;

    float rotationX = 0F;
    float rotationY = 0F;

    private List<float> rotArrayX = new List<float>();
    float rotAverageX = 0F;

    private List<float> rotArrayY = new List<float>();
    float rotAverageY = 0F;

    public float frameCounter = 20;

    Quaternion originalRotation;

    void BatState()
    {
        string b_state = "Flying";

        if (Input.GetMouseButton(0))
        {
            b_state = "Landing";
        }
        
        switch (b_state)
        {
                case "Flying":
                    Flying();
                    break;
                case "Landing":
                    Landing();
                    break;
                case "Landed":
                    Landed();
                    break;
                case "Sonaring":
                    Sonaring();
                    break;
        }
    }

    void Landed()
    {
            
    }

    void Sonaring()
    {
        
    }

    void Landing()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray;
        
            if (Physics.Raycast(transform.position, transform.forward, out hit, 3000.0F))
            {
                xForce = 0;
                yForce = 0;
                Transform objectHit = hit.transform;
                Debug.DrawRay(transform.position, transform.forward,Color.yellow,1000000);
                transform.position = Vector3.MoveTowards(transform.position, hit.point, 200 * Time.deltaTime);
                // GameObject.Instantiate(colliderSpawn,hit.point,Quaternion(0,0,0,0))
                if (transform.position == objectHit.position)
                {
                    
                }
                // Maybe add cool marker feature
                // GameObject.Instantiate(InstanciatedObject,hit.point,InstanciatedObject.transform.rotation);
                
            }
        }
    }

    void Flying()
    {
        xForce += Input.GetAxisRaw("Horizontal");
        yForce += Input.GetAxisRaw("Vertical");
        xForce = xForce / 1.1F;
        yForce = yForce / 1.1F;


        if (xForce > maxForce)
        {
            xForce = maxForce;
        }
        if (yForce > maxForce)
        {
            yForce = maxForce;
        }

        var input = new Vector3(xForce, 0.0f, yForce);
        //var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        input = transform.TransformVector(input);
        //velocity += (input * Time.deltaTime);

        transform.position += input * Time.deltaTime;

        if (axes == RotationAxes.MouseXAndY)
        {
            rotAverageY = 0f;
            rotAverageX = 0f;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;

            rotArrayY.Add(rotationY);
            rotArrayX.Add(rotationX);

            if (rotArrayY.Count >= frameCounter)
            {
                rotArrayY.RemoveAt(0);
            }
            if (rotArrayX.Count >= frameCounter)
            {
                rotArrayX.RemoveAt(0);
            }

            for (int j = 0; j < rotArrayY.Count; j++)
            {
                rotAverageY += rotArrayY[j];
            }
            for (int i = 0; i < rotArrayX.Count; i++)
            {
                rotAverageX += rotArrayX[i];
            }

            rotAverageY /= rotArrayY.Count;
            rotAverageX /= rotArrayX.Count;

            rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);
            rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

            Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);

            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotAverageX = 0f;

            rotationX += Input.GetAxis("Mouse X") * sensitivityX;

            rotArrayX.Add(rotationX);

            if (rotArrayX.Count >= frameCounter)
            {
                rotArrayX.RemoveAt(0);
            }
            for (int i = 0; i < rotArrayX.Count; i++)
            {
                rotAverageX += rotArrayX[i];
            }
            rotAverageX /= rotArrayX.Count;

            rotAverageX = ClampAngle(rotAverageX, minimumX, maximumX);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotAverageX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotAverageY = 0f;

            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotArrayY.Add(rotationY);

            if (rotArrayY.Count >= frameCounter)
            {
                rotArrayY.RemoveAt(0);
            }
            for (int j = 0; j < rotArrayY.Count; j++)
            {
                rotAverageY += rotArrayY[j];
            }
            rotAverageY /= rotArrayY.Count;

            rotAverageY = ClampAngle(rotAverageY, minimumY, maximumY);

            Quaternion yQuaternion = Quaternion.AngleAxis(rotAverageY, Vector3.left);
            transform.localRotation = originalRotation * yQuaternion;
        }
    }

    void Update()
    {
       BatState();
    }

    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb)
            rb.freezeRotation = true;
        originalRotation = transform.localRotation;
        Cursor.visible = false;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        angle = angle % 360;
        if ((angle >= -360F) && (angle <= 360F))
        {
            if (angle < -360F)
            {
                angle += 360F;
            }
            if (angle > 360F)
            {
                angle -= 360F;
            }
        }
        return Mathf.Clamp(angle, min, max);
    }
}
