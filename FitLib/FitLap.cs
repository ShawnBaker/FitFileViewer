// Copyright © 2019 Shawn Baker using the MIT License.
using System;
using System.Collections.Generic;
using Dynastream.Fit;

namespace FitLib
{
	/// <summary>
	/// A FIT lap.
	/// </summary>
	public class FitLap
	{
		public int FirstRecord { get; set; } = -1;
		public int LastRecord { get; set; } = -1;

		public System.DateTime? Timestamp { get; set; } = null;
		public System.DateTime? StartTime { get; set; } = null;
		public TimeSpan? TotalElapsedTime { get; set; } = null;
		public TimeSpan? TotalMovingTime { get; set; } = null;
		public TimeSpan? TotalTimerTime { get; set; } = null;

		public int? TotalCalories { get; set; } = null;
		public uint? TotalWork { get; set; } = null;
		public float? TotalDistance { get; set; } = null;

		public int? AveCadence { get; set; } = null;
		public int? MaxCadence { get; set; } = null;
		public int? AveHeartRate { get; set; } = null;
		public int? MaxHeartRate { get; set; } = null;
		public int? MinHeartRate { get; set; } = null;
		public int? AvePower { get; set; } = null;
		public int? MaxPower { get; set; } = null;
		public float? AveSpeed { get; set; } = null;
		public float? MaxSpeed { get; set; } = null;
		public float? AveAltitude { get; set; } = null;
		public float? MaxAltitude { get; set; } = null;
		public float? MinAltitude { get; set; } = null;
		public int? TotalAscent { get; set; } = null;
		public int? TotalDescent { get; set; } = null;
		public float? AveGrade { get; set; } = null;
		public int? AveRunningCadence { get; set; } = null;
		public int? MaxRunningCadence { get; set; } = null;
		public int? AveTemperature { get; set; } = null;
		public int? MaxTemperature { get; set; } = null;

		public float? StartPositionLat { get; set; } = null;
		public float? StartPositionLong { get; set; } = null;
		public float? EndPositionLat { get; set; } = null;
		public float? EndPositionLong { get; set; } = null;

		public string AveCadencePosition { get; set; } = null;
		public string AveLeftPowerPhase { get; set; } = null;
		public string AveLeftPowerPhasePeak { get; set; } = null;
		public string AvePowerPosition { get; set; } = null;
		public string AveRightPowerPhase { get; set; } = null;
		public string AveRightPowerPhasePeak { get; set; } = null;
		public string AveSaturatedHemoglobinPercent { get; set; } = null;
		public string AveTotalHemoglobinConc { get; set; } = null;
		public string MaxCadencePosition { get; set; } = null;
		public string MaxPowerPosition { get; set; } = null;
		public string StrokeCounts { get; set; } = null;
		public string ZoneCounts { get; set; } = null;

		public int? OpponentScore { get; set; } = null;
		public int? PlayerScore { get; set; } = null;
		public Sport? Sport { get; set; } = null;
		public SubSport? SubSport { get; set; } = null;
		public SwimStroke? SwimStroke { get; set; } = null;
		public float? AveStrokeDistance { get; set; } = null;
		//public int? StrokeCount { get; set; } = null;
		public int? NumLengths { get; set; } = null;
		public uint? TotalStrides { get; set; } = null;
		public float? AveStepLength { get; set; } = null;
		public Intensity? Intensity { get; set; } = null;
		public LapTrigger? LapTrigger { get; set; } = null;
		public int? RepetitionNum { get; set; } = null;

		public float? AveCombinedPedalSmoothness { get; set; } = null;
		public float? AveFractionalCadence { get; set; } = null;
		public int? AveLeftPco { get; set; } = null;
		public float? AveLeftPedalSmoothness { get; set; } = null;
		public float? AveLeftTorqueEffectiveness { get; set; } = null;
		public int? AveLevMotorPower { get; set; } = null;
		public float? AveNegGrade { get; set; } = null;
		public float? AveNegVerticalSpeed { get; set; } = null;
		public float? AvePosGrade { get; set; } = null;
		public float? AvePosVerticalSpeed { get; set; } = null;
		public int? AveRightPco { get; set; } = null;
		public float? AveRightPedalSmoothness { get; set; } = null;
		public float? AveRightTorqueEffectiveness { get; set; } = null;
		public float? AveStanceTime { get; set; } = null;
		public float? AveStanceTimeBalance { get; set; } = null;
		public float? AveStanceTimePercent { get; set; } = null;
		public float? AvgVam { get; set; } = null;
		public float? AvgVerticalOscillation { get; set; } = null;
		public float? AvgVerticalRatio { get; set; } = null;
		public float? EnhancedAvgAltitude { get; set; } = null;
		public float? EnhancedAvgSpeed { get; set; } = null;
		public float? EnhancedMaxAltitude { get; set; } = null;
		public float? EnhancedMaxSpeed { get; set; } = null;
		public float? EnhancedMinAltitude { get; set; } = null;
		
		public float? FirstLengthIndex { get; set; } = null;
		public float? GpsAccuracy { get; set; } = null;
		public float? LeftRightBalance { get; set; } = null;
		public float? LevBatteryConsumption { get; set; } = null;
		public float? MaxFractionalCadence { get; set; } = null;
		public float? MaxLevMotorPower { get; set; } = null;
		public float? MaxNegGrade { get; set; } = null;
		public float? MaxNegVerticalSpeed { get; set; } = null;
		public float? MaxPosGrade { get; set; } = null;
		public float? MaxPosVerticalSpeed { get; set; } = null;
		public string MaxSaturatedHemoglobinPercent { get; set; } = null;
		public string MaxTotalHemoglobinConc { get; set; } = null;

		public string MinSaturatedHemoglobinPercent { get; set; } = null;
		public string MinTotalHemoglobinConc { get; set; } = null;
		public float? NormalizedPower { get; set; } = null;
		public float? NumActiveLengths { get; set; } = null;
		public float? StandCount { get; set; } = null;
		public string TimeInCadenceZone { get; set; } = null;
		public string TimeInHrZone { get; set; } = null;
		public string TimeInPowerZone { get; set; } = null;
		public string TimeInSpeedZone { get; set; } = null;
		public float? TimeStanding { get; set; } = null;
		public float? TotalCycles { get; set; } = null;
		public float? TotalFatCalories { get; set; } = null;
		public float? TotalFractionalCycles { get; set; } = null;
		public float? WktStepIndex { get; set; } = null;

		public Event? Event { get; set; } = null;
		public int? EventGroup { get; set; } = null;
		public EventType? EventType { get; set; } = null;

		public FitLap(LapMesg msg, int first, int last)
		{
			FirstRecord = first;
			LastRecord = last;

			AveAltitude = msg.GetAvgAltitude();
			AveCadence = msg.GetAvgCadence();
			AveCadencePosition = FitFile.GetByteList(msg.GetNumAvgCadencePosition(), msg.GetAvgCadencePosition);
			AveCombinedPedalSmoothness = msg.GetAvgCombinedPedalSmoothness();
			AveFractionalCadence = msg.GetAvgFractionalCadence();
			AveGrade = msg.GetAvgGrade();
			AveHeartRate = msg.GetAvgHeartRate();
			AveLeftPco = msg.GetAvgLeftPco();
			AveLeftPedalSmoothness = msg.GetAvgLeftPedalSmoothness();
			AveLeftPowerPhase = FitFile.GetFloatList(msg.GetNumAvgLeftPowerPhase(), msg.GetAvgLeftPowerPhase);
			AveLeftPowerPhasePeak = FitFile.GetFloatList(msg.GetNumAvgLeftPowerPhasePeak(), msg.GetAvgLeftPowerPhasePeak);
			AveLeftTorqueEffectiveness = msg.GetAvgLeftTorqueEffectiveness();
			AveLevMotorPower = msg.GetAvgLevMotorPower();
			AveNegGrade = msg.GetAvgNegGrade();
			AveNegVerticalSpeed = msg.GetAvgNegVerticalSpeed();
			AvePosGrade = msg.GetAvgPosGrade();
			AvePosVerticalSpeed = msg.GetAvgPosVerticalSpeed();
			AvePower = msg.GetAvgPower();
			AvePowerPosition = FitFile.GetUShortList(msg.GetNumAvgPowerPosition(), msg.GetAvgPowerPosition);
			AveRightPco = msg.GetAvgRightPco();
			AveRightPedalSmoothness = msg.GetAvgRightPedalSmoothness();
			AveRightPowerPhase = FitFile.GetFloatList(msg.GetNumAvgRightPowerPhase(), msg.GetAvgRightPowerPhase);
			AveRightPowerPhasePeak = FitFile.GetFloatList(msg.GetNumAvgRightPowerPhasePeak(), msg.GetAvgRightPowerPhasePeak);
			AveRightTorqueEffectiveness = msg.GetAvgRightTorqueEffectiveness();
			AveRunningCadence = msg.GetAvgRunningCadence();
			AveSaturatedHemoglobinPercent = FitFile.GetFloatList(msg.GetNumAvgSaturatedHemoglobinPercent(), msg.GetAvgSaturatedHemoglobinPercent);
			AveSpeed = FitFile.GetSpeed(msg.GetAvgSpeed());
			AveStanceTime = msg.GetAvgStanceTime();
			AveStanceTimeBalance = msg.GetAvgStanceTimeBalance();
			AveStanceTimePercent = msg.GetAvgStanceTimePercent();
			AveStepLength = msg.GetAvgStepLength();
			AveStrokeDistance = msg.GetAvgStrokeDistance();
			AveTemperature = msg.GetAvgTemperature();
			AveTotalHemoglobinConc = FitFile.GetFloatList(msg.GetNumAvgTotalHemoglobinConc(), msg.GetAvgTotalHemoglobinConc);
			AvgVam = msg.GetAvgVam();
			AvgVerticalOscillation = msg.GetAvgVerticalOscillation();
			AvgVerticalRatio = msg.GetAvgVerticalRatio();
			EndPositionLat = FitFile.GetDegrees(msg.GetEndPositionLat());
			EndPositionLong = FitFile.GetDegrees(msg.GetEndPositionLong());
			EnhancedAvgAltitude = msg.GetEnhancedAvgAltitude();
			EnhancedAvgSpeed = FitFile.GetSpeed(msg.GetEnhancedAvgSpeed());
			EnhancedMaxAltitude = msg.GetEnhancedMaxAltitude();
			EnhancedMaxSpeed = FitFile.GetSpeed(msg.GetEnhancedMaxSpeed());
			EnhancedMinAltitude = msg.GetEnhancedMinAltitude();
			Event = msg.GetEvent();
			EventGroup = msg.GetEventGroup();
			EventType = msg.GetEventType();
			FirstLengthIndex = msg.GetFirstLengthIndex();
			GpsAccuracy = msg.GetGpsAccuracy();
			Intensity = msg.GetIntensity();
			LapTrigger = msg.GetLapTrigger();
			LeftRightBalance = msg.GetLeftRightBalance();
			LevBatteryConsumption = msg.GetLevBatteryConsumption();
			MaxAltitude = msg.GetMaxAltitude();
			MaxCadence = msg.GetMaxCadence();
			MaxCadencePosition = FitFile.GetByteList(msg.GetNumMaxCadencePosition(), msg.GetMaxCadencePosition);
			MaxFractionalCadence = msg.GetMaxFractionalCadence();
			MaxHeartRate = msg.GetMaxHeartRate();
			MaxLevMotorPower = msg.GetMaxLevMotorPower();
			MaxNegGrade = msg.GetMaxNegGrade();
			MaxNegVerticalSpeed = msg.GetMaxNegVerticalSpeed();
			MaxPosGrade = msg.GetMaxPosGrade();
			MaxPosVerticalSpeed = msg.GetMaxPosVerticalSpeed();
			MaxPower = msg.GetMaxPower();
			MaxPowerPosition = FitFile.GetUShortList(msg.GetNumMaxPowerPosition(), msg.GetMaxPowerPosition);
			MaxRunningCadence = msg.GetMaxRunningCadence();
			MaxSaturatedHemoglobinPercent = FitFile.GetFloatList(msg.GetNumMaxSaturatedHemoglobinPercent(), msg.GetMaxSaturatedHemoglobinPercent);
			MaxSpeed = FitFile.GetSpeed(msg.GetMaxSpeed());
			MaxTemperature = msg.GetMaxTemperature();
			MaxTotalHemoglobinConc = FitFile.GetFloatList(msg.GetNumMaxTotalHemoglobinConc(), msg.GetMaxTotalHemoglobinConc);
			MinAltitude = msg.GetMinAltitude();
			MinHeartRate = msg.GetMinHeartRate();
			MinSaturatedHemoglobinPercent = FitFile.GetFloatList(msg.GetNumMinSaturatedHemoglobinPercent(), msg.GetMinSaturatedHemoglobinPercent);
			MinTotalHemoglobinConc = FitFile.GetFloatList(msg.GetNumMinTotalHemoglobinConc(), msg.GetMinTotalHemoglobinConc);
			NormalizedPower = msg.GetNormalizedPower();
			NumActiveLengths = msg.GetNumActiveLengths();
			NumLengths = msg.GetNumLengths();
			OpponentScore = msg.GetOpponentScore();
			PlayerScore = msg.GetPlayerScore();
			RepetitionNum = msg.GetRepetitionNum();
			Sport = msg.GetSport();
			StandCount = msg.GetStandCount();
			StartPositionLat = FitFile.GetDegrees(msg.GetStartPositionLat());
			StartPositionLong = FitFile.GetDegrees(msg.GetStartPositionLong());
			StartTime = FitFile.GetDateTime(msg.GetStartTime());
			StrokeCounts = FitFile.GetUShortList(msg.GetNumStrokeCount(), msg.GetStrokeCount);
			SubSport = msg.GetSubSport();
			SwimStroke = msg.GetSwimStroke();
			TimeInCadenceZone = FitFile.GetFloatList(msg.GetNumTimeInCadenceZone(), msg.GetTimeInCadenceZone);
			TimeInHrZone = FitFile.GetFloatList(msg.GetNumTimeInHrZone(), msg.GetTimeInHrZone);
			TimeInPowerZone = FitFile.GetFloatList(msg.GetNumTimeInPowerZone(), msg.GetTimeInPowerZone);
			TimeInSpeedZone = FitFile.GetFloatList(msg.GetNumTimeInSpeedZone(), msg.GetTimeInSpeedZone);
			Timestamp = FitFile.GetDateTime(msg.GetTimestamp());
			TimeStanding = msg.GetTimeStanding();
			TotalAscent = msg.GetTotalAscent();
			TotalCalories = msg.GetTotalCalories();
			TotalCycles = msg.GetTotalCycles();
			TotalDescent = msg.GetTotalDescent();
			TotalDistance = FitFile.GetDistance(msg.GetTotalDistance());
			TotalElapsedTime = FitFile.GetTimeSpan(msg.GetTotalElapsedTime());
			TotalFatCalories = msg.GetTotalFatCalories();
			TotalFractionalCycles = msg.GetTotalFractionalCycles();
			TotalMovingTime = FitFile.GetTimeSpan(msg.GetTotalMovingTime());
			TotalStrides = msg.GetTotalStrides();
			TotalTimerTime = FitFile.GetTimeSpan(msg.GetTotalTimerTime());
			TotalWork = FitFile.GetWork(msg.GetTotalWork());
			WktStepIndex = msg.GetWktStepIndex();
			ZoneCounts = FitFile.GetUShortList(msg.GetNumZoneCount(), msg.GetZoneCount);
		}
	}

	/// <summary>
	/// List of FIT laps.
	/// </summary>
	public class FitLapList : List<FitLap> { }
}
