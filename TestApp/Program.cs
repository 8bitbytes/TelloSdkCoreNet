using System;
using TelloSdkCoreNet;
namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var wrapper = new SdkWrapper();
            /* var resp = wrapper.ExecuteActions(new TelloSdkCoreNet.actions.Action[]
             {
                 wrapper.BaseActions.TakeOff(),
                 wrapper.FlipActions.FlipBackLeft(),
                 wrapper.FlipActions.FlipBackRight(),
                 wrapper.RotationActions.RotateClockwise(45),
                 wrapper.FlyActions.FlyDownward(40),
                 wrapper.BaseActions.Land()
             },2);
             if(resp == SdkWrapper.SdkReponses.FAIL)
             {
                 WriteError(wrapper.LastException.ToString());
             }*/


            //wrapper.BaseActions.Land().Execute();
            PrintMenu();

            Console.ReadLine();
        }

        static void PrintMenu(){
            var done = false;
            var wrapper = new SdkWrapper();
            //wrapper.BaseActions.CommandMode().Execute();
            while(!done){
                Console.Clear();
                Console.WriteLine("1.TakeOff");
                Console.WriteLine("2.Land");
                Console.WriteLine("3.Flip");
                Console.WriteLine("4.Fly forward");
                Console.WriteLine("5.Fly back");
                Console.WriteLine("6.Rotate");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        {
                            wrapper.BaseActions.TakeOff().Execute();
                            break;
                        }
                    case "2":
                        {
                            wrapper.BaseActions.Land().Execute();
                            break;
                        }
                    case "3":
                        {
                            wrapper.FlipActions.FlipForward().Execute();
                            break;
                        }
                    case "4":
                        {
                            wrapper.FlyActions.FlyForward(50).Execute();
                            break;
                        }
                    case "5":
                        {
                            wrapper.FlyActions.FlyBack(50).Execute();
                            break;
                        }
                    case "6":
                        {
                            wrapper.RotationActions.RotateClockwise(360).Execute();
                            break;
                        }

                }
            }
        }
        static void WriteError(string message)
        {
            var fgRestore = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ForegroundColor = fgRestore;
        }
    }
}
