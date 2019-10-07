// Copyright © 2019 Shawn Baker using the MIT License.
using System;
using System.Collections.Generic;
using Dynastream.Fit;

namespace FitLib
{
	/// <summary>
	/// A FIT length.
	/// </summary>
	public class FitLength
	{
		public int FirstRecord { get; set; } = -1;
		public int LastRecord { get; set; } = -1;

		public System.DateTime? Timestamp { get; set; } = null;
		public System.DateTime? StartTime { get; set; } = null;
		public TimeSpan? TotalElapsedTime { get; set; } = null;
		public TimeSpan? TotalTimerTime { get; set; } = null;

		public int? TotalCalories { get; set; } = null;
		public SwimStroke? SwimStroke { get; set; } = null;
		public LengthType? LengthType { get; set; } = null;
		public int? AvgSwimmingCadence { get; set; } = null;
		public float? AvgSpeed { get; set; } = null;

		public string StrokeCounts { get; set; } = null;
		public int? TotalStrokes { get; set; } = null;
		public string ZoneCounts { get; set; } = null;
		public int? OpponentScore { get; set; } = null;
		public int? PlayerScore { get; set; } = null;

		public Event? Event { get; set; } = null;
		public int? EventGroup { get; set; } = null;
		public EventType? EventType { get; set; } = null;

		public FitLength(LengthMesg msg, int first, int last)
		{
			FirstRecord = first;
			LastRecord = last;

			AvgSwimmingCadence = msg.GetAvgSwimmingCadence();
			AvgSpeed = FitFile.GetSpeed(msg.GetAvgSpeed());
			Event = msg.GetEvent();
			EventGroup = msg.GetEventGroup();
			EventType = msg.GetEventType();
			LengthType = msg.GetLengthType();
			OpponentScore = msg.GetOpponentScore();
			PlayerScore = msg.GetPlayerScore();
			StartTime = FitFile.GetDateTime(msg.GetStartTime());
			StrokeCounts = FitFile.GetUShortList(msg.GetNumStrokeCount(), msg.GetStrokeCount);
			SwimStroke = msg.GetSwimStroke();
			Timestamp = FitFile.GetDateTime(msg.GetTimestamp());
			TotalElapsedTime = FitFile.GetTimeSpan(msg.GetTotalElapsedTime());
			TotalTimerTime = FitFile.GetTimeSpan(msg.GetTotalTimerTime());
			TotalCalories = msg.GetTotalCalories();
			TotalStrokes = msg.GetTotalStrokes();
			ZoneCounts = FitFile.GetUShortList(msg.GetNumZoneCount(), msg.GetZoneCount);
		}
	}

	/// <summary>
	/// List of FIT lengths.
	/// </summary>
	public class FitLengthList : List<FitLength> { }
}
