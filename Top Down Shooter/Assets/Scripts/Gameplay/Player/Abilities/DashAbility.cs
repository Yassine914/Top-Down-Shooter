using UnityEngine;
public class DashAbility : MonoBehaviour
{
      [SerializeField] private float moveSpeed;
      private Rigidbody2D rb;
      private Vector2 moveInput;

      private float activeMoveSpeed;
      [SerializeField] private float dashSpeed;
      [SerializeField] private float dashLength = .5f, dashCooldown = 1f;

      private float dashCounter;
      private float dashCoolCounter;

      private void Start()
      {
            rb = GetComponent<Rigidbody2D>();
            activeMoveSpeed = moveSpeed;
      }

      private void Update()
      {
            Move();
      }

      void Move()
      {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
            
            moveInput.Normalize();

            rb.velocity = moveInput * activeMoveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                  if (dashCoolCounter <= 0 && dashCounter <= 0)
                  {
                        activeMoveSpeed = dashSpeed;
                        dashCounter = dashLength;
                  }
            }

            if (dashCounter > 0)
            {
                  dashCounter -= Time.deltaTime;

                  if (dashCounter <= 0)
                  {
                        activeMoveSpeed = moveSpeed;
                        dashCoolCounter = dashCooldown;
                  }
            }

            if (dashCoolCounter > 0)
            {
                  dashCounter -= Time.deltaTime;
            }
      }
}
