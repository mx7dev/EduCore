import { Component, EventEmitter, inject, Input, Output, signal } from '@angular/core';
import { form, FormField, required, submit } from '@angular/forms/signals';
import { PeriodoService, CrearPeriodoDto } from '../../../core/services/periodo';
import { Button } from 'primeng/button';
import { Dialog } from 'primeng/dialog';
import { InputText } from 'primeng/inputtext';
import { Toast } from 'primeng/toast';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-periodo-form',
  imports: [Button, Dialog, InputText, FormField, Toast],
  providers: [MessageService],
  templateUrl: './periodo-form.html',
  styleUrl: './periodo-form.scss'
})
export class PeriodoForm {
  @Input() visible = false;
  @Output() visibleChange = new EventEmitter<boolean>();
  @Output() periodoGuardado = new EventEmitter<void>();

  private periodoService = inject(PeriodoService);
  private messageService = inject(MessageService);
  guardando = false;

  modelo = signal({ anio: '', descripcion: '' });

  formulario = form(this.modelo, (path) => {
    required(path.anio, { message: 'El año es obligatorio' });
  });

  cerrar() {
    this.visible = false;
    this.visibleChange.emit(false);
    this.modelo.set({ anio: '', descripcion: '' });
  }

  guardar() {
    submit(this.formulario, async () => {
      this.guardando = true;
      const v = this.modelo();
      const dto: CrearPeriodoDto = {
        anio: Number(v.anio),
        descripcion: v.descripcion || undefined
      };

      this.periodoService.crear(dto).subscribe({
        next: (response: any) => {
          this.guardando = false;
          if (response.success) {
            this.messageService.add({ severity: 'success', summary: 'Éxito', detail: response.message });
            this.periodoGuardado.emit();
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