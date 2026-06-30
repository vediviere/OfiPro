import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'urgencyName',
  standalone: true,
})
export class UrgencyNamePipe implements PipeTransform {
  transform(value: number | null | undefined): string {
    switch (value) {
      case 1:
        return 'Flexible';
      case 2:
        return 'Esta semana';
      case 3:
        return 'Urgente';
      default:
        return 'No especificada';
    }
  }
}
