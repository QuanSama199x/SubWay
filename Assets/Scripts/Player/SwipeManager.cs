using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static SwipeManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject emty;
    public GameObject Player;

    public int XValue;
    public int jumpPower, maxJumpPower = 9, minJumpPower = 6;

    public float timeroll;

    public bool countJump, isjump, istimeroll;


    public GameObject colliderPlayer;


    public Vector3 nonEmty;

    void Start()
    {
        nonEmty.x = emty.transform.position.x;
        countJump = true;
        timeroll = 0;
        jumpPower = minJumpPower;
    }

    public void moveLeft()
    {
        if (emty.transform.position.x == 0 && Vector3.Distance(Player.transform.position, emty.transform.position) <= 1f)
        {
            emty.transform.position = new Vector3(-XValue, 0.5f, 0);
        }
        if (emty.transform.position.x == 3 && Vector3.Distance(Player.transform.position, emty.transform.position) <= 1f)
        {
            emty.transform.position = new Vector3(0, 0.5f, 0);
        }

        Player.transform.forward = new Vector3(-10, 0, 10);

    }

    public void moveRight()
    {
        if (emty.transform.position.x == 0 && Vector3.Distance(Player.transform.position, emty.transform.position) <= 0.5f)
        {
            emty.transform.position = new Vector3(XValue, 0.5f, 0);
        }
        if (emty.transform.position.x == -3 && Vector3.Distance(Player.transform.position, emty.transform.position) <= 0.5f)
        {
            emty.transform.position = new Vector3(0, 0.5f, 0);
        }
        Player.transform.forward = new Vector3(1, 0, 1);

    }

    public void Jump()
    {
        Player.GetComponent<Rigidbody>().velocity = new Vector3(0, jumpPower, 0);

        isjump = true;
    }

    public void Down()
    {
        Player.GetComponent<Rigidbody>().velocity = new Vector3(0, -jumpPower * 2, 0);
    }
}