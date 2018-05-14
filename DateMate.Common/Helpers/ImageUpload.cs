using System;
using System.IO;
using System.Web;

namespace DateMate.Common.Helpers
{
	// Mainly file upload stuffs. Quasi superfluous class.
	public class ImageUpload
	{
		// IIS can't serve static file names without file extensions (without unreliable, tedious
		// configuration), so we'll just accept a few specific file extensions.
		private static string[] allowedExtensions = new string[] { ".gif", ".jpg", ".jpeg", ".png" };
		public HttpPostedFileBase Image { get; set; }
		public string FileExtension { get; set; }
		public string FileName { get; set; }

		public ImageUpload(HttpPostedFileBase image)
		{
			Image = image;
			FileExtension = Path.GetExtension(image.FileName);
			FileName = CreateFileName();
		}

		// Returns true if file name has a valid extension. TODO: Validate MIME-type.
		public bool ValidateImage()
		{
			foreach (var extension in allowedExtensions)
				if (FileExtension == extension)
					return true;

			return false;
		}

		private string CreateFileName()
		{
			return Guid.NewGuid().ToString() + FileExtension;
		}
	}
}
