import { Component, Input, OnInit, Optional, Self } from '@angular/core';
import { ControlValueAccessor, NgControl, ValidatorFn, Validators } from '@angular/forms';
import { NavigationStart } from '@angular/router';
import { RxwebValidators } from '@rxweb/reactive-form-validators';

@Component({
  selector: 'generic-email-input',
  templateUrl: './generic-email-input.component.html',
  styleUrls: ['./generic-email-input.component.css']
})
export class GenericEmailInputComponent implements OnInit, ControlValueAccessor {
  @Input() placeholder: string;
  value: string;
  disabled: boolean = false;

  Validators: ValidatorFn[];

  @Input() readonly: boolean = false;

  onchange: (value) => void;
  ontouched: () => void;

  constructor(
    @Optional() @Self() public ngControl: NgControl
  ) {

    // Replace the provider from above with this.
    if (this.ngControl != null) {
      // Setting the value accessor directly (instead of using
      // the providers) to avoid running into a circular import.
      this.ngControl.valueAccessor = this;

    }
  }
 
  writeValue(obj: string): void {
    this.value = obj;

  }
  registerOnChange(fn: any): void {
    this.onchange = fn;

  }
  registerOnTouched(fn: any): void {
    this.ontouched = fn;

  }
  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }

  ngOnInit(): void {
    const validatorsList = [RxwebValidators.email()];//[RxwebValidators.email()];[Validators.pattern("^[a-zA-Z0-9._-]+@[a-zA-Z0-9.]+\.[a-zA-Z]{2,4}$")]
    validatorsList.push(this.ngControl.control.validator);
    this.ngControl.control.setValidators(validatorsList);
  }

  
}
