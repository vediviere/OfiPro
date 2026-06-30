import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'contractStatusName',
  standalone: true,
})
export class ContractStatusNamePipe implements PipeTransform {
  transform(value: number | null | undefined): string {
    switch (value) {
      case 1:
        return 'Pendiente de inicio';
      case 2:
        return 'En proceso';
      case 3:
        return 'Pendiente de confirmación';
      case 4:
        return 'Finalizado';
      case 5:
        return 'Cancelado';
      default:
        return 'Desconocido';
    }
  }
}
