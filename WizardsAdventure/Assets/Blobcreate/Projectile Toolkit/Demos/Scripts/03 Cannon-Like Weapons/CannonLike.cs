using Blobcreate.Universal;
using UnityEngine;

namespace Blobcreate.ProjectileToolkit.Demo
{
	public class CannonLike : MonoBehaviour
	{
		public LaunchType type;
		public Rigidbody shell;
		public Transform barrel;
		public Transform launchPoint;

		// ForceByAngle
		public float launchAngle;

		// AnglesBySpeed and ForcesBySpeed
		public float launchSpeed;
		public bool useHighAngle;
		public bool useLowAngle;

		bool hasTarget;
		Vector3 lookPoint;

		void Update()
		{
			// Target
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var mouseHitInfo, 200f))
			{
				lookPoint = mouseHitInfo.point;
				lookPoint.y = transform.position.y;

				if (Input.GetMouseButtonDown(0))
					hasTarget = true;
			}

			var launchVelocity = default(Vector3);

			// Rotate and launch
			if (type == LaunchType.VelocityByAngle)
			{
				launchVelocity = Projectile.VelocityByAngle(launchPoint.position, mouseHitInfo.point, launchAngle);
				transform.rotation = Quaternion.LookRotation(lookPoint - transform.position);
				barrel.localRotation = Quaternion.AngleAxis(-launchAngle, Vector3.right);
			}
			else if (type == LaunchType.AnglesBySpeed)
			{
				if (Projectile.AnglesBySpeed(launchPoint.position, mouseHitInfo.point, launchSpeed,
					out var lowA, out var highA))
				{
					transform.rotation = Quaternion.LookRotation(lookPoint - transform.position);

					// Rotates along local x.
					if (useLowAngle)
						barrel.localRotation = Quaternion.AngleAxis(-lowA, Vector3.right);
					else if (useHighAngle)
						barrel.localRotation = Quaternion.AngleAxis(-highA, Vector3.right);

					launchVelocity = barrel.forward * launchSpeed;
				}
			}
			else if (type == LaunchType.VelocitiesBySpeed)
			{
				if (Projectile.VelocitiesBySpeed(launchPoint.position, mouseHitInfo.point, launchSpeed,
					out var lowV, out var highV))
				{
					// VelocitiesBySpeed is an extended version of AnglesBySpeed.
					// It is more convenient than AnglesBySpeed when the rotation
					// is not separated into y axis and x axis.

					transform.rotation = Quaternion.LookRotation(lookPoint - transform.position);

					if (useLowAngle)
					{
						barrel.rotation = Quaternion.LookRotation(lowV);
						launchVelocity = lowV;
					}
					else if (useHighAngle)
					{
						barrel.rotation = Quaternion.LookRotation(highV);
						launchVelocity = highV;
					}
				}
			}

			if (!hasTarget)
				return;

			if (launchVelocity != default)
			{
				var bullet = Instantiate(shell, launchPoint.position, Quaternion.identity);
				// Don't forget to call Launch(...) (or use your own explosion logic instead).
				bullet.GetComponent<ProjectileBehaviour>().Launch(Vector3.one);
				bullet.AddForce(launchVelocity, ForceMode.VelocityChange);
			}
				
			hasTarget = false;
		}


		public enum LaunchType
		{
			VelocityByAngle,
			AnglesBySpeed,
			VelocitiesBySpeed
		}
	}
}