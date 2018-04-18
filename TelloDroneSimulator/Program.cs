using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TelloDroneSimulator
{
    public class UDPListener
    {
        private const int listenPort = 8889;
        private static int speed = 0;
        private static int battery = 100;
        public static int Main()
        {
            bool done = false;
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            string received_data;
            byte[] receive_byte_array;
            try
            {
                while (!done)
                {
                    Console.WriteLine("Waiting for broadcast");
                    receive_byte_array = listener.Receive(ref groupEP);
                    Console.WriteLine("Received a broadcast from {0}", groupEP.ToString());
                    received_data = Encoding.ASCII.GetString(receive_byte_array, 0, receive_byte_array.Length);
                    Console.WriteLine("data follows \n{0}\n\n", received_data);
                    if (received_data.IndexOf("?") > -1)
                    {
                        var resp = processRequestReponseMessage(received_data);
                        SendMessage(listener, resp, groupEP);
                    }
                    else
                    {
                        processCommand(received_data);
                        SendMessage(listener, "OK", groupEP);
                    }
                }
            }
            catch (Exception e)
            {
                writeErrorMessage(e.ToString());
                SendMessage(listener, e.Message, groupEP);
            }
            listener.Close();
            return 0;
        }
        private static void SendMessage(UdpClient client,string message,IPEndPoint endpoint)
        {
            var msgBytes = Encoding.ASCII.GetBytes(message);
            client.Connect(endpoint);
            client.Send(msgBytes, msgBytes.Length);
        }
        private static string processRequestReponseMessage(string command)
        {
            switch (command)
            {
                case "speed?":
                    {
                        return speed.ToString();
                    }
                case "battery?":
                    {
                        var retval = battery;
                        battery -= 10;
                        if(battery < 0)
                        {
                            battery = 0;
                        }
                        return retval.ToString();
                    }
                case "time?":
                    {
                        return "10";
                    }
            }
            return string.Empty;
        }
        private static void processCommand(string command)
        {
            var fgRestore = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            var cmdPart = command.Split(" ");

            switch (cmdPart[0])
            {
                case "command":
                    {
                        Console.WriteLine("Ready to start accepting commands");
                        break;
                    }
                case "takeoff":
                    {
                        Console.WriteLine("Initiating auto take off");
                        break;
                    }
                case "land":
                    {
                        Console.WriteLine("Initiating auto landing");
                        break;
                    }
                case "up":
                    {
                        if (cmdPart.Length == 1)
                        {
                            writeErrorMessage("Cannot move up. Up value not provided");
                            break;
                        }
                        Console.WriteLine($"Moving aricraft up {cmdPart[1]} cm");
                        break;
                    }
                case "down":
                    {
                        if (cmdPart.Length == 1)
                        {
                            writeErrorMessage("Cannot move down. down value not provided");
                            break;
                        }
                        Console.WriteLine($"Moving aricraft down {cmdPart[1]} cm");
                        break;
                    }
                case "left":
                    {
                        if (cmdPart.Length == 1)
                        {
                            writeErrorMessage("Cannot move left. Left value not provided");
                            break;
                        }
                        Console.WriteLine($"Moving aricraft left {cmdPart[1]} cm");
                        break;
                    }
                case "right":
                    {
                        if (cmdPart.Length == 1)
                        {
                            writeErrorMessage("Cannot move right. Right value not provided");
                            break;
                        }
                        Console.WriteLine($"Moving aricraft right {cmdPart[1]} cm");
                        break;
                    }
                case "forward":
                    {
                        if (cmdPart.Length == 1)
                        {
                            writeErrorMessage("Cannot move forward. Forward value not provided");
                            break;
                        }
                        Console.WriteLine($"Moving aricraft forward {cmdPart[1]} cm");
                        break;
                    }
                case "back":
                    {
                        if (cmdPart.Length == 1)
                        {
                            writeErrorMessage("Cannot move back. Back value not provided");
                            break;
                        }
                        Console.WriteLine($"Moving aricraft back {cmdPart[1]} cm");
                        break;
                    }
                case "cw":
                    {
                        if (cmdPart.Length == 1)
                        {
                            writeErrorMessage("Cannot rotate clockwise. Degree value not provided");
                            break;
                        }
                        Console.WriteLine($"Moving aricraft clockwise {cmdPart[1]} degrees");
                        break;
                    }
                case "ccw":
                    {
                        if (cmdPart.Length == 1)
                        {
                            writeErrorMessage("Cannot rotate counter-clockwise. Degree value not provided");
                            break;
                        }
                        Console.WriteLine($"Moving aricraft counter-clockwise {cmdPart[1]} degrees");
                        break;
                    }
                case "flip":
                    {
                        if (cmdPart.Length == 1)
                        {
                            writeErrorMessage("Cannot flip. Flip direction not provided");
                            break;
                        }
                        Console.WriteLine($"Flipping aricraft {cmdPart[1]}");
                        break;
                    }
                case "speed":
                    {
                        if (cmdPart.Length == 1)
                        {
                            writeErrorMessage("Cannot set speed. Speed not provided");
                            break;
                        }
                        speed = Convert.ToInt32(cmdPart[1]);
                        Console.WriteLine($"Setting aircraft speed to {cmdPart[1]} cm/s");
                        break;
                    }
               
            }
            Console.ForegroundColor = fgRestore;
        }

        private static void writeErrorMessage(string message)
        {
            var fgRestore = Console.ForegroundColor;
            Console.WriteLine($"Error: {message}");
            Console.ForegroundColor = fgRestore;
        }
    } // end of class UDPListener
}


