using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

// A program to test what is faster - list or hash table, take a look:
namespace Combination
{
    struct UserInfo
    {
        public int userId;
        public string userName;
        public UserInfo (int id, string name)
        {
            userId = id;
            userName = name;
        }
    }

    class Program
    {
        static Hashtable userInfoHash;
        static List<UserInfo> userInfoList;
        static Stopwatch sw;

        static void Main(string[] args)
        {
            userInfoHash = new Hashtable();
            userInfoList = new List<UserInfo>();
            sw = new Stopwatch();

            //Adding elements
            for (int i = 0; i < 4000000; i++)
            {
                userInfoHash.Add(i, "user" + i);
                userInfoList.Add(new UserInfo(i, "user" + i));
            }

            //removing
            if (userInfoHash.ContainsKey(0))
            {
                userInfoHash.Remove(0);
            }

            //set values
            if (userInfoHash.ContainsKey(1))
            {
                userInfoHash[1] = "replacentName";
            }

            ////looping
            //foreach(DictionaryEntry entry in userInfoHash)
            //{
            //    Console.WriteLine("Key: " + entry.Key + " / Value: " + entry.Value);
            //}

            //Access
            Random randomUserGen = new Random();
            int randomUser = -1;

            sw.Start();
            float startTime = 0;
            float endTime = 0;
            float deltaTime = 0;

            int cycles = 5;
            int cycle = 0;
            string userName = string.Empty;

            while (cycle < cycles)
            {
                randomUser = randomUserGen.Next(3000000, 4000000);

                startTime = sw.ElapsedMilliseconds;
                //acces from list
                userName = GetUserFromList(randomUser);
                endTime = sw.ElapsedMilliseconds;
                deltaTime = endTime - startTime;
                Console.WriteLine("Time taken to retrieve " + userName + " fom list took " + string.Format("{0:0.##}", deltaTime) + "ms\n");


                startTime = sw.ElapsedMilliseconds;
                //acces from hashtable
                userName = (string)userInfoHash[randomUser];
                endTime = sw.ElapsedMilliseconds;
                deltaTime = endTime - startTime;
                Console.WriteLine("Time taken to retrieve " + userName + " fom hash took " + string.Format("{0:0.##}", deltaTime) + "ms\n");

                cycle++;
            }
        }

        static string GetUserFromList(int userId)
        {
            for (int i = 0; i < userInfoList.Count; i++)
            {
                if (userInfoList[i].userId == userId)
                {
                    return userInfoList[i].userName;
                }
            }
            return string.Empty;
        }
    }
}
