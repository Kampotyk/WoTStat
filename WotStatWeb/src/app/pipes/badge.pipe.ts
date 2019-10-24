import { Pipe, PipeTransform } from '@angular/core';
import { Badge } from '../models/badge.enum';

@Pipe({
  name: 'badge'
})
export class BadgePipe implements PipeTransform {

  transform(value: number): string {
    return Badge[value];
  }

}
