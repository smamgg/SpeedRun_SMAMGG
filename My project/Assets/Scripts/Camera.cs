using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform followTarget;
    public PlayerSlime player;
    private UnityEngine.Camera mainCam;

    public float baseSize = 60f;        // 기본 줌 크기
    public float zoomPerAccel = 1.12f;  // 가속도 1당 줌 증가량
    public float zoomLerpSpeed = 3f;    // 줌 반응 속도
    public float minZoom = 50f;         // 최소 줌 (가까움)
    public float maxZoom = 90f;         // 최대 줌 (멀어짐)


    private void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        
    }

    void LateUpdate()
    {
        transform.position = followTarget.position;


        float targetFov = baseSize + player.accel * zoomPerAccel;
        targetFov = Mathf.Clamp(targetFov, minZoom, maxZoom);


        mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, targetFov, Time.deltaTime * zoomLerpSpeed);
    }
}