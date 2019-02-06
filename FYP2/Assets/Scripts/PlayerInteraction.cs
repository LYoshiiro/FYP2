using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour {
// Class Reference
    [SerializeField] private Core rCore;
    [SerializeField] private PlayerCamera rPlayerCamera;
	[SerializeField] private PlayerInventory rPlayerInventory;
	[SerializeField] private PlayerStatus rPlayerStatus;
	[SerializeField] private ItemManager rItemManager;
	[SerializeField] private MapGenerator rMapGenerator;
	[SerializeField] private SkillSystem rSkillSystem;
	[SerializeField] private CursorIndicator rCursorIndicator;
	[SerializeField] private UI rUI;

// Facing Direction
	private RaycastHit hit;
	private Ray ray;

// Interaction
    public float fBounceTime;
    private float fBouncePress;
	[SerializeField] private float fSpeedModifier;
	public bool bProgressBar;
	public bool bTool;
	public string sPlacing;

	private void Start() {
	// Set Initial Values
		bProgressBar = false;
		bTool = false;
		sPlacing = string.Empty;
	}

    private void FixedUpdate() {
 // Check if game is paused
        if (rCore.bPause != true) {
		// // If game isn't paused, update elapse time
		// 	fBounceTime += Time.deltaTime;
        // Set Ray to fire from camera to mouse
			ray = rPlayerCamera.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

		// Get RaycastHit Point in the world space
			if (Physics.Raycast(ray, out hit, 100f)) {

			// Check for if an object is being placed
				if (sPlacing != string.Empty) {
				// Check if player has enough energy to plant stuff
					if (rPlayerStatus.GetStatus(1) >= 2) {
					// Print sPlacing
						// rCore.Pnt(sPlacing);
					// Set the indicator
						if (sPlacing != "Bucket")
							rUI.SetIndicator(sPlacing, 2);
						else
							rUI.SetIndicator(sPlacing, 4);
					// When LMB is pressed
						if (Input.GetMouseButtonDown(0)) {
						// Disable the progress bar
							bProgressBar = false;

						// Check if the Object hit was a tile
							if (hit.transform.GetComponentInParent<Tile>() != null) {
							// Check if the tile is obstructed
								if (hit.transform.GetComponentInParent<Tile>().bObstructed == false) {
								// Print what was hit
									// rCore.Pnt(hit.transform.parent.name);
									// rCore.Pnt(hit.transform.GetComponentInParent<Tile>().bObstructed);
								// Check if the Placing OBject failed to parsed
									if ((rMapGenerator.SpawnEnvironment(sPlacing, hit.transform.parent) == true) ||
										(rMapGenerator.SpawnPlaceable(sPlacing, hit.transform.parent) == true)) {
									// Notify system that a fireplace was placed down.
										if (sPlacing == "Fireplace") rMapGenerator.FireplaceSpawned();
									// Search through the array to find instance
										for (int k = 0; k < rItemManager.GetItems().ToArray().Length; k++) {
											if (rItemManager.GetItems().ToArray()[k].sName == sPlacing) {
											// Update Count
												rItemManager.GetItems().ToArray()[k].CountUpdate(-1);
											// Reset Reference
												sPlacing = string.Empty;
											// Update Menu
												rPlayerInventory.bMenuChange = true;
											// Reset Cursor
												rCursorIndicator.SetCursor(0);
											// Reset Indicator
												rUI.SetIndicator(sPlacing, 0);
											}
										}
									}
								}
							}

						// Check specifically if its the water that is being interacted with
							else if (hit.transform.GetComponent<WaterMotion>() != null) {
							// Item Manager Check
								if (rItemManager != null) {
								// Check for Raft
									if (rItemManager.GetItems().Find(i => i.sName == "Raft").iCount > 0) {
									// Place Placeholder
										rMapGenerator.SpawnRaft(hit.point);
									// Win Game
										rCore.bWin = true;
									// Pause Game
										rCore.bPause = true;
									}
								}
							}

						// Check specifically if its something that is on fire
							else if (hit.transform.GetComponent<Fire>() != null) {
							// Put out the fire
								if (rMapGenerator.PutOutFire(hit.transform) == true) {
								// Reset Reference
									sPlacing = string.Empty;
								// Update Menu
									rPlayerInventory.bMenuChange = true;
								// Reset Cursor
									rCursorIndicator.SetCursor(0);
								// Reset Indicator
									rUI.SetIndicator(sPlacing, 0);
								}
							}
						
						// Check specifially if its Fireplace
							else if (hit.transform.GetComponent<Fireplace>() != null) {
							// Check if it is a bucket trying to interact with it
								if (sPlacing == "Bucket") {
								// Despawn FirePlace
									hit.transform.GetComponent<Fireplace>().Despawn();
								// Reset Reference
									sPlacing = string.Empty;
								// Update Menu
									rPlayerInventory.bMenuChange = true;
								// Reset Cursor
									rCursorIndicator.SetCursor(0);
								// Reset Indicator
									rUI.SetIndicator(sPlacing, 0);
								}
							}
						}
					}
				}

			// When LMB is held down
				if (Input.GetMouseButton(0)) {
					if (rPlayerStatus.GetStatus(1) >= 1) {
					// Check Bounce Timer
						if (fBounceTime > 1.5f - fSpeedModifier) {

						// Check if hit object has environment class
							if (hit.transform.GetComponentInParent<Environment>() != null) {
							// Display the distance between the gathering object and the player
								// rCore.Pnt(Vector3.Magnitude((transform.position - hit.transform.position)));

							// Check if the distance between the player and the environment object is 'close'
								if (Vector3.Magnitude(transform.position - hit.transform.position) < 1.3f) {
								// Attach a reference transform to the hit object
									Transform tGather = hit.transform;
								// Check if the Item Manager is working or not
									if (rItemManager != null) {
									// Parse the material just gathered and update the datalist
										rItemManager.Gather(tGather.GetComponentInParent<Environment>().sNode);

									// Give Player Experience
										rSkillSystem.IncreaseExperience(15);

									// Reduce Energy
										rPlayerStatus.Action(1);

									// Remove environment object
										tGather.GetComponentInParent<Environment>().Despawn();

									// Reset Progress Bar
										bProgressBar = false;
									// Reset Tool
										rItemManager.DespawnTool();
										bTool = false;
									}
									else
										rCore.Pnt("Missing Item Manager!");
								}
							}
						// Reset Bounce Timer
							fBounceTime = 0.0f;
						}

						else
						// Update Hold Time
							fBounceTime += Time.deltaTime;

						// Display Progress Bar
						if ((hit.transform.GetComponentInParent<Environment>() != null) && (Vector3.Magnitude(transform.position - hit.transform.position) < 1.3f)) {
							bProgressBar = true;
						// Spawn Tool Once
							if (bTool == false) {
								rItemManager.SpawnTool(hit.transform.GetComponentInParent<Environment>().sNode);
								bTool = true;
							}
						// Generate Speed Modifier from levels and tools
							fSpeedModifier = rItemManager.SpeedModify(hit.transform.GetComponentInParent<Environment>().sNode);
						}
					}
				}

			// When LMB is released
				if (Input.GetMouseButtonUp(0)) {
				// Reset Bounce Timer
					fBounceTime = 0.0f;
				// Reset Progress Bar
					bProgressBar = false;
				// Reset Tool
					bTool = false;
					rItemManager.DespawnTool();
				}
            }
        }
	}

// Get Animation Speed Modifier
	public float GetModifier() {
		return fSpeedModifier;
	}

// Reset Splacing
	public void ResetSplacing() {
		sPlacing = string.Empty;
	}
}
