using UnityEngine;

namespace Blobcreate.Universal
{
	public class CharacterMovement : MonoBehaviour
	{
		[SerializeField] float speed = 10f;
		[SerializeField] float jumpSpeed = 10f;
		[SerializeField] Vector3 gravity = new Vector3(0f, -9.81f, 0f);
		[SerializeField] protected float offGroundPenalty = 0.6f;
		[SerializeField] protected float airControl = 0.4f;

		protected CharacterController controller;
		protected bool isGroundedLastFrame = true;
		protected Vector3 moveVec;
		protected Vector3 airVelocity;

		public Vector3 Direction { get; set; }
		public float JumpInput { get; set; }

		public float Speed { get => speed; set => speed = value; }
		public float JumpSpeed { get => jumpSpeed; set => jumpSpeed = value; }
		public Vector3 Gravity { get => gravity; set => gravity = value; }
		public bool IsGrounded => controller.isGrounded;
		public bool IsGroundedLastFrame => isGroundedLastFrame;

		public void AddExplosionForce(float maxForce, Vector3 explotionPos, float radius, float uplit)
		{
			var fVec = transform.position - explotionPos + new Vector3(0f, uplit, 0f);
			fVec.Normalize();
			var f = Mathf.Lerp(maxForce, 1f, fVec.sqrMagnitude / (radius * radius));
			AddForce(f * fVec);
		}

		public void AddForce(Vector3 force, bool reset = false)
		{
			if (reset)
				moveVec = force;
			else
				moveVec += force;

			controller.Move(force * Time.deltaTime);
		}


		protected virtual void Start()
		{
			controller = GetComponent<CharacterController>();
		}

		protected virtual void Update()
		{
			CalculateMovement();

			controller.Move((moveVec + airVelocity) * Time.deltaTime);
		}

		protected void CalculateMovement()
		{
			if (controller.isGrounded)
			{
				moveVec = speed * Direction;
				airVelocity = Vector3.zero;
				if (JumpInput > 0f)
					moveVec.y = JumpInput;

				isGroundedLastFrame = true;
			}
			else
			{
				if (isGroundedLastFrame)
					moveVec = new Vector3(moveVec.x * offGroundPenalty, moveVec.y, moveVec.z * offGroundPenalty);

				airVelocity = airControl * speed * Direction;

				if (Mathf.Abs(controller.velocity.x) < 1f)
					moveVec.x = 0f;
				if (Mathf.Abs(controller.velocity.z) < 1f)
					moveVec.z = 0f;

				isGroundedLastFrame = false;
			}

			moveVec += gravity * Time.deltaTime;
		}
	}
}