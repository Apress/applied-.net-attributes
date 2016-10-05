using System;
using System.IO;
using System.Security.Permissions;

public class FileOpener
{
	private const int CHUNK_SIZE = 100;

	public FileOpener()
	{
	}

	[FileIOPermission(SecurityAction.Deny, Read=@"C:\Windows\System32")]
	public int ReadFile(string filePath)
	{
		int retVal = 0;
		
//		FileIOPermission filePermDeny = new FileIOPermission(
//			FileIOPermissionAccess.Read, 
//			Environment.SystemDirectory);
//		filePermDeny.Deny();

		using(FileStream fs = File.Open(filePath, FileMode.Open, 
				  FileAccess.Read))
		{
			byte[] fileData = new byte[CHUNK_SIZE];
			int bytesRead = 0;

			while((bytesRead = fs.Read(fileData, 0, fileData.Length)) > 0)
			{
				//  Do something wonderful with the file's data.
				retVal += bytesRead;
			}
		}

		return retVal;
	}
}
