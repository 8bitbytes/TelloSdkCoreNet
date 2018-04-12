using System;
using TelloSdkCoreNet;
namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //ExecuteFlightPlan(SdkWrapper.Instance);
            PrintMenu(SdkWrapper.Instance);
            Console.ReadLine();
            SdkWrapper.Instance.Shutdown();
        }
        static void ExecuteFlightPlan(SdkWrapper wrapper)
        {
            try
            {
                var fp = TelloSdkCoreNet.flightplans.FlightPlan.Materialize(System.IO.File.ReadAllText(@"/Users/peterhallock/Documents/git/TelloSdkCoreNet/TestApp/TestApp\FP_4-10-18 102058 PM_b59c89eb.json.fp"));
                wrapper.ExecuteFlightPlan(fp);
            }
            catch (Exception ex)
            {
                WriteError(ex.Message);
                Console.ReadLine();
            }
            
        }
        static void CreateFlightPlan()
        {
             /*   var fp = new TelloSdkCoreNet.flightplans.FlightPlan();
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

                fp.Save("");*/

        }
        static void PrintMenu(SdkWrapper wrapper)
        {
            var done = false;

            //wrapper.BaseActions.CommandMode().Execute();
            while (!done)
            {
                TelloSdkCoreNet.actions.Action action = null;
                Console.Clear();
                Console.WriteLine("1.TakeOff");
                Console.WriteLine("2.Land");
                Console.WriteLine("3.Flip");
                Console.WriteLine("4.Fly forward");
                Console.WriteLine("5.Fly back");
                Console.WriteLine("6.Rotate");
                Console.WriteLine("7.Battery %");
                Console.WriteLine("8.Execute flight plan");
                Console.WriteLine("9.Exit");
                var choice = Console.ReadLine();
                try
                {
                    switch (choice)
                    {
                        case "1":
                            {
                                action = wrapper.BaseActions.TakeOff();
                                break;
                            }
                        case "2":
                            {
                                action = wrapper.BaseActions.Land();
                                break;
                            }
                        case "3":
                            {
                                action = wrapper.FlipActions.FlipBackLeft();
                                break;
                            }
                        case "4":
                            {
                                action = wrapper.FlyActions.FlyForward(50);
                                break;
                            }
                        case "5":
                            {
                                action = wrapper.FlyActions.FlyBack(50);
                                break;
                            }
                        case "6":
                            {
                                action = wrapper.RotationActions.RotateClockwise(360);
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
                        case "8":
                            {
                                ExecuteFlightPlan(wrapper);
                                break;
                            }
                        case "9":
                            {
                                done = true;
                                break;
                            }

                    }
                    if (action != null)
                    {
                        var resp1 = action.Execute();
                        if (resp1 == SdkWrapper.SdkReponses.FAIL)
                        {
                            if (action.LastException != null)
                            {
                                throw action.LastException;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    WriteError(ex.Message);
                    Console.ReadLine();
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
