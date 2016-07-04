using System;
using System.Linq.Expressions;
using UnityEngine;
using System.Collections;

public static class Misc {
	public static bool Between(float n, float min, float max){ return ((n > min) && (n < max)); }
	public static bool Between(int n, int min, int max) { return Between(n, min, max); }
	public static bool Between(double n, double min, double max) { return Between((float)n, (float)min, (float)max); }

	public static bool BetweenInclusive(float n, float min, float max) { return ((n >= min) && (n <= max)); }
	public static bool BetweenInclusive(int n, int min, int max) { return BetweenInclusive(n, min, max); }
	public static bool BetweenInclusive(double n, double min, double max) { return BetweenInclusive((float)n, (float)min, (float)max); }
}

public static class MemberInfo
{
	public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
	{
		MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
		return expressionBody.Member.Name;
	}
}

namespace UnityEngine
{
	public static class Vector3Extension
	{
		public static Vector3 Round(this Vector3 v)
		{
			float x = v.x;
			float y = v.y;
			float z = v.z;
			x = (float)Math.Round((decimal)x, 0, MidpointRounding.AwayFromZero);
			y = (float)Math.Round((decimal)y, 0, MidpointRounding.AwayFromZero);
			z = (float)Math.Round((decimal)z, 0, MidpointRounding.AwayFromZero);

			return new Vector3(x, y, z);
		}
	}

	public static class QuaternionExtension
	{
		public static Quaternion SnapToRightAngles(this Quaternion q)
		{
			Vector3 angles = q.eulerAngles;
			angles.x = (float)Math.Round(angles.x / 90, 0, MidpointRounding.AwayFromZero) * 90;
			angles.y = (float)Math.Round(angles.y / 90, 0, MidpointRounding.AwayFromZero) * 90;
			angles.z = (float)Math.Round(angles.z / 90, 0, MidpointRounding.AwayFromZero) * 90;
			return Quaternion.Euler(angles.x, angles.y, angles.z);
		}
	}
}