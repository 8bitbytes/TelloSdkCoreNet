using System;
using TelloSdkCoreNet;
namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var wrapper = new SdkWrapper())
            {
                try
                {
                    var fp = TelloSdkCoreNet.flightplans.FlightPlan.Materialize(System.IO.File.ReadAllText(@"C:\Users\hallp\Downloads\TelloSdkCoreNet-development\TelloSdkCoreNet-development\TestApp\FP_4-10-2018 24809 PM_1f6623a2.json.fp"));
                    fp.Items.Add(new TelloSdkCoreNet.flightplans.FlightPlanItem
                    {
                        Action = wrapper.BaseActions.QueryBattery()
                    });

                    wrapper.ExecuteFlightPlan(fp);
                }
                catch(Exception ex)
                {
                    WriteError(ex.Message);
                }
            }
            
            PrintMenu();

            Console.ReadLine();
        }
        static void CreateFlightPlan()
        {
            using (var wrapper = new SdkWrapper())
            {
                var fp = new TelloSdkCoreNet.flightplans.FlightPlan();
                var fpItem = new TelloSdkCoreNet.flightplans.FlightPlanItem();

                fpItem.Action = wrapper.BaseActions.TakeOff();

                fp.Items.Add(fpItem);

                fpItem = new TelloSdkCoreNet.flightplans.FlightPlanItem();
                fpItem.Action = wrapper.FlipActions.FlipBackLeft();
                fp.Items.Add(fpItem);

                fpItem = new TelloSdkCoreNet.flightplans.FlightPlanItem();
                fpItem.Action = wrapper.FlipActions.FlipBackRight();
                fp.Items.Add(fpItem);

                fpItem = new TelloSdkCoreNet.flightplans.FlightPlanItem();
                fpItem.Action = wrapper.RotationActions.RotateClockwise(45);
                fp.Items.Add(fpItem);

                fpItem = new TelloSdkCoreNet.flightplans.FlightPlanItem();
                fpItem.Action = wrapper.FlyActions.FlyDownward(40);
                fp.Items.Add(fpItem);

                fpItem = new TelloSdkCoreNet.flightplans.FlightPlanItem();
                fpItem.Action = wrapper.BaseActions.Land();
                fp.Items.Add(fpItem);
            }

            //fp.Save("");
        }
        static void PrintMenu(){
            var done = false;
            using (var wrapper = new SdkWrapper())
            {
                //wrapper.BaseActions.CommandMode().Execute();
                while (!done)
                {
                    Console.Clear();
                    Console.WriteLine("1.TakeOff");
                    Console.WriteLine("2.Land");
                    Console.WriteLine("3.Flip");
                    Console.WriteLine("4.Fly forward");
                    Console.WriteLine("5.Fly back");
                    Console.WriteLine("6.Rotate");
                    Console.WriteLine("7.Battery %");

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
                        case "7":
                            {
                                var resp = wrapper.BaseActions.QueryBattery().Execute();
                                if (resp == SdkWrapper.SdkReponses.OK)
                                {
                                    Console.WriteLine($"Battery percentage is {wrapper.BaseActions.QueryBattery().ServerResponse}%");
                                    Console.ReadLine();
                                }
                                break;
                            }

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
