using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    private bool useJoystick;
    [SerializeField] private float joystickMinThreshold;
    private Vector2 movement;
    private Vector2 mousePos;
    private Joystick moveJoystick;
    private Joystick lookJoystick;
    private float _z;
    private float __z;
    private float z;
    private GameObject abilities, abilitiesMobile, abilitiesDesktop;

    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            StartJoystick();
        }
        else
        {
            EndJoystick();
        }
    }

    private void StartJoystick()
    {
        abilities = GameObject.FindGameObjectWithTag("Abilities");
        abilitiesMobile = GameObject.FindGameObjectWithTag("AbilitiesMobile");
        
        GameObject.FindGameObjectWithTag("MoveJoystick").SetActive(true);
        GameObject.FindGameObjectWithTag("ShootJoystick").SetActive(true);
        
        abilities.GetComponent<RectTransform>().position = abilitiesMobile.GetComponent<RectTransform>().position;
        abilities.GetComponent<RectTransform>().anchorMax = abilitiesMobile.GetComponent<RectTransform>().anchorMax;
        abilities.GetComponent<RectTransform>().anchorMin = abilitiesMobile.GetComponent<RectTransform>().anchorMin;
        abilities.GetComponent<RectTransform>().anchoredPosition = abilitiesMobile.GetComponent<RectTransform>().anchoredPosition;

        moveJoystick = GameObject.FindGameObjectWithTag("MoveJoystick").GetComponent<Joystick>();
        lookJoystick = GameObject.FindGameObjectWithTag("ShootJoystick").GetComponent<Joystick>();
        useJoystick = true;
    }

    private void EndJoystick()
    {
        abilities = GameObject.FindGameObjectWithTag("Abilities");
        abilitiesDesktop = GameObject.FindGameObjectWithTag("AbilitiesDesktop");
        
        GameObject.FindGameObjectWithTag("MoveJoystick").SetActive(false);
        
        abilities.GetComponent<RectTransform>().position = abilitiesDesktop.GetComponent<RectTransform>().position;
        abilities.GetComponent<RectTransform>().anchorMax = abilitiesDesktop.GetComponent<RectTransform>().anchorMax;
        abilities.GetComponent<RectTransform>().anchorMin = abilitiesDesktop.GetComponent<RectTransform>().anchorMin;
        abilities.GetComponent<RectTransform>().anchoredPosition = abilitiesDesktop.GetComponent<RectTransform>().anchoredPosition;

        useJoystick = false;
    }

    private void Update()
    {
        if(useJoystick) return;
        
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        if(useJoystick)
            UseJoysticks();
        else
            UseInput();
        
        rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void UseJoysticks()
    {
        if (moveJoystick.Horizontal >= joystickMinThreshold)
            movement.x = 1;
        else if (moveJoystick.Horizontal <= -joystickMinThreshold)
            movement.x = -1;
        else
            movement.x = 0;
        
        if (moveJoystick.Vertical >= joystickMinThreshold)
            movement.y = 1;
        else if (moveJoystick.Vertical <= -joystickMinThreshold)
            movement.y = -1;
        else
            movement.y = 0;
        
        
        if (lookJoystick.Horizontal is < .1f and > -.1f && lookJoystick.Vertical is < .1f and > -.1f) return;
        
        z = Mathf.Atan2(-lookJoystick.Horizontal, lookJoystick.Vertical) * Mathf.Rad2Deg;
        if (z != 0)
        { 
            _z = z; 
            __z = z; 
        }
        else{
            _z=__z;
        }
        
        rb.rotation = _z;

    }

    private void UseInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
