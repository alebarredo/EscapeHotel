using UnityEngine;

public static class TransformExtensions
{
	/// <summary>
	/// Determines if is in line of sight the specified mystring.
	/// </summary>
	/// <param name="origin">Transform origin</param>
	/// <param name="target">Target direction</param>
	/// <param name="fieldOfView">Field of View</param>
	/// <param name="collisionMask">Check against layers</param>
	/// <param name="offset">transforms origin offset</param>
	/// <returns>yes or no</returns>

	public static bool IsInLineOfSight(this Transform origin, Vector3 target, float FieldOfView, LayerMask collisionMask, Vector3 offset)
	{
		Vector3 direction = target - origin.position;

		if (Vector3.Angle(origin.forward, direction.normalized) < FieldOfView / 2)
		{
			float distanceToTarget = Vector3.Distance(origin.position, target);

			//something blocking view
			if (Physics.Raycast(origin.position + offset + origin.forward * 5f, direction.normalized, distanceToTarget, collisionMask))
				return false;

			return true;
		}

		return false;
	}
}