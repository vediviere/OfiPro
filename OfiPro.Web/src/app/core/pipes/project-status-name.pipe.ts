import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'projectStatusName',
  standalone: true,
})
export class ProjectStatusNamePipe implements PipeTransform {
  transform(value: number | null | undefined): string {
    switch (value) {
      case 1:
        return 'Publicado';
      case 2:
        return 'Asignado';
      case 3:
        return 'En proceso';
      case 4:
        return 'Pendiente de confirmación';
      case 5:
        return 'Finalizado';
      case 6:
        return 'Cancelado';
      case 7:
        return 'Expirado';
      default:
        return 'Desconocido';
    }
  }
}
