import * as uuid from 'uuid';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class GuidService {

  constructor() { }

  criar(): string {
    const novoGuid: string = uuid.v4();
    return novoGuid;
  }
}
