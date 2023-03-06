using System.IO;

namespace MoveFiles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Path from the source directory
            // IMPORTANT ADD A BACKSLASH AT THE END OF THE PATH STRING!!
            string sourceDirectoryPath = @"C:\";

            // Path from the destination directory
            string destinationDirectoryPath = @"C:\";

            StartMovingFiles(sourceDirectoryPath, destinationDirectoryPath);
        }

        /// <summary>
        /// Starts the process
        /// </summary>
        /// <param name="sourceDirectoryPath"></param>
        /// <param name="destinationDirectoryPath"></param>
        private static void StartMovingFiles (string sourceDirectoryPath, string destinationDirectoryPath)
        {
            try
            {
                // DateTime variable for the filter
                DateTime dateFilter = new DateTime(2018, 01, 01);

                // List with all filtered filenames
                List<string> fileNames = GetFilteredFileNames(sourceDirectoryPath, dateFilter);

                // Loop through each fileName
                fileNames.ForEach(fileName =>
                {
                    //Combine paths with filename to create a source and a destination path 
                    string sourceFilePath = Path.Combine(sourceDirectoryPath, fileName);
                    string destinationFilePath = Path.Combine(destinationDirectoryPath, fileName);

                    // If file is existing move it from source to destination
                    // File.Exists() is used to check if the user has permission to access this folder or file. Otherwise the program would crash
                    if (File.Exists(sourceFilePath)) 
                        File.Move(sourceFilePath, destinationFilePath);
                    
                });

                Console.WriteLine($"Succesfully copied {fileNames.Count} files");
            }
            catch(Exception ex)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Get all filtered file names
        /// </summary>
        /// <param name="sourceDirectoryPath"></param>
        /// <param name="dateFilter"></param>
        /// <returns>List of filtered file names</returns>
        private static List<string> GetFilteredFileNames(string sourceDirectoryPath, DateTime dateFilter)
        {
            DirectoryInfo directory = new DirectoryInfo(sourceDirectoryPath);

            // Get all files in the directory
            FileInfo[] files = directory.GetFiles();

            List<string> filteredFilePaths = new();

            // Loop through each file and add only filenames where editDate < dateFilter to the List
            foreach (FileInfo file in files.Where(editDate => editDate.LastWriteTime < dateFilter))
            {
                filteredFilePaths.Add(file.Name);
            }

            return filteredFilePaths;
        }
    }
}