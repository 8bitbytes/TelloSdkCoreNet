import { Injectable } from '@angular/core';
import { CendActions } from '../model/actions/cendactions';
import { FlipActions } from '../model/actions/flipactions';
import { FlyActions } from '../model/actions/flyactions';
import { QueryActions } from '../model/actions/queryactions';
import { RotationActions } from '../model/actions/rotationactions';
import { DroneAction } from '../model/actions/Droneaction';

import { MoveDirections } from '../enums/movedirections.enum'
import { FlipDirections } from '../enums/flipdirections.enum'
import { Event } from '../enums/event.enum';
import { TelloApiService } from './telloapi.service';
import { Action } from 'rxjs/scheduler/Action';


@Injectable()
export class ActionService
{
    cendActions:CendActions;
    flipActions:FlipActions;
    flyActions:FlyActions;
    queryActions:QueryActions;
    rotationActions:RotationActions;
    ioConnection: any;
    actionList:DroneAction[]=[];
    

    constructor(private socketService:TelloApiService)
    {
       this.cendActions = new CendActions(socketService);
       this.flipActions = new FlipActions(socketService);
       this.flyActions = new FlyActions(socketService);
       this.queryActions = new QueryActions(socketService);
       this.rotationActions = new RotationActions(socketService);
    }
    
    TakeOff():DroneAction
    {
        return this.cendActions.TakeOff();
    }

    Land():DroneAction
    {
        return this.cendActions.Land();
    }

    Flip(direction:FlipDirections):DroneAction
    {
        return this.flipActions.Flip(direction);
    }

    Move(direction:MoveDirections,distance:number):DroneAction
    {
        return this.flyActions.Move(direction,distance);
    }

    RotateClockwise(degrees:number) :DroneAction
    {
        return this.rotationActions.Clockwise(degrees);
    }

    RotateCounterClockwise(degrees:number):DroneAction
    {
        return this.rotationActions.CounterClockWise(degrees);
    }

    GetBatteryPercentage():DroneAction
    {
       return this.queryActions.Battery()
    }

    GetFlightTime():DroneAction
    {
        return this.queryActions.Time();
    }

    GetSpeed():DroneAction
    {
        return this.queryActions.Speed();
    }

}