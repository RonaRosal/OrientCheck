using System;
using System.Collections.Generic;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;

class Image
{
    // fields to store values
    public const int BLACK = 0;
    public const int WHITE = 255;
    public static string _path;

    public double _imageWidth;
    public double _imageHeight;


    public byte [,] pixelValue;

    public double _total;

   public double _xSum;
   public double _ySum;

    public double _xCenterOfMass;
    public double _yCenterOfMass;


    // constructor
    public Image(string path)
    {
        // loads image from given folder
        SixLabors.ImageSharp.Image<Gray8> image = SixLabors.ImageSharp.Image.Load<Gray8>(path);
        byte[,] data = new byte[image.Width, image.Height];
        for (int i = 0; i < image.Width; i++)
        {
            for (int j = 0; j < image.Height; j++)
            {
                data[i, j] = image[i, j].PackedValue;
                pixelValue = data; ;

                // assign values to fields
                _imageHeight = j;
                _imageWidth = i;

                int value = pixelValue[i, j];

                // Get total of pixel intensity
                if (pixelValue[i, j] > BLACK)
                {
                    _total += value;
                    

                }

            }

        }
        //Call Method for computing x,y center of mass
        xCenterofMass(pixelValue);
        yCenterofMass(pixelValue);

    }


  
    //Method to compute the X Center of Mass
    public double xCenterofMass(byte[,] fileImage)
    {
        //Console.WriteLine("Calculating the Center of Mass..");

        _imageHeight = fileImage.GetLength(0);
        _imageWidth = fileImage.GetLength(1);

        for (int xRow = 0; xRow < _imageHeight; xRow++)
        {
            for (int xCol = 0; xCol < _imageWidth; xCol++)
            {
                int value = fileImage[xRow, xCol];

                if (value > BLACK)
                {
                    _total += value;
                    _xSum = value * xRow;
                }

            }
        }

        _xCenterOfMass = (_xSum / _total) / _imageWidth;

        return _xCenterOfMass;
    }

    // Method to Compute the Y Center of Mass
    public double yCenterofMass(byte[,] fileImage)
    {
        _imageHeight = fileImage.GetLength(0);
        _imageWidth = fileImage.GetLength(1);

        for (int yRow = 0; yRow < _imageHeight; yRow++)
        {
            for (int yCol = 0; yCol < _imageWidth; yCol++)
            {
                int value = fileImage[yRow, yCol];

                if (value > BLACK)
                {
                    _total += value;
                    _ySum = value * yCol;
                }

            }
        }

        _yCenterOfMass = (_ySum / _total) / _imageHeight;
        return _yCenterOfMass;
        
    }

}

    class program
{
        // loads images from a folder
        public static List<Image> LoadImages(string folder)
        {
            // get filenames within the given folder
            string[] filePaths = Directory.GetFiles(folder);

             Console.Write("Loading images in image-folder..");
        
        // list that holds filepaths
            List<Image> images = new List<Image>();

            for (int f = 0; f < filePaths.Length; f++)
            {
              // Console.WriteLine(filePaths[f]);

                images.Add(new Image(filePaths[f]));
                Image._path = filePaths[f];
                
            }
           
            Console.Write("Done!\n");
            
            Console.WriteLine();
            return images;
        }

        static void Main(string[] args)
        {
            // check if number of arguments is valid
            if (args.Length >= 1)
            {
                // if valid try to execute code ELSE catch possible exceptions
                try
                {
                    string filename = "File Name";
                    string xCoord = "|x-coor|";
                    string yCoord = "|y-coord|";


                // Load .png files from directory
                 List<Image> images = LoadImages(args[0]);
               
                Console.WriteLine("{0,-15}{1,-15}{2,15}", filename, xCoord, yCoord);
                Console.WriteLine();


                // center of mass ...(?)

               
                // Display number of filepath found
                int imageCount = images.Count;
                Console.WriteLine("{0} numbers of images checked..", imageCount);
                Console.ReadLine();

                // anomalies...(?)

                }
                catch (DirectoryNotFoundException)
                {
                    Console.WriteLine("Loading images in " + args[0] + "...");
                    Console.WriteLine("Error: Directory " + args[0] + "does not exist");
                    Console.ReadLine();
                }
            }
            else
            {
                Console.WriteLine("Error: image-dir not given!\nUsage: orient-check.exe image-dir");
                Console.ReadLine();
            }
        }
    }


