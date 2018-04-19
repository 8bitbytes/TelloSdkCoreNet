import { DroneAction } from '../actions/droneaction'
import { TelloApiService } from '../../services/telloapi.service';

export class CendActions
{
    constructor(private service:TelloApiService) {}
     
    TakeOff():DroneAction
    {
       return new DroneAction('Takeoff','takeoff',this.service);
    }

    Land():DroneAction
    {
       return new DroneAction('Land','land',this.service);
    }
   
}