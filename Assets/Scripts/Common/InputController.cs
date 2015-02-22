using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour, IController
{
    // 0,0 is the bottom left corner
    private const double TouchHeightFromBottom = 0.35;
    private const double TouchFromSides = 0.35;

    private bool wasTouchingLeft = false;
    private bool wasTouchingRight = false;

	#region Public Properties
    

    /// <summary>
    /// Tells whether the user wants to issue the "move left" command
    /// </summary>
	public bool MoveLeft
	{
		get
		{
			return Input.GetButtonDown("MoveLeft") || BottomLeftTap;
		}
	}

    /// <summary>
    /// Tells whether the user wants to issue the "move right" command
    /// </summary>
	public bool MoveRight
	{
		get
		{
			return Input.GetButtonDown("MoveRight") || BottomRightTap;
		}
	}

    /// <summary>
    /// Tells whether the bottom left corner of the screen was touched.
    /// </summary>
    public bool BottomLeftTap
    {
        get
        {
            bool tapped = false;
            // If the last time we checked we were touching, then they
            // must not have lifted the finger so ignore it until it's lifted
            if(wasTouchingLeft)
            {
                // Check if it's lifted
                wasTouchingLeft = IsTouchWithinRange(0, TouchFromSides, 0, TouchHeightFromBottom);
            }
            else
            {
                // If we weren't touching it last time then maybe we're touching this time
                tapped = IsTouchWithinRange(0, TouchFromSides, 0, TouchHeightFromBottom);
                if(tapped)
                {
                    wasTouchingLeft = true;
                }
            }

            return tapped;
        }
    }

    /// <summary>
    /// Tells whether the bottom right corner of the screen was touched
    /// </summary>
    public bool BottomRightTap
    {
        get
        {
            bool tapped = false;
            // If the last time we checked we were touching, then they
            // must not have lifted the finger so ignore it until it's lifted
            if (wasTouchingRight)
            {
                // Check if it's lifted
                wasTouchingRight = IsTouchWithinRange(1.0 - TouchFromSides, 1.0, 0, TouchHeightFromBottom);
            }
            else
            {
                // If we weren't touching it last time then maybe we're touching this time
                tapped = IsTouchWithinRange(1.0 - TouchFromSides, 1.0, 0, TouchHeightFromBottom);
                if (tapped)
                {
                    wasTouchingRight = true;
                }
            }

            return tapped;
        }
    }

	#endregion Public Properties

    private bool IsTouchWithinRange(double leftX, double rightX, double bottomY, double topY)
    {
        bool withinRange = false;
        foreach (Touch touch in Input.touches)
        {
            // Convert physical to normalized coordinates
            Vector2 normalized = NormalizeToScreen(touch.position);

            Debug.Log("Normalized: " + normalized.x + ", " + normalized.y);
            bool withinXRange = normalized.x >= leftX && normalized.x <= rightX;
            bool withinYRange = normalized.y >= bottomY && normalized.y <= topY;

            if(withinXRange && withinYRange)
            {
                withinRange = true;
                break;
            }
        }

        return withinRange;
    }

    private Vector2 NormalizeToScreen(Vector2 input)
    {
        return new Vector2(input.x / Screen.width, input.y / Screen.height);
    }

    #region IController
    public void Cleanup()
    {
    }
    #endregion
}
