import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'evidenceTypeName',
  standalone: true,
})
export class EvidenceTypeNamePipe implements PipeTransform {
  transform(value: number | null | undefined): string {
    switch (value) {
      case 1:
        return 'Antes';
      case 2:
        return 'Durante';
      case 3:
        return 'Después';
      default:
        return 'No especificada';
    }
  }
}
