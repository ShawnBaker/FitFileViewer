// Copyright © 2019 Shawn Baker using the MIT License.
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Dynastream.Fit;
using FitLib;
using Microsoft.Win32;

namespace FitFileViewer
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private Dictionary<ushort, string> manufacturers = new Dictionary<ushort, string>();
		private OpenFileDialog openFileDialog;
		private FitFile fitFile = null;
		private List<string> deviceInfoProperties = new List<string>();
		private List<string> sessionProperties = new List<string>();
		private List<string> lapProperties = new List<string>();
		private List<string> lengthProperties = new List<string>();
		private List<string> recordProperties = new List<string>();

		/// <summary>
		/// Constructor - Initializes the controls.
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();

			// get the list of manufacturers
			FieldInfo[] fields = typeof(Manufacturer).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
			foreach (FieldInfo fi in fields)
			{
				manufacturers[(ushort)fi.GetValue(null)] = fi.Name;
			}

			// create the open file dialog
			openFileDialog = new OpenFileDialog();
			openFileDialog.FileName = "";
			openFileDialog.DefaultExt = "fit";
			openFileDialog.Filter = "Fit files (*.fit)|*.fit|All files (*.*)|*.*";
			openFileDialog.FilterIndex = 1;

			// initialize the display
			ClearDisplay();
		}

		/// <summary>
		/// Opens a FIT file.
		/// </summary>
		private void OpenFileButton_Click(object sender, RoutedEventArgs e)
		{
			// set the initial directory from Settings.OpenFilePath
			string path = (string)Properties.Settings.Default["OpenFilePath"];
			if (path.Length == 0)
			{
				path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			}
			openFileDialog.InitialDirectory = path;

			// prompt the user for a file name
			bool? result = openFileDialog.ShowDialog(this);
			if (result == true)
			{
				// update Settings.OpenFilePath from the file name
				path = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
				Properties.Settings.Default["OpenFilePath"] = path;
				Properties.Settings.Default.Save();
				string fileName = openFileDialog.FileName;

				// load the FIT file
				fitFile = new FitFile(fileName);
				deviceInfoProperties = GetNonNullPropertiesName(fitFile.DeviceInfos);
				sessionProperties = GetNonNullPropertiesName(fitFile.Sessions);
				lapProperties = GetNonNullPropertiesName(fitFile.Laps);
				lengthProperties = GetNonNullPropertiesName(fitFile.Lengths);
				recordProperties = GetNonNullPropertiesName(fitFile.Records);

				// display the file
				DisplayFitFile();
			}
		}

		/// <summary>
		/// Displays the about window.
		/// </summary>
		private void AboutButton_Click(object sender, RoutedEventArgs e)
		{
			AboutWindow window = new AboutWindow();
			window.Owner = this;
			window.ShowDialog();
		}

		/// <summary>
		/// Clears all the display controls.
		/// </summary>
		private void ClearDisplay()
		{
			// clear the file name
			FileNameTextBox.Text = "";

			// clear the record summary
			NumRecordsLabel.Content = "0";
			NumLengthsLabel.Content = "0";
			NumLapsLabel.Content = "0";
			NumSessionsLabel.Content = "0";
			TimeLabel.Content = "0";
			DistanceLabel.Content = "0";
			AverageCadenceLabel.Content = "0";
			AverageHeartRateLabel.Content = "0";
			AveragePowerLabel.Content = "0";
			AverageSpeedLabel.Content = "0";
			MaxCadenceLabel.Content = "0";
			MaxHeartRateLabel.Content = "0";
			MaxPowerLabel.Content = "0";
			MaxSpeedLabel.Content = "0";

			// clear the data grids
			FileIDDataGrid.ItemsSource = null;
			DeviceInfoDataGrid.ItemsSource = null;
			UserProfileDataGrid.ItemsSource = null;
			ActivityDataGrid.ItemsSource = null;
			SessionsDataGrid.ItemsSource = null;
			LapsDataGrid.ItemsSource = null;
			LengthsDataGrid.ItemsSource = null;
			RecordsDataGrid.ItemsSource = null;
			MessagesDataGrid.ItemsSource = null;
		}

		/// <summary>
		/// Displays the FIT file.
		/// </summary>
		private void DisplayFitFile()
		{
			ClearDisplay();
			if (fitFile != null)
			{
				// display the file name
				FileNameTextBox.Text = fitFile.FileName;
				FileNameTextBox.Focus();
				FileNameTextBox.Select(FileNameTextBox.Text.Length, 0);

				// display the record summary
				NumRecordsLabel.Content = fitFile.Records.Count.ToString();
				NumLengthsLabel.Content = fitFile.Lengths.Count.ToString();
				NumLapsLabel.Content = fitFile.Laps.Count.ToString();
				NumSessionsLabel.Content = fitFile.Sessions.Count.ToString();
				FitRecord last = fitFile.Records[fitFile.Records.Count - 1];
				TimeLabel.Content = (last.Timestamp - fitFile.Records[0].Timestamp).ToString();
				DistanceLabel.Content = (last.Distance ?? 0).ToString("0.#");
				FitRecordSummary summary = fitFile.Records.Summary;
				AverageCadenceLabel.Content = summary.AveCadence.ToString("0");
				AverageHeartRateLabel.Content = summary.AveHeartRate.ToString("0");
				AveragePowerLabel.Content = summary.AvePower.ToString("0");
				AverageSpeedLabel.Content = summary.AveSpeed.ToString("0.#");
				MaxCadenceLabel.Content = summary.MaxCadence.ToString();
				MaxHeartRateLabel.Content = summary.MaxHeartRate.ToString();
				MaxPowerLabel.Content = summary.MaxPower.ToString();
				MaxSpeedLabel.Content = summary.MaxSpeed.ToString("0.#");

				// display the data grids
				FileIDDataGrid.ItemsSource = GetNonNullPropertiesValues(typeof(FitFileID), fitFile.FileID);
				DeviceInfoDataGrid.ItemsSource = fitFile.DeviceInfos;
				UserProfileDataGrid.ItemsSource = GetNonNullPropertiesValues(typeof(FitUserProfile), fitFile.UserProfile);
				ActivityDataGrid.ItemsSource = GetNonNullPropertiesValues(typeof(FitActivity), fitFile.Activity);
				SessionsDataGrid.ItemsSource = fitFile.Sessions;
				LapsDataGrid.ItemsSource = fitFile.Laps;
				LengthsDataGrid.ItemsSource = fitFile.Lengths;
				RecordsDataGrid.ItemsSource = fitFile.Records;
				MessagesDataGrid.ItemsSource = fitFile.Messages;
			}
		}

		/// <summary>
		/// Adds the row number column to the records grid.
		/// </summary>
		private void RecordsDataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
		{
			e.Row.Header = e.Row.GetIndex().ToString();
		}

		/// <summary>
		/// Hides the null columns in the device info grid.
		/// </summary>
		private void DeviceInfoDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			e.Cancel = !deviceInfoProperties.Contains(e.PropertyName);
		}

		/// <summary>
		/// Hides the null columns in the session grid.
		/// </summary>
		private void SessionsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			e.Cancel = !sessionProperties.Contains(e.PropertyName);
		}

		/// <summary>
		/// Hides the null columns in the lap grid.
		/// </summary>
		private void LapsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			e.Cancel = !lapProperties.Contains(e.PropertyName);
		}

		/// <summary>
		/// Hides the null columns in the length grid.
		/// </summary>
		private void LengthsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			e.Cancel = !lengthProperties.Contains(e.PropertyName);
		}

		/// <summary>
		/// Hides the null columns in the records grid.
		/// </summary>
		private void RecordsDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
		{
			e.Cancel = !recordProperties.Contains(e.PropertyName);
		}

		/// <summary>
		/// Gets a list of the non-null property names in a list of objects.
		/// </summary>
		/// <typeparam name="T">Class of the objects in the list.</typeparam>
		/// <param name="list">List of T objects.</param>
		/// <returns>List of the non-null property names.</returns>
		private List<string> GetNonNullPropertiesName<T>(List<T> list)
		{
			List<string> properties = new List<string>();
			Type itemType = typeof(T);
			foreach (PropertyInfo property in itemType.GetProperties())
			{
				foreach (T t in list)
				{
					if (property.GetValue(t) != null)
					{
						properties.Add(property.Name);
						break;
					}
				}
			}
			return properties;
		}

		/// <summary>
		/// Gets a list of the non-null property names and values from a type and object of that type.
		/// </summary>
		/// <param name="type">Type of the object.</param>
		/// <param name="obj">Object to get the list for.</param>
		/// <returns>List of the non-null property names and values.</returns>
		private PropertyValueList GetNonNullPropertiesValues(Type type, object obj)
		{
			PropertyValueList propertyValues = new PropertyValueList();
			if (obj != null)
			{
				foreach (PropertyInfo property in type.GetProperties())
				{
					object value = property.GetValue(obj);
					if (value != null)
					{
						propertyValues.Add(new PropertyValue(property.Name, value.ToString()));
					}
				}
			}
			return propertyValues;
		}

		/// <summary>
		/// Represents a property name and value.
		/// </summary>
		private class PropertyValue
		{
			public string Property { get; set; }
			public string Value { get; set; }

			public PropertyValue(string property, string value)
			{
				Property = property;
				Value = value;
			}
		}

		private class PropertyValueList : List<PropertyValue> { }
	}
}
