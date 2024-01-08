using System.IO;
using System.Text;
using System.IO;
using System.Text;

namespace UnsecureApp.Controllers
{
    /// <summary>
    /// Controller for handling file operations.
    /// </summary>
    public class MyController
    {
        /// <summary>
        /// Reads the content of a file.
        /// </summary>
        /// <param name="userInput">The relative path of the file to read.</param>
        /// <returns>The content of the file as a string.</returns>
        /// <exception cref="UnauthorizedAccessException">Thrown when an attempt is made to access a file outside of the current directory.</exception>
        public string ReadFile(string userInput)
        {
            string safePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), userInput));

            if (!safePath.StartsWith(Directory.GetCurrentDirectory()))
            {
                throw new UnauthorizedAccessException();
            }

            StringBuilder fileContent = new StringBuilder();

            using (FileStream fs = File.Open(safePath, FileMode.Open))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                // Rest of the code...
            }
        }
    }
}
namespace UnsecureApp.Controllers
{
    public class MyController
    {
        public string ReadFile(string userInput)
        {
            string safePath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), userInput));

            if (!safePath.StartsWith(Directory.GetCurrentDirectory()))
            {
                throw new UnauthorizedAccessException();
            }

            StringBuilder fileContent = new StringBuilder();

            using (FileStream fs = File.Open(safePath, FileMode.Open))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);

                int bytesRead;
                while ((bytesRead = fs.Read(b, 0, b.Length)) > 0)
                {
                    fileContent.Append(temp.GetString(b, 0, bytesRead));
                }
            }

            return fileContent.ToString();
        }
    }
}