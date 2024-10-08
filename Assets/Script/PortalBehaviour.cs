﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PortalBehaviour : MonoBehaviour
{
    public Transform playerCamera;
    public Transform portal;
    public Transform otherPortal;
    public Transform portalCamera;

    private bool playerIsOverlapping = false;
    private float lastTeleportTime = 0;
    private Transform player;

    public bool isActive = true;

    private void Start()
    {
        player = playerCamera.transform.parent;
    }


    private void Update()
    {
        //Get the position of the player in otherPortal space
        Vector3 localPlayerPosition = otherPortal.InverseTransformPoint(playerCamera.position);
        
        //We take the position of the player in the local space of the second portal and apply the inverse to the camera position
        //of the first portal
        Vector3 oppositeNormalOfOtherPortal = otherPortal.transform.up;
        //We reflect at the negative normal of the portal surface
        Vector3 reflectedPosition = -Vector3.Reflect(localPlayerPosition, oppositeNormalOfOtherPortal);
        portalCamera.transform.position = portal.TransformPoint(reflectedPosition);

        //We take the forward vector of the players camera in the other portals local space and apply the the negative forward direction
        //to this portals camera. Only the y component of the forward vector isn't changed to prevent vertical view inversion.
        Vector3 playerCamForwardLocal = otherPortal.InverseTransformVector(playerCamera.forward);
        Vector3 newForward = -portal.transform.TransformVector(playerCamForwardLocal);
        portalCamera.transform.forward = new Vector3(newForward.x, -newForward.y, newForward.z);

        if(lastTeleportTime <= Time.time - 0.5f)
        {
            otherPortal.gameObject.GetComponentInChildren<PortalBehaviour>().isActive = true;
        }

        if (isActive && playerIsOverlapping)
        {
            otherPortal.gameObject.GetComponentInChildren<PortalBehaviour>().isActive = false;
            lastTeleportTime = Time.time;
            CharacterController controller = playerCamera.GetComponentInParent<CharacterController>();
            controller.enabled = false;
            /*localPlayerPosition = portal.InverseTransformPoint(playerCamera.position);
            oppositeNormalOfOtherPortal = portal.transform.up;
            reflectedPosition = -Vector3.Reflect(localPlayerPosition, oppositeNormalOfOtherPortal);*/
            //player.position = otherPortal.TransformPoint(reflectedPosition);

            Vector3 portalToPlayer = player.position - portal.position;
            float dotProduct = Vector3.Dot(portal.forward, portalToPlayer);

            //Debug.Log("Dot: " + dotProduct);

            if(dotProduct < 0f)
            {
                float rotationDiff = 0f;
                /*if (portal.rotation.eulerAngles.y > otherPortal.rotation.eulerAngles.y)
                {
                    rotationDiff = Quaternion.Angle(portal.rotation, otherPortal.rotation);
                }
                else
                {
                    rotationDiff = -Quaternion.Angle(portal.rotation, otherPortal.rotation);
                }*/
                rotationDiff = otherPortal.rotation.eulerAngles.y - portal.rotation.eulerAngles.y;
                //Debug.Log("Euler: " + otherPortal.rotation.eulerAngles.y);
                //rotationDiff = Math.Abs(rotationDiff);
                rotationDiff += 180;//- otherPortal.rotation.eulerAngles.y;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                player.position = otherPortal.position + positionOffset;
                controller.enabled = true;
                playerIsOverlapping = false;
            }

            /*Vector3 playerForwardLocal = portal.InverseTransformVector(player.forward);
            newForward = -otherPortal.transform.TransformVector(playerForwardLocal);
            player.transform.forward = new Vector3(newForward.x, -newForward.y, newForward.z);
            */

            controller.enabled = true;
            playerIsOverlapping = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            playerIsOverlapping = false;
        }
    }

}