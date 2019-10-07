// Copyright © 2019 Shawn Baker using the MIT License.
using Dynastream.Fit;

namespace FitLib
{
	/// <summary>
	/// A FIT user profile.
	/// </summary>
	public class FitUserProfile
	{
		public string FriendlyName { get; set; } = null;
		public Gender? Gender { get; set; } = null;
		public int? Age { get; set; } = null;
		public float? Weight { get; set; } = null;
		public float? Height { get; set; } = null;
		public Language? Language { get; set; } = null;
		public int? RestingHeartRate { get; set; } = null;
		public int? DefaultMaxHeartRate { get; set; } = null;
		public int? DefaultMaxBikingHeartRate { get; set; } = null;
		public int? DefaultMaxRunningHeartRate { get; set; } = null;
		public int? LocalId { get; set; } = null;
		public uint? SleepTime { get; set; } = null;
		public uint? WakeTime { get; set; } = null;
		public uint? DiveCount { get; set; } = null;
		public ActivityClass? ActivityClass { get; set; } = null;
		public float? UserRunningStepLength { get; set; } = null;
		public float? UserWalkingStepLength { get; set; } = null;
		public DisplayMeasure? DepthSetting { get; set; } = null;
		public DisplayMeasure? DistSetting { get; set; } = null;
		public DisplayMeasure? ElevSetting { get; set; } = null;
		public DisplayMeasure? HeightSetting { get; set; } = null;
		public DisplayHeart? HrSetting { get; set; } = null;
		public DisplayPosition? PositionSetting { get; set; } = null;
		public DisplayPower? PowerSetting { get; set; } = null;
		public DisplayMeasure? SpeedSetting { get; set; } = null;
		public DisplayMeasure? TemperatureSetting { get; set; } = null;
		public DisplayMeasure? WeightSetting { get; set; } = null;

		public FitUserProfile(UserProfileMesg msg)
		{
			ActivityClass = msg.GetActivityClass();
			Age = msg.GetAge();
			DefaultMaxBikingHeartRate = msg.GetDefaultMaxBikingHeartRate();
			DefaultMaxHeartRate = msg.GetDefaultMaxHeartRate();
			DefaultMaxRunningHeartRate = msg.GetDefaultMaxRunningHeartRate();
			DiveCount = msg.GetDiveCount();
			FriendlyName = msg.GetFriendlyNameAsString();
			Gender = msg.GetGender();
			//msg.GetGlobalId();
			Height = msg.GetHeight();
			Language = msg.GetLanguage();
			LocalId = msg.GetLocalId();
			//NumGlobalId = msg.GetNumGlobalId();
			RestingHeartRate = msg.GetRestingHeartRate();
			SleepTime = msg.GetSleepTime();
			UserRunningStepLength = msg.GetUserRunningStepLength();
			UserWalkingStepLength = msg.GetUserWalkingStepLength();
			WakeTime = msg.GetWakeTime();
			Weight = msg.GetWeight();

			DepthSetting = msg.GetDepthSetting();
			DistSetting = msg.GetDistSetting();
			ElevSetting = msg.GetElevSetting();
			HeightSetting = msg.GetHeightSetting();
			HrSetting = msg.GetHrSetting();
			PositionSetting = msg.GetPositionSetting();
			PowerSetting = msg.GetPowerSetting();
			SpeedSetting = msg.GetSpeedSetting();
			TemperatureSetting = msg.GetTemperatureSetting();
			WeightSetting = msg.GetWeightSetting();
		}
	}
}
