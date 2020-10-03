using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PortalBehaviour : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;

    private void Update()
    {
        //Get the position of the player in otherPortal space
        Vector3 localPlayerPosition = otherPortal.InverseTransformPoint(playerCamera.position);
        
        //We take the position of the player in the local space of the second portal and apply the inverse to the camera position
        //of the first portal
        Vector3 oppositeNormalOfOtherPortal = otherPortal.transform.up;
        //We reflect at the negative normal of the portal surface
        Vector3 reflectedPosition = -Vector3.Reflect(localPlayerPosition, oppositeNormalOfOtherPortal);
        transform.position = portal.TransformPoint(reflectedPosition);

        //We take the forward vector of the players camera in the other portals local space and apply the the negative forward direction
        //to this portals camera. Only the y component of the forward vector isn't changed to prevent vertical view inversion.
        Vector3 playerCamForwardLocal = otherPortal.InverseTransformVector(playerCamera.forward);
        Vector3 newForward = -portal.transform.TransformVector(playerCamForwardLocal);
        transform.forward = new Vector3(newForward.x, -newForward.y, newForward.z);
    }

}