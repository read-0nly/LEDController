using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace HappyLighting
{
    internal class Webhost
    {
        public static bool keepLooping = false;
        public static HttpListener listener = new HttpListener();

        public static void Stop()
        {
            keepLooping = false;
            listener.Abort();
        }
        static string ButtonTable = """
                    <div style="position:absolute;left: 50%;top:100px;">
                    <table style="width:150px;border-spacing: 5px;position: relative; left: -50%;">
                        <tr>
                            <td style="width:25%;" id="alert" onclick="window.location = './startalert';"class="button inactiveButton">Alert</td>
                            <td style="width:25%;" id="tick" onclick="window.location = './starttick';"class="button inactiveButton">Tick</td>
                        </tr>      
                        <tr>
                            <td style="width:25%;" id="kill" colspan=2 class="button redButton" onclick="window.location = './kill';">Power</td>
                        </tr>                
                    </table>
                    </div>
""";
        static string kickback = "<script>setTimeout(()=>{window.location = './'},1000)</script>";
        public static void Start()
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            keepLooping = true;
            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                keepLooping = false;
                return;
            }
            if (listener.Prefixes.Count == 0)
            {
                listener.Prefixes.Add("http://*:8080/");
            }
            listener.IgnoreWriteExceptions = true;
            listener.Start();
            while (keepLooping)
            {
                Console.WriteLine("Listening...");
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = null;
                try
                {
                    context = listener.GetContext();
                }
                catch(System.Net.HttpListenerException exception)
                {
                    return;
                }

                HttpListenerRequest request = context.Request;
                // Obtain a response object.
                HttpListenerResponse response = context.Response;
                response.AppendHeader("Access-Control-Allow-Origin", "*");
                response.AppendHeader("Access-Control-Allow-Headers", "*");
                response.AppendHeader("Content-Type", "text/html; charset=utf-8");

                string responseString = """
                <head>
                    <style type="text/css">  
                    body{
                        background-color:black;
                        color:white;
                    }
                    h1{
                        text-align:center;
                        font-family:consolas;
                        color:lime;
                    }
                    .button{
                        text-align:center;
                        height:50px;
                    }
                    .inactiveButton{
                        background-color:SlateGrey;
                        color:white; 
                        border:2px solid white;
                        padding:2px;
                    }
                    .redButton{
                        background-color:DarkSlateGrey;
                        color:red;                    
                        border:2px solid red;
                        padding:2px;
                    }
                    .greenButton{
                        background-color:DarkSlateGrey;
                        color:lime;          
                        border:2px solid lime;    
                        padding:2px;              
                    }
                    .cyanButton{
                        background-color:DarkSlateGrey;
                        color:cyan;         
                        border:2px solid cyan;   
                        padding:2px;          
                    }
                    </style>
                </head><body><br>
""";
                if (request.RawUrl.ToString().ToLower().Contains("/ping"))
                {
                    responseString += "<h1>Pong</h1>";

                }
                else if (request.RawUrl.ToString().ToLower().Contains("/kill"))
                {
                    responseString += kickback +"<h1>Killing</h1>"+ ButtonTable;
                    byte[] bufferKill = System.Text.Encoding.UTF8.GetBytes(responseString);
                    // Get a response stream and write the response to it.
                    response.ContentLength64 = bufferKill.Length;
                    System.IO.Stream outputKill = response.OutputStream;
                    outputKill.Write(bufferKill, 0, bufferKill.Length);
                    // You must close the output stream.
                    outputKill.Close();
                    listener.Stop();
                    return;
                }

                else if (request.RawUrl.ToString().ToLower().Contains("/starttick"))
                {
                    DeviceManager.StartTick = true;
                    responseString += kickback + "<h1>Starting Tick</h1>";
                }
                else if (request.RawUrl.ToString().ToLower().Contains("/stoptick"))
                {
                    DeviceManager.StopTick = true;
                    responseString += kickback + "<h1>Stopping Tick</h1>";
                }
                else if(request.RawUrl.ToString().ToLower().Contains("/startalert"))
                {
                    DeviceManager.StartAlert = true;
                    responseString += kickback + "<h1>Starting Alert</h1>";
                }
                else if(request.RawUrl.ToString().ToLower().Contains("/stopalert"))
                {
                    DeviceManager.StopAlert = true;
                    responseString += kickback + "<h1>Stopping Alert</h1>";
                }
                else
                {
                    responseString += "<h1>LED CONTROL SYSTEMS v0.3</h1>";
                }
                responseString += ButtonTable;
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                // You must close the output stream.
                output.Close();
            }
            listener.Stop();
        }
    }
}
