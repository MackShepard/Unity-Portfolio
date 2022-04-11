using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformerEffectorReverse : MonoBehaviour
{
    private bool isPlayerStayOnPlatform;
    private PlatformEffector2D _currentPlatform;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)&& isPlayerStayOnPlatform && _currentPlatform!=null)
        {
            _currentPlatform.surfaceArc = 0;
        } 
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        PlatformEffector2D platformTemp = collision.gameObject.GetComponent<PlatformEffector2D>();
        if (platformTemp != null)
        {
            isPlayerStayOnPlatform = true;
            _currentPlatform = platformTemp;
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        PlatformEffector2D platformTemp = collision.gameObject.GetComponent<PlatformEffector2D>();
        if (platformTemp != null)
        {
            platformTemp.surfaceArc = 179.9f;
            isPlayerStayOnPlatform = false;
        }
        
        _currentPlatform = null;
        
    }
}
