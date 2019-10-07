// Copyright © 2019 Shawn Baker using the MIT License.
using System.Collections.Generic;
using FitFileViewer.Properties;

namespace FitLib
{
	/// <summary>
	/// A FIT message.
	/// </summary>
	public class FitMessage
	{
		public virtual string Name { get; set;  }
		public virtual string Description { get; set;  }

		public FitMessage()
		{
		}

		public FitMessage(string name, string description = null)
		{
			Name = name;
			Description = description;
		}
	}

	public class FitRecordMessage : FitMessage
	{
		public int First;
		public int Last;

		public override string Name
		{
			get => (First <= Last) ? Resources.Records : Resources.Record;
		}

		public override string Description
		{
			get => (First < Last) ? string.Format("{0} - {1}", First, Last) : First.ToString();
		}

		public FitRecordMessage(int first)
		{
			First = first;
			Last = first;
		}
	}

	/// <summary>
	/// A list of FIT messages.
	/// </summary>
	public class FitMessageList : List<FitMessage> { }
}
