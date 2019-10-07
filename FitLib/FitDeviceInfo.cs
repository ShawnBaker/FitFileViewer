// Copyright © 2019 Shawn Baker using the MIT License.
using System.Collections.Generic;
using Dynastream.Fit;

namespace FitLib
{
	/// <summary>
	/// A FIT device info.
	/// </summary>
	public class FitDeviceInfo
	{
		public System.DateTime? Timestamp { get; set; } = null;
		public System.TimeSpan? CumOperatingTime { get; set; } = null;
		public int? Manufacturer { get; set; } = null;
		public string ProductName { get; set; } = null;
		public int? Product { get; set; } = null;
		public int? FaveroProduct { get; set; } = null;
		public uint? GarminProduct { get; set; } = null;
		public uint? SerialNumber { get; set; } = null;
		public int? AntDeviceNumber { get; set; } = null;
		public int? AntDeviceType { get; set; } = null;
		public AntNetwork? AntNetwork { get; set; } = null;
		public int? AntplusDeviceType { get; set; } = null;
		public int? AntTransmissionType { get; set; } = null;
		public int? BatteryStatus { get; set; } = null;
		public float? BatteryVoltage { get; set; } = null;
		public string Descriptor  { get; set; } = null;
		public int? DeviceIndex { get; set; } = null;
		public int? DeviceType { get; set; } = null;
		public int? HardwareVersion { get; set; } = null;
		public float? SoftwareVersion { get; set; } = null;
		public SourceType? SourceType { get; set; } = null;
		public BodyLocation? SensorPosition { get; set; } = null;

		public FitDeviceInfo(DeviceInfoMesg msg)
		{
			AntDeviceNumber = msg.GetAntDeviceNumber();
			AntDeviceType = msg.GetAntDeviceType();
			AntNetwork = msg.GetAntNetwork();
			AntplusDeviceType = msg.GetAntplusDeviceType();
			AntTransmissionType = msg.GetAntTransmissionType();
			BatteryStatus = msg.GetBatteryStatus();
			BatteryVoltage = msg.GetBatteryVoltage();
			CumOperatingTime = FitFile.GetTimeSpan(msg.GetCumOperatingTime());
			Descriptor = msg.GetDescriptorAsString();
			DeviceIndex = msg.GetDeviceIndex();
			DeviceType = msg.GetDeviceType();
			FaveroProduct = msg.GetFaveroProduct();
			GarminProduct = msg.GetGarminProduct();
			HardwareVersion = msg.GetHardwareVersion();
			Manufacturer = msg.GetManufacturer();
			Product = msg.GetProduct();
			ProductName = msg.GetProductNameAsString();
			SensorPosition = msg.GetSensorPosition();
			SerialNumber = msg.GetSerialNumber();
			SoftwareVersion = msg.GetSoftwareVersion();
			SourceType = msg.GetSourceType();
			Timestamp = FitFile.GetDateTime(msg.GetTimestamp());
		}
	}

	/// <summary>
	/// List of FIT device infos.
	/// </summary>
	public class FitDeviceInfoList : List<FitDeviceInfo> { }
}
