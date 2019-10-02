using System;
using System.Collections.Generic;
using System.IO;
using Dynastream.Fit;

namespace FitFileViewer
{
	public class FitFile
	{
		public string FileName { get; set; }
		public Dynastream.Fit.File Type { get; set; }
		public ushort Manufacturer { get; set; }
		public ushort Product { get; set; }
		public uint SerialNumber { get; set; }
		public ushort Number { get; set; }
		public System.DateTime CreationTime { get; set; }
		public string FriendlyName { get; set; }
		public Gender Gender { get; set; }
		public byte Age { get; set; }
		public float Weight { get; set; }
		public FitRecordList Records { get; set; } = new FitRecordList();
		public FitRecordGroupList Lengths { get; set; } = new FitRecordGroupList();
		public FitRecordGroupList Laps { get; set; } = new FitRecordGroupList();
		public FitRecordGroupList Sessions { get; set; } = new FitRecordGroupList();
		public FitRecordGroupList Activities { get; set; } = new FitRecordGroupList();

		/// <summary>
		/// Constructor
		/// </summary>
		public FitFile()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="fileName">Full path to the FIT file to be opened.</param>
		public FitFile(string fileName)
		{
			Load(fileName);
		}

		/// <summary>
		/// Loads a FIT file.
		/// </summary>
		/// <param name="fileName">Full path to the FIT file to be opened.</param>
		public void Load(string fileName)
		{
			try
			{
				// save the file name
				FileName = fileName;

				// clear any existing laps and records
				Laps.Clear();
				Records.Clear();

				// create the decoder and broadcaster
				Decode decoder = new Decode();
				MesgBroadcaster msgBroadcaster = new MesgBroadcaster();

				// connect the decoder to the broadcaster
				decoder.MesgEvent += msgBroadcaster.OnMesg;
				decoder.MesgDefinitionEvent += msgBroadcaster.OnMesgDefinition;
				decoder.DeveloperFieldDescriptionEvent += HandleDeveloperFieldDescriptionEvent;

				// connect the message event handlers
				msgBroadcaster.MesgDefinitionEvent += HandleMesgDefn;
				msgBroadcaster.FileIdMesgEvent += HandleFileIDMesg;
				msgBroadcaster.UserProfileMesgEvent += HandleUserProfileMesg;
				msgBroadcaster.MonitoringMesgEvent += HandleMonitoringMessage;
				msgBroadcaster.DeviceInfoMesgEvent += HandleDeviceInfoMessage;
				msgBroadcaster.RecordMesgEvent += HandleRecordMessage;
				msgBroadcaster.LengthMesgEvent += HandleLengthMessage;
				msgBroadcaster.LapMesgEvent += HandleLapMessage;
				msgBroadcaster.SessionMesgEvent += HandleSessionMessage;
				msgBroadcaster.ActivityMesgEvent += HandleActivityMessage;

				// open and check the file
				FileStream stream = new FileStream(fileName, FileMode.Open);
				bool status = decoder.IsFIT(stream);
				status &= decoder.CheckIntegrity(stream);

				// Process the file
				if (status)
				{
					decoder.Read(stream);
				}
				else
				{
					try
					{
						if (decoder.InvalidDataSize)
						{
							decoder.Read(stream);
						}
						else
						{
							decoder.Read(stream, DecodeMode.InvalidHeader);
						}
					}
					catch (FitException ex)
					{
					}
				}
				stream.Close();
			}
            catch (Exception ex)
            {
                Console.WriteLine("Load exception: " + ex.Message);
            }
		}

		/// <summary>
		/// HandleMesgDefn
		/// </summary>
		static void HandleMesgDefn(object sender, MesgDefinitionEventArgs e)
		{
			Console.WriteLine("HandleMesgDef: Received Defn for local message #{0}, global num {1}", e.mesgDef.LocalMesgNum, e.mesgDef.GlobalMesgNum);
			Console.WriteLine("\tIt has {0} fields {1} developer fields and is {2} bytes long",
				e.mesgDef.NumFields,
				e.mesgDef.NumDevFields,
				e.mesgDef.GetMesgSize());
		}

		/// <summary>
		/// HandleFileIDMesg
		/// </summary>
		private void HandleFileIDMesg(object sender, MesgEventArgs e)
		{
			FileIdMesg msg = (FileIdMesg)e.mesg;
			try
			{
				Type = msg.GetType() ?? Dynastream.Fit.File.Invalid;
				Manufacturer = msg.GetManufacturer() ?? Dynastream.Fit.Manufacturer.Invalid;
				Product = msg.GetProduct() ?? 0;
				SerialNumber = msg.GetSerialNumber() ?? 0;
				Number = msg.GetNumber() ?? 0;
				CreationTime = (msg.GetTimeCreated() != null) ? msg.GetTimeCreated().GetDateTime() : System.DateTime.MinValue;
			}
			catch (Exception ex)
			{
				Console.WriteLine("HandleFileIDMesg exception: " + ex.Message);
			}
		}

		private void HandleDeveloperFieldDescriptionEvent(object sender, DeveloperFieldDescriptionEventArgs args)
		{
			Console.WriteLine("New Developer Field Description");
			Console.WriteLine("   App Id: {0}", args.Description.ApplicationId);
			Console.WriteLine("   App Version: {0}", args.Description.ApplicationVersion);
			Console.WriteLine("   Field Number: {0}", args.Description.FieldDefinitionNumber);
		}

		/// <summary>
		/// HandleUserProfileMesg
		/// </summary>
		private void HandleUserProfileMesg(object sender, MesgEventArgs e)
		{
			UserProfileMesg msg = (UserProfileMesg)e.mesg;
			try
			{
				FriendlyName = msg.GetFriendlyNameAsString();
				Gender = msg.GetGender() ?? Gender.Invalid;
				Age = msg.GetAge() ?? 0;
				Weight = msg.GetWeight() ?? 0;
			}
			catch (Exception ex)
			{
				Console.WriteLine("HandleUserProfileMesg exception: " + ex.Message);
			}
		}

		private void HandleMonitoringMessage(object sender, MesgEventArgs e)
		{
			MonitoringMesg msg = (MonitoringMesg)e.mesg;
			try
			{
				Console.WriteLine("\tTimestamp  {0}", msg.GetTimestamp());
				Console.WriteLine("\tActivityType {0}", msg.GetActivityType());
				switch (msg.GetActivityType()) // Cycles is a dynamic field
				{
					case ActivityType.Walking:
					case ActivityType.Running:
						Console.WriteLine("\tSteps {0}", msg.GetSteps());
						break;
					case ActivityType.Cycling:
					case ActivityType.Swimming:
						Console.WriteLine("\tStrokes {0}", msg.GetStrokes());
						break;
					default:
						Console.WriteLine("\tCycles {0}", msg.GetCycles());
						break;
				}
			}
			catch (FitException exception)
			{
				Console.WriteLine("\tOnDeviceInfoMesg Error {0}", exception.Message);
				Console.WriteLine("\t{0}", exception.InnerException);
			}
		}

		private void HandleDeviceInfoMessage(object sender, MesgEventArgs e)
		{
			DeviceInfoMesg msg = (DeviceInfoMesg)e.mesg;
			try
			{
				Console.WriteLine("\tTimestamp  {0}", msg.GetTimestamp());
				Console.WriteLine("\tBattery Status{0}", msg.GetBatteryStatus());
			}
			catch (Exception ex)
			{
				Console.WriteLine("HandleDeviceInfoMessage exception: " + ex.Message);
			}
		}

		private void HandleRecordMessage(object sender, MesgEventArgs e)
		{
			RecordMesg msg = (RecordMesg)e.mesg;
			FitRecord record = new FitRecord(this);
			record.Time = new Dynastream.Fit.DateTime(msg.GetTimestamp()).GetDateTime().ToLocalTime();
			record.Distance = (msg.GetDistance() ?? 0) / 1000;
			record.Speed = (msg.GetSpeed() ?? 0) * 3.6f;
			record.Power = msg.GetPower() ?? 0;
			record.Cadence = msg.GetCadence() ?? 0;
			record.HeartRate = msg.GetHeartRate() ?? 0;
			Records.Add(record);
		}

		private void HandleLengthMessage(object sender, MesgEventArgs e)
		{
			LengthMesg msg = (LengthMesg)e.mesg;
			FitRecordGroup len = new FitRecordGroup(this);
			len.FirstRecord = (Lengths.Count > 0) ? Lengths[Lengths.Count - 1].LastRecord + 1 : 0;
			len.LastRecord = Records.Count - 1;
			Lengths.Add(len);
		}

		private void HandleLapMessage(object sender, MesgEventArgs e)
		{
			LapMesg msg = (LapMesg)e.mesg;
			FitRecordGroup lap = new FitRecordGroup(this);
			lap.FirstRecord = (Laps.Count > 0) ? Laps[Laps.Count - 1].LastRecord + 1 : 0;
			lap.LastRecord = Records.Count - 1;
			lap.Calories = msg.GetTotalCalories() ?? 0;
			Laps.Add(lap);
		}

		private void HandleSessionMessage(object sender, MesgEventArgs e)
		{
			SessionMesg msg = (SessionMesg)e.mesg;
			FitRecordGroup session = new FitRecordGroup(this);
			session.FirstRecord = (Sessions.Count > 0) ? Sessions[Sessions.Count - 1].LastRecord + 1 : 0;
			session.LastRecord = Records.Count - 1;
			session.Calories = msg.GetTotalCalories() ?? 0;
			Sessions.Add(session);
		}

		private void HandleActivityMessage(object sender, MesgEventArgs e)
		{
			ActivityMesg msg = (ActivityMesg)e.mesg;
			FitRecordGroup activity = new FitRecordGroup(this);
			activity.FirstRecord = (Activities.Count > 0) ? Activities[Activities.Count - 1].LastRecord + 1 : 0;
			activity.LastRecord = Records.Count - 1;
			Activities.Add(activity);
		}

		private float GetField(Mesg msg, byte fieldNumber)
		{
			Field profileField = Profile.GetField(msg.Num, fieldNumber);
			if (profileField == null)
			{
				return 0;
			}
			int n = msg.GetNumFields();
			int n2 = msg.GetNumFieldValues(fieldNumber);

			float value = 0;
			IEnumerable<FieldBase> fields = msg.GetOverrideField(fieldNumber);
			foreach (FieldBase field in fields)
			{
				object obj = field.GetValue();
				if (float.TryParse(obj.ToString(), out value))
				{
					break;
				}
			}
			return value;
		}
	}

	public class FitRecord
	{
		public System.DateTime Time { get; set; }
		public float Distance { get; set; }
		public float Speed { get; set; }
		public int Power { get; set; }
		public int Cadence { get; set; }
		public int HeartRate { get; set; }
		public TimeSpan ElapsedTime => Time - fitFile.CreationTime;

		private FitFile fitFile;

		public FitRecord(FitFile ff)
		{
			fitFile = ff;
		}
	}

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
						summary.AveSpeed += record.Speed;
						summary.AvePower += record.Power;
						summary.AveHeartRate += record.HeartRate;
						summary.AveCadence += record.Cadence;
						if (record.Speed > summary.MaxSpeed)
						{
							summary.MaxSpeed = record.Speed;
						}
						if (record.Power > summary.MaxPower)
						{
							summary.MaxPower = record.Power;
						}
						if (record.HeartRate > summary.MaxHeartRate)
						{
							summary.MaxHeartRate = record.HeartRate;
						}
						if (record.Cadence > summary.MaxCadence)
						{
							summary.MaxCadence = record.Cadence;
						}
					}
					summary.AveSpeed = summary.AveSpeed / Count;
					summary.AvePower = summary.AvePower / Count;
					summary.AveHeartRate = summary.AveHeartRate / Count;
					summary.AveCadence = summary.AveCadence / Count;
				}
				return summary;
			}
		}
	}

	public class FitRecordGroup
	{
		public int FirstRecord { get; set; } = -1;
		public int LastRecord { get; set; } = -1;

		private FitFile fitFile;
		private FitRecordSummary summary = null;

		public FitRecordGroup(FitFile ff)
		{
			fitFile = ff;
		}

		public bool IsOK => (FirstRecord >= 0 && FirstRecord<fitFile.Records.Count && LastRecord >= 0 && LastRecord<fitFile.Records.Count && LastRecord >= FirstRecord);

		public FitRecordSummary Summary
		{
			get
			{
				if (summary == null && IsOK)
				{
					summary = new FitRecordSummary();
					summary.StartTime = fitFile.Records[FirstRecord].Time;
					summary.ElapsedTime = fitFile.Records[LastRecord].Time - fitFile.Records[FirstRecord].Time;
					for (int i = FirstRecord; i <= LastRecord; i++)
					{
						FitRecord record = fitFile.Records[i];
						summary.AveSpeed += record.Speed;
						summary.AvePower += record.Power;
						summary.AveHeartRate += record.HeartRate;
						summary.AveCadence += record.Cadence;
						if (record.Speed > summary.MaxSpeed)
						{
							summary.MaxSpeed = record.Speed;
						}
						if (record.Power > summary.MaxPower)
						{
							summary.MaxPower = record.Power;
						}
						if (record.HeartRate > summary.MaxHeartRate)
						{
							summary.MaxHeartRate = record.HeartRate;
						}
						if (record.Cadence > summary.MaxCadence)
						{
							summary.MaxCadence = record.Cadence;
						}
					}
					int numRecords = LastRecord - FirstRecord + 1;
					summary.AveSpeed = summary.AveSpeed / numRecords;
					summary.AvePower = summary.AvePower / numRecords;
					summary.AveHeartRate = summary.AveHeartRate / numRecords;
					summary.AveCadence = summary.AveCadence / numRecords;
					summary.Distance = fitFile.Records[LastRecord].Distance - fitFile.Records[FirstRecord].Distance;
				}
				return summary;
			}
		}

		public System.DateTime StartTime => Summary.StartTime;
		public TimeSpan ElapsedTime => Summary.ElapsedTime;
		public double Distance => Summary.Distance;
		public double AveCadence => Summary.AveCadence;
		public double MaxCadence => Summary.MaxCadence;
		public double AveHeartRate => Summary.AveHeartRate;
		public double MaxHeartRate => Summary.MaxHeartRate;
		public double AvePower => Summary.AvePower;
		public double MaxPower => Summary.MaxPower;
		public double AveSpeed => Summary.AveSpeed;
		public double MaxSpeed => Summary.MaxSpeed;
		public int Calories { get; set; }
	}

	public class FitRecordGroupList : List<FitRecordGroup> { }

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
