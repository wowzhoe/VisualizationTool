using UnityEngine;

namespace VisualizationTool.Controls
{
    public class WASD
    {
        private float walkingSpeed = 7.5f;
        private float runningSpeed = 11.5f;
        private float jumpSpeed = 8f;
        private float gravity = 20f;
        private float lookSpeed = 2f;
        private float lookXLimit = 45f;
        private float rotationX = 0;
        private float rotationY = 0;
        private CharacterController controller;
        private Vector3 moveDirection = Vector3.zero;
        private bool canMove = true;
        private bool useGravity = false;

        public void Move(Transform transform)
        {
            Move(transform, null);
        }

        public void Move(Transform transform, Camera camera)
        {
            Cursor.lockState = CursorLockMode.None;

            if (controller == null)
            {
                if (transform.GetComponent<CharacterController>() == null)
                {
                    transform.gameObject.AddComponent<CharacterController>();
                }

                controller = transform.GetComponent<CharacterController>();
            }

            // We are grounded, so recalculate move direction based on axes
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            // Press Left Shift to run
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);

            if (useGravity)
            {
                moveDirection.y = Input.GetButton("Jump") && canMove && controller.isGrounded
                    ? jumpSpeed
                    : movementDirectionY;

                // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
                // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
                // as an acceleration (ms^-2)
                if (!controller.isGrounded)
                {
                    moveDirection.y -= gravity * Time.deltaTime;
                }
            }

            // Move the controller
            controller.Move(moveDirection * Time.deltaTime);

            // Player and Camera rotation
            if (canMove)
            {
                rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
                rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

                rotationY += Input.GetAxis("Mouse X") * lookSpeed;
                rotationY = Mathf.Clamp(rotationY, -lookXLimit * 8, lookXLimit * 8);

                if (camera != null)
                {
                    //camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                }
                
                transform.rotation = Quaternion.Euler(rotationX, rotationY, 0);
                //transform.eulerAngles += new Vector3(rotationX, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
    }
}