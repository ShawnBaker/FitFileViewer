// Copyright © 2019 Shawn Baker using the MIT License.
using System;

namespace FitLib
{
	/// <summary>
	/// A FIT record summary.
	/// </summary>
	public class FitRecordSummary
	{
		public System.DateTime StartTime = System.DateTime.Now;
		public TimeSpan ElapsedTime = TimeSpan.Zero;
		public double Distance = 0;
		public double AveSpeed = 0;
		public double AvePower = 0;
		public double AveHeartRate = 0;
		public double AveCadence = 0;
		public double MaxSpeed = 0;
		public int MaxPower = 0;
		public int MaxHeartRate = 0;
		public int MaxCadence = 0;
	}
}
