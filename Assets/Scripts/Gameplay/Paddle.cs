using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A paddle
/// </summary>
public class Paddle : MonoBehaviour
{
    #region Fields

    // saved for efficiency
    Rigidbody2D rb2d;
    float halfColliderWidth;
    BoxCollider2D bc2d;

    // aiming support
    const float BounceAngleHalfRange = 60 * Mathf.Deg2Rad;

    // change size of paddle
    Timer sizeTimer;
    const float sizeChangeDuration = 2.0f;
    const float sizeChangePerSecond = 1.5f;
    int sizeChangeDirection = 1;
    Vector3 localScale;

    #endregion

    #region Unity methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // save for efficiency
        rb2d = GetComponent<Rigidbody2D>();
        bc2d = GetComponent<BoxCollider2D>();
        halfColliderWidth = bc2d.size.x / 2;

        // start size change timer
        sizeTimer = gameObject.AddComponent<Timer>();
        sizeTimer.Duration = sizeChangePerSecond;
        sizeTimer.Run();

        // initialize size
        localScale = transform.localScale;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // switch expansion and contraction
        if (sizeTimer.Finished)
        {
            sizeChangeDirection *= -1;
            sizeTimer.Run();
        }
        localScale.x += sizeChangeDirection * sizeChangePerSecond * Time.deltaTime;
        transform.localScale = localScale;
    }

    /// <summary>
    /// FixedUpdate is called 50 times per second
    /// </summary>
    void FixedUpdate()
    {
        // move for horizontal input
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            Vector2 position = rb2d.position;
            position.x += horizontalInput * ConfigurationUtils.PaddleMoveUnitsPerSecond *
                Time.deltaTime;
            position.x = CalculateClampedX(position.x);
            rb2d.MovePosition(position);
        }
    }

    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="coll">collision info</param>
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball") &&
            TopCollision(coll))
        {
            // calculate new ball direction
            float ballOffsetFromPaddleCenter = transform.position.x -
                coll.transform.position.x;
            // recalculate the width of the paddle because the value changes
            halfColliderWidth = transform.localScale.x * bc2d.size.x / 2;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                halfColliderWidth;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            float angle = Mathf.PI / 2 + angleOffset;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

            // tell ball to set direction to new direction
            Ball ballScript = coll.gameObject.GetComponent<Ball>();
            Kone koneScript = coll.gameObject.GetComponent<Kone>();
            if (ballScript != null)
            {
                ballScript.SetDirection(direction);
            }
            else
            {
                koneScript.SetDirection(direction);
            }
        }
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Calculates an x position to clamp the paddle in the screen
    /// </summary>
    /// <param name="x">the x position to clamp</param>
    /// <returns>the clamped x position</returns>
    float CalculateClampedX(float x)
    {
        // clamp left and right edges
        if (x - halfColliderWidth < ScreenUtils.ScreenLeft)
        {
            x = ScreenUtils.ScreenLeft + halfColliderWidth;
        }
        else if (x + halfColliderWidth > ScreenUtils.ScreenRight)
        {
            x = ScreenUtils.ScreenRight - halfColliderWidth;
        }
        return x;
    }

    /// <summary>
    /// Checks for a collision on the top of the paddle
    /// </summary>
    /// <returns><c>true</c>, if collision was on the top of the paddle, <c>false</c> otherwise.</returns>
    /// <param name="coll">collision info</param>
    bool TopCollision(Collision2D coll)
    {
        const float tolerance = 0.05f;

        // on top collisions, both contact points are at the same y location
        ContactPoint2D[] contacts = new ContactPoint2D[2];
        coll.GetContacts(contacts);
        return Mathf.Abs(contacts[0].point.y - contacts[1].point.y) < tolerance;
    }

    #endregion
}
