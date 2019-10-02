using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using Dynastream.Fit;
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
			DisplayFitFile();
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
		/// Displays the FIT file.
		/// </summary>
		private void DisplayFitFile()
		{
			if (fitFile != null)
			{
				// display the file values
				FileNameTextBox.Text = fitFile.FileName;
				FileNameTextBox.Focus();
				FileNameTextBox.Select(FileNameTextBox.Text.Length, 0);
				TypesLabel.Content = fitFile.Type.ToString();
				ManufacturerLabel.Content = manufacturers[fitFile.Manufacturer];
				ProductLabel.Content = fitFile.Product.ToString();
				SerialNumberLabel.Content = fitFile.SerialNumber.ToString();
				CreationTimeLabel.Content = fitFile.CreationTime.ToString("yyyy/MM/dd HH:mm:ss");

				// display the record summary
				NumRecordsLabel.Content = fitFile.Records.Count.ToString();
				NumLengthsLabel.Content = fitFile.Lengths.Count.ToString();
				NumLapsLabel.Content = fitFile.Laps.Count.ToString();
				NumSessionsLabel.Content = fitFile.Sessions.Count.ToString();
				NumActivitiesLabel.Content = fitFile.Activities.Count.ToString();
				TimeLabel.Content = (fitFile.Records[fitFile.Records.Count - 1].Time - fitFile.Records[0].Time).ToString();
				DistanceLabel.Content = fitFile.Records[fitFile.Records.Count - 1].Distance.ToString("0.#");
				FitRecordSummary summary = fitFile.Records.Summary;
				AverageCadenceLabel.Content = summary.AveCadence.ToString("0");
				AverageHeartRateLabel.Content = summary.AveHeartRate.ToString("0");
				AveragePowerLabel.Content = summary.AvePower.ToString("0");
				AverageSpeedLabel.Content = summary.AveSpeed.ToString("0.#");
				MaxCadenceLabel.Content = summary.MaxCadence.ToString();
				MaxHeartRateLabel.Content = summary.MaxHeartRate.ToString();
				MaxPowerLabel.Content = summary.MaxPower.ToString();
				MaxSpeedLabel.Content = summary.MaxSpeed.ToString("0.#");

				// display the records, lengths, laps, sessions and activities
				RecordsList.ItemsSource = fitFile.Records;
				LengthsList.ItemsSource = fitFile.Lengths;
				LapsList.ItemsSource = fitFile.Laps;
				SessionsList.ItemsSource = fitFile.Sessions;
				ActivitiesList.ItemsSource = fitFile.Activities;
			}
			else
			{
				FileNameTextBox.Text = "";
				TypesLabel.Content = "";
				ManufacturerLabel.Content = "";
				ProductLabel.Content = "";
				SerialNumberLabel.Content = "";
				CreationTimeLabel.Content = System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

				// display the record summary
				NumRecordsLabel.Content = "0";
				NumLengthsLabel.Content = "0";
				NumLapsLabel.Content = "0";
				NumSessionsLabel.Content = "0";
				NumActivitiesLabel.Content = "0";
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

				// display the records, lengths, laps, sessions and activities
				RecordsList.ItemsSource = null;
				LengthsList.ItemsSource = null;
				LapsList.ItemsSource = null;
				SessionsList.ItemsSource = null;
				ActivitiesList.ItemsSource = null;
			}
		}
	}

	[ValueConversion(typeof(IList), typeof(int))]
	public sealed class RecordNumberConverter : FrameworkContentElement, IValueConverter
	{
		public Object Convert(Object data_item, Type t, Object p, CultureInfo _) =>
			((IList)DataContext).IndexOf(data_item);

		public Object ConvertBack(Object o, Type t, Object p, CultureInfo _) =>
			throw new NotImplementedException();
	};
}
