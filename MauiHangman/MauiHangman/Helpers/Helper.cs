using System.Reflection;

namespace MauiHangman.Helpers;

public class Helper
{
    public static string GetRandomLineFromEmbeddedResource(string resourcePath)
    {
        // Get the assembly that contains the resource
        var assembly = Assembly.GetExecutingAssembly();

        // Open the stream to read the embedded resource
        using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
        {
            if (stream == null)
                throw new FileNotFoundException("Resource not found.", resourcePath);

            using (StreamReader reader = new StreamReader(stream))
            {
                // Read all lines from the file
                List<string> lines = new List<string>();
                while (!reader.EndOfStream)
                {
                    lines.Add(reader.ReadLine());
                }

                // Check if the file is not empty
                if (lines.Count == 0)
                {
                    return "The file is empty.";
                }

                // Create a random number generator
                Random random = new Random();

                // Get a random index
                int randomIndex = random.Next(lines.Count);

                // Return the random line
                return lines[randomIndex];
            }
        }
    }
}