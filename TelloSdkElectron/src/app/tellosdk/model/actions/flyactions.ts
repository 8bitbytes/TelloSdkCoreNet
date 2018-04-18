import { DroneAction } from '../actions/droneaction'
import { TelloApiService } from '../../services/telloapi.service';
import { MoveDirections } from '../../enums/movedirections.enum'

export class FlyActions
{
    constructor(private service:TelloApiService) {}
     
    Move(direction:MoveDirections,distance:number)  : DroneAction
    {
       if(distance < 20 || distance > 500) 
       {
           throw new Error(`distance of ${distance} is out of range`)
       }
       return new DroneAction(`Fly ${MoveDirections[direction]}`,`move/${direction}/${distance}`,this.service);
    }
}