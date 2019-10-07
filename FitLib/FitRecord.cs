// Copyright © 2019 Shawn Baker using the MIT License.
using System;
using System.Collections.Generic;
using Dynastream.Fit;

namespace FitLib
{
	/// <summary>
	/// A FIT record.
	/// </summary>
	public class FitRecord
	{
		public System.DateTime? Timestamp { get; set; } = null;
		public float? Distance { get; set; } = null;
		public float? Speed { get; set; } = null;
		public int? Power { get; set; } = null;
		public int? Cadence { get; set; } = null;
		public int? HeartRate { get; set; } = null;
		public float? Latitude { get; set; } = null;
		public float? Longitude { get; set; } = null;
		public float? Altitude { get; set; } = null;
		public float? Grade { get; set; } = null;
		public int? Temperature { get; set; } = null;
		public int? Resistence { get; set; } = null;
		public float? StepLength { get; set; } = null;
		public StrokeType? StrokeType { get; set; } = null;

		public uint? AbsolutePressure { get; set; } = null;
		public uint? AccumulatedPower { get; set; } = null;
		public ActivityType? ActivityType { get; set; } = null;
		public float? BallSpeed { get; set; } = null;
		public float? BatterySoc { get; set; } = null;
		public float? Cadence256 { get; set; } = null;
		public int? Calories { get; set; } = null;
		public int? CnsLoad { get; set; } = null;
		public float? CombinedPedalSmoothness { get; set; } = null;
		public int? CompressedAccumulatedPower { get; set; } = null;
		public string CompressedSpeedDistance { get; set; } = null;
		public float? CycleLength { get; set; } = null;
		public int? Cycles { get; set; } = null;
		public float? Depth { get; set; } = null;
		public int? DeviceIndex { get; set; } = null;
		public float? EnhancedAltitude { get; set; } = null;
		public float? EnhancedSpeed { get; set; } = null;
		public float? FractionalCadence { get; set; } = null;
		public int? GpsAccuracy { get; set; } = null;
		public int? LeftPco { get; set; } = null;
		public float? LeftPedalSmoothness { get; set; } = null;
		public string LeftPowerPhase { get; set; } = null;
		public string LeftPowerPhasePeak { get; set; } = null;
		public int? LeftRightBalance { get; set; } = null;
		public float? LeftTorqueEffectiveness { get; set; } = null;
		public int? MotorPower { get; set; } = null;
		public int? N2Load { get; set; } = null;
		public uint? NdlTime { get; set; } = null;
		public float? NextStopDepth { get; set; } = null;
		public uint? NextStopTime { get; set; } = null;
		public int? RightPco { get; set; } = null;
		public float? RightPedalSmoothness { get; set; } = null;
		public string RightPowerPhase { get; set; } = null;
		public string RightPowerPhasePeak { get; set; } = null;
		public float? RightTorqueEffectiveness { get; set; } = null;
		public float? SaturatedHemoglobinPercent { get; set; } = null;
		public float? SaturatedHemoglobinPercentMax { get; set; } = null;
		public float? SaturatedHemoglobinPercentMin { get; set; } = null;
		public string Speed1s { get; set; } = null;
		public float? StanceTime { get; set; } = null;
		public float? StanceTimeBalance { get; set; } = null;
		public float? StanceTimePercent { get; set; } = null;
		public float? Time128 { get; set; } = null;
		public TimeSpan? TimeFromCourse { get; set; } = null;
		public uint? TotalCycles { get; set; } = null;
		public float? TotalHemoglobinConc { get; set; } = null;
		public float? TotalHemoglobinConcMax { get; set; } = null;
		public float? TotalHemoglobinConcMin { get; set; } = null;
		public float? VerticalOscillation { get; set; } = null;
		public float? VerticalRatio { get; set; } = null;
		public float? VerticalSpeed { get; set; } = null;
		public int? Zone { get; set; } = null;

		public FitRecord(RecordMesg msg, FitRecordList records)
		{
			AbsolutePressure = msg.GetAbsolutePressure();
			AccumulatedPower = msg.GetAccumulatedPower();
			ActivityType = msg.GetActivityType();
			Altitude = msg.GetAltitude();
			BallSpeed = FitFile.GetSpeed(msg.GetBallSpeed());
			BatterySoc = msg.GetBatterySoc();
			Cadence256 = msg.GetCadence256();
			Cadence = msg.GetCadence();
			Calories = msg.GetCalories();
			CnsLoad = msg.GetCnsLoad();
			CombinedPedalSmoothness = msg.GetCombinedPedalSmoothness();
			CompressedAccumulatedPower = msg.GetCompressedAccumulatedPower();
			CompressedSpeedDistance = FitFile.GetByteList(msg.GetNumCompressedSpeedDistance(), msg.GetCompressedSpeedDistance);
			CycleLength = msg.GetCycleLength();
			Cycles = msg.GetCycles();
			Depth = msg.GetDepth();
			DeviceIndex = msg.GetDeviceIndex();
			Distance = FitFile.GetDistance(msg.GetDistance());
			EnhancedAltitude = msg.GetEnhancedAltitude();
			EnhancedSpeed = FitFile.GetSpeed(msg.GetEnhancedSpeed());
			FractionalCadence = msg.GetFractionalCadence();
			GpsAccuracy = msg.GetGpsAccuracy();
			Grade = msg.GetGrade();
			HeartRate = msg.GetHeartRate();
			LeftPco = msg.GetLeftPco();
			LeftPedalSmoothness = msg.GetLeftPedalSmoothness();
			LeftPowerPhase = FitFile.GetFloatList(msg.GetNumLeftPowerPhase(), msg.GetLeftPowerPhase);
			LeftPowerPhasePeak = FitFile.GetFloatList(msg.GetNumLeftPowerPhasePeak(), msg.GetLeftPowerPhasePeak);
			LeftRightBalance = msg.GetLeftRightBalance();
			LeftTorqueEffectiveness = msg.GetLeftTorqueEffectiveness();
			MotorPower = msg.GetMotorPower();
			N2Load = msg.GetN2Load();
			NdlTime = msg.GetNdlTime();
			NextStopDepth = msg.GetNextStopDepth();
			NextStopTime = msg.GetNextStopTime();
			Latitude = FitFile.GetDegrees(msg.GetPositionLat());
			Longitude = FitFile.GetDegrees(msg.GetPositionLong());
			Power = msg.GetPower();
			Resistence = msg.GetResistance();
			RightPco = msg.GetRightPco();
			RightPedalSmoothness = msg.GetRightPedalSmoothness();
			RightPowerPhase = FitFile.GetFloatList(msg.GetNumRightPowerPhase(), msg.GetRightPowerPhase);
			RightPowerPhasePeak = FitFile.GetFloatList(msg.GetNumRightPowerPhasePeak(), msg.GetRightPowerPhasePeak);
			RightTorqueEffectiveness = msg.GetRightTorqueEffectiveness();
			SaturatedHemoglobinPercent = msg.GetSaturatedHemoglobinPercent();
			SaturatedHemoglobinPercentMax = msg.GetSaturatedHemoglobinPercentMax();
			SaturatedHemoglobinPercentMin = msg.GetSaturatedHemoglobinPercentMin();
			Speed = FitFile.GetSpeed(msg.GetSpeed());
			Speed1s = FitFile.GetFloatList(msg.GetNumSpeed1s(), msg.GetSpeed1s);
			StanceTime = msg.GetStanceTime();
			StanceTimeBalance = msg.GetStanceTimeBalance();
			StanceTimePercent = msg.GetStanceTimePercent();
			StepLength = msg.GetStepLength();
			StrokeType = msg.GetStrokeType();
			Temperature = msg.GetTemperature();
			Time128 = msg.GetTime128();
			TimeFromCourse = FitFile.GetTimeSpan(msg.GetTimeFromCourse());
			Timestamp = FitFile.GetDateTime(msg.GetTimestamp());
			TotalCycles = msg.GetTotalCycles();
			TotalHemoglobinConc = msg.GetTotalHemoglobinConc();
			TotalHemoglobinConcMax = msg.GetTotalHemoglobinConcMax();
			TotalHemoglobinConcMin = msg.GetTotalHemoglobinConcMin();
			VerticalOscillation = msg.GetVerticalOscillation();
			VerticalRatio = msg.GetVerticalRatio();
			VerticalSpeed = FitFile.GetSpeed(msg.GetVerticalSpeed());
			Zone = msg.GetZone();
		}
	}

	/// <summary>
	/// List of FIT records.
	/// </summary>
	public class FitRecordList : List<FitRecord>
	{
		public FitRecordSummary Summary
		{
			get
			{
				FitRecordSummary summary = new FitRecordSummary();
				if (Count > 0)
				{
					foreach (FitRecord record in this)
					{
						if (record.Speed != null)
						{
							summary.AveSpeed += record.Speed.Value;
							if (record.Speed.Value > summary.MaxSpeed)
							{
								summary.MaxSpeed = record.Speed.Value;
							}
						}
						if (record.Power != null)
						{
							summary.AvePower += record.Power.Value;
							if (record.Power.Value > summary.MaxPower)
							{
								summary.MaxPower = record.Power.Value;
							}
						}
						if (record.HeartRate != null)
						{
							summary.AveHeartRate += record.HeartRate.Value;
							if (record.HeartRate.Value > summary.MaxHeartRate)
							{
								summary.MaxHeartRate = record.HeartRate.Value;
							}
						}
						if (record.Cadence != null)
						{
							summary.AveCadence += record.Cadence.Value;
							if (record.Cadence.Value > summary.MaxCadence)
							{
								summary.MaxCadence = record.Cadence.Value;
							}
						}
					}
					summary.AveSpeed = summary.AveSpeed / Count;
					summary.AvePower = summary.AvePower / Count;
					summary.AveHeartRate = summary.AveHeartRate / Count;
					summary.AveCadence = summary.AveCadence / Count;
					if (this[Count - 1].Distance != null && this[0].Distance != null)
					{
						summary.Distance = this[Count - 1].Distance.Value - this[0].Distance.Value;
					}
				}
				return summary;
			}
		}
	}
}
