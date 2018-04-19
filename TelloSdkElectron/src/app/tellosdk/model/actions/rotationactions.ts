import { DroneAction } from '../actions/droneaction'
import { TelloApiService } from '../../services/telloapi.service';

export class RotationActions
{
    readonly apiPath :string = 'Rotate/'
    constructor(private service:TelloApiService) {}
     
    Clockwise(degrees:number):DroneAction
    {
       if(degrees < 1 || degrees > 3600)
       {
          throw new Error(`distance of ${degrees} is out of range`)
       }
       return new DroneAction('Rotate clockwise',`${this.apiPath}clockwise/${degrees}`,this.service);
    }

    CounterClockWise(degrees:number):DroneAction
    {
       if(degrees < 1 || degrees > 3600)
       {
          throw new Error(`distance of ${degrees} is out of range`)
       }
       return new DroneAction('Rotate counter clockwise',`${this.apiPath}counterclockwise/${degrees}`,this.service);
    }
   
}