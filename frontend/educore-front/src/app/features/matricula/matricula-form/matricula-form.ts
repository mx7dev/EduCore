import { Component, EventEmitter, inject, Input, Output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { httpResource } from '@angular/common/http';
import { MatriculaService, CrearMatriculaDto } from '../../../core/services/matricula';
import { AlumnoService, AlumnoDto } from '../../../core/services/alumno';
import { SeccionService, SeccionDto } from '../../../core/services/seccion';
import { ResponseDto } from '../../../core/services/response';
import { Button } from 'primeng/button';
import { Dialog } from 'primeng/dialog';
import { Select } from 'primeng/select';
import { Toast } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-matricula-form',
  imports: [Button, Dialog, Select, FormsModule, Toast],
  providers: [MessageService],
  templateUrl: './matricula-form.html',
  styleUrl: './matricula-form.scss'
})
export class MatriculaForm {
  @Input() visible = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() matriculaGuardada = new EventEmitter<void>();

  private matriculaService = inject(MatriculaService);
  private alumnoService = inject(AlumnoService);
  private seccionService = inject(SeccionService);
  private messageService = inject(MessageService);
  guardando = false;

  alumnoSeleccionado: number | null = null;
  seccionSeleccionada: number | null = null;

  alumnosResponse = httpResource<ResponseDto<AlumnoDto[]>>(() => this.alumnoService.apiUrl);
  seccionesResponse = httpResource<ResponseDto<SeccionDto[]>>(() => this.seccionService.apiUrl);

  get alumnos() {
    return [...(this.alumnosResponse.value()?.data ?? [])].map(a => ({
      label: `${a.codigo} - ${a.nombre} ${a.apellidoPaterno}`,
      value: a.id
    }));
  }

  get secciones() {
    return [...(this.seccionesResponse.value()?.data ?? [])].map(s => ({
      label: `${s.nombre} - ${s.numeroGrado}° ${s.nivelGrado} (${s.turno})`,
      value: s.id
    }));
  }

  cerrar() {
    this.visible = false;
    this.visibleChange.emit(false);
    this.alumnoSeleccionado = null;
    this.seccionSeleccionada = null;
  }

  guardar() {
    if (!this.alumnoSeleccionado || !this.seccionSeleccionada) {
      this.messageService.add({ severity: 'warn', summary: 'Atención', detail: 'Selecciona alumno y sección' });
      return;
    }

    this.guardando = true;
    const dto: CrearMatriculaDto = {
      alumnoId: this.alumnoSeleccionado,
      seccionId: this.seccionSeleccionada
    };

    this.matriculaService.crear(dto).subscribe({
      next: (response: any) => {
        this.guardando = false;
        if (response.success) {
          this.messageService.add({ severity: 'success', summary: 'Éxito', detail: response.message });
          this.matriculaGuardada.emit();
          this.cerrar();
        } else {
          this.messageService.add({ severity: 'error', summary: 'Error', detail: response.message });
        }
      },
      error: () => {
        this.guardando = false;
        this.messageService.add({ severity: 'error', summary: 'Error', detail: 'Error inesperado' });
      }
    });
  }
}