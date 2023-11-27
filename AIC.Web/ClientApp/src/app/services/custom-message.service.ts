import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomMessageService {
  message: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  constructor() { }
}
