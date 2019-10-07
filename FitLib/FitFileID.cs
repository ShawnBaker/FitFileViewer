// Copyright © 2019 Shawn Baker using the MIT License.
using Dynastream.Fit;

namespace FitLib
{
	/// <summary>
	/// A FIT file ID.
	/// </summary>
	public class FitFileID
	{
		public File? Type { get; set; } = null;
		public int? Manufacturer { get; set; } = null;
		public string ProductName { get; set; } = null;
		public int? Product { get; set; } = null;
		public int? GarminProduct { get; set; } = null;
		public int? FaveroProduct { get; set; } = null;
		public uint? SerialNumber { get; set; } = null;
		public int? Number { get; set; } = null;
		public System.DateTime? CreationTime { get; set; } = null;

		public FitFileID(FileIdMesg msg)
		{
			FaveroProduct = msg.GetFaveroProduct();
			GarminProduct = msg.GetGarminProduct();
			Manufacturer = msg.GetManufacturer();
			Number = msg.GetNumber();
			ProductName = msg.GetProductNameAsString();
			Product = msg.GetProduct();
			SerialNumber = msg.GetSerialNumber();
			CreationTime = FitFile.GetDateTime(msg.GetTimeCreated());
			Type = msg.GetType();
		}
	}
}
