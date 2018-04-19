import { DroneAction } from '../actions/droneaction'
import { TelloApiService } from '../../services/telloapi.service';
import { FlipDirections } from '../../enums/flipdirections.enum'

export class FlipActions
{
    constructor(private service:TelloApiService) {}
     
    Flip(direction:FlipDirections)  : DroneAction
    {
       return new DroneAction(`flip ${FlipDirections[direction]}`,`flip/${direction}`,this.service);
    }
}