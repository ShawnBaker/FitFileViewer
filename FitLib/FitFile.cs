// Copyright © 2019 Shawn Baker using the MIT License.
using System;
using System.IO;
using Dynastream.Fit;
using FitFileViewer.Properties;

namespace FitLib
{
	public class FitFile
	{
		// public delegates
		public delegate byte? GetByte(int index);
		public delegate ushort? GetUShort(int index);
		public delegate int? GetInt(int index);
		public delegate float? GetFloat(int index);

		// public data fields
		public string FileName;
		public FitMessageList Messages = new FitMessageList();
		public FitFileID FileID;
		public FitDeviceInfoList DeviceInfos = new FitDeviceInfoList();
		public FitUserProfile UserProfile;
		public FitActivity Activity;
		public FitSessionList Sessions = new FitSessionList();
		public FitLapList Laps = new FitLapList();
		public FitLengthList Lengths = new FitLengthList();
		public FitRecordList Records = new FitRecordList();

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

				// connect the message event handlers
				msgBroadcaster.FileIdMesgEvent += HandleFileIDMesg;
				msgBroadcaster.DeviceInfoMesgEvent += HandleDeviceInfoMessage;
				msgBroadcaster.UserProfileMesgEvent += HandleUserProfileMesg;
				msgBroadcaster.ActivityMesgEvent += HandleActivityMessage;
				msgBroadcaster.SessionMesgEvent += HandleSessionMessage;
				msgBroadcaster.LapMesgEvent += HandleLapMessage;
				msgBroadcaster.LengthMesgEvent += HandleLengthMessage;
				msgBroadcaster.RecordMesgEvent += HandleRecordMessage;

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
						Console.WriteLine("FIT exception: " + ex.Message);
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
		/// Updates the file ID.
		/// </summary>
		private void HandleFileIDMesg(object sender, MesgEventArgs e)
		{
			FileIdMesg msg = (FileIdMesg)e.mesg;
			FileID = new FitFileID(msg);
			FitMessage message = new FitMessage(Resources.FileID);
			Messages.Add(message);
		}

		/// <summary>
		/// Updates the device information.
		/// </summary>
		private void HandleDeviceInfoMessage(object sender, MesgEventArgs e)
		{
			DeviceInfoMesg msg = (DeviceInfoMesg)e.mesg;
			FitDeviceInfo deviceInfo = new FitDeviceInfo(msg);
			DeviceInfos.Add(deviceInfo);
			FitMessage message = new FitMessage(Resources.DeviceInfo);
			Messages.Add(message);
		}

		/// <summary>
		/// Updates the user profile.
		/// </summary>
		private void HandleUserProfileMesg(object sender, MesgEventArgs e)
		{
			UserProfileMesg msg = (UserProfileMesg)e.mesg;
			UserProfile = new FitUserProfile(msg);
			FitMessage message = new FitMessage(Resources.UserProfile);
			Messages.Add(message);
		}

		/// <summary>
		/// Updates the Activity.
		/// </summary>
		private void HandleActivityMessage(object sender, MesgEventArgs e)
		{
			ActivityMesg msg = (ActivityMesg)e.mesg;
			Activity = new FitActivity(msg);
			FitMessage message = new FitMessage(Resources.Activity);
			Messages.Add(message);
		}

		/// <summary>
		/// Adds a new session.
		/// </summary>
		private void HandleSessionMessage(object sender, MesgEventArgs e)
		{
			SessionMesg msg = (SessionMesg)e.mesg;
			FitSession session = new FitSession(msg, (Sessions.Count > 0) ? Sessions[Sessions.Count - 1].LastRecord + 1 : 0, Records.Count - 1);
			Sessions.Add(session);
			string description = (session.FirstRecord < session.LastRecord) ? string.Format("{0} - {1}", session.FirstRecord, session.LastRecord) : session.FirstRecord.ToString();
			FitMessage message = new FitMessage(Resources.Session, description);
			Messages.Add(message);
		}

		/// <summary>
		/// Adds a new lap.
		/// </summary>
		private void HandleLapMessage(object sender, MesgEventArgs e)
		{
			LapMesg msg = (LapMesg)e.mesg;
			FitLap lap = new FitLap(msg, (Laps.Count > 0) ? Laps[Laps.Count - 1].LastRecord + 1 : 0, Records.Count - 1);
			Laps.Add(lap);
			string description = (lap.FirstRecord < lap.LastRecord) ? string.Format("{0} - {1}", lap.FirstRecord, lap.LastRecord) : lap.FirstRecord.ToString();
			FitMessage message = new FitMessage(Resources.Lap, description);
			Messages.Add(message);
		}

		/// <summary>
		/// Adds a new length.
		/// </summary>
		private void HandleLengthMessage(object sender, MesgEventArgs e)
		{
			LengthMesg msg = (LengthMesg)e.mesg;
			FitLength length = new FitLength(msg, (Lengths.Count > 0) ? Lengths[Lengths.Count - 1].LastRecord + 1 : 0, Records.Count - 1);
			Lengths.Add(length);
			string description = (length.FirstRecord < length.LastRecord) ? string.Format("{0} - {1}", length.FirstRecord, length.LastRecord) : length.FirstRecord.ToString();
			FitMessage message = new FitMessage(Resources.Length, description);
			Messages.Add(message);
		}

		/// <summary>
		/// Adds a new record.
		/// </summary>
		private void HandleRecordMessage(object sender, MesgEventArgs e)
		{
			RecordMesg msg = (RecordMesg)e.mesg;
			FitRecord record = new FitRecord(msg, Records);
			Records.Add(record);
			FitRecordMessage message = null;
			if (Messages.Count > 0)
			{
				message = Messages[Messages.Count - 1] as FitRecordMessage;
			}
			if (message != null)
			{
				message.Last = Records.Count - 1;
			}
			else
			{
				message = new FitRecordMessage(Records.Count - 1);
				Messages.Add(message);
			}
		}

		/// <summary>
		/// Gets a system date/time from a Dynastream date/time.
		/// </summary>
		/// <param name="dateTime">Dynastream date/time.</param>
		/// <returns>System date/time.</returns>
		public static System.DateTime? GetDateTime(Dynastream.Fit.DateTime dateTime)
		{
			if (dateTime != null)
			{
				return dateTime.GetDateTime().ToLocalTime();
			}
			return null;
		}

		/// <summary>
		/// Gets a time span from a number of seconds.
		/// </summary>
		/// <param name="seconds">Number of seconds.</param>
		/// <returns>Time span.</returns>
		public static TimeSpan? GetTimeSpan(float? seconds)
		{
			if (seconds != null)
			{
				return new TimeSpan(0, 0, (int)seconds);
			}
			return null;
		}

		/// <summary>
		/// Gets a K distance from a M distance.
		/// </summary>
		/// <param name="distance">Distance in meters.</param>
		/// <returns>Distance in kilometers.</returns>
		public static float? GetDistance(float? distance)
		{
			if (distance != null)
			{
				distance /= 1000;
			}
			return distance;
		}

		/// <summary>
		/// Gets a k/h speed from a m/s speed.
		/// </summary>
		/// <param name="distance">Speed in m/s.</param>
		/// <returns>Speed in k/h.</returns>
		public static float? GetSpeed(float? speed)
		{
			if (speed != null)
			{
				speed *= 3.6f;
			}
			return speed;
		}

		/// <summary>
		/// Gets a number of degrees from a number of semi-circles.
		/// </summary>
		/// <param name="semicircles">Number of semi-circles.</param>
		/// <returns>Number of degrees.</returns>
		public static float? GetDegrees(int? semicircles)
		{
			if (semicircles != null)
			{
				return (float)(semicircles * (180 / Math.Pow(2, 31)));
			}
			return null;
		}

		/// <summary>
		/// Gets a KJ work from a J work.
		/// </summary>
		/// <param name="work">Work in joules.</param>
		/// <returns>Work in kilo-joules.</returns>
		public static uint? GetWork(uint? work)
		{
			if (work != null)
			{
				work /= 1000;
			}
			return work;
		}

		/// <summary>
		/// Gets a list of byte values using a callback function.
		/// </summary>
		/// <param name="n">Number of bytes to get.</param>
		/// <param name="getInt">Function to be called to get each byte.</param>
		/// <returns>CSV list of bytes.</returns>
		public static string GetByteList(int n, GetByte getByte)
		{
			if (n == 0) return null;
			string list = "";
			for (int i = 0; i < n; i++)
			{
				if (i > 0)
				{
					list += ",";
				}
				byte? value = getByte(i);
				if (value != null)
				{
					list += value.Value.ToString();
				}
			}
			return list;
		}

		/// <summary>
		/// Gets a list of ushort values using a callback function.
		/// </summary>
		/// <param name="n">Number of ushorts to get.</param>
		/// <param name="getInt">Function to be called to get each ushort.</param>
		/// <returns>CSV list of ushorts.</returns>
		public static string GetUShortList(int n, GetUShort getUshort)
		{
			if (n == 0) return null;
			string list = "";
			for (int i = 0; i < n; i++)
			{
				if (i > 0)
				{
					list += ",";
				}
				ushort? value = getUshort(i);
				if (value != null)
				{
					list += value.Value.ToString();
				}
			}
			return list;
		}

		/// <summary>
		/// Gets a list of integer values using a callback function.
		/// </summary>
		/// <param name="n">Number of integers to get.</param>
		/// <param name="getInt">Function to be called to get each integer.</param>
		/// <returns>CSV list of integers.</returns>
		public static string GetIntList(int n, GetInt getInt)
		{
			if (n == 0) return null;
			string list = "";
			for (int i = 0; i < n; i++)
			{
				if (i > 0)
				{
					list += ",";
				}
				int? value = getInt(i);
				if (value != null)
				{
					list += value.Value.ToString();
				}
			}
			return list;
		}

		/// <summary>
		/// Gets a list of float values using a callback function.
		/// </summary>
		/// <param name="n">Number of floats to get.</param>
		/// <param name="getInt">Function to be called to get each float.</param>
		/// <returns>CSV list of floats.</returns>
		public static string GetFloatList(int n, GetFloat getFloat)
		{
			if (n == 0) return null;
			string list = "";
			for (int i = 0; i < n; i++)
			{
				if (i > 0)
				{
					list += ",";
				}
				float? value = getFloat(i);
				if (value != null)
				{
					list += value.Value.ToString("0.0");
				}
			}
			return list;
		}
	}
}
