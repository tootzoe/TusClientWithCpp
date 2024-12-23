using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TusClient2 = TusClient.TusClient;

namespace TusClient
{
      class TootTusUploader
    {
         


        [DllExport]
        public static double AmazingSin(int ofs, double x, double y)
        {
            return y / 2 * Math.Sin(ofs * 2 * Math.PI / x);
        }
         
         
        //  [DllExport("GetDomain", CallingConvention = CallingConvention.StdCall)]
        [DllExport]
        static public string GetDomainName(bool result)
        {
            System.Security.Principal.WindowsIdentity currentUser = System.Security.Principal.WindowsIdentity.GetCurrent();
              Console.WriteLine(currentUser.Name.Split('\\')[0]); 
            return currentUser.Name.Split('\\')[0];
        }


        private static ManualResetEvent resetEvent = new ManualResetEvent(false);

        [DllExport]
        static public bool TusUploadFile(string pathname)
        { 
          //  ThreadPool.QueueUserWorkItem(_ =>
          //  { 
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.DefaultConnectionLimit = 9999;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;


                   // var fl = new FileInfo(@"C:\Users\thor\Pictures\livecodingPrinciple.jpg"); 
                    var fl = new FileInfo(pathname); 
                    var tusClient = new TusClient2();
         

                    var fileUrl = tusClient.Create(@"https://tootzoe.com/tus", fl);

                    System.Diagnostics.Debug.WriteLine("toot start TusUploadTask..............");
                     
                    tusClient.Uploading += new TusClient2.UploadingEvent(TusUploadingEvent);
                      
                    tusClient.Upload(fileUrl, fl);
                    //
                    //var svrInfo = tusClient.getServerInfo(@"https://tootzoe.com/tus");
                    //System.Diagnostics.Debug.WriteLine(svrInfo.Version);
                    //System.Diagnostics.Debug.WriteLine(svrInfo.SupportedVersions);
                    //System.Diagnostics.Debug.WriteLine(svrInfo.Extensions);
                    //System.Diagnostics.Debug.WriteLine(svrInfo.MaxSize);
                    //
                    // tusClient.Delete(fileUrl); // this delete uploaded file from remote server

                  //  System.Diagnostics.Debug.WriteLine(" tusClient.Upload Done................"); 

                  //  resetEvent.Set();

                }
                catch (Exception exc) {
                    System.Diagnostics.Debug.WriteLine("toot  Exception : " , exc.ToString());
                }
           // }); 

           //  resetEvent.WaitOne();  
       
            return true;
        }

        static private void TusUploadingEvent(long bytesTransferred, long bytesTotal)
        {
            var percent = bytesTransferred / bytesTotal * 100.0;


            string tmpStr = String.Format("Up {0:0.00}% {1} of {2}", percent, bytesTransferred, bytesTotal);

            System.Diagnostics.Debug.WriteLine(tmpStr);
            //Console.WriteLine("Up {0:0.00}% {1} of {2}", percent, bytesTransferred, bytesTotal);
        }


    }
}
