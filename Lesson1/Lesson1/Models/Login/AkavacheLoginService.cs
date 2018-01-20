//using Lesson1.Models.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Reactive.Linq;
//using Akavache;

//namespace Lesson1.Models.Login
//{
//    class AkavacheLoginService : ILoginService
//    {
//        public async Task<bool> TryToLoginAsync(string username, string password)
//        {
//            try
//            {
//                BlobCache.ApplicationName = "XamarinLessons";
//                string storePass = await BlobCache.Secure.GetObject<string>(username);
//                return password == storePass;
//            }
//            catch (KeyNotFoundException)
//            {
//                await BlobCache.Secure.InsertObject<string>(username, password);
//                return true;
//            }
//            catch(Exception ex)
//            {
//                int a = 1;
//                return false;
//            }
//        }
//    }
//}
