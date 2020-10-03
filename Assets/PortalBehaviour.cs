using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PortalBehaviour : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portalCamera;
    public Transform portal;
    public Transform otherPortal;

    private void Update()
    {
        //Get the position of the player in otherPortal space
        Vector3 localPlayerPosition = otherPortal.InverseTransformPoint(playerCamera.position);
        Debug.Log("localPlayerPosition = " + localPlayerPosition);
        //Vector3 oppositeNormalOfOtherPortal = otherPortal.GetComponent<Plane>().normal * -1;
        
        //evt. hier auch forward und nicht invertieren des reflect vectors.
        Vector3 oppositeNormalOfOtherPortal = otherPortal.transform.up;
        Vector3 reflectedPosition = -Vector3.Reflect(localPlayerPosition, oppositeNormalOfOtherPortal);
        transform.position = portal.TransformPoint(reflectedPosition);
        
        //Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
        //transform.position = portal.position + playerOffsetFromPortal;

        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(portal.rotation, otherPortal.rotation);
        Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, -Vector3.up);
        Vector3 newCameraDirection = portalRotationDifference * -playerCamera.forward;
        newCameraDirection = new Vector3(newCameraDirection.x, -newCameraDirection.y, newCameraDirection.z);
        transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
    }

}