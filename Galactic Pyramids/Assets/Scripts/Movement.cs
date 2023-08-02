/*
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
	public PlayerControls control;

	GameObject Pivoter;

	// height to Pyramids center
	const float PyramidCentreHeight = 20.412f;

	// how long to flip 90 degrees?
	const float TimeRequiredToFlip = 0.5f;

	public enum Direction
	{
		NONE = -1,
		FORWARD = 0,
		BACK,
		RIGHTUP,
		RIGHTDOWN,
		LEFTUP,
		LEFTDOWN,
		ROTATERIGHT,
		ROTATELEFT
	};

	Vector3 Forward = new Vector3(0, 0, 28.867f);
	Vector3 Back = new Vector3(0, 0, -28.867f);
	Vector3 LeftUp = new Vector3(-25, 0, 14.43f);
	Vector3 LeftDown = new Vector3(-25, 0, -14.43f);
	Vector3 RightUp = new Vector3(25, 0, 14.43f);
	Vector3 RightDown = new Vector3(25, 0, -14.43f);
	
	Direction Command;
	bool Rotation = false;

	Direction CurrentRotation;
	float NonzeroWhenMoving;

	bool TileDown = true;
	bool AdjTileEmpty = true;
	bool AdjTilesEmpty = true;

	void Awake()
	{
		control = new PlayerControls();
		control.Player.Movement.performed += MovementInput;
		control.Player.Rotation.performed += RotationInput;

		Command = Direction.NONE;
	}
	void MovementInput(InputAction.CallbackContext ctx)
	{
		Vector2 WASD = ctx.ReadValue<Vector2>();
		
		if (WASD.y == 1 && TileDown == false && AdjTileEmpty == true)
		{
			Command = Direction.FORWARD;
			TileDown = true;
		}

		if (WASD.y == -1 && TileDown == true && AdjTileEmpty == true)
		{
			Command = Direction.BACK;
			TileDown = false;
		}

		if (WASD.x == -1 && TileDown == false && AdjTileEmpty == true)
		{
			Command = Direction.LEFTDOWN;
			TileDown = true;
		}
		else if (WASD.x == -1 && TileDown == true && AdjTileEmpty == true)
		{
			Command = Direction.LEFTUP;
			TileDown = false;
		}

		if (WASD.x == 1 && TileDown == false && AdjTileEmpty == true)
		{
			Command = Direction.RIGHTDOWN;
			TileDown = true;
		}
		else if (WASD.x == 1 && TileDown == true && AdjTileEmpty == true)
		{
			Command = Direction.RIGHTUP;
			TileDown = false;
		}
	}
	
	void RotationInput(InputAction.CallbackContext ctx)
        {
		float Rotation = ctx.ReadValue<float>();

		if (Rotation == 1 && AdjTilesEmpty)
			Command = Direction.ROTATERIGHT;
		else if (Rotation == -1 && AdjTilesEmpty)
			Command = Direction.ROTATELEFT;
		}
		
	void OnEnable()
	{
		control.Player.Enable();
	}	
	
	void Start()
	{
		NonzeroWhenMoving = 0;
		Pivoter = new GameObject("Pivoter");
	}

	void HandleMovement()
	{
		control.Player.Movement.Disable();

		float fraction = NonzeroWhenMoving / TimeRequiredToFlip;

		NonzeroWhenMoving += Time.deltaTime;

		if (fraction >= 1)
		{
			fraction = 1;   // end precisely!
			NonzeroWhenMoving = 0;   // done moving
			Command = Direction.NONE;

			control.Player.Movement.Enable();
		}

		float angle = 109.471f * fraction;

		switch (CurrentRotation)
		{
			case Direction.FORWARD:
				Pivoter.transform.rotation = Quaternion.Euler(angle, 0, 0);
				break;
			case Direction.BACK:
				Pivoter.transform.rotation = Quaternion.Euler(-angle, 0, 0);
				break;
			case Direction.RIGHTUP:
				{
					Quaternion RotateX_RightUp = Quaternion.AngleAxis(angle, Vector3.right);
					Pivoter.transform.rotation = Quaternion.Euler(0, 60, 0) * RotateX_RightUp;

					break;
				}
			case Direction.RIGHTDOWN:
				{
					Quaternion RotateX_RightDown = Quaternion.AngleAxis(angle, Vector3.right);
					Pivoter.transform.rotation = Quaternion.Euler(0, 120, 0) * RotateX_RightDown;
					
					break;
				}
			case Direction.LEFTUP:
				{
					Quaternion RotateX_LeftUp = Quaternion.AngleAxis(angle, Vector3.right);
					Pivoter.transform.rotation = Quaternion.Euler(0, -60, 0) * RotateX_LeftUp;
					break;
				}
			case Direction.LEFTDOWN:
				{
					Quaternion RotateX_LeftDown = Quaternion.AngleAxis(angle, Vector3.right);
					Pivoter.transform.rotation = Quaternion.Euler(0, -120, 0) * RotateX_LeftDown;
					break;
				}
		}

		if (NonzeroWhenMoving == 0)
		{
			transform.SetParent(null);  // deparent us from the pivoter
		}

	}

	void HandleRotation()
	{
		control.Player.Movement.Disable();

		float fraction = NonzeroWhenMoving / TimeRequiredToFlip;

		NonzeroWhenMoving += Time.deltaTime;

		if (fraction >= 1)
		{
			fraction = 1;   // end precisely!
			NonzeroWhenMoving = 0;   // done moving
			Command = Direction.NONE;

			control.Player.Movement.Enable();
		}

		float angle = 120f * fraction;

		switch (CurrentRotation)
		{
			case Direction.ROTATERIGHT:
				Pivoter.transform.rotation = Quaternion.Euler(0, angle, 0);
				break;
			case Direction.ROTATELEFT:
				Pivoter.transform.rotation = Quaternion.Euler(0, -angle, 0);
				break;
		}

		if (NonzeroWhenMoving == 0)
		{
			transform.SetParent(null);  // deparent us from the pivoter
		}

	}

	void Update()
	{
		// we always GET the command, but we might not process it.
		// We only process commands when we reach flatness again.

		if (NonzeroWhenMoving > 0 && Rotation == false)
			HandleMovement();
		else if (NonzeroWhenMoving > 0)
			HandleRotation();

		if (NonzeroWhenMoving == 0)
		{
			if (Command != Direction.NONE)
			{
				Vector3 SpotBeneathUs = transform.position + Vector3.down * PyramidCentreHeight;

				Vector3 WhereToPivot = SpotBeneathUs;

				switch (Command)
				{
					case Direction.FORWARD:
						{
							WhereToPivot += Forward;
							Pivoter.transform.rotation = Quaternion.identity;
							Rotation = false;
							break;
						}
						
					case Direction.BACK:
						{
							WhereToPivot += Back;
							Pivoter.transform.rotation = Quaternion.identity;
							Rotation = false;
							break;
						}
					case Direction.LEFTUP:
						{
							WhereToPivot += LeftUp;
							Pivoter.transform.rotation = Quaternion.identity;
							Pivoter.transform.Rotate(0, -60, 0);
							Rotation = false;
							break;
						}
					case Direction.LEFTDOWN:
						{
							WhereToPivot += LeftDown;
							Pivoter.transform.rotation = Quaternion.identity;
							Pivoter.transform.Rotate(0, -120, 0);
							Rotation = false;
							break;
						}
					case Direction.RIGHTUP:
						{
							WhereToPivot += RightUp;
							Pivoter.transform.rotation = Quaternion.identity;
							Pivoter.transform.Rotate(0, 60, 0);
							Rotation = false;
							break;
						}
					case Direction.RIGHTDOWN:
						{
							WhereToPivot += RightDown;
							Pivoter.transform.rotation = Quaternion.identity;
							Pivoter.transform.Rotate(0, 120, 0);
							Rotation = false;
							break;
						}
					case Direction.ROTATELEFT:
						{
							Pivoter.transform.rotation = Quaternion.identity;
							Rotation = true;
							break;
						}
					case Direction.ROTATERIGHT:
						{
							Pivoter.transform.rotation = Quaternion.identity;
							Rotation = true;
							break;
						}
				}
				
				CurrentRotation = Command;

				// move the pivoter to where it needs to be
				Pivoter.transform.position = WhereToPivot;
								
				// parent us onto it so we flip over
				transform.SetParent(Pivoter.transform);

				NonzeroWhenMoving += Time.deltaTime;    // trigger motion

				if (Rotation == false)
					HandleMovement();
				else if (Rotation == true)
					HandleRotation();
			}
		}
	}
	void OnDisable()
	{
		control.Player.Disable();
	}
}
*/