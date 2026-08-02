import { Component, inject } from '@angular/core';
import { httpResource } from '@angular/common/http';
import { AlumnoService, AlumnoDto } from '../../../core/services/alumno';
import { ResponseDto } from '../../../core/services/response';
import { Button } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { AlumnoForm } from '../alumno-form/alumno-form';

@Component({
  selector: 'app-alumno-list',
  imports: [Button, TableModule, AlumnoForm],
  templateUrl: './alumno-list.html',
  styleUrl: './alumno-list.scss'
})
export class AlumnoList {
  private alumnoService = inject(AlumnoService);
  
  alumnosResponse = httpResource<ResponseDto<AlumnoDto[]>>(() => this.alumnoService.apiUrl);
  mostrarFormulario = false;

  get alumnos(): AlumnoDto[] {
    return this.alumnosResponse.value()?.data ?? [];
  }

  abrirFormulario() {
    this.mostrarFormulario = true;
  }

  eliminar(id: number) {
    if (confirm('¿Estás seguro de eliminar este alumno?')) {
      this.alumnoService.eliminar(id).subscribe({
        next: () => this.alumnosResponse.reload()
      });
    }
  }
}