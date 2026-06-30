import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'proposalStatusName',
  standalone: true,
})
export class ProposalStatusNamePipe implements PipeTransform {
  transform(value: number | null | undefined): string {
    switch (value) {
      case 1:
        return 'Pendiente';
      case 2:
        return 'Aceptada';
      case 3:
        return 'Rechazada';
      case 4:
        return 'Retirada';
      default:
        return 'Desconocida';
    }
  }
}
