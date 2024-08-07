import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateDifference',
})
export class DateDifferencePipe implements PipeTransform {
  transform(value: Date | string): string {
    const currentDate = new Date();
    const givenDate = new Date(value);
    const diffInMs = currentDate.getTime() - givenDate.getTime();
    const diffInHours = diffInMs / (1000 * 60 * 60);

    if (diffInHours < 24) {
      return `${Math.floor(diffInHours)} hours ago`;
    } else {
      const diffInDays = diffInHours / 24;
      return `${Math.floor(diffInDays)} days ago`;
    }
  }
}
