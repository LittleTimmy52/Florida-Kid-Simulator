using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point2PlayerCamController : MonoBehaviour
{
    #region PUBLIC FEILDS

    [Header("Mouse Lock")] public bool isMouseLocked;
    /*[Header("Camera Feild of Veiw")] public bool isFeildOfViewEnambled;
    public float cameraFeildOfViewMin;
    public float cameraFeildOfViewMax;
    public float feildOfViewIncrement;*/
    [Header("Camera Rotate X Settings")] public float cameraRotateXMin;
    public float cameraRotateXMax;
    [Header("Mouse Smoothing")] public float mouseSmooth;
    [Header("Y Cam Rotate")] public float yCamRotate;

    #endregion

    #region PRIVATE FILDS
        
    private float m_mouseX;
    private float m_mouseY;
    private float m_rotateX;
    //private float m_mouseScroll;
    private Transform m_parent;
    //private float m_feildOfView;
    private Camera m_camera;
    private Point2GameManager gM;

    #endregion

    #region UNITY ROUTINES

    private void Awake()
    {
        #region INITALIZE FEILDS

        m_camera = gameObject.GetComponent<Camera>();
        m_parent = transform.parent;

        gM = GameObject.Find("Game Manager").GetComponent<Point2GameManager>();

        /*if (m_camera != null)
        {
            m_feildOfView = m_camera.fieldOfView;
        }*/

        #endregion
    }

    private void Update()
    {
        mouseSmooth = PlayerPrefs.GetFloat("Sensitivity");

        MouseInputs();

        if (gM.isGameActive == true)
        {
            RotatePlayerY();
            RotateCameraX();
            //CameraZoom();

            isMouseLocked = true;
        }else
        {
            isMouseLocked = false;
        }

        MouseLock();
    }

    #endregion

    #region HELPER ROUTINES

    private void MouseInputs()
    {
        #region  COLLECT MOUSE INPUTS

        m_mouseX = Input.GetAxis("Mouse X") * mouseSmooth;
        m_mouseY = Input.GetAxis("Mouse Y") * mouseSmooth;
        //m_mouseScroll = Input.GetAxis("Mouse ScrollWheel");

        #endregion
    }

    private void RotatePlayerY()
    {
        #region  ROTATE PLAYER Y

        m_parent.Rotate(Vector3.up * m_mouseX);

        #endregion
    }

    private void RotateCameraX()
    {
        #region  ROTATE CAMERA X AXIS
        
        m_rotateX += m_mouseY;
        m_rotateX = Mathf.Clamp(m_rotateX, cameraRotateXMin, cameraRotateXMax);
        m_camera.transform.localRotation = Quaternion.Euler(-m_rotateX, yCamRotate, 0f);

        #endregion
    }

    private void MouseLock()
    {
        #region  LOCK MOUSE COURSER

        if (isMouseLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            return;
        }
        Cursor.lockState = CursorLockMode.None;

        #endregion
    }

    /*private void CameraZoom()
    {
        if (isFeildOfViewEnambled)
        {
            if (m_mouseScroll > 0.0f)
            {
                if (m_feildOfView + feildOfViewIncrement >= cameraFeildOfViewMin && m_feildOfView + feildOfViewIncrement <= cameraFeildOfViewMax)
                {
                    m_feildOfView += feildOfViewIncrement;
                    m_camera.fieldOfView = m_feildOfView;
                }
            }

            if (m_mouseScroll < 0.0f)
            {
                if (m_feildOfView - feildOfViewIncrement >= cameraFeildOfViewMin && m_feildOfView - feildOfViewIncrement <= cameraFeildOfViewMax)
                {
                    m_feildOfView -= feildOfViewIncrement;
                    m_camera.fieldOfView = m_feildOfView;
                }
            }
        }
    }*/

    #endregion
}
