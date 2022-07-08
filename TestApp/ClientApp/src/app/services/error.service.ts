import { Injectable } from '@angular/core';
import {throwError as observableThrowError} from 'rxjs';

@Injectable()
export class ErrorService {
    constructor() {
    }

    public notAuthenticatedError() {
        return observableThrowError(
            new Error('You must be authenticated first.')
        );
    }

    public handleCustomerError(error: any) {
        const errMsg = (error.message) ? error.message :
          error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        return observableThrowError(new Error(errMsg));
    }

    public handlePatchError(error: any) {
        const errMsg = error.error ? `${error.error}` :
            error.status ? `${error.status} - ${error.statusText}` : 'Server error';
            return observableThrowError(new Error(errMsg));
    }

    public handleError(error: any) {
        const errMsg = error.status ? `${error.status} - ${error.statusText}` : 'Server error';
        return observableThrowError(new Error(errMsg));
    }

    public handleUserError(error: any) {
        return observableThrowError(error.error);
    }

    public customError(message: string) {
        return observableThrowError(new Error(message));
    }
}
