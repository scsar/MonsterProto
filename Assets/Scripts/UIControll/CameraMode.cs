using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static OrthoParallax;

public class CameraMode : MonoBehaviour
{
    public enum CameraModeType
    {
        Mode1,
        Mode2,
        Mode3,
    }
    public CameraModeType currentmode;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        currentmode = CameraModeType.Mode1;
    }
    public void changeCameraModeType1()
    {
        currentmode = CameraModeType.Mode1;
    }
    public void changeCameraMode2()
    {
        currentmode = CameraModeType.Mode2;
    }
    public void changeCameraMode3()
    {
        currentmode = CameraModeType.Mode3;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentmode == CameraModeType.Mode1)
        {
            CameraMove1();
        }
        else if (currentmode == CameraModeType.Mode2)
            CameraMove2();
        else
            CameraMove3();
    }
    private void CameraMove1()
    {
        Vector3 newPosition = new Vector3(player.position.x, transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
    private void CameraMove2() {
        Debug.Log("2·Î");
        Vector3 newPosition = new Vector3(player.position.x, player.position.y, 0);
        transform.position = newPosition;
    }
    private void CameraMove3(){


    }
}
