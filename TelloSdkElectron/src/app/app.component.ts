import { Component } from '@angular/core';
import { ActionService } from './tellosdk/services/action.service';
import { DroneAction } from './tellosdk/model/actions/droneaction';
import { MoveDirections } from './tellosdk/enums/movedirections.enum';
import { FlipDirections } from './tellosdk/enums/flipdirections.enum';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'app';
  actionList:DroneAction[]=[];

  constructor(private actionService:ActionService){}
  

  takeOff():void
  {
    this.actionList.push(this.actionService.TakeOff());
  }

  land():void
  {
    this.actionList.push(this.actionService.Land());
  }

  flipLeft():void
  {
    this.actionList.push(this.actionService.Flip(FlipDirections.left));
  }

  flipRight():void
  {
    this.actionList.push(this.actionService.Flip(FlipDirections.right));
  }

  flipForward():void
  {
    this.actionList.push(this.actionService.Flip(FlipDirections.forward));
  }

  flipBack():void
  {
    this.actionList.push(this.actionService.Flip(FlipDirections.backward));
  }

  async executeAllActions()
  {
     for(var i=0;i<this.actionList.length;i++)
     {
         this.actionList[i].Execute();
         await this.delay(2500);
     }
     this.actionList=[];
  }

  private delay(ms:number)
  {
    return new Promise(resolve => setTimeout(resolve,ms));
  }
}
