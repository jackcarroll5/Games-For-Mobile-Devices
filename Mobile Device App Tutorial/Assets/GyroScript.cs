using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroScript : MonoBehaviour
{
    private Quaternion origin = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {
        getGyroOrigin();
        Input.gyro.enabled = true;
    }

    private void getGyroOrigin()
    {
        origin = Input.gyro.attitude;
    }

    private Quaternion ConvertRightToLeftQuaternion (Quaternion rightQuaternion)
    {
        return new Quaternion(-rightQuaternion.x, -rightQuaternion.z, -rightQuaternion.y,
            rightQuaternion.w);
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = ConvertRightToLeftQuaternion(Quaternion.Inverse(origin)
            * Input.gyro.attitude);
    }
}
