using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScaling : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float developedHorizontalResolution = 1242;
    [SerializeField] private float developedVerticalResolution = 2688;

    private float initSize;

    private void Awake() {
        initSize = Camera.main.orthographicSize;
    }

    private void OnGUI() {

        float currentAspect = (float) Screen.width / (float) Screen.height;
		Camera.main.orthographicSize = developedHorizontalResolution / currentAspect / (developedVerticalResolution / initSize);
	}
}

