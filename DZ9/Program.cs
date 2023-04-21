using System.Net;

namespace DZ9
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Откуда будем качать
            string remoteUri = "https://wallup.net/wp-content/uploads/2019/09/879026-nhra-drag-race-racing-hot-rod-rods.jpg";
            // Как назовем файл на диске
            string fileName = "bigimage.jpg";
            var remoteUriArray = new string[10] { "https://wallup.net/wp-content/uploads/2019/09/879026-nhra-drag-race-racing-hot-rod-rods.jpg", 
                                                "https://wallup.net/wp-content/uploads/2019/09/879026-nhra-drag-race-racing-hot-rod-rods.jpg", 
                                                "https://wallup.net/wp-content/uploads/2019/09/879026-nhra-drag-race-racing-hot-rod-rods.jpg",
                                                "https://wallup.net/wp-content/uploads/2019/09/879026-nhra-drag-race-racing-hot-rod-rods.jpg",
                                                "https://wallup.net/wp-content/uploads/2019/09/879026-nhra-drag-race-racing-hot-rod-rods.jpg",
                                                "https://wallup.net/wp-content/uploads/2019/09/879026-nhra-drag-race-racing-hot-rod-rods.jpg",
                                                "https://wallup.net/wp-content/uploads/2019/09/879026-nhra-drag-race-racing-hot-rod-rods.jpg",
                                                "https://wallup.net/wp-content/uploads/2019/09/879026-nhra-drag-race-racing-hot-rod-rods.jpg",
                                                "https://wallup.net/wp-content/uploads/2019/09/879026-nhra-drag-race-racing-hot-rod-rods.jpg",
                                                "https://wallup.net/wp-content/uploads/2019/09/879026-nhra-drag-race-racing-hot-rod-rods.jpg"};
            var fileNameArray = new string[10] { "bigimage1.jpg",
                                                "bigimage2.jpg",
                                                "bigimage3.jpg",
                                                "bigimage4.jpg",
                                                "bigimage5.jpg",
                                                "bigimage6.jpg",
                                                "bigimage7.jpg",
                                                "bigimage8.jpg",
                                                "bigimage9.jpg",
                                                "bigimage10.jpg",};
            ImageDownloader ImageDownloader = new ImageDownloader(remoteUri, fileName);
            ImageDownloader.DownloadStarted += DisplayMessage;
            ImageDownloader.DownloadCompleted += DisplayMessage;

            //Console.WriteLine("Качаю \"{0}\" из \"{1}\" .......\n\n", fileName, remoteUri);
            void DisplayMessage(string message) => Console.WriteLine(message);
            bool FileProgress = false;
            Task.Run(() => FileProgress = ImageDownloader.Download().Result);


            var fileProgress = new bool[10];
            var IimageDownloader = new ImageDownloader[10];
            for (int ii = 0; ii < 10; ii++)
            {
                IimageDownloader[ii] = new ImageDownloader(remoteUriArray[ii], fileNameArray[ii]);
                IimageDownloader[ii].DownloadStarted += DisplayMessage;
                IimageDownloader[ii].DownloadCompleted += DisplayMessage;
                fileProgress[ii] = false;
            }
            
            
            Task.Run(() => fileProgress[0] = IimageDownloader[0].Download().Result);
            Task.Run(() => fileProgress[1] = IimageDownloader[1].Download().Result);
            Task.Run(() => fileProgress[2] = IimageDownloader[2].Download().Result);
            Task.Run(() => fileProgress[3] = IimageDownloader[3].Download().Result);
            Task.Run(() => fileProgress[4] = IimageDownloader[4].Download().Result);
            Task.Run(() => fileProgress[5] = IimageDownloader[5].Download().Result);
            Task.Run(() => fileProgress[6] = IimageDownloader[6].Download().Result);
            Task.Run(() => fileProgress[7] = IimageDownloader[7].Download().Result);
            Task.Run(() => fileProgress[8] = IimageDownloader[8].Download().Result);
            Task.Run(() => fileProgress[9] = IimageDownloader[9].Download().Result);

            Console.WriteLine("Нажмите клавишу 'A' для выхода или любую клавишу для проверки статуса скачивания");

            var choise = Console.ReadKey().KeyChar;

            while (choise.Equals('A')==false)

            {
                Console.WriteLine("Файл скачен     " + FileProgress); 
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Файл "+i+" скачен     " + fileProgress[i]);
                }
                choise = Console.ReadKey().KeyChar;
            }
            System.Environment.Exit(1);
        }


        public class ImageDownloader : WebClient
        {
            public string remoteUri { get; set; }
            public string fileName { get; set; }
            public ImageDownloader(string _remoteUri, string _fileName)
            {
                remoteUri = _remoteUri;
                fileName = _fileName;
            }
            public event Action<string> DownloadStarted;
            public event Action<string> DownloadCompleted;

            //public void Download ()
            //{
            //    ImageStarted?.Invoke("Скачивание файла началось");
            //    DownloadFile (remoteUri, fileName);
            //    ImageCompleted?.Invoke("Скачивание файла закончилось");
            //}

            public async Task<bool> Download()
            {
                DownloadStarted?.Invoke("Скачивание файла "+ fileName + " из "+ remoteUri + "  началось\n");
                Task DownloadTask = DownloadFileTaskAsync(remoteUri, fileName);
                await DownloadTask;
                DownloadCompleted?.Invoke("Скачивание файла " + fileName + " из " + remoteUri + "  закончилось\n");
                return DownloadTask.IsCompleted;
            }
        }
    }
}