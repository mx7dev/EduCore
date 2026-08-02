import { Component, EventEmitter, inject, Input, Output, signal, computed } from '@angular/core';
import { form, FormField, required, submit } from '@angular/forms/signals';
import { FormsModule } from '@angular/forms';
import { httpResource } from '@angular/common/http';
import { SeccionService, CrearSeccionDto } from '../../../core/services/seccion';
import { GradoService, GradoDto } from '../../../core/services/grado';
import { PeriodoService, PeriodoDto } from '../../../core/services/periodo';
import { ResponseDto } from '../../../core/services/response';
import { Button } from 'primeng/button';
import { Dialog } from 'primeng/dialog';
import { InputText } from 'primeng/inputtext';
import { Select } from 'primeng/select';
import { Toast } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-seccion-form',
  imports: [Button, Dialog, InputText, FormField, Select, FormsModule, Toast],
  providers: [MessageService],
  templateUrl: './seccion-form.html',
  styleUrl: './seccion-form.scss'
})
export class SeccionForm {
  @Input() visible = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() seccionGuardada = new EventEmitter<void>();

  private seccionService = inject(SeccionService);
  private gradoService = inject(GradoService);
  private periodoService = inject(PeriodoService);
  private messageService = inject(MessageService);
  guardando = false;
  turnoSeleccionado = 1;
  gradoSeleccionado: number | null = null;
  periodoSeleccionado: number | null = null;

  turnos = [
    { label: 'Mañana', value: 1 },
    { label: 'Tarde', value: 2 },
    { label: 'Noche', value: 3 }
  ];

  gradosResponse = httpResource<ResponseDto<GradoDto[]>>(() => this.gradoService.apiUrl);
  periodosResponse = httpResource<ResponseDto<PeriodoDto[]>>(() => this.periodoService.apiUrl);

  get grados() {
    return (this.gradosResponse.value()?.data ?? []).map(g => ({
      label: `${g.numero}° ${g.nivel}`,
      value: g.id
    }));
  }

  get periodos() {
    return (this.periodosResponse.value()?.data ?? []).map(p => ({
      label: `${p.anio} ${p.descripcion ? '- ' + p.descripcion : ''}`,
      value: p.id
    }));
  }

  modelo = signal({ nombre: '' });

  formulario = form(this.modelo, (path) => {
    required(path.nombre, { message: 'El nombre de la sección es obligatorio' });
  });

  cerrar() {
    this.visible = false;
    this.visibleChange.emit(false);
    this.modelo.set({ nombre: '' });
    this.turnoSeleccionado = 1;
    this.gradoSeleccionado = null;
    this.periodoSeleccionado = null;
  }

  guardar() {
    if (!this.gradoSeleccionado || !this.periodoSeleccionado) {
      this.messageService.add({ severity: 'warn', summary: 'Atención', detail: 'Selecciona grado y periodo' });
      return;
    }

    submit(this.formulario, async () => {
      this.guardando = true;
      const dto: CrearSeccionDto = {
        nombre: this.modelo().nombre,
        turno: this.turnoSeleccionado,
        gradoId: this.gradoSeleccionado!,
        periodoId: this.periodoSeleccionado!
      };

      this.seccionService.crear(dto).subscribe({
        next: (response: any) => {
          this.guardando = false;
          if (response.success) {
            this.messageService.add({ severity: 'success', summary: 'Éxito', detail: response.message });
            this.seccionGuardada.emit();
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
    });
  }
}