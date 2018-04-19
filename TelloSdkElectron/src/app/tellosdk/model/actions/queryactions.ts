import { DroneAction } from '../actions/droneaction'
import { TelloApiService } from '../../services/telloapi.service';

export class QueryActions
{
    readonly apiPath : string = 'query/'    
    constructor(private service:TelloApiService) {}

    Speed():DroneAction
    {
       return new DroneAction('Speed Query',`${this.apiPath}speed`,this.service);
    }

    Battery():DroneAction
    {
        return new DroneAction('Battery Query',`${this.apiPath}battery`,this.service);
    }

    Time():DroneAction
    {
        return new DroneAction('Time Query',`${this.apiPath}time`,this.service);
    }
}