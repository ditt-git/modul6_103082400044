using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace tpmodul6_103082400044
{
    internal class SayaTubeVideo
    {
        private int id;
        private string title;
        private int playCount;

        public SayaTubeVideo(string title)
        {
            if (string.IsNullOrWhiteSpace(title) || title.Length > 200)
            {
                throw new ArgumentException("Judul video tidak valid.");
            }

            this.title = title;
            Random random = new Random();
            this.id = random.Next(10000, 99999); // 5 digit random
            this.playCount = 0;
        }

        public void IncreasePlayCount(int count)
        {
            Debug.Assert(count <= 25000000 && count >= 0, "Play Count Maksimal 25.000.000 per panggilan atau tidak boleh negatif");
            if (count > 25000000 || count < 0) throw new ArgumentOutOfRangeException("Input penambahan tidak valid.");

            checked
            {
                playCount += count;
            }
        }

        //Method Get
        public string GetTitle()
        {
            return title;
        }

        public int GetPlayCount()
        {
            return playCount;
        }

        public void PrintVideoDetails()
        {
            Console.WriteLine($"ID Film: {id}");
            Console.WriteLine($"Judul Film: {title}");
            Console.WriteLine($"Count Play: {playCount}");
            Console.WriteLine("");
        }
    }

    internal class SayaTubeUser
    {
        private string username;
        private List<SayaTubeVideo> uploadedVideos;

        public SayaTubeUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username) || username.Length > 100)
                throw new ArgumentException("Username tidak valid");

            this.username = username;
            this.uploadedVideos = new List<SayaTubeVideo>();
        }

        public void AddVideo(SayaTubeVideo video)
        {
            Debug.Assert(video != null && video.GetPlayCount() < int.MaxValue, "Video tidak boleh null dan play count kurang dari int maksimum");
            if (video == null || video.GetPlayCount() >= int.MaxValue) throw new ArgumentException("Video tidak valid untuk ditambahkan.");

            uploadedVideos.Add(video);
        }

        public int GetTotalVideoPlayCount()
        {
            int total = 0;
            foreach (var v in uploadedVideos) total += v.GetPlayCount();
            return total;
        }

        public void PrintAllVideoPlaycount()
        {
            Console.WriteLine($"User: {username}");
            Console.WriteLine();
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine($"Video {i + 1} : {uploadedVideos[i].GetTitle()}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SayaTubeUser user = new SayaTubeUser("Pradipta");

            string[] daftarFilm = { "Searching", "Missing", "Interstellar", "Insidious", "Upin Ipin",
                                   "Toy Story", "Iron Man", "Avengers", "Peninsula", "Superman" };

            foreach (var judul in daftarFilm)
            {
                SayaTubeVideo v = new SayaTubeVideo($"Review Film {judul} oleh Pradipta");
                v.IncreasePlayCount(1000);
                user.AddVideo(v);
            }

            user.PrintAllVideoPlaycount();
            Console.WriteLine($"Total Play Count: {user.GetTotalVideoPlayCount()}");
        }
    }
}