import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe as AngularDatePipe } from '@angular/common';

@Pipe({
  name: 'customDate'
})
export class CustomDatePipe implements PipeTransform {
  constructor(private datePipe: AngularDatePipe) {}

  transform(value: Date | string): string {
    const date = new Date(value);
    const dayName = this.datePipe.transform(date, 'EEEE');
    const day = this.datePipe.transform(date, 'dd');
    const month = this.datePipe.transform(date, 'MMMM');
    const year = this.datePipe.transform(date, 'yyyy');
    return `${dayName}, ${day} ${month} ${year}`;
  }
}
