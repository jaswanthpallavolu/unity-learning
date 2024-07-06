using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class AimCameraController : MonoBehaviour
{
    public CinemachineVirtualCamera aimVirtualCamera;
    // public AxisState xAxis, yAxis;
    public float normalSensitivity = 100f;
    public float aimSensitivity = 50f;
    public float speed = 2f;
    [SerializeField] Transform camFollowPos;
    [SerializeField] Transform headAimTarget;
    [SerializeField] private LayerMask aimColliderMask = new LayerMask();
    [SerializeField] private Transform debugTransform;

    [SerializeField] private Transform bulletProjectile;
    [SerializeField] private Transform bulletProjectilePos;

    float currentSensitivity;
    float xRotation;
    float yRotation;
    Vector3 mouseWorldPosition;
    Vector3 aimPosition;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        mouseWorldPosition = Vector3.zero;
        // Vector3 worldAimTarget = mouseWorldPosition;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        aimPosition = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, 20f));
        // xAxis.Update(Time.deltaTime);
        // yAxis.Update(Time.deltaTime);

        ToggleViewCamera();
        ViewCameraMovement();
        // Debug.Log("screenCenterPoint " + screenCenterPoint);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        headAimTarget.position = aimPosition;

        if (Physics.Raycast(ray, out RaycastHit hit, 20f, aimColliderMask))
        {
            debugTransform.position = hit.point;
            mouseWorldPosition = hit.point;
        }
        ShootProjectile();
    }

    void ShootProjectile()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (mouseWorldPosition == Vector3.zero)
            {
                mouseWorldPosition = aimPosition;
            }
            Vector3 aimDirection = (mouseWorldPosition - bulletProjectilePos.position).normalized;
            Instantiate(bulletProjectile, bulletProjectilePos.position, Quaternion.LookRotation(aimDirection, Vector3.up));
            // Instantiate(bulletProjectile, bulletProjectilePos.position, Quaternion.identity);
        }
    }
    void ToggleViewCamera()
    {
        if (Input.GetMouseButton(1))
        {
            aimVirtualCamera.gameObject.SetActive(true);
            currentSensitivity = aimSensitivity;


        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            currentSensitivity = normalSensitivity;
        }
    }
    void ViewCameraMovement()
    {
        // camFollowPos.localEulerAngles = new Vector3(yAxis.Value, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        // transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis.Value, transform.eulerAngles.z);

        float mouseX = Input.GetAxis("Mouse X") * currentSensitivity * speed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * currentSensitivity * speed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -60f, 60f);

        yRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -180f, 180f);

        float angle = camFollowPos.localEulerAngles.y;
        // transform.Rotate(Vector3.up * mouseX);
        // Debug.Log(angle);
        camFollowPos.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (Mathf.Clamp(angle, 30, 320f) != angle)
        {
            Debug.Log(angle + "rotate");
            transform.Rotate(Vector3.up * mouseX);
        }



    }
}
