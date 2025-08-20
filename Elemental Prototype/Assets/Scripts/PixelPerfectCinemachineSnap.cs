using UnityEngine;
using Cinemachine;

[ExecuteAlways]
[RequireComponent(typeof(CinemachineVirtualCamera))]
public class PixelPerfectCinemachineSnap : MonoBehaviour
{
    public int pixelsPerUnit = 10;  // Match your sprites

    private CinemachineVirtualCamera vcam;

    void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void LateUpdate()
    {
        if (vcam == null) return;

        Transform camTransform = vcam.VirtualCameraGameObject.transform;
        Vector3 pos = camTransform.position;

        // World size of one pixel based on camera orthographic size and native vertical resolution
        float orthoSize = vcam.m_Lens.OrthographicSize;
        float nativeResolutionY = 360f; // matches Step 1
        float pixelSize = (2f * orthoSize) / nativeResolutionY;

        // Snap X/Y
        pos.x = Mathf.Round(pos.x / pixelSize) * pixelSize;
        pos.y = Mathf.Round(pos.y / pixelSize) * pixelSize;

        camTransform.position = pos;
    }
}
