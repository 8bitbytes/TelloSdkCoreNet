import { TelloApiService } from '../../services/telloapi.service';

export class DroneAction
{
   readonly name:string;
   readonly command:string;

   constructor(name:string,command:string,private service:TelloApiService)
   {
     this.name = name;
     this.command = command;
   }

   Execute():void{
     this.service.send(this).subscribe();
   }
  
}