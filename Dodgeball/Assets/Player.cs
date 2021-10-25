using UnityEngine;

/// <summary>
/// Control the player on screen
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    /// <summary>
    /// Prefab for the orbs we will shoot
    /// </summary>
    public GameObject OrbPrefab;

    public Rigidbody2D RigidBody;

    /// <summary>
    /// How fast our engines can accelerate us
    /// </summary>
    public float EnginePower = 1000;
    
    /// <summary>
    /// How fast we turn in place
    /// </summary>
    public float RotateSpeed = 100;

    /// <summary>
    /// How fast we should shoot our orbs
    /// </summary>
    public float OrbVelocity = 1000;

    private void Start()
    {
        RigidBody = GetComponent<Rigidbody2D>();

    }


    /// <summary>
    /// Fire if the player is pushing the button for the Fire axis
    /// Unlike the Enemies, the player has no cooldown, so they shoot a whole blob of orbs
    /// The orb should be placed one unit "in front" of the player.  transform.right will give us a vector
    /// in the direction the player is facing.
    /// It should move in the same direction (transform.right), but at speed OrbVelocity.
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void Update()
    {
        // TODO
        if (Input.GetAxis("Fire") > 0)
        {
            var right = RigidBody.transform.right;
            var pos = new Vector2(RigidBody.position.x + right.x, RigidBody.position.y + right.y);
            var go = Instantiate(OrbPrefab, pos, Quaternion.identity, RigidBody.transform);
            go.GetComponent<Rigidbody2D>().velocity = right * OrbVelocity;
        }
        
    }

    /// <summary>
    /// Accelerate and rotate as directed by the player
    /// Apply a force in the direction (Horizontal, Vertical) with magnitude EnginePower
    /// Note that this is in *world* coordinates, so the direction of our thrust doesn't change as we rotate
    /// Set our angularVelocity to the Rotate axis time RotateSpeed
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void FixedUpdate()
    {
        // TODO
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);
        dir = dir * EnginePower;
        RigidBody.AddForce(dir);

        float RotateAxis = Input.GetAxis("Rotate");
        float negRotate = Input.GetAxis()
        RigidBody.angularVelocity = RotateSpeed * (1 + RotateAxis) / 2;
    }

    /// <summary>
    /// If this is called, we got knocked off screen.  Deduct a point!
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void OnBecameInvisible()
    {
        ScoreKeeper.ScorePoints(-1);
    }
}
