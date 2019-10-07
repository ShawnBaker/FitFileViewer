// Copyright © 2019 Shawn Baker using the MIT License.
using System;
using Dynastream.Fit;

namespace FitLib
{
	/// <summary>
	/// A FIT activity.
	/// </summary>
	public class FitActivity
	{
		public System.DateTime? Timestamp { get; set; } = null;
		public uint? LocalTimestamp { get; set; } = null;
		public TimeSpan? TotalTimerTime { get; set; } = null;
		public Activity? Type { get; set; } = null;
		public int? NumSessions { get; set; } = null;
		public int? EventGroup { get; set; } = null;
		public Event? Event { get; set; } = null;
		public EventType? EventType { get; set; } = null;

		public FitActivity(ActivityMesg msg)
		{
			Event  = msg.GetEvent();
			EventGroup = msg.GetEventGroup();
			EventType = msg.GetEventType();
			LocalTimestamp = msg.GetLocalTimestamp();
			NumSessions = msg.GetNumSessions();
			Timestamp = FitFile.GetDateTime(msg.GetTimestamp());
			TotalTimerTime = FitFile.GetTimeSpan(msg.GetTotalTimerTime());
			Type = msg.GetType();
		}
	}
}
