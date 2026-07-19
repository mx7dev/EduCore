import { Component, inject } from '@angular/core';
import { AlumnoDto, AlumnoService } from '../../../core/services/alumno';
import { Button } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { httpResource } from '@angular/common/http';

@Component({
  selector: 'app-alumno-list',
  imports: [Button, TableModule],
  templateUrl: './alumno-list.html',
  styleUrl: './alumno-list.scss'
})
export class AlumnoList {
  private alumnoService = inject(AlumnoService);
  alumnos = httpResource<AlumnoDto[]>(() => this.alumnoService.apiUrl);

  abrirFormulario() {}

  eliminar(id: number) {
    if (confirm('¿Estás seguro de eliminar este alumno?')) {
      // lo implementamos después
    }
  }
}