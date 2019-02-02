using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
// Class Reference
    [SerializeField] private Core rCore;
    [SerializeField] private MapGenerator rMap;
	[SerializeField] private Rigidbody rBody;
    [SerializeField] private Camera rCamera;
    [SerializeField] private Transform tTornado;

// Movement
    [SerializeField] private string sMoveX;
    [SerializeField] private string sMoveZ;
    [SerializeField] private float fSpeed;
    private float fMoveX;
    private float fMoveZ;

// Jump
    // private bool bJump;
    // private bool bInAir;

// Out of Bounce
    [SerializeField] private Transform tWater;
    private Vector3 v3Last;
    private bool bOutofBounds;

	private void Start() {
	// Set Initial Variables
        // bInAir = false;
    // Set Player to ignore water collision
        Physics.IgnoreCollision(tWater.GetComponent<Collider>(), GetComponentInChildren<Collider>());
	}
	
    private void FixedUpdate() {
    // Check if game is paused
        if (rCore.bPause != true) {
        // Get Axis Input
            fMoveX = Input.GetAxis(sMoveX);
            fMoveZ = Input.GetAxis(sMoveZ);

        // Shift Player closer to tornado

        // Get Jump Input (Disabled due to not used)
            // if (bInAir != true)
            //     if (Input.GetKeyDown(KeyCode.Space))
            //         bJump = true;
        
        // Check if Player is in the air
            // if (transform.position.y > 0.1f) bInAir = true;
            // else                             bInAir = false;
        
        // Apply Axis Movement Input
            Vector3 v3Move = new Vector3(fMoveX, 0, fMoveZ) * (Time.deltaTime * fSpeed);
            transform.forward = new Vector3(rCamera.transform.forward.x, 0, rCamera.transform.forward.z);
            transform.Translate(v3Move, Space.Self);

        // Apply Jump Movement Input
            // if (bJump) {
            // // unset jump
            //     bJump = false;
            //     if (rBody != null) {
            //         Vector3 v3Jump = transform.up * fSpeed;
            //         rBody.AddForce(v3Jump, ForceMode.Impulse);
            //     }
            // }
        
        // Get Player's last closest point
            if ((transform.position.x <  (rMap.GetIslandSize().x / 2.0f) && transform.position.z <  (rMap.GetIslandSize().y / 2.0f)) &&
				(transform.position.x > -(rMap.GetIslandSize().x / 2.0f) && transform.position.z > -(rMap.GetIslandSize().y / 2.0f)))
                    v3Last = new Vector3(transform.position.x, 0, transform.position.z);

        // Check last closest point then displace last marked point
			if (v3Last.x > 0 || v3Last.x == 0)
				v3Last.x = Mathf.Floor(v3Last.x) + 0.5f;
			else if (v3Last.x < 0)
				v3Last.x = Mathf.Ceil(v3Last.x)  - 0.5f;

			if (v3Last.z > 0 || v3Last.z == 0)
				v3Last.z = Mathf.Floor(v3Last.z) + 0.5f;
			else if (v3Last.z < 0)
				v3Last.z = Mathf.Ceil(v3Last.z)  - 0.5f;

        // Check if the Player is on the Base Plate or not
			if (transform.position.y < (tWater.position.y - rCore.BoundLengths(transform).y))	bOutofBounds = true;
			else																				            bOutofBounds = false; 
        
        // Set Player back on the ground to the last estimated point
			if (bOutofBounds == true)
				transform.position = v3Last;
        }
    }
}
